using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public Animator transition;

    public void OnRestartButtonClick() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("score", 0);
    }
    public void OnMenuButtonClick() 
    {
        PlayerPrefs.SetInt("score", 0);
        StartCoroutine(LoadLevel("Menu"));
    }

    IEnumerator LoadLevel(string scene) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(scene);
    }

}
