using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWin : MonoBehaviour
{
    public Text text_win;

    int choose_for_win;

    private void Start() 
    {
        MotivationForWin();
    }

    public void MotivationForWin() 
    {
        choose_for_win = UnityEngine.Random.Range(1, 11);

        if (choose_for_win == 1) 
        {
            text_win.text = "Отличная работа!";
        }

        if (choose_for_win == 2) 
        {
            text_win.text = "Вы уже явно стали быстрее считать!";
        }

        if (choose_for_win == 3) 
        {
            text_win.text = "Победа победа вместо обеда ;)";
        }

        if (choose_for_win == 4) 
        {
            text_win.text = "Так держать!";
        }

        if (choose_for_win == 5) 
        {
            text_win.text = "Вы идёте к успеху!";
        }

        if (choose_for_win == 6) 
        {
            text_win.text = "Для представления натурального числа в памяти компьютера, оно обычно переводится в двоичную систему счисления";
        }

        if (choose_for_win == 7) 
        {
            text_win.text = "Занимаясь по 15 минут в день, вы можете развиваться. А какую пользу принесут 15 минут, проведенные в соц сетях?";
        }

        if (choose_for_win == 8) 
        {
            text_win.text = "Главное - постоянный прогресс, а не совершенство. Не сбавляйте темпы!";
        }

        if (choose_for_win == 9) 
        {
            text_win.text = "Вы на коне! Пусть огонь знаний не угасает!";
        }

        if (choose_for_win == 10) 
        {
            text_win.text = "Вы прокачали этот навык!";
        }
    }
}
