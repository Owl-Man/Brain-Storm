using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public class UpdateStabilitySystem : MonoBehaviour
{
    public bool ClearCheckedUSS;

    void Start()
    {
    	try 
    	{
    		if (PlayerPrefs.GetInt("CheckedUSS") != 1) 
    	    {
    		    USS.MainSystem();
            	PlayerPrefs.SetInt("CheckedUSS", 1);

            	Debug.Log("USS finish work");
    	    }
    	    else 
    	    {
    	    	USS.AdditionalySystem(); //Мелкие проверки, которые являются допольнительной частью USS
    	    }

    	    if (ClearCheckedUSS == true) 
    	    {
    		    PlayerPrefs.SetInt("CheckedUSS", 0);
    		    Debug.Log("Cleared, USS ready to work after restart AND remember disable ClearCheckedUSS");
    	    }
    	}
    	catch (Exception ex) 
    	{
    		Debug.Log(ex);
    	}
    }
}

public static class USS
{
	private static int AllUsers;
	private static int CheckingUser = 1;

	public static void AdditionalySystem() //Player Prefs и другие дополнительные проверки
	{
		if (PlayerPrefs.GetInt("CheckedUSS") == null) 
		{
			PlayerPrefs.SetInt("CheckedUSS", 0);
		}

		if (PlayerPrefs.GetInt("DoubleBonusFinishTask") == null) 
		{
			PlayerPrefs.SetInt("DoubleBonusFinishTask", 0);
		}

		if (PlayerPrefs.GetInt("IsUnlockedChallengeMode") != 1 && PlayerPrefs.GetInt("IsUnlockedChallengeMode") != 0) 
		{
			PlayerPrefs.SetInt("IsUnlockedChallengeMode", 0);
		}
	}

	public static void MainSystem() //Главный метод с задачами для USS
	{
		AllUsers = PlayerPrefs.GetInt("AllUsers");

		AdditionalySystem();

		ShopCheck();
	}

	private static void ShopCheck() 
	{
		DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM Shop;");

		while (CheckingUser <= AllUsers)
		{
		    try 
		    {
		    	if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isDoubleCoinsBonusActive FROM Shop WHERE id = {CheckingUser};")) == null)
		        {
			        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isDoubleCoinsBonusActive = {0} WHERE id = '"+CheckingUser+"';");
		        }
		    }
		    catch(Exception ex)
		    {
		    	MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isDoubleCoinsBonusActive = {0} WHERE id = '"+CheckingUser+"';");
		    }

		    CheckingUser++;
		}

		if (CheckingUser > AllUsers)
		{
			CheckingUser = 1;
		}
	}
}