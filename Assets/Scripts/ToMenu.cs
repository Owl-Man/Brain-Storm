using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    public GameObject warning;

    public Animator transition;
    
    private void Start() 
    {
        warning.SetActive(false);
    }

    public void OnMenuButtonClick() 
    {
        warning.SetActive(true);
    }

    public void OnBackForWarningButtonClick() 
    {
        warning.SetActive(false);
    }

    public void OnAcceptButtonClick() 
    {
       StartCoroutine(LoadLevel("Menu"));
    }

    IEnumerator LoadLevel(string scene) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(0.4f);

        SceneManager.LoadScene(scene);
    }
}
