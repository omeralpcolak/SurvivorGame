using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Selection", menuName ="Game Selection")]
public class GameSelections : ScriptableObject
{
    public GameObject selectedMap;
    public GameObject selectedPlayer;
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

    public void UpdateCoinValue(int coinAmount)
    {
        coin = coinAmount;
    }
}