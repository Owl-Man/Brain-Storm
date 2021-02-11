using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	[Header ("Base")]
    int coins;
	public Text CoinsExit;
	int CurrentUser;

	[Header ("RevivalBonus")]
	int isRevivalBonusActive;

    public Button RevivalBonusButton;

    public GameObject ObjectCostOfRevivalBonusText;

	public Text BuyForRevivalBonusText;
	public Text CostOfRevivalBonusText;

	public int CostOfRevivalBonus = 100;

	[Header ("DoubleCoinsBonus")]
	int isDoubleBonusActive;

	public GameObject NotifFinishDoubleBonusPanel;
	public Text ShowEarnedCoins;
	public Text ShowAllCoins;

	public Button DoubleBonusButton;

	public GameObject ObjectCostOfDoubleBonusText;

	public Text BuyForDoubleBonusText;
	public Text CostOfDoubleBonusText;

	public int CostOfDoubleBonus = 50;

	[Header ("Other")]

	public GameObject EffectOfThunder;


	public void Start() 
	{
		CurrentUser = PlayerPrefs.GetInt("CurrentUser");

		UpdateShop();
	}

	IEnumerator UpdateShopEnableSystem() 
	{
		yield return new WaitForSeconds(2f);
		
		UpdateShop();
	}

    public void UpdateShop() 
    {
    	if (PlayerPrefs.GetInt("CheckedUSS") != 1) //Выход из метода и новая попытка через корутин при не выполненной проверки USS
    	{
    		StartCoroutine(UpdateShopEnableSystem());

    		return;
    	}

        DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM Shop;");

        coins = ShopManager.Balance();
        isRevivalBonusActive = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isRevivalBonusActive FROM Shop WHERE id = {CurrentUser};"));
        isDoubleBonusActive = Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isDoubleCoinsBonusActive FROM Shop WHERE id = {CurrentUser};"));

        CoinsExit.text = coins.ToString();
        CostOfRevivalBonusText.text = CostOfRevivalBonus.ToString();
        CostOfDoubleBonusText.text = CostOfDoubleBonus.ToString();

        CheckForFinishDoubleBonusTasks();
        CheckCanUserBuyBonuses();
    }

    public void CheckCanUserBuyBonuses() 
    {
      //<---------------------------------RevivalBonus---------------------------------->

    	if (coins < CostOfRevivalBonus)
		{
			RevivalBonusButton.interactable = false;
		}

		if (isRevivalBonusActive == 1) 
		{
			RevivalBonusButton.interactable = false;
			BuyForRevivalBonusText.text = "Куплено";
			ObjectCostOfRevivalBonusText.SetActive(false);
		}

      //<------------------------------DoubleCoinsBonus------------------------------------>

        if (coins < CostOfDoubleBonus) 
        {
        	DoubleBonusButton.interactable = false;
        }

        if (isDoubleBonusActive == 1) 
        {
        	DoubleBonusButton.interactable = false;
        	BuyForDoubleBonusText.text = "Куплено";
        	ObjectCostOfDoubleBonusText.SetActive(false);
        }
    }

    public void CheckForFinishDoubleBonusTasks() //Проверка на выполнение 10 заданий при купленом бонусе
    {
    	DataTable playerboard = MyDataBaseConnection.GetTable("SELECT * FROM Shop;");
    	
    	if (Convert.ToInt32(MyDataBaseConnection.ExecuteQueryWithAnswer($"SELECT isDoubleCoinsBonusActive FROM Shop WHERE id = {CurrentUser};")) == 1 && PlayerPrefs.GetInt("DoubleBonusFinishTasks") >= 10) 
    	{
    		EffectOfThunder.SetActive(false);

    		NotifFinishDoubleBonusPanel.SetActive(true);
    		PlayerPrefs.SetInt("DoubleBonusFinishTasks", 0);
    		MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isDoubleCoinsBonusActive = {0} WHERE id = '"+CurrentUser+"';");
    		ShowEarnedCoins.text = "+" + CostOfDoubleBonus * 2;
    		Reward.Coins(CostOfDoubleBonus * 2);
    		coins = ShopManager.Balance();
    		ShowAllCoins.text = coins.ToString();
    	}
    }

//<------------------------------------------------------------SHOP BUTTONS---------------------------------------------------------->

    public void OnRevivalBonusButtonClick() 
	{
		MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isRevivalBonusActive = {1} WHERE id = '"+CurrentUser+"';");
		ShopManager.MinusCoinsFromBalance(CostOfRevivalBonus);
		UpdateShop();
	}

	public void OnDoubleCoinsButtonClick()
	{
		MyDataBaseConnection.ExecuteQueryWithoutAnswer($"UPDATE Shop SET isDoubleCoinsBonusActive = {1} WHERE id = '"+CurrentUser+"';");
		ShopManager.MinusCoinsFromBalance(CostOfDoubleBonus);
		UpdateShop();
	}

	public void OnAcceptFinishDoubleBonusTask() 
	{
		NotifFinishDoubleBonusPanel.SetActive(false);

		if (PlayerPrefs.GetInt("isEffectsActive") == 1) 
        {
            EffectOfThunder.SetActive(true);
        }
        
		UpdateShop();
	}
}
