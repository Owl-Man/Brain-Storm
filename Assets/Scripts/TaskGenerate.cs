using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class TaskGenerate : MonoBehaviour
{
    [Header ("Индикатор")]
    public Slider mySlider;

    [Header ("Осталось времени")]
    public int timeLeft = 6;
    private float gameTime;

    [Header ("Прочие")]

    public bool maytimerrun = true;
    public bool isdied = false;

    public bool isrecord = false;
    public bool isRecordShowed = false;

    public bool isChangePosition = false;

    public bool isDieByTime;


    public Transform effectpoint;
    public Transform effectpoint_gameover;
    public Transform effectpoint_timeleft;
    public Transform effect_medal_point;

    public GameObject effect;
    public GameObject gameover;
    public GameObject rightpanel;
    public GameObject aftergameover;
    public GameObject timeleftpanel;
    public GameObject gameoverfortimeleft;
    public GameObject answer_panel1;
    public GameObject answer_panel2;
    public GameObject answer_panel3;
    public GameObject record_panel;
    public GameObject record_panel_after;
    public GameObject finish_panel;
    public GameObject revival_bonus_panel;

    public int score;
    public int limit_score;
    public Text scoreDisplay;

    public Text question_text;
    public Text answer_text1;
    public Text answer_text2;
    public Text answer_text3;
    public Text right_answer_after_gameover;
    public Text Show_time;

    int choose_type_question;

    int MaxScore;

    int CurrentUser;

    bool isProchenTask = false;
    bool isDiskriminantTask = false;
    bool isQuadUravTask = false;
    bool isSinCosTanTask = false;

    public AudioSource ButtonSound;

    private void Start() 
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats");

        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");

        CurrentUser = PlayerPrefs.GetInt("CurrentUser");

        int all_plays_count = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT AllPlaysCount FROM PlayerStats WHERE id = '"+CurrentUser+"';"));
        all_plays_count++;
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET AllPlaysCount = '"+all_plays_count+"' WHERE id = '"+CurrentUser+"';");

        ChooseDifferentMode();
    }


    public void ChooseDifferentMode() 
    {
        if (TaskModes.isEasyMode == true) 
        {
            EasyTaskGeneration();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (TaskModes.isMiddleMode == true) 
        {
            MiddleTaskGeneration();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (TaskModes.isHighMode == true && isProchenTask == true)
        {
            HighTaskGeneration();
            mySlider.maxValue = 15;
            mySlider.value = 15;
            timeLeft = 15;
        }

        else if (TaskModes.isHighMode == true && isDiskriminantTask == true)
        {
            HighTaskGeneration();
            mySlider.maxValue = 65;
            mySlider.value = 65;
            timeLeft = 65;
        }

        else if (TaskModes.isHighMode == true && isQuadUravTask == true) 
        {
            HighTaskGeneration();
            mySlider.maxValue = 7;
            mySlider.value = 7;
            timeLeft = 7;
        }

        else if (TaskModes.isHighMode == true && isSinCosTanTask == true) 
        {
            HighTaskGeneration();
            mySlider.maxValue = 20;
            mySlider.value = 20;
            timeLeft = 20;
        }

        else if (TaskModes.isHighMode == true) 
        {
            HighTaskGeneration();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (TaskModes.isBulletChallengeMode == true) 
        {
            BulletChallengeTaskGeneration();
            mySlider.maxValue = 3;
            mySlider.value = 3;
            timeLeft = 3;
        }

        if (TaskModes.isPodvoxChallengeMode == true) 
        {
            PodvoxChallengeTaskGeneration();
            mySlider.maxValue = 50;
            mySlider.value = 50;
            timeLeft = 50;
        }

        if (TaskModes.isMeshalkaChallengeMode == true) 
        {
            MeshalkaChallengeModeTaskGeneration();
            mySlider.maxValue = 7;
            mySlider.value = 7;
            timeLeft = 7;
        }
    }

    public void Update()
    {
        if (maytimerrun == true) 
        {
            mySlider.value = timeLeft;
            gameTime += 1 * Time.deltaTime;

            if (gameTime >= 1) 
            {
                timeLeft -= 1;
                gameTime = 0;
            }

            if (timeLeft <= 0 && isdied == false) 
            {
                StartCoroutine(FalseForTimer());
            }

            Show_time.text = timeLeft.ToString();
        }


        //Meshalka challenge
        
        //if (isChangePosition == true) 
        //{
        //    int statePositionOfAnswers = UnityEngine.Random.Range(1, 6);

        //    int speed = 5;

        //    isChangePosition = false;
        //}
    }


    public void SyncDataOfLevel() 
    {
        PlayerPrefs.GetInt("score", score);
        scoreDisplay.text = score.ToString();

        question_text.text = LevelsDataBase.question_text;

        choose_type_question = LevelsDataBase.choose_type_question;

        if (LevelsDataBase.isTaskString == true)
        {
            if (LevelsDataBase.choose_right_answer == 1) 
            {
                answer_text1.text = LevelsDataBase.right_answer_string;
                answer_text2.text = LevelsDataBase.choose_fake_answer1_string;
                answer_text3.text = LevelsDataBase.choose_fake_answer2_string;
            }

            if (LevelsDataBase.choose_right_answer == 2) 
            {
                answer_text2.text = LevelsDataBase.right_answer_string;
                answer_text1.text = LevelsDataBase.choose_fake_answer1_string;
                answer_text3.text = LevelsDataBase.choose_fake_answer2_string;
            }

            if (LevelsDataBase.choose_right_answer == 3) 
            {
                answer_text3.text = LevelsDataBase.right_answer_string;
                answer_text1.text = LevelsDataBase.choose_fake_answer1_string;
                answer_text2.text = LevelsDataBase.choose_fake_answer2_string;
            }

        }
        else 
        {
            StartCoroutine(CheckFakeAnswer());

            if (LevelsDataBase.choose_right_answer == 1) 
            {
                answer_text1.text = LevelsDataBase.right_answer.ToString();
                answer_text2.text = LevelsDataBase.choose_fake_answer1.ToString();
                answer_text3.text = LevelsDataBase.choose_fake_answer2.ToString();
            }

            if (LevelsDataBase.choose_right_answer == 2) 
            {
                answer_text2.text = LevelsDataBase.right_answer.ToString();
                answer_text1.text = LevelsDataBase.choose_fake_answer1.ToString();
                answer_text3.text = LevelsDataBase.choose_fake_answer2.ToString();
            }

            if (LevelsDataBase.choose_right_answer == 3) 
            {
                answer_text3.text = LevelsDataBase.right_answer.ToString();
                answer_text1.text = LevelsDataBase.choose_fake_answer1.ToString();
                answer_text2.text = LevelsDataBase.choose_fake_answer2.ToString();
            }
        }
    }



    public void EasyTaskGeneration() 
    {
        LevelsDataBase.Level2();
        SyncDataOfLevel();
    }

    public void MiddleTaskGeneration() 
    {
        LevelsDataBase.Level4();
        SyncDataOfLevel();
    }

    public void HighTaskGeneration() 
    {
        choose_type_question = UnityEngine.Random.Range(1, 5);

        if (choose_type_question == 1) 
        {
            //ProchentTasks
            LevelsDataBase.Level7();
            SyncDataOfLevel();
            isProchenTask = true;

        }

        if (choose_type_question == 2) 
        {
            //DiskriminantTasks
            LevelsDataBase.Level10();
            SyncDataOfLevel();
            isDiskriminantTask = true;
        }

        if (choose_type_question == 3) 
        {
            //QuadUravTasks
            LevelsDataBase.Level6();
            SyncDataOfLevel();
            isQuadUravTask = true;
        }

        if (choose_type_question == 4) 
        {
            //SinCosTanTasks
            LevelsDataBase.Level9();
            SyncDataOfLevel();
            isSinCosTanTask = true;
        }
    }

    public void BulletChallengeTaskGeneration() 
    {
        limit_score = 10;

        timeLeft = 3;

        LevelsDataBase.Level1();
        SyncDataOfLevel();
    }

    public void PodvoxChallengeTaskGeneration() 
    {
        limit_score = 7;
        
        timeLeft = 50;

        LevelsDataBase.SpecialPodvoxTasks();
        SyncDataOfLevel();
    }

    public void MeshalkaChallengeModeTaskGeneration() 
    {
        LevelsDataBase.Level4();
        SyncDataOfLevel();

        StartCoroutine(ChangePositionOfAnswer());
    }

    IEnumerator RestartTimer() 
    {
        maytimerrun = false;
        yield return new WaitForSeconds(1f);
        timeLeft = 6;
        maytimerrun = true;
    }

    IEnumerator FalseForTimer() 
    {
        Handheld.Vibrate();
        maytimerrun = false;
        isdied = true;

        gameoverfortimeleft.SetActive(false);
        timeleftpanel.SetActive(true);
        Instantiate(effect, effectpoint_timeleft.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        timeleftpanel.SetActive(false);

        if (isrecord == true)
        {
            record_panel_after.SetActive(true);
        }

        if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isRevivalBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1) 
        {
            revival_bonus_panel.SetActive(true);
        }

        else 
        {
            gameoverfortimeleft.SetActive(true);
        }
    }

    IEnumerator Right()
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats;");

        StartCoroutine(RestartTimer());
        score++;
        PlayerPrefs.SetInt("score", score);

        if (TaskModes.isEasyMode == true) 
        {
            Reward.Coins(2);
            MaxScore = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreEasyInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        }
            
        if (TaskModes.isMiddleMode == true) 
        {
            Reward.Coins(5);
            MaxScore = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreMiddleInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        }

        if (TaskModes.isHighMode == true) 
        {
            Reward.Coins(10);
            MaxScore = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreHighInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        }

        if (score > MaxScore) //Проверка, заработал ли игрок больше опыта, чем его прошлый рекорд
        {
            if (TaskModes.isEasyMode == true) 
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET ScoreEasyInfinityMode = {score} WHERE id = '"+CurrentUser+"';");
            }
            
            if (TaskModes.isMiddleMode == true) 
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET ScoreMiddleInfinityMode = {score} WHERE id = '"+CurrentUser+"';");
            }

            if (TaskModes.isHighMode == true) 
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET ScoreHighInfinityMode = {score} WHERE id = '"+CurrentUser+"';");
            }

            if ((TaskModes.isBulletChallengeMode == false || TaskModes.isPodvoxChallengeMode == false) && isRecordShowed == false) 
            {
                record_panel.SetActive(true);
                isrecord = true;

                isRecordShowed = true;
            }
        }

        rightpanel.SetActive(true);
        answer_panel1.SetActive(false);
        answer_panel2.SetActive(false);
        answer_panel3.SetActive(false);
        Instantiate(effect, effectpoint.position, Quaternion.identity);
        CheckForPlusDoubleBonusTask(); //Если куплен бонус Удвоение монет, то идет прибавление завершенных заданий
        yield return new WaitForSeconds(1f);
        
        rightpanel.SetActive(false);

        if (score > limit_score && (TaskModes.isBulletChallengeMode == true || TaskModes.isPodvoxChallengeMode == true))
        {
            StartCoroutine(FinishingChallenge());
        }
        else
        {
            answer_panel1.SetActive(true);
            answer_panel2.SetActive(true);
            answer_panel3.SetActive(true);
            ChooseDifferentMode();
        }
    }

    IEnumerator FinishingChallenge() 
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM IsLevelsFinished;");

        Reward.Coins(30);

        maytimerrun = false;

        int isChallengeFinished;

        if (TaskModes.isBulletChallengeMode == true)
        {
            isChallengeFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT BulletChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

            if (isChallengeFinished == 0)
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET BulletChallenge = {1} WHERE stroka = {CurrentUser};");
                PlusCountChallengesFinished();
            }
        }

        if (TaskModes.isPodvoxChallengeMode == true) 
        {
            isChallengeFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT PodvoxChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

            if (isChallengeFinished == 0)
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET PodvoxChallenge = {1} WHERE stroka = {CurrentUser};");
                PlusCountChallengesFinished();
            }
        }

        if (TaskModes.isMeshalkaChallengeMode == true) 
        {
            isChallengeFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT MeshalkaChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

            if (isChallengeFinished == 0)
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET MeshalkaChallenge = {1} WHERE stroka = {CurrentUser};");
                PlusCountChallengesFinished();
            }
        }        

        finish_panel.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Instantiate(effect, effect_medal_point.position, Quaternion.identity);
    }

    public void PlusCountChallengesFinished() 
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats;");

        int countChallengesFinished = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ChallengesFinished FROM PlayerStats WHERE id = {CurrentUser};"));

        countChallengesFinished++;

        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET ChallengesFinished = {countChallengesFinished} WHERE id = {CurrentUser};");
    }

    IEnumerator False() 
    {
        Handheld.Vibrate();
        maytimerrun = false;
        isdied = true;
        gameover.SetActive(false);
        aftergameover.SetActive(true);

        if (LevelsDataBase.isTaskString == true) 
        {
            right_answer_after_gameover.text = LevelsDataBase.right_answer_string;
        }

        else 
        {
            right_answer_after_gameover.text = LevelsDataBase.right_answer.ToString();
        }
        
        Instantiate(effect, effectpoint_gameover.position, Quaternion.identity);
        yield return new WaitForSeconds(2.5f);

        if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isRevivalBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1) 
        {
            revival_bonus_panel.SetActive(true); //Если есть бонус возрождения, откроется окошко с предложением использовать его
        }

        else 
        {
            gameover.SetActive(true);

            if (isrecord == true)
            {
                record_panel_after.SetActive(true);
            }
        }

        aftergameover.SetActive(false);
    }

    public void OnAcceptUseRevivalBonusButtonClick() 
    {
        revival_bonus_panel.SetActive(false);
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isRevivalBonusActive = {0} WHERE id = {CurrentUser};");
        score--;
        maytimerrun = true;
        isdied = false;
        ChooseDifferentMode();
    }

    public void OnNoForUseRevivalBonusButtonClick() 
    {
        if (isDieByTime == true) 
        {
            gameoverfortimeleft.SetActive(true);
        }
        else 
        {
            gameover.SetActive(true);
        }
    }

    public void CheckForPlusDoubleBonusTask() //Если куплен бонус Удвоение монет, то идет прибавление завершенных заданий
    {
        if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isDoubleCoinsBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1) 
        {
            int doubleBonusFinishTasks = PlayerPrefs.GetInt("DoubleBonusFinishTasks");
            doubleBonusFinishTasks++;
            PlayerPrefs.SetInt("DoubleBonusFinishTasks", doubleBonusFinishTasks);
        }
    }
    
    IEnumerator CheckFakeAnswer() 
    {
        if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.right_answer)
        {
            LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.right_answer) 
            {
                LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
            }

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.right_answer) 
            {
                LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
            }
        }

        if (LevelsDataBase.choose_fake_answer2 == LevelsDataBase.right_answer) 
        {
            LevelsDataBase.choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer2 == LevelsDataBase.right_answer) 
            {
                LevelsDataBase.choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);
            }

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.right_answer) 
            {
                LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
            }
        }

        if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.choose_fake_answer2) 
        {
            LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
            LevelsDataBase.choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.choose_fake_answer2) 
            {
                LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
                LevelsDataBase.choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);
            }

            yield return new WaitForSeconds(0.005f);

            if (LevelsDataBase.choose_fake_answer1 == LevelsDataBase.choose_fake_answer2) 
            {
                LevelsDataBase.choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
                LevelsDataBase.choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);
            }
        }
    }

    IEnumerator ChangePositionOfAnswer() 
    {
        yield return new WaitForSeconds(2f);

        isChangePosition = true;

        StartCoroutine(ChangePositionOfAnswer());
    }


    public void OnAnswer1ButtonClick() 
    {
        SoundOfButtonClick();

        if (LevelsDataBase.choose_right_answer == 1 && isdied == false) 
        {
            StartCoroutine(Right());

        }
        else if (isdied == false)
        {
            StartCoroutine(False());
        }
    }

    public void OnAnswer2ButtonClick() 
    {
        SoundOfButtonClick();

        if (LevelsDataBase.choose_right_answer == 2 && isdied == false) 
        {
            StartCoroutine(Right());
        }
        else if (isdied == false)
        {
            StartCoroutine(False());
        }
    }

    public void OnAnswer3ButtonClick() 
    {
        SoundOfButtonClick();

        if (LevelsDataBase.choose_right_answer == 3 && isdied == false) 
        {
            StartCoroutine(Right());
        }
        else if (isdied == false)
        {
            StartCoroutine(False());
        }
    }
    public void SoundOfButtonClick() 
    {
        if (PlayerPrefs.GetInt("isSoundActive") == 1)
        {
            ButtonSound.Play();
        }
    }
}

public static class TaskModes
{
    public static bool isEasyMode;
    public static bool isMiddleMode;
    public static bool isHighMode;
    public static bool isBulletChallengeMode;
    public static bool isPodvoxChallengeMode;
    public static bool isMeshalkaChallengeMode;

    public static void EasyMode()
    {
        isEasyMode = true;
        isMiddleMode = false;
        isHighMode = false;
        isBulletChallengeMode = false;
        isPodvoxChallengeMode = false;
    }

    public static void MiddleMode()
    {
        isEasyMode = false;
        isMiddleMode = true;
        isHighMode = false;
        isBulletChallengeMode = false;
        isPodvoxChallengeMode = false;
        isMeshalkaChallengeMode = false;
    }

    public static void HighMode()
    {
        isEasyMode = false;
        isMiddleMode = false;
        isHighMode = true;
        isBulletChallengeMode = false;
        isPodvoxChallengeMode = false;
        isMeshalkaChallengeMode = false;
    }

    public static void BulletChallengeMode()
    {
        isEasyMode = false;
        isMiddleMode = false;
        isHighMode = false;
        isBulletChallengeMode = true;
        isPodvoxChallengeMode = false;
        isMeshalkaChallengeMode = false;
    }

    public static void PodvoxChallengeMode()
    {
        isEasyMode = false;
        isMiddleMode = false;
        isHighMode = false;
        isBulletChallengeMode = false;
        isPodvoxChallengeMode = true;
        isMeshalkaChallengeMode = false;
    }

    public static void MeshalkaChallengeMode() 
    {
        isEasyMode = false;
        isMiddleMode = false;
        isHighMode = false;
        isBulletChallengeMode = false;
        isPodvoxChallengeMode = false;
        isMeshalkaChallengeMode = true;
    }
}