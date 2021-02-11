using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public int score;
    public Text scoreDisplay;

    private void Awake() 
    {
        score = PlayerPrefs.GetInt("score", score);
        scoreDisplay.text = score.ToString();
    }

    private void Start() 
    {
        score = PlayerPrefs.GetInt("score", score);
        scoreDisplay.text = score.ToString();
    }

    private void FixedUpdate() 
    {
        score = PlayerPrefs.GetInt("score", score);
        scoreDisplay.text = score.ToString();
    }
}
