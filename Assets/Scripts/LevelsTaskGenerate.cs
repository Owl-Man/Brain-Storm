using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class LevelsTaskGenerate : MonoBehaviour
{
    public int level_now;

    [Header ("Индикатор")]
    public Slider mySlider;

    [Header ("Осталось времени")]
    public int timeLeft = 6;
    private float gameTime;

    [Header ("Прочие")]

    public bool maytimerrun = true;
    public bool isdied = false;

    public bool isDieByTime;

    public bool ispaused = false;

    int timeLeftOnPause = 0;


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
    public GameObject finishpanel;
    public GameObject answer_panel1;
    public GameObject answer_panel2;
    public GameObject answer_panel3;

    public GameObject revival_bonus_panel;

    public Text question_text;
    public Text answer_text1;
    public Text answer_text2;
    public Text answer_text3;
    public Text right_answer_after_gameover;
    public Text now_task;
    public Text max_tasks;
    public Text now_task_aftergameover;
    public Text max_tasks_aftergameover;
    public Text now_task_aftertimeexit;
    public Text max_tasks_aftertimeexit;
    public Text show_time;

    public Text ShowReceivedCoins;
    public Text ShowBalance;

    int choose_type_question;

    public int now_number_task = 0;
    public int limit_tasks = 6;

    int CurrentUser;

    public AudioSource ButtonSound;

    private void Start() 
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats");

        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");

        Application.runInBackground = true;

        CurrentUser = PlayerPrefs.GetInt("CurrentUser");

        int all_plays_count = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT AllPlaysCount FROM PlayerStats WHERE id = '"+CurrentUser+"';"));
        all_plays_count++;
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET AllPlaysCount = '"+all_plays_count+"' WHERE id = '"+CurrentUser+"';");

        ChooseLevelTaskGenerate();
    }

    private void Update() 
    {
        if (maytimerrun == true) 
        {
            mySlider.value = timeLeft;
            gameTime += 1 * Time.deltaTime;
        }

        if (gameTime >= 1 && maytimerrun == true) 
        {
            timeLeft -= 1;
            gameTime = 0;
        }
        if (timeLeft <= 0 && isdied == false) 
        {
            StartCoroutine(FalseForTimer());
        }

        show_time.text = mySlider.value.ToString();
    }

    void OnApplicationFocus(bool hasFocus) 
    {
        ispaused = !hasFocus;

        if (ispaused == false) 
        {
            timeLeft = timeLeft - timeLeftOnPause;
        }
    }

    void OnApplicationPause (bool pauseStatus) 
    {
        ispaused = pauseStatus;

        if (ispaused == true) 
        {
            timeLeftOnPause++;
        }
    }

    public void ChooseLevelTaskGenerate() 
    {
        level_now = Levels.level_now;

        if (Levels.level_now == 1) 
        {
            TaskGenerationFor1Level();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (Levels.level_now == 2) 
        {
            TaskGenerationFor2Level();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (Levels.level_now == 3) 
        {
            TaskGenerationFor3Level();
            mySlider.maxValue = 6;
            mySlider.value = 6;
            timeLeft = 6;
        }

        if (Levels.level_now == 4) 
        {
            TaskGenerationFor4Level();
            mySlider.maxValue = 7;
            mySlider.value = 7;
            timeLeft = 7;
        }

        if (Levels.level_now == 5) 
        {
            TaskGenerationFor5Level();
            mySlider.maxValue = 15;
            mySlider.value = 15;
            timeLeft = 15;
        }

        if (Levels.level_now == 6) 
        {
            TaskGenerationFor6Level();
            mySlider.maxValue = 10;
            mySlider.value = 10;
            timeLeft = 10;
        }

        if (Levels.level_now == 7) 
        {
            TaskGenerationFor7Level();
            mySlider.maxValue = 25;
            mySlider.value = 25;
            timeLeft = 25;
        }

        if (Levels.level_now == 8) 
        {
            TaskGenerationFor8Level();
            mySlider.maxValue = 20;
            mySlider.value = 20;
            timeLeft = 20;
        }

        if (Levels.level_now == 9) 
        {
            TaskGenerationFor9Level();
            mySlider.maxValue = 20;
            mySlider.value = 20;
            timeLeft = 20;
        }

        if (Levels.level_now == 10) 
        {
            TaskGenerationFor10Level();
            mySlider.maxValue = 80;
            mySlider.value = 80;
            timeLeft = 80;
        }

        if (Levels.level_now == 11) 
        {
            TaskGenerationFor11Level();
            mySlider.maxValue = 60;
            mySlider.value = 60;
            timeLeft = 60;
        }

        if (Levels.level_now == 12) 
        {
            TaskGenerationFor12Level();
            mySlider.maxValue = 40;
            mySlider.value = 40;
            timeLeft = 40;
        }

        max_tasks.text = limit_tasks.ToString();
    }

    public void SyncDataOfLevel() 
    {
        now_task.text = now_number_task.ToString();
        max_tasks.text = limit_tasks.ToString();

        question_text.text = LevelsDataBase.question_text;

        Debug.Log(LevelsDataBase.question_text);

        choose_type_question = LevelsDataBase.choose_type_question;

        if (LevelsDataBase.isTaskString == true)
        {
            Debug.Log(LevelsDataBase.right_answer_string);

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
            Debug.Log(LevelsDataBase.right_answer);

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


    public void TaskGenerationFor1Level() 
    {
        LevelsDataBase.Level1();
        SyncDataOfLevel();

        limit_tasks = 5;

        now_number_task++;
    }

    public void TaskGenerationFor2Level() 
    {
        LevelsDataBase.Level2();
        SyncDataOfLevel();

        limit_tasks = 6;

        now_number_task++;
    }

    public void TaskGenerationFor3Level() 
    {
        LevelsDataBase.Level3();
        SyncDataOfLevel();

        limit_tasks = 8;

        now_number_task++;
    }

    public void TaskGenerationFor4Level() 
    {
        LevelsDataBase.Level4();
        SyncDataOfLevel();

        limit_tasks = 8;

        now_number_task++;
    }

    public void TaskGenerationFor5Level() 
    {
        LevelsDataBase.Level5();
        SyncDataOfLevel();

        limit_tasks = 5;

        now_number_task++;   
    }

    public void TaskGenerationFor6Level() 
    {
        LevelsDataBase.Level6();
        SyncDataOfLevel();

        limit_tasks = 6;

        now_number_task++;
    }

    public void TaskGenerationFor7Level() 
    {
        LevelsDataBase.Level7();
        SyncDataOfLevel();

        limit_tasks = 5;

        now_number_task++;
    }

    public void TaskGenerationFor8Level() 
    {
        LevelsDataBase.Level8();
        SyncDataOfLevel();

        limit_tasks = 6;
        
        now_number_task++;
    }

    public void TaskGenerationFor9Level() 
    {
        LevelsDataBase.Level9();
        SyncDataOfLevel();

        limit_tasks = 6;

        now_number_task++;
    }

    public void TaskGenerationFor10Level() 
    {
        LevelsDataBase.Level10();
        SyncDataOfLevel();

        limit_tasks = 3;

        now_number_task++;
    }

    public void TaskGenerationFor11Level() 
    {
        LevelsDataBase.Level11();
        SyncDataOfLevel();

        limit_tasks = 4;

        now_number_task++;
    }

    public void TaskGenerationFor12Level() 
    {
        LevelsDataBase.Level12();
        SyncDataOfLevel();

        limit_tasks = 5;

        now_number_task++;
    }


    IEnumerator RestartTimer() 
    {
        maytimerrun = false;
        yield return new WaitForSeconds(1f);
        maytimerrun = true;
    }

    IEnumerator FalseForTimer()
    {
        isDieByTime = true;

        Handheld.Vibrate();
        maytimerrun = false;
        isdied = true;

        gameoverfortimeleft.SetActive(false);
        timeleftpanel.SetActive(true);
        Instantiate(effect, effectpoint_timeleft.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        timeleftpanel.SetActive(false);

        if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isRevivalBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1) 
        {
            revival_bonus_panel.SetActive(true);
        }
        else 
        {
            gameoverfortimeleft.SetActive(true);
        }

        now_number_task--;

        now_task_aftertimeexit.text = now_number_task.ToString();
        max_tasks_aftertimeexit.text = limit_tasks.ToString();
    }

    IEnumerator Right()
    {
        StartCoroutine(RestartTimer());
        rightpanel.SetActive(true);
        answer_panel1.SetActive(false);
        answer_panel2.SetActive(false);
        answer_panel3.SetActive(false);

        CheckForPlusDoubleBonusTask(); //Если куплен бонус Удвоение монет, то идет прибавление завершенных заданий

        Instantiate(effect, effectpoint.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        rightpanel.SetActive(false);

        if (now_number_task < limit_tasks)
        {
            now_task.text = now_number_task.ToString();
            answer_panel1.SetActive(true);
            answer_panel2.SetActive(true);
            answer_panel3.SetActive(true);
            ChooseLevelTaskGenerate();
        }
        else
        {
            DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM IsLevelsFinished;");

            int isLevelFinished;

            if (Levels.level_now == 1)
            {
                Reward.Coins(10);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT OneLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET OneLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 2) 
            {
                Reward.Coins(20);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TwoLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET TwoLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 3) 
            {
               Reward.Coins(25);
               isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ThreeLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET ThreeLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 4) 
            {
                Reward.Coins(30);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FourLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET FourLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 5) 
            {
                Reward.Coins(35);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FiveLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET FiveLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 6) 
            {
                Reward.Coins(40);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT SixLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET SixLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 7) 
            {
                Reward.Coins(50);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT SevenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET SevenLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 8) 
            {
                Reward.Coins(60);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT EightLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET EightLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 9) 
            {
                Reward.Coins(70);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NineLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET NineLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 10) 
            {
                Reward.Coins(80);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET TenLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 11) 
            {
                Reward.Coins(90);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ElevenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET ElevenLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            if (Levels.level_now == 12) 
            {
                Reward.Coins(100);
                isLevelFinished = Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TwelveLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"));

                if (isLevelFinished == 0)
                {
                    MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET TwelveLevel = {1} WHERE stroka = {CurrentUser};");
                    PlusCountLevelsFinished();
                }
            }

            ShowReceivedCoins.text = "+" + Reward.GlobalRewardCoins.ToString();
            ShowBalance.text = (ShopManager.Balance()).ToString();

            maytimerrun = false;
            finishpanel.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            Instantiate(effect, effect_medal_point.position, Quaternion.identity);
        }
    }

    public void PlusCountLevelsFinished() 
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats;");

        int countLevelFinished = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT LevelsFinished FROM PlayerStats WHERE id = {CurrentUser};"));

        countLevelFinished++;

        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET LevelsFinished = {countLevelFinished} WHERE id = {CurrentUser};");
    }

    IEnumerator False() 
    {
        isDieByTime = false;

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
        
        aftergameover.SetActive(false);

        if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isRevivalBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1) 
        {
            revival_bonus_panel.SetActive(true); //Если есть бонус возрождения, откроется окошко с предложением использовать его
        }
        else 
        {
            gameover.SetActive(true);
        }

        if (now_number_task > 1) 
        {
            now_number_task--;
        }

        now_task_aftergameover.text = now_number_task.ToString();
        max_tasks_aftergameover.text = limit_tasks.ToString();
    }

    public void OnAcceptUseRevivalBonusButtonClick() 
    {
        revival_bonus_panel.SetActive(false);
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isRevivalBonusActive = {0} WHERE id = {CurrentUser};");
        now_number_task--;
        maytimerrun = true;
        isdied = false;
        ChooseLevelTaskGenerate();
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

    public void CheckForPlusDoubleBonusTask() 
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

    public void OnNextLevelButtonClick() 
    {
        SoundOfButtonClick();

        if (level_now != 12) 
        {
            Levels.level_now++;
            SceneManager.LoadScene("Levels");
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

public static class Levels
{
    public static int level_now;
}
