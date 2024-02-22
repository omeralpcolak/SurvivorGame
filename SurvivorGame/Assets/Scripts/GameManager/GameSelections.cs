using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[CreateAssetMenu(fileName ="New Game Selection", menuName ="Game Selection")]
public class GameSelections : ScriptableObject
{
    public GameObject selectedMap;
    public GameObject selectedPlayer;
    public int coin;
    public int inGameEarnedCoin;


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
        inGameEarnedCoin = 0;
    }

    public void LoadCoinValue()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
    }

    public void UpdateCoinValue(int coinAmount)
    {
        inGameEarnedCoin += coinAmount;
        coin += coinAmount;
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.Save();
    }
}
