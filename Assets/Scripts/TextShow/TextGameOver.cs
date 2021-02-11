using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGameOver : MonoBehaviour
{
    public Text text_game_over;

    int choose_for_game_over;

    private void Start() 
    {
        MotivationForGameOver();
    }

    public void MotivationForGameOver() 
    {
        choose_for_game_over = UnityEngine.Random.Range(1, 8);

        if (choose_for_game_over == 1) 
        {
            text_game_over.text = "Ты не проигравший до тех пор, пока ты не сдался!";
        }

        if (choose_for_game_over == 2) 
        {
            text_game_over.text = "Без труда, не выловишь и рыбку из пруда!";
        }

        if (choose_for_game_over == 3) 
        {
            text_game_over.text = "Попробуйте еще раз, не сдавайтесь!";
        }

        if (choose_for_game_over == 4) 
        {
            text_game_over.text = "Не отчаивайтесь, повторение мать учения!";
        }

        if (choose_for_game_over == 5) 
        {
            text_game_over.text = "Считай еще быстрей, у тебя получится!";
        }

        if (choose_for_game_over == 6) 
        {
            text_game_over.text = "Непростое задание! Отлично справляетесь!";
        }

        if (choose_for_game_over == 7) 
        {
            text_game_over.text = "Ничего страшного! Скоро вы освоите эту тему.";
        }
    }
}
