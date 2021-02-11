using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;

public static class ShopManager
{
	public static int coins;
	public static int CurrentUser = PlayerPrefs.GetInt("CurrentUser");

	public static int Balance() //Дает возможность узнать баланс игрока
	{
		DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM Shop;");

		coins = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT Coins FROM Shop WHERE id = {CurrentUser};"));

		return coins; //Возвращает требуемый баланс монет
	}

	public static void MinusCoinsFromBalance(int coinsMinus) // вычитает из баланса указанное количество монет
	{
		coins = Balance();
		coins -= coinsMinus;
		MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET Coins = '"+coins+"' WHERE id = '"+CurrentUser+"';");
	}
}

public static class Reward
{
	public static int GlobalRewardCoins; //Позволяет другим скриптам узнать сколько монет получил игрок

    public static void Coins(int coins) //Вознаграждает игрока указанными монетами
    {
        ShopManager.coins = ShopManager.Balance() + coins;
        GlobalRewardCoins = coins;
        MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET Coins = '"+ShopManager.coins+"' WHERE id = '"+ShopManager.CurrentUser+"';");
    }
}
