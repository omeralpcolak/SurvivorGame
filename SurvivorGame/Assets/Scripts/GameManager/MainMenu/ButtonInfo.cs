using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public GameSelections gameSelections;
    public GameObject holdingObject; //map or character.
    public bool isItMap;

    public void SelectGameobject()
    {
        if (isItMap)
        {
            gameSelections.selectedMap = holdingObject;
            MainMenuManager.instance.StartTheGameScene();
        }
        else
        {
            gameSelections.selectedPlayer = holdingObject;
            MainMenuManager.instance.ActivateMenu(MainMenuManager.MenuType.MAPMENU);
        }
    }
}
