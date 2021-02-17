using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBaseProfil : MonoBehaviour
{
    [Header ("Icons")]
    public GameObject[] LevelsFinishedIcons = new GameObject[12];

    public GameObject BulletChallengeFinishedIcon;
    public GameObject PodvoxChallengeFinishedIcon;
    public GameObject MeshalkaChallengeFinishedIcon;
    public GameObject ManGenderIcon;
    public GameObject WomenGenderIcon;
    public GameObject ManGenderForEditIcon;
    public GameObject WomenGenderForEditIcon;
    public GameObject ManGenderForNewUserIcon;
    public GameObject WomenGenderForNewUserIcon;

    [Header ("Прочие")]

    public GameObject EffectOfThunder;

    public GameObject resetpanel;
    public GameObject warning_for_delete_user_panel;
    public GameObject WaitForLoadPanel;

    public GameObject[] UsersButtons = new GameObject[5];

    public GameObject NewUserButton;

    public Animator transition;

    public AudioSource ButtonSound;

    [Header ("For New User")]
    public GameObject user_list_panel;
    public GameObject new_user_panel;
    public GameObject error_name_new_user_panel;
    public Text ErrorFromNewUserNickname;

    [Header ("For Edit Data")]
    public GameObject edit_name_panel;
    public GameObject edit_button;
    public GameObject error_name_player_edit_panel;
    public Text ErorFromEditNickname;

    [Header ("For First Start")]
    public GameObject first_start_panel;
    public Text NamePlayerExit;
    public Text ErorFromNickname;
    public GameObject error_name_player_panel;

    [Header ("Ranks")]
    public GameObject[] RanksIcons = new GameObject[4];
    public GameObject[] ShadowRanks = new GameObject[4];
    
    [Header ("Exit")]
    public Text LevelsFinishedExit;
    public Text ScoreEasyInfinityModeExit;
    public Text ScoreMiddleInfinityModeExit;
    public Text ScoreHighInfinityModeExit;
    public Text zvanie;
    public Text ChallengeFinishedExit;
    public Text YearsOldExit;
    public Text ClassExit;
    public Text GenderExit;
    public Text User1NameExit;
    public Text User2NameExit;
    public Text User3NameExit;
    public Text User4NameExit;
    public Text User5NameExit;
    public Text AllUsers;
    public Text AllPlaysCountExit;
    public GameObject YearsOldExitObject;
    public GameObject ClassExitObject;
    public GameObject GenderExitObject;

    [Header ("For Sync DataBase")]  
    string nickname;
    int years_old;
    int _class;
    int is_man_or_women = 0;
    string Zvanie;
    int levels_finished;
    int score_easy_infinity_mode;
    int score_middle_infinity_mode;
    int score_high_infinity_mode;
    int challenges_finished;
    int all_plays_count;

    int CurrentUser;
    int AccountThatDeletingNow;
    int CountOfAllUsers;

    [Header ("Enter field")]
    public InputField NamePlayerEnter;
    public InputField EditNamePlayerEnter;
    public InputField YearsOldEnter;
    public InputField EditYearsOldEnter;
    public InputField ClassEnter;
    public InputField EditClassEnter;
    public InputField NewUserNameEner;
    public InputField NewUserYearsOldEnter;
    public InputField NewUserClassEnter;

//Тот кто открыл этот скрипт даун

    private void Start() 
    {

        if (PlayerPrefs.GetInt("first_start") != 1) 
        {
            EffectOfThunder.SetActive(false);
            first_start_panel.SetActive(true);

            PlayerPrefs.SetInt("CurrentUser", 1);
            PlayerPrefs.SetInt("AllUsers", 1);
        }

        else
        {
            CurrentUser = PlayerPrefs.GetInt("CurrentUser");
            CountOfAllUsers = PlayerPrefs.GetInt("AllUsers");

            if (CurrentUser == 0) 
            {
                AllUsersUpdate();
                EffectOfThunder.SetActive(false);
                user_list_panel.SetActive(true);
            }

            SyncPlayerStats();
            SyncLevelsFinished();
            RoleUpdate();
        }
    }
//  <----------------------------------------------- NEW ACCOUNT FOR USER! --------------------------------------------->
    public void OnAcceptNewUserButtonClick() 
    {
        SoundOfButtonClick();
        nickname = NewUserNameEner.text;

        int lengthNickname = nickname.Length;

        if (string.IsNullOrWhiteSpace(NewUserYearsOldEnter.text)) 
            years_old = 0;

        else 
            years_old = Convert.ToInt32(NewUserYearsOldEnter.text);

        if (string.IsNullOrWhiteSpace(NewUserClassEnter.text)) 
            _class = 0;

        else 
            _class = Convert.ToInt32(NewUserClassEnter.text);

        if (string.IsNullOrWhiteSpace(NewUserNameEner.text))
        {
            error_name_new_user_panel.SetActive(true);
            ErrorFromNewUserNickname.text = "Имя не может быть пустое";
        }

        else if (lengthNickname > 13) 
        {
            error_name_new_user_panel.SetActive(true);
            ErrorFromNewUserNickname.text = "Максимальное количество букв в имени 13";
        }

        else 
        {
            WaitForLoadPanelSystem();
            
            try
            {
                CountOfAllUsers = PlayerPrefs.GetInt("AllUsers");
                CountOfAllUsers++;
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO PlayerStats (NamePlayer, AllPlaysCount, YearsOld, IsMenOrWomen, Class, LevelsFinished, ScoreEasyInfinityMode, ScoreMiddleInfinityMode, ScoreHighInfinityMode, ChallengesFinished, id) VALUES ('"+nickname+"', '"+0+"', '"+years_old+"', '"+is_man_or_women+"', '"+_class+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+CountOfAllUsers+"');");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO IsLevelsFinished (OneLevel, TwoLevel, ThreeLevel, FourLevel, FiveLevel, SixLevel, SevenLevel, EightLevel, NineLevel, TenLevel, ElevenLevel, TwelveLevel, BulletChallenge, PodvoxChallenge, MeshalkaChallenge, stroka) VALUES ('"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"' ,'"+0+"', '"+0+"', '"+0+"', '"+0+"','"+CountOfAllUsers+"');");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO Shop (Coins, isRevivalBonusActive, isDoubleCoinsBonusActive, id) VALUES ('"+0+"', '"+0+"', '"+0+"', '"+CountOfAllUsers+"');");
                PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);
                PlayerPrefs.SetInt("CurrentUser", CountOfAllUsers);
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }

            SyncPlayerStats();

            Invoke("LoadLevel", 0.8f);
        }
    }

    public void OnNewUserButtonClick() 
    {
        SoundOfButtonClick();
        user_list_panel.SetActive(false);
        new_user_panel.SetActive(true);
    }

    public void OnBackForNewUserButtonClick() 
    {
        SoundOfButtonClick();
        user_list_panel.SetActive(true);
        new_user_panel.SetActive(false);
    }

    public void OnExitFromAccountButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("CurrentUser", 0);
        StartCoroutine(LoadLevel("Menu"));
    }
    
//  <----------------------------------------------- FIRST ACCOUNT FOR USER! --------------------------------------------->
    public void OnAcceptDataPlayerButtonClick() 
    {
        SoundOfButtonClick();
        nickname = NamePlayerEnter.text;

        int lengthNickname = nickname.Length;

        if (string.IsNullOrWhiteSpace(YearsOldEnter.text)) 
            years_old = 0;

        else 
            years_old = Convert.ToInt32(YearsOldEnter.text);  

        if (string.IsNullOrWhiteSpace(ClassEnter.text)) 
            _class = 0;

        else 
            _class = Convert.ToInt32(ClassEnter.text);   

        if (string.IsNullOrWhiteSpace(NamePlayerEnter.text))
        {
            error_name_player_panel.SetActive(true);
            ErorFromNickname.text = "Имя не может быть пустое";
        }

        else if (lengthNickname > 13) 
        {
            error_name_player_panel.SetActive(true);
            ErorFromNickname.text = "Максимальное количество букв в имени 13";
        }

        else 
        {
            WaitForLoadPanelSystem();

            try
            {
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO PlayerStats (NamePlayer, AllPlaysCount, YearsOld, IsMenOrWomen, Class, LevelsFinished, ScoreEasyInfinityMode, ScoreMiddleInfinityMode, ScoreHighInfinityMode, ChallengesFinished, id) VALUES ('"+nickname+"', '"+0+"', '"+years_old+"', '"+is_man_or_women+"', '"+_class+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+1+"');");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO IsLevelsFinished (OneLevel, TwoLevel, ThreeLevel, FourLevel, FiveLevel, SixLevel, SevenLevel, EightLevel, NineLevel, TenLevel, ElevenLevel, TwelveLevel, BulletChallenge, PodvoxChallenge, MeshalkaChallenge, stroka) VALUES ('"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+0+"' ,'"+0+"', '"+0+"', '"+0+"', '"+0+"', '"+1+"');");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"INSERT INTO Shop (Coins, isRevivalBonusActive, isDoubleCoinsBonusActive, id) VALUES ('"+0+"', '"+0+"', '"+0+"', '"+1+"');");
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
            }

            SyncPlayerStats();
            PlayerPrefs.SetInt("first_start", 1);

            Invoke("LoadLevel", 0.8f);
        }
    }

//  <----------------------------------------------- EDIT ACCOUNT FOR USER! --------------------------------------------->
    public void OnAcceptEditDataPlayerButtonClick() 
    {
        SoundOfButtonClick();
        CurrentUser = PlayerPrefs.GetInt("CurrentUser");

        int lengthNickname = nickname.Length;

        if (string.IsNullOrWhiteSpace(EditYearsOldEnter.text)) 
            years_old = 0;

        else 
            years_old = Convert.ToInt32(EditYearsOldEnter.text);

        if (string.IsNullOrWhiteSpace(EditClassEnter.text)) 
            _class = 0;

        else 
            _class = Convert.ToInt32(EditClassEnter.text);   

        if (string.IsNullOrWhiteSpace(EditNamePlayerEnter.text))
        {
           error_name_player_edit_panel.SetActive(true);
           ErorFromEditNickname.text = "Имя не может быть пустое";
        }

        else if (lengthNickname > 13) 
        {
            error_name_player_edit_panel.SetActive(true);
            ErorFromEditNickname.text = "Максимальное количество букв в имени 13";
        }

        else 
        {
            WaitForLoadPanelSystem();

            try 
            {
                nickname = EditNamePlayerEnter.text;
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET NamePlayer = '"+nickname+"' WHERE id = '"+CurrentUser+"';");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET YearsOld = '"+years_old+"' WHERE id = '"+CurrentUser+"';");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET Class = '"+_class+"' WHERE id = '"+CurrentUser+"';");
                MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET IsMenOrWomen = '"+is_man_or_women+"' WHERE id = '"+CurrentUser+"';");
            }
            catch (Exception ex) 
            {
                Debug.Log(ex);
            }
            
            edit_button.SetActive(true);

            edit_name_panel.SetActive(false);
            error_name_player_edit_panel.SetActive(false);
            
            SyncPlayerStats();
            Invoke("LoadLevel", 0.8f);
        }

    }

    public void WaitForLoadPanelSystem()
    {
        WaitForLoadPanel.SetActive(true);
        EffectOfThunder.SetActive(false);
    }

    public void LoadLevel() 
    {
        StartCoroutine(LoadLevel("Menu"));
    }
    
    public void OnEditNameButtonClick() 
    {
        SoundOfButtonClick();
        edit_name_panel.SetActive(true);
        edit_button.SetActive(false);
        error_name_player_edit_panel.SetActive(false);
        EditNamePlayerEnter.text = nickname;
        EditYearsOldEnter.text = years_old.ToString();
        EditClassEnter.text = _class.ToString();
    }

    public void OnNoEditNameButtonClick() 
    {
        SoundOfButtonClick();
        edit_name_panel.SetActive(false);
        edit_button.SetActive(true);
    }

    public void OnWarningResetFromProfilButtonClick() 
    {
        SoundOfButtonClick();
        resetpanel.SetActive(true);
    }

    public void OnResetFromProfilButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("first_start", 0);

        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats;");

        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished;");

        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop");

        PlayerPrefs.SetInt("AllUsers", 0);
        PlayerPrefs.SetInt("FPS", 60);
        PlayerPrefs.SetInt("isSoundActive", 1);
        PlayerPrefs.SetInt("isMusicActive", 1);

        StartCoroutine(LoadLevel("Menu"));
    }

    public void OnNoResetFromProfilButtonClick() 
    {
        SoundOfButtonClick();
        resetpanel.SetActive(false);
    }

    public void SyncPlayerStats()
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PlayerStats;");

        nickname = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {CurrentUser};");
        years_old = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT YearsOld FROM PlayerStats WHERE id = {CurrentUser};"));
        is_man_or_women = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT IsMenOrWomen FROM PlayerStats WHERE id = {CurrentUser};"));
        _class = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Class FROM PlayerStats WHERE id = {CurrentUser};"));
        levels_finished = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT LevelsFinished FROM PlayerStats WHERE id = {CurrentUser};"));
        all_plays_count = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT AllPlaysCount FROM PlayerStats WHERE id = {CurrentUser};"));

        score_easy_infinity_mode = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreEasyInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        score_middle_infinity_mode = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreMiddleInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        score_high_infinity_mode = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ScoreHighInfinityMode FROM PlayerStats WHERE id = {CurrentUser};"));
        challenges_finished = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ChallengesFinished FROM PlayerStats WHERE id = {CurrentUser};"));

        NamePlayerExit.text = nickname;
        AllPlaysCountExit.text = all_plays_count.ToString();
        YearsOldExit.text = "Возраст: " + years_old.ToString();
        ClassExit.text = "Класс: " + _class.ToString();
        LevelsFinishedExit.text = levels_finished.ToString();

        ScoreEasyInfinityModeExit.text = score_easy_infinity_mode.ToString();
        ScoreMiddleInfinityModeExit.text = score_middle_infinity_mode.ToString();
        ScoreHighInfinityModeExit.text = score_high_infinity_mode.ToString();
        ChallengeFinishedExit.text = challenges_finished.ToString();

        if (years_old == 0) 
        {
            YearsOldExitObject.SetActive(false);
        }

        if (_class == 0) 
        {
            ClassExitObject.SetActive(false);
        }

        if (is_man_or_women == 0) 
        {
            GenderExitObject.SetActive(false);
        }

        else if (is_man_or_women == 1) 
        {
            GenderExit.text = "Пол: Мужской";
        }

        else if (is_man_or_women == 2) 
        {
            GenderExit.text = "Пол: Женский";
        }
    }

    public void RoleUpdate()
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM ZvaniesTable;");

        if (levels_finished < 3 || score_easy_infinity_mode < 5) 
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {1};");

            Color c1 = new Color(55, 255, 12, 255);
            zvanie.color = c1;
        }

        if (levels_finished >= 3 && score_easy_infinity_mode >= 5)
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {2};");

            RanksIcons[0].SetActive(true);
            ShadowRanks[0].SetActive(false);

            Color c2 = new Color(12, 255, 234, 255);
            zvanie.color = c2;
        }

        if (levels_finished >= 5 && score_middle_infinity_mode >= 4 && score_easy_infinity_mode >= 10) 
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {3};");

            RanksIcons[1].SetActive(true);
            ShadowRanks[1].SetActive(false);

            Color c3 = new Color(125, 102, 241, 255);
            zvanie.color = c3;
        }

        if (levels_finished >= 6 && score_middle_infinity_mode >= 10 && score_easy_infinity_mode >= 10)
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {4};");

            RanksIcons[2].SetActive(true);
            ShadowRanks[2].SetActive(false);

            Color c4 = new Color(252, 14, 235, 255);
            zvanie.color = c4;
        }

        if (levels_finished >= 9 && score_high_infinity_mode >= 4 && score_middle_infinity_mode >= 15 && score_easy_infinity_mode >= 10) 
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {5};");

            RanksIcons[3].SetActive(true);
            ShadowRanks[3].SetActive(false);

            Color c5 = new Color(255, 38, 47, 255);
            zvanie.color = c5;
        }

        if (levels_finished == 12 && score_high_infinity_mode >= 8 && score_middle_infinity_mode >= 15 && score_easy_infinity_mode >= 10) 
        {
            Zvanie = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Zvanies FROM ZvaniesTable WHERE number = {6};");

            RanksIcons[4].SetActive(true);
            ShadowRanks[4].SetActive(false);

            Color c6 = new Color(255, 222, 43, 255);
            zvanie.color = c6;
        }

        zvanie.text = Zvanie;
    }

    public void SyncLevelsFinished()
    {
        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM IsLevelsFinished;");

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT OneLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[0].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TwoLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};"))== 1)
            LevelsFinishedIcons[1].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ThreeLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[2].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FourLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[3].SetActive(true);
        
        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FiveLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[4].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT SixLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[5].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT SevenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[6].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT EightLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[7].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NineLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[8].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[9].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT ElevenLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[10].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT TwelveLevel FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            LevelsFinishedIcons[11].SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT BulletChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1) 
            BulletChallengeFinishedIcon.SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT PodvoxChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1) 
            PodvoxChallengeFinishedIcon.SetActive(true);

        if (Convert.ToInt16(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT MeshalkaChallenge FROM IsLevelsFinished WHERE stroka = {CurrentUser};")) == 1)
            MeshalkaChallengeFinishedIcon.SetActive(true);
    }

    public void OnMenGenderButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 1;
        ManGenderIcon.SetActive(true);
        WomenGenderIcon.SetActive(false);
    }

    public void OnWomenGenderButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 2;
        WomenGenderIcon.SetActive(true);
        ManGenderIcon.SetActive(false);
    }

    public void OnMenGenderForEditButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 1;
        ManGenderForEditIcon.SetActive(true);
        WomenGenderForEditIcon.SetActive(false);
    }

    public void OnWomenGenderForEditButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 2;
        ManGenderForEditIcon.SetActive(false);
        WomenGenderForEditIcon.SetActive(true);
    }

    public void OnMenGenderForNewUserButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 1;
        ManGenderForEditIcon.SetActive(true);
        WomenGenderForEditIcon.SetActive(false);
    }

    public void OnWomenGenderForNewUserButtonClick() 
    {
        SoundOfButtonClick();
        is_man_or_women = 2;
        ManGenderForNewUserIcon.SetActive(false);
        WomenGenderForNewUserIcon.SetActive(true);
    }

    public void User1ButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("CurrentUser", 1);
        StartCoroutine(LoadLevel("Menu"));
    }

    public void User2ButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("CurrentUser", 2);
        StartCoroutine(LoadLevel("Menu"));
    }

    public void User3ButtonClick() 
    {
        PlayerPrefs.SetInt("CurrentUser", 3);
        StartCoroutine(LoadLevel("Menu"));
    }

    public void User4ButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("CurrentUser", 4);
        StartCoroutine(LoadLevel("Menu"));
    }

    public void User5ButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("CurrentUser", 5);
        StartCoroutine(LoadLevel("Menu"));
    }

    public void OnDeleteAccount1ButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(true);
        AccountThatDeletingNow = 1;
    }

    public void OnDeleteAccount2ButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(true);
        AccountThatDeletingNow = 2;
    }

    public void OnDeleteAccount3ButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(true);
        AccountThatDeletingNow = 3;
    }

    public void OnDeleteAccount4ButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(true);
        AccountThatDeletingNow = 4;
    }

    public void OnDeleteAccount5ButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(true);
        AccountThatDeletingNow = 5;
    }

    public void OnBackForDeleteAccountButtonClick()
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(false);
    }

    public void OnAcceptForDeleteAccountButtonClick() 
    {
        SoundOfButtonClick();
        warning_for_delete_user_panel.SetActive(false);

        if (AccountThatDeletingNow == 1) 
            DeleteUser1();
        if (AccountThatDeletingNow == 2) 
            DeleteUser2();
        if (AccountThatDeletingNow == 3) 
            DeleteUser3();
        if (AccountThatDeletingNow == 4) 
            DeleteUser4();
        if (AccountThatDeletingNow == 5) 
            DeleteUser5();
    }

    public void DeleteUser1() 
    {
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats WHERE id = {1};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished WHERE stroka = {1};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop WHERE id = {1};");

        if (CountOfAllUsers == 2) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {1} WHERE id = 2;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {1} WHERE stroka = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {1} WHERE id = 2;");
        }

        if (CountOfAllUsers == 3) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {1} WHERE stroka = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
        }

        if (CountOfAllUsers == 4) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {1} WHERE stroka = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
        }

        if (CountOfAllUsers == 5) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {4} WHERE id = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {1} WHERE stroka = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {4} WHERE stroka = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {1} WHERE id = 2;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {4} WHERE id = 5;");
        }

        CountOfAllUsers--;
        PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);

        AllUsersUpdate();

        if (CountOfAllUsers == 0) 
        {
            PlayerPrefs.SetInt("first_start", 0);

            WaitForLoadPanel.SetActive(true);
            Invoke("LoadLevel", 0.6f);
        }
    }

    public void DeleteUser2() 
    {
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats WHERE id = {2};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished WHERE stroka = {2};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop WHERE id = {2};");

        if (CountOfAllUsers == 3) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
        }

        if (CountOfAllUsers == 4) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
        }

        if (CountOfAllUsers == 5) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {4} WHERE id = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {2} WHERE stroka = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {4} WHERE stroka = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {2} WHERE id = 3;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {4} WHERE id = 5;");
        }

        CountOfAllUsers--;
        PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);

        AllUsersUpdate();
    }

    public void DeleteUser3() 
    {
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats WHERE id = {3};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished WHERE stroka = {3};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop WHERE id = {3};");

        if (CountOfAllUsers == 4) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
        }

        if (CountOfAllUsers == 5) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {4} WHERE id = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {3} WHERE stroka = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {4} WHERE stroka = 5;");

            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {3} WHERE id = 4;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {4} WHERE id = 5;");
        }

        CountOfAllUsers--;
        PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);

        AllUsersUpdate();
    }

    public void DeleteUser4() 
    {
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats WHERE id = {4};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished WHERE stroka = {4};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop WHERE id = {4};");

        if (CountOfAllUsers == 5) 
        {
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE PlayerStats SET id = {4} WHERE id = 5;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE IsLevelsFinished SET stroka = {4} WHERE stroka = 5;");
            MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET id = {4} WHERE id = 5;");
        }

        CountOfAllUsers--;
        PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);
    }

    public void DeleteUser5() 
    {
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM PlayerStats WHERE id = {5};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM IsLevelsFinished WHERE stroka = {5};");
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"DELETE FROM Shop WHERE = id {5};");

        CountOfAllUsers--;
        PlayerPrefs.SetInt("AllUsers", CountOfAllUsers);
    }

    IEnumerator LoadLevel(string scene) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.36f);

        SceneManager.LoadScene(scene);
    }

    public void SoundOfButtonClick() 
    {
        if (PlayerPrefs.GetInt("isSoundActive") == 1)
            ButtonSound.Play();
    }

    public void AllUsersUpdate() 
    {
        CurrentUser = PlayerPrefs.GetInt("CurrentUser");
        AllUsers.text = (PlayerPrefs.GetInt("AllUsers")).ToString();
        CountOfAllUsers = PlayerPrefs.GetInt("AllUsers");

        if (CountOfAllUsers == 0) 
        {
            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");

            UsersButtons[0].SetActive(false);
            UsersButtons[1].SetActive(false);
            UsersButtons[2].SetActive(false);
            UsersButtons[3].SetActive(false);
            UsersButtons[4].SetActive(false);
            
            PlayerPrefs.SetInt("first_start", 0);
            StartCoroutine(LoadLevel("Menu"));
        }

        if (CountOfAllUsers == 1) 
        {
            NewUserButton.SetActive(true);

            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");

            UsersButtons[0].SetActive(true);
            UsersButtons[1].SetActive(false);
            UsersButtons[2].SetActive(false);
            UsersButtons[3].SetActive(false);
            UsersButtons[4].SetActive(false);
        }
        if (CountOfAllUsers == 2) 
        {
            NewUserButton.SetActive(true);

            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");
            User2NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {2};");

            UsersButtons[0].SetActive(true);
            UsersButtons[1].SetActive(true);
            UsersButtons[2].SetActive(false);
            UsersButtons[3].SetActive(false);
            UsersButtons[4].SetActive(false);
        }
        if (CountOfAllUsers == 3) 
        {
            NewUserButton.SetActive(true);

            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");
            User2NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {2};");
            User3NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {3};");

            UsersButtons[0].SetActive(true);
            UsersButtons[1].SetActive(true);
            UsersButtons[2].SetActive(true);
            UsersButtons[3].SetActive(false);
            UsersButtons[4].SetActive(false);
        }
        if (CountOfAllUsers == 4) 
        {
            NewUserButton.SetActive(true);

            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");
            User2NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {2};");
            User3NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {3};");
            User4NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {4};");

            UsersButtons[0].SetActive(true);
            UsersButtons[1].SetActive(true);
            UsersButtons[2].SetActive(true);
            UsersButtons[3].SetActive(true);
            UsersButtons[4].SetActive(false);
        }
        if (CountOfAllUsers == 5) 
        {
            NewUserButton.SetActive(false);

            User1NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {1};");
            User2NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {2};");
            User3NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {3};");
            User4NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {4};");
            User5NameExit.text = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT NamePlayer FROM PlayerStats WHERE id = {5};");

            UsersButtons[0].SetActive(true);
            UsersButtons[1].SetActive(true);
            UsersButtons[2].SetActive(true);
            UsersButtons[3].SetActive(true);
            UsersButtons[4].SetActive(true);
        }
    }
}
