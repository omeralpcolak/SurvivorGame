using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonInfo : MonoBehaviour
{
    public GameSelections gameSelections;
    public GameObject holdingObject; //map or character.
    public bool isItMap;
    public Sprite icon;
    public string text;
    private Button button;
    private TMP_Text textComponent;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        textComponent = GetComponentInChildren<TMP_Text>();
        button.GetComponent<Image>().sprite = icon;
        textComponent.text = text;
        
    }

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
