using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

// <--------------------------------ALL CALCULATIONS PROCESSES TAKE PLACE HERE--------------------------->
public static class LevelsDataBase
{
	public static int choose_right_answer;
	public static float right_answer;
	public static int choose_type_question;
	public static int choose_sqrt_question_number;
    public static float choose_question_number1;
    public static float choose_question_number2;
    public static int choose_fake_answer1;
    public static int choose_fake_answer2;
    public static string right_answer_string;
    public static string choose_fake_answer1_string;
    public static string choose_fake_answer2_string;

    public static string task;
    public static string prochent;
    public static string chislo;

    public static bool isTaskString;

    public static string question_text;

//<--------------------------------CONDITIONS FOR TASK GENERATE--------------------------------->
	public static void Level1()
	{
		isTaskString = false;

		choose_right_answer = UnityEngine.Random.Range(1, 4);

        choose_question_number1 = UnityEngine.Random.Range(4, 20);
        choose_question_number2 = UnityEngine.Random.Range(4, 20);

        choose_fake_answer1 = UnityEngine.Random.Range(-20, 20);
        choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);

        choose_type_question = UnityEngine.Random.Range(1, 4);

		if (choose_type_question == 1)
            Sum();

        if (choose_type_question == 2)
            Minus();

        if (choose_type_question == 3)
            Time();
	}

	public static void Level2() 
	{
		isTaskString = false;

		choose_right_answer = UnityEngine.Random.Range(1, 4);

        choose_question_number1 = UnityEngine.Random.Range(4, 24);
        choose_question_number2 = UnityEngine.Random.Range(4, 24);

        choose_fake_answer1 = UnityEngine.Random.Range(-20, 24);
        choose_fake_answer2 = UnityEngine.Random.Range(-20, 24);

        choose_type_question = UnityEngine.Random.Range(1, 8);

        if (choose_type_question == 1)
            Sum();

        if (choose_type_question == 2)
            Minus();

        if (choose_type_question == 3)
            Time();

        if (choose_type_question == 4 || choose_type_question == 5 || choose_type_question == 6 || choose_type_question == 7)
        {
            choose_question_number1 = UnityEngine.Random.Range(1, 11);
            choose_question_number2 = UnityEngine.Random.Range(1, 11);

           if (choose_type_question == 4) 
                SumForStepenRight();

           if (choose_type_question == 5) 
                SumForStepenLeft();

           if (choose_type_question == 6) 
                MinusForStepenRight();

           if (choose_type_question == 7) 
                MinusForStepenLeft();
        }
	}

	public static void Level3() 
	{
		isTaskString = false;

		choose_right_answer = UnityEngine.Random.Range(1, 4);

        choose_question_number1 = UnityEngine.Random.Range(-5, 26);
        choose_question_number2 = UnityEngine.Random.Range(-10, 26);

        choose_fake_answer1 = UnityEngine.Random.Range(-20, 26);
        choose_fake_answer2 = UnityEngine.Random.Range(-20, 26);

        choose_type_question = UnityEngine.Random.Range(1, 10);

        if (choose_type_question == 1)
            Sum();

        if (choose_type_question == 2)
            Minus();

        if (choose_type_question == 3)
            Time();

        if (choose_type_question == 4 || choose_type_question == 5 || choose_type_question == 6
         || choose_type_question == 7 || choose_type_question == 8 || choose_type_question == 9)
        {
            choose_question_number1 = UnityEngine.Random.Range(1, 11);
            choose_question_number2 = UnityEngine.Random.Range(1, 11);

           if (choose_type_question == 4) 
                SumForStepenRight();

           if (choose_type_question == 5) 
                SumForStepenLeft();

           if (choose_type_question == 6) 
                MinusForStepenRight();

           if (choose_type_question == 7) 
                MinusForStepenLeft();

           if (choose_type_question == 8) 
                TimeForStepenRight();

           if (choose_type_question == 9) 
                TimeForStepenLeft();
        }
	}

	public static void Level4() 
	{
        isTaskString = false;

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        choose_question_number1 = UnityEngine.Random.Range(-20, 20);
        choose_question_number2 = UnityEngine.Random.Range(-20, 20);

        choose_fake_answer1 = UnityEngine.Random.Range(2, 20);  
        choose_fake_answer2 = UnityEngine.Random.Range(-20, 20);

        choose_type_question = UnityEngine.Random.Range(1, 22);


        if (choose_type_question == 1)
            Sum();

        if (choose_type_question == 2)
            Minus();

        if (choose_type_question == 3)
            Time();

        if (choose_type_question == 4 || choose_type_question == 5 || choose_type_question == 6
         || choose_type_question == 7 || choose_type_question == 8 || choose_type_question == 9
         || choose_type_question == 10 || choose_type_question == 11 || choose_type_question == 12) 
        {
            choose_sqrt_question_number = UnityEngine.Random.Range(1, 8);

            if (choose_sqrt_question_number == 1)
            {
                choose_question_number1 = 4;
                choose_question_number2 = 16;
            }

            if (choose_sqrt_question_number == 2)
            {
                choose_question_number1 = 25;
                choose_question_number2 = 16;
            }

            if (choose_sqrt_question_number == 3)
            {
                choose_question_number1 = 64;
                choose_question_number2 = 4;
            }

            if (choose_sqrt_question_number == 4)
            {
                choose_question_number1 = 9;
                choose_question_number2 = 81;
            }

            if (choose_sqrt_question_number == 5)
            {
                choose_question_number1 = 121;
                choose_question_number2 = 16;
            }

            if (choose_sqrt_question_number == 6)
            {
                choose_question_number1 = 4;
                choose_question_number2 = 9;
            }

            if (choose_sqrt_question_number == 7)
            {
                choose_question_number1 = 64;
                choose_question_number2 = 81;
            }

            if (choose_type_question == 4)
                SumForSqrtBoth();

            if (choose_type_question == 5)
                MinusForSqrtBoth();

            if (choose_type_question == 6)
                TimeForSqrtBoth();

            if (choose_type_question == 7)
                SumForSqrtRight();

            if (choose_type_question == 8)
                SumForSqrtLeft();

            if (choose_type_question == 9)
                MinusForSqrtRight();

            if (choose_type_question == 10)
                MinusForSqrtLeft();

            if (choose_type_question == 11)
                TimeForSqrtRight();

            if (choose_type_question == 12)
                TimeForSqrtLeft();
        }

        if (choose_type_question == 13 || choose_type_question == 14 || choose_type_question == 15
         || choose_type_question == 16 || choose_type_question == 17 || choose_type_question == 18
         || choose_type_question == 19 || choose_type_question == 20 || choose_type_question == 21) 
        {
            choose_question_number1 = UnityEngine.Random.Range(1, 11);
            choose_question_number2 = UnityEngine.Random.Range(1, 11);


            if (choose_type_question == 13) 
                SumForStepenBoth();

            if (choose_type_question == 14) 
                MinusForStepenBoth();

           if (choose_type_question == 15) 
                TimeForStepenBoth();

           if (choose_type_question == 16) 
                SumForStepenRight();

           if (choose_type_question == 17) 
                SumForStepenLeft();

           if (choose_type_question == 18) 
                MinusForStepenRight();

           if (choose_type_question == 19) 
                MinusForStepenLeft();

           if (choose_type_question == 20) 
                TimeForStepenRight();

           if (choose_type_question == 21) 
                TimeForStepenLeft();
        }
	}

    public static void Level5() 
    {
        isTaskString = true;

        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM QuadUravTasks");

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        choose_type_question = UnityEngine.Random.Range(1, 15);

        string task = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Task FROM QuadUravTasks WHERE number = {choose_type_question};");

        question_text = task;

        right_answer_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT RightAnswer FROM QuadUravTasks WHERE number = {choose_type_question};");

        choose_fake_answer1_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer1 FROM QuadUravTasks WHERE number = {choose_type_question};");
        choose_fake_answer2_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer2 FROM QuadUravTasks WHERE number = {choose_type_question};");
    }

    public static void Level6() 
    {
        Level5();
    }

	public static void Level7() 
	{
		isTaskString = false;

		DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM ProchentTasks");

		choose_type_question = UnityEngine.Random.Range(1, 11);

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        prochent = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Prochent FROM ProchentTasks WHERE Number = {choose_type_question};");
        chislo = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Chislo FROM ProchentTasks WHERE Number = {choose_type_question};");

        question_text = "Найдите " + prochent + "% от " + chislo;

        right_answer = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT RightAnswer FROM ProchentTasks WHERE Number = {choose_type_question};"));

        choose_fake_answer1 = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer1 FROM ProchentTasks WHERE Number = {choose_type_question};"));
        choose_fake_answer2 = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer2 FROM ProchentTasks WHERE Number = {choose_type_question};"));
	}

	public static void Level8() 
	{
		Level7();
	}

	public static void Level9() 
	{
	    isTaskString = true;

	    DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM SinCosTanTasks");

		choose_type_question = UnityEngine.Random.Range(1, 10);

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        task = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Task FROM SinCosTanTasks WHERE number = {choose_type_question};");

        question_text = task;

        right_answer_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT RightAnswer FROM SinCosTanTasks WHERE number = {choose_type_question};");

        choose_fake_answer1_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer1 FROM SinCosTanTasks WHERE number = {choose_type_question};");
        choose_fake_answer2_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer2 FROM SinCosTanTasks WHERE number = {choose_type_question};");
	}

	public static void Level10() 
	{
		isTaskString = false;

		DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM DiskriminantTasks");

        choose_type_question = UnityEngine.Random.Range(1, 21);

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        task = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Task FROM DiskriminantTasks WHERE Number = {choose_type_question};");

        question_text = "Найдите дискриминант " + task;

        right_answer = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT RightAnswer FROM DiskriminantTasks WHERE Number = {choose_type_question};"));

        choose_fake_answer1 = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer1 FROM DiskriminantTasks WHERE Number = {choose_type_question};"));
        choose_fake_answer2 = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer2 FROM DiskriminantTasks WHERE Number = {choose_type_question};"));
	}

	public static void Level11() 
	{
		Level10();
 	}

	public static void Level12() 
	{
		Level10();
	}

	public static void SpecialPodvoxTasks() 
	{
		isTaskString = true;
		
		DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM PodvoxTasks");

		choose_type_question = UnityEngine.Random.Range(1, 10);

        choose_right_answer = UnityEngine.Random.Range(1, 4);

        task = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Task FROM PodvoxTasks WHERE number = {choose_type_question};");

        question_text = task;

        right_answer_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT RightAnswer FROM PodvoxTasks WHERE number = {choose_type_question};");

        choose_fake_answer1_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer1 FROM PodvoxTasks WHERE number = {choose_type_question};");
        choose_fake_answer2_string = MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT FakeAnswer2 FROM PodvoxTasks WHERE number = {choose_type_question};");
	}

//<-----------------------------------------------CALCULATIONS PROCESSES---------------------------------------------->
    public static void Sum()
    {
        right_answer = choose_question_number1 + choose_question_number2;
        question_text = choose_question_number1 + " + " + choose_question_number2;
    }

    public static void Minus()
    {
        right_answer = choose_question_number1 - choose_question_number2;
        question_text = choose_question_number1 + " - " + choose_question_number2;
    }

    public static void Time()  //умножение
    {
        right_answer = choose_question_number1 * choose_question_number2;
        question_text = choose_question_number1 + " * " + choose_question_number2;
    }

    public static void SumForStepenRight() 
    {
        right_answer = choose_question_number1 + Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + " + " + choose_question_number2 + "²";
    }

    public static void SumForStepenLeft() 
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) + choose_question_number2;
        question_text = choose_question_number1 + "²" + " + " + choose_question_number2;
    }

    public static void MinusForStepenRight() 
    {
        right_answer = choose_question_number1 - Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + " - " + choose_question_number2 + "²";
    }

    public static void MinusForStepenLeft() 
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) - choose_question_number2;
        question_text = choose_question_number1 + "²" + " - " + choose_question_number2;
    }

    public static void TimeForStepenRight() 
    {
        right_answer = choose_question_number1 * Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + " * " + choose_question_number2 + "²";
    }

    public static void TimeForStepenLeft() 
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) * choose_question_number2;
        question_text = choose_question_number1 + "²" + " * " + choose_question_number2;
    }

    public static void SumForStepenBoth() //с обеих стороны степени
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) + Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + "²" + " + " + choose_question_number2 + "²";
    }

    public static void MinusForStepenBoth() //с обеих стороны степени
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) - Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + "²" + " - " + choose_question_number2 + "²";
    }

    public static void TimeForStepenBoth() //с обеих стороны степени
    {
        right_answer = Mathf.Pow(choose_question_number1, 2) * Mathf.Pow(choose_question_number2, 2);
        question_text = choose_question_number1 + "²" + " * " + choose_question_number2 + "²";
    }

    public static void SumForSqrtRight() 
    {
        right_answer = choose_question_number1 + Mathf.Sqrt(choose_question_number2);
        question_text = choose_question_number1 + " + " + "√" + choose_question_number2;
    }

    public static void SumForSqrtLeft() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) + choose_question_number2;
        question_text = "√" + choose_question_number1 + " + " + choose_question_number2;
    }

    public static void MinusForSqrtRight() 
    {
        right_answer = choose_question_number1 - Mathf.Sqrt(choose_question_number2);
        question_text = choose_question_number1 + " - " + "√" + choose_question_number2;
    }

    public static void MinusForSqrtLeft() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) - choose_question_number2;
        question_text = "√" + choose_question_number1 + " - " + choose_question_number2;
    }

    public static void TimeForSqrtRight() 
    {
        right_answer = choose_question_number1 * Mathf.Sqrt(choose_question_number2);
        question_text = choose_question_number1 + " * " + "√" + choose_question_number2;
    }

    public static void TimeForSqrtLeft() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) * choose_question_number2;
        question_text = "√" + choose_question_number1 + " * " + choose_question_number2;
    }

    public static void SumForSqrtBoth() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) + Mathf.Sqrt(choose_question_number2);
        question_text = "√" + choose_question_number1 + " + " + "√" + choose_question_number2;
    }

    public static void MinusForSqrtBoth() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) - Mathf.Sqrt(choose_question_number2);
        question_text = "√" + choose_question_number1 + " - " + "√" + choose_question_number2;
    }

    public static void TimeForSqrtBoth() 
    {
        right_answer = Mathf.Sqrt(choose_question_number1) * Mathf.Sqrt(choose_question_number2);
        question_text = "√" + choose_question_number1 + " * " + "√" + choose_question_number2;
    }
}
