using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName ="New Game Selection", menuName ="Game Selection")]
public class GameSelections : ScriptableObject
{
    public static int Diamond
    {
        get => PlayerPrefs.GetInt("Diamond", 10);
        set => PlayerPrefs.SetInt("Diamond", value);
    }
    public GameObject selectedPlayer;
    public GameObject selectedMap;
    public int coin;


    public void InstantiateSelectedObjects()
    {
        Instantiate(selectedMap, Vector3.zero, Quaternion.identity);
        Instantiate(selectedPlayer, Vector3.zero, Quaternion.identity);
    }

    public void ResetSelectedObjects()
    {
        selectedMap = null;
        selectedPlayer = null;
    }

    public void OnEnable()
    {
        LoadCoinValue();
        //ResetSelectedObjects();
    }

    public void LoadCoinValue()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    public void UpdateCoinValue(int coinAmount)
    {
        coin = coinAmount;
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }

    public void SaveCoin()
    {
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }

    public void PurchaseTheItem(int price, string itemName)
    {
        if (coin >= price)
        {
            coin -= price;
            SaveCoin();
            MainMenuManager.instance.UpdateCoinText();
        }

        PlayerPrefs.SetInt(itemName + "_Purchased", 1);
        PlayerPrefs.Save();
    }

    public bool isItemPurchased(string itemName)
    {
        return PlayerPrefs.GetInt(itemName + "_Purchased", 0) == 1;
    }
}
