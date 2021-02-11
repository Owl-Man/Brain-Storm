using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [Header ("OtherPanels")]
    public GameObject choose_mode_panel;
    public GameObject choose_level_panel;
    public GameObject profil_panel;
    public GameObject first_enter_panel;
    public GameObject choose_different_panel;
    public GameObject help_zvanies_panel;
    public GameObject choose_challenge_mode_panel;
    public GameObject opisanie_challenge_panel;
    public GameObject settings_panel;
    public GameObject shop_panel;
    public GameObject warning_for_exit_panel;
    public GameObject unlock_challenge_mode_panel;
    public GameObject lock_icon;

    [Header ("BookPanels")]
    public GameObject book_panel;
    public GameObject quad_uravnenie_panel;
    public GameObject teorema_vieta_panel;
    public GameObject prochent_panel;
    public GameObject svoistva_quad_urav_panel;
    public GameObject sin_cos_tan_panel;

    [Header ("Music and Sound")]
    public GameObject MusicOnButton;
    public GameObject MusicOffButton;
    public GameObject SoundOnButton;
    public GameObject SoundOffButton;

    [Header ("Icons")]
    public GameObject Icon120FPS;
    public GameObject Icon90FPS;
    public GameObject Icon60FPS;
    public GameObject OnEffectsIcon;
    public GameObject OffEffectIcon;

    [Header ("Texts")]
    public Text NameChallenge;
    public Text OpisanieChallenge;
    public Text TimeForChallenge;
    public Text ShowAllCoinsInUnlockChallengeMode;

    [Header ("State")]
    public bool isInMainMenu = true;
    public bool isInSelectModePanel = false;
    public bool isInSelectDifferentModePanel = false;
    public bool isInSelectLevelPanel = false;
    public bool isInSelectChallengePanel = false;
    public bool isInOpisanieForChallengePanel = false;
    public bool isInSettingsPanel = false;
    public bool isInBookPanel = false;
    public bool isInQuadUravBookPanel = false;
    public bool isInTeoremaVietaPanel = false;
    public bool isInProchentPanel = false;
    public bool isInProfilPanel = false;
    public bool isInSvoistvaPanel = false;
    public bool isInSinCosTanPanel = false;
    public bool isInShopPanel = false;    
    public bool isInWarningForExitPanel = false;
    public bool isInHelpForZvaniesPanel = false;

    [Header ("Other")]
    public bool isBulletChallengeMode = false;
    public bool isPodvoxChallengeMode = false;
    public bool isMeshalkaChallengeMode = false;

    public Button challenge_button;

    public Animator ProfilAnim;
    public Animator ShopAnim;
    public Animator BookAnim;
    public Animator QuadUravAnim;
    public Animator TeoremaVietaAnim;
    public Animator ProchentAnim;
    public Animator SvoistvaAnim;
    public Animator SinCosTanAnim;

    public float timeforSmoothAnim = 0.4f;

    public GameObject EffectThunder;

    public Animator transition;

    public AudioSource ButtonSound;
    public GameObject BackgroundMusic;

    private void Start() 
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        
        if (PlayerPrefs.GetInt("first_start") != 1) 
        {
            PlayerPrefs.SetInt("isEffectsActive", 1);
            PlayerPrefs.SetInt("FPS", 90);
            PlayerPrefs.SetInt("isSoundActive", 1);
            PlayerPrefs.SetInt("isMusicActive", 1);
            PlayerPrefs.SetInt("IsUnlockedChallengeMode", 0);
        }

        isInMainMenu = true;
        

        CheckMusicAndSoundActive();
        CheckEffectsActive();
    }

    private void FixedUpdate() 
    {
//   <---------------------------ANDROID BUTTONS FUNCTION------------------------->

        if (Application.platform == RuntimePlatform.Android) 
        {
            if (Input.GetKeyDown(KeyCode.Escape)) //back button press
            {
                if (isInMainMenu == true) 
                {
                    OnExitButtonClick();
                }

                if (isInProfilPanel == true) 
                {
                    OnProfilBackButtonClick();
                }

                if (isInSettingsPanel == true) 
                {
                    OnBackForSettingsButtonClick();
                }

                if (isInSelectModePanel == true) 
                {
                    OnBackForModeSelectButtonClick();
                }

                if (isInSelectDifferentModePanel == true) 
                {
                    OnBackFromInfinityModeButtonClick();
                }

                if (isInSelectLevelPanel == true) 
                {
                    OnBackForLevelSelectButtonClick();
                }

                if (isInSelectChallengePanel == true) 
                {
                    OnBackChallengeModeButtonClick();
                }

                if (isInOpisanieForChallengePanel == true) 
                {
                    OnBackForOpisanieButtonClick();
                }

                if (isInBookPanel == true) 
                {
                    OnBackForBookButtonClick();
                }

                if (isInQuadUravBookPanel == true) 
                {
                    OnBackForQuadUravnenieButtonClick();
                }

                if (isInTeoremaVietaPanel == true) 
                {
                    OnBackForTeoremaVietaButtonClick();
                }

                if (isInProchentPanel == true) 
                {
                    OnBackForProchentButtonClick();
                }

                if (isInSvoistvaPanel == true) 
                {
                    OnBackForSvoistvaButtonClick();
                }

                if (isInSinCosTanPanel == true) 
                {
                    OnBackForSinCosTanButtonClick();
                }

                if (isInWarningForExitPanel == true) 
                {
                    OnNoForExitButtonClick();
                }

                if (isInHelpForZvaniesPanel == true) 
                {
                    OnBackForHelpZvaniesButtonClick();
                }
            }

            //if (Input.GetKeyDown(KeyCode.Home)) // home button press
            //{
                //Application.Quit();
            //}
        }
    }
    
    public void OnPlayButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = false;
        isInSelectModePanel = true;

        //Проверка открытого испытания

        if (PlayerPrefs.GetInt("IsUnlockedChallengeMode") == 1) 
        {
            lock_icon.SetActive(false);
        }
        else 
        {
            challenge_button.interactable = false;
        }

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(false);
        }

        choose_mode_panel.SetActive(true);
        choose_level_panel.SetActive(false);
    }

    public void OnBackForModeSelectButtonClick()
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInSelectModePanel = false;

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        choose_mode_panel.SetActive(false);
    }

    public void OnProfilButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = false;
        isInProfilPanel = true;

        StartCoroutine(SmoothSetActiveFalseEffectThunder());

        profil_panel.SetActive(true);
    }

    public void OnProfilBackButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInProfilPanel = false;

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        StartCoroutine(CloseProfilPanel());
    }

    IEnumerator CloseProfilPanel() 
    {
        ProfilAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        profil_panel.SetActive(false);
    }

    public void OnShopButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = false;
        isInShopPanel = true;

        StartCoroutine(SmoothSetActiveFalseEffectThunder());

        shop_panel.SetActive(true);
    }

    public void OnBackForShopButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInShopPanel = false;

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        StartCoroutine(CloseShopPanel());
    }

    IEnumerator CloseShopPanel() 
    {
        ShopAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        shop_panel.SetActive(false);
    }

    public void OnSettingsButtonClick() 
    {
        SoundOfButtonClick();
        CheckMusicAndSoundActive();
        CheckFPSActive();

        isInMainMenu = false;
        isInSettingsPanel = true;

        StartCoroutine(SmoothSetActiveFalseEffectThunder());

        settings_panel.SetActive(true);
    }

    public void OnBackForSettingsButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInSettingsPanel = false;

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        CheckEffectsActive();

        settings_panel.SetActive(false);
    }

    public void OnBookButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = false;
        isInBookPanel = true;

        StartCoroutine(SmoothSetActiveFalseEffectThunder());

        book_panel.SetActive(true);
    }

    public void OnBackForBookButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInBookPanel = false;

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        StartCoroutine(CloseBookPanel());
    }

    IEnumerator CloseBookPanel() 
    {
        BookAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        book_panel.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        SoundOfButtonClick();

        EffectThunder.SetActive(false);

        isInMainMenu = false;
        isInWarningForExitPanel = true;

        warning_for_exit_panel.SetActive(true);
    }

    public void OnAcceptForExitButtonClick() 
    {
        SoundOfButtonClick();
        Application.Quit();
    }

    public void OnNoForExitButtonClick() 
    {
        SoundOfButtonClick();

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);
        }

        isInMainMenu = true;
        isInWarningForExitPanel = false;

        warning_for_exit_panel.SetActive(false);
    }

    public void OnHelpForZvaniesButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = false;
        isInHelpForZvaniesPanel = true;

        help_zvanies_panel.SetActive(true);
    }

    public void OnBackForHelpZvaniesButtonClick() 
    {
        SoundOfButtonClick();

        isInMainMenu = true;
        isInHelpForZvaniesPanel = false;

        help_zvanies_panel.SetActive(false);
    }

//<--------------------------------Unlock challenge buttons-------------------------------->

    public void OnUnlockChallengeButtonClick() 
    {
        SoundOfButtonClick();

        unlock_challenge_mode_panel.SetActive(true);

        ShowAllCoinsInUnlockChallengeMode.text = (ShopManager.Balance()).ToString();
    }

    public void OnBackForUnlockChallengeButtonClick() 
    {
        SoundOfButtonClick();

        unlock_challenge_mode_panel.SetActive(false);
    }

    public void OnAcceptForUnlockChallengeModeButtonClick() 
    {
        SoundOfButtonClick();

        if (ShopManager.Balance() >= 70) 
        {
            ShopManager.MinusCoinsFromBalance(70);

            PlayerPrefs.SetInt("IsUnlockedChallengeMode", 1);

            lock_icon.SetActive(false);

            challenge_button.interactable = true;

            unlock_challenge_mode_panel.SetActive(false);
        }
    }

//<--------------------------------------------------BOOK BUTTONS-------------------------------------------------------->

    public void OnQuadUravnenieButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = false;
        isInQuadUravBookPanel = true;

        quad_uravnenie_panel.SetActive(true);
    }

    public void OnBackForQuadUravnenieButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = true;
        isInQuadUravBookPanel = false;

        StartCoroutine(CloseQuadPanel());
    }

    IEnumerator CloseQuadPanel() 
    {
        QuadUravAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        quad_uravnenie_panel.SetActive(false);
    }


    public void OnTeoremaVietaButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = false;
        isInTeoremaVietaPanel = true;

        teorema_vieta_panel.SetActive(true);
    }

    public void OnBackForTeoremaVietaButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = true;
        isInTeoremaVietaPanel = false;

        StartCoroutine(CloseTeoremaVietaPanel());
    }

    IEnumerator CloseTeoremaVietaPanel() 
    {
        TeoremaVietaAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        teorema_vieta_panel.SetActive(false);
    }


    public void OnProchentButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = false;
        isInProchentPanel = true;

        prochent_panel.SetActive(true);
    }

    public void OnBackForProchentButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = true;
        isInProchentPanel = false;
        StartCoroutine(CloseProchentPanel());
    }

    IEnumerator CloseProchentPanel() 
    {
        ProchentAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        prochent_panel.SetActive(false);
    }

    public void OnSvoistvaButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = false;
        isInSvoistvaPanel = true;

        svoistva_quad_urav_panel.SetActive(true);
    }

    public void OnBackForSvoistvaButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = true;
        isInSvoistvaPanel = false;

        StartCoroutine(CloseSvoistvaPanel());
    }

    IEnumerator CloseSvoistvaPanel() 
    {
        SvoistvaAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        svoistva_quad_urav_panel.SetActive(false);
    }


    public void OnSinCosTanButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = false;
        isInSinCosTanPanel = true;

        sin_cos_tan_panel.SetActive(true);
    }

    public void OnBackForSinCosTanButtonClick() 
    {
        SoundOfButtonClick();

        isInBookPanel = true;
        isInSinCosTanPanel = false;

        StartCoroutine(CloseSinCosTanPanel());
    }

    IEnumerator CloseSinCosTanPanel() 
    {
        SinCosTanAnim.Play("EndSmoothAnim");
        yield return new WaitForSeconds(timeforSmoothAnim);
        sin_cos_tan_panel.SetActive(false);
    }


//<-----------------------------------------------CHALLENGES BUTTONS----------------------------------------------------->

    public void OnChallengeModeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectModePanel = false;
        isInSelectChallengePanel = true;

        choose_challenge_mode_panel.SetActive(true);
        choose_mode_panel.SetActive(false);
    }

    public void OnBackChallengeModeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectModePanel = true;
        isInSelectChallengePanel = false;

        choose_challenge_mode_panel.SetActive(false);
        choose_mode_panel.SetActive(true);
    }

    public void OnBulletChallengeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectChallengePanel = false;
        isInOpisanieForChallengePanel = true;

        opisanie_challenge_panel.SetActive(true);
        choose_challenge_mode_panel.SetActive(false);
        

        isBulletChallengeMode = true;

        NameChallenge.text = "Быстрее чем пуля";
        OpisanieChallenge.text = "Нет времени объяснять, считай быстрее!";
        TimeForChallenge.text = "3 секунды";
    }

    public void OnPodvoxChallengeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectChallengePanel = false;
        isInOpisanieForChallengePanel = true;

        opisanie_challenge_panel.SetActive(true);
        choose_challenge_mode_panel.SetActive(false);

        isPodvoxChallengeMode = true;

        NameChallenge.text = "Да это же легко...";
        OpisanieChallenge.text = "Задания не так просты, как ты думаешь. Проверь свою смекалку.";
        TimeForChallenge.text = "50 секунд";
    }

    public void OnMeshalkaChallengeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectChallengePanel = false;
        isInOpisanieForChallengePanel = true;

        opisanie_challenge_panel.SetActive(true);
        choose_challenge_mode_panel.SetActive(false);

        isMeshalkaChallengeMode = true;

        NameChallenge.text = "Кручу, верчу, обмануть хочу";
        OpisanieChallenge.text = "Будь внимателен, ответы постояно меняют свое положение! Кто их мешает до сих пор неизвестно, но твоя задача не дать себя обмануть!";
        TimeForChallenge.text = "7 секунд";
    }

    public void OnAcceptForChallengeModeButtonClick() 
    {
        SoundOfButtonClick();

        if (isBulletChallengeMode == true) 
        {
            TaskModes.BulletChallengeMode();
        }
        
        else if (isPodvoxChallengeMode == true) 
        {
            TaskModes.PodvoxChallengeMode();
        }

        else if (isMeshalkaChallengeMode == true) 
        {
            TaskModes.MeshalkaChallengeMode();
        }

        StartCoroutine(LoadLevel("DifferentModes"));
    }

    public void OnBackForOpisanieButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectChallengePanel = true;
        isInOpisanieForChallengePanel = false;

        opisanie_challenge_panel.SetActive(false);
        choose_challenge_mode_panel.SetActive(true);
    }
//<--------------------------------------------------------INFINITY MODE BUTTON------------------------------------------------------------->
    public void OnInfinityModeButtonClick()
    {
        SoundOfButtonClick();

        isInSelectModePanel = false;
        isInSelectDifferentModePanel = true;

        choose_different_panel.SetActive(true);
        choose_mode_panel.SetActive(false);
    }

    public void OnBackFromInfinityModeButtonClick() 
    {
        SoundOfButtonClick();

        isInSelectModePanel = true;
        isInSelectDifferentModePanel = false;

        choose_different_panel.SetActive(false);
        choose_mode_panel.SetActive(true);
    }

    public void OnEasyDifferentModeButtonClick() 
    {
        SoundOfButtonClick();
        TaskModes.EasyMode();
        StartCoroutine(LoadLevel("DifferentModes"));
    }

    public void OnMiddleDifferentModeButtonClick() 
    {
        SoundOfButtonClick();
        TaskModes.MiddleMode();
        StartCoroutine(LoadLevel("DifferentModes"));
    }

    public void OnHighDifferentModeButtonClick() 
    {
        SoundOfButtonClick();
        TaskModes.HighMode();
        StartCoroutine(LoadLevel("DifferentModes"));
    }

//<-------------------------------------------------------LEVELS BUTTONS--------------------------------------------------->
    
    public void OnLevelModeButtonClick()
    {
        SoundOfButtonClick();

        isInSelectModePanel = false;
        isInSelectLevelPanel = true;

        choose_mode_panel.SetActive(false);
        choose_level_panel.SetActive(true);
    }

    public void OnBackForLevelSelectButtonClick()
    {
        SoundOfButtonClick();

        isInSelectModePanel = true;
        isInSelectLevelPanel = false;

        choose_level_panel.SetActive(false);
        choose_mode_panel.SetActive(true);
    }
    
    public void On1LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 1;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On2LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 2;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On3LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 3;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On4LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 4;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On5LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 5;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On6LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 6;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On7LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 7;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On8LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 8;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On9LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 9;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On10LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 10;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On11LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 11;
        StartCoroutine(LoadLevel("Levels"));
    }

    public void On12LevelButtonClick()
    {
        SoundOfButtonClick();
        Levels.level_now = 12;
        StartCoroutine(LoadLevel("Levels"));
    }

// <--------------------------------------------------------SETTING BUTTONS------------------------------------------------------------>

    public void On120FPSButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("FPS", 120);
        Application.targetFrameRate = 120;

        CheckFPSActive();
    }

    public void On90FPSButtonClick()
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("FPS", 90);
        Application.targetFrameRate = 90;

        CheckFPSActive();
    }

    public void On60FPSButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("FPS", 60);
        Application.targetFrameRate = 60;

        CheckFPSActive();
    }

    public void OnEffectsOnButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isEffectsActive", 1);

        OnEffectsIcon.SetActive(true);
        OffEffectIcon.SetActive(false);
    }

    public void OnEffectsOffButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isEffectsActive", 0);
        CheckEffectsActive();
    }

    public void OnMusicOnButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isMusicActive", 0);
        CheckMusicAndSoundActive();
    }

    public void OnMusicOffButtonClick()
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isMusicActive", 1);
        CheckMusicAndSoundActive();
    }

    public void OnSoundOnButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isSoundActive", 0);
        CheckMusicAndSoundActive();
    }

    public void OnSoundOffButtonClick() 
    {
        SoundOfButtonClick();
        PlayerPrefs.SetInt("isSoundActive", 1);
        CheckMusicAndSoundActive();
    }

//<---------------------------------------------------------OTHER-------------------------------------------------------->

    IEnumerator SmoothSetActiveFalseEffectThunder() 
    {
        yield return new WaitForSeconds(0.15f);

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(false);
        }
    }

    IEnumerator LoadLevel(string scene) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(scene);
    }

    public void SoundOfButtonClick() 
    {
        if (PlayerPrefs.GetInt("isSoundActive") == 1)
        {
            ButtonSound.Play();
        }
    }

    public void CheckMusicAndSoundActive() 
    {
        if (PlayerPrefs.GetInt("isMusicActive") == 1)
        {
            BackgroundMusic.SetActive(true);

            MusicOffButton.SetActive(true);
            MusicOnButton.SetActive(false);
        }

        if (PlayerPrefs.GetInt("isMusicActive") == 0)
        {
            BackgroundMusic.SetActive(false);

            MusicOffButton.SetActive(false);
            MusicOnButton.SetActive(true);
        }

        if (PlayerPrefs.GetInt("isSoundActive") == 1)
        {
            SoundOffButton.SetActive(true);
            SoundOnButton.SetActive(false);
        }

        if (PlayerPrefs.GetInt("isSoundActive") == 0)
        {
            SoundOffButton.SetActive(false);
            SoundOnButton.SetActive(true);
        }
    }

    public void CheckEffectsActive() 
    {
        if (PlayerPrefs.GetInt("isEffectsActive") == 0) 
        {
            EffectThunder.SetActive(false);

            OnEffectsIcon.SetActive(false);
            OffEffectIcon.SetActive(true);
        }

        if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectThunder.SetActive(true);

            OnEffectsIcon.SetActive(true);
            OffEffectIcon.SetActive(false);
        }
    }

    public void CheckFPSActive() 
    {
        if (PlayerPrefs.GetInt("FPS") == 120) 
        {
            Icon120FPS.SetActive(true);
            Icon90FPS.SetActive(false);
            Icon60FPS.SetActive(false);
        }

        if (PlayerPrefs.GetInt("FPS") == 90) 
        {
            Icon120FPS.SetActive(false);
            Icon90FPS.SetActive(true);
            Icon60FPS.SetActive(false);
        }

        if (PlayerPrefs.GetInt("FPS") == 60) 
        {
            Icon120FPS.SetActive(false);
            Icon90FPS.SetActive(false);
            Icon60FPS.SetActive(true);
        }
    }
}
