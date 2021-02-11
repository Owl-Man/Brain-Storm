using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreDisplay;

    private void FixedUpdate() 
    {
        PlayerPrefs.GetInt("score", score);
        scoreDisplay.text = score.ToString();
    }

    public void Right()
    {
        score++;
        PlayerPrefs.SetInt("score", score);
    }
}
