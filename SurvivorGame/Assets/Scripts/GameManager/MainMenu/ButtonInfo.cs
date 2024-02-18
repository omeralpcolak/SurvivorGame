using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ButtonInfo : MonoBehaviour
{
    public GameSelections gameSelections;
    public GameObject holdingObject; //map or character.
    public GameObject cloneObject;
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
            InstantiateMenuObject(holdingObject, new Vector3(0, 0.0f, 0), new Vector3(0, 0,0));
            MainMenuManager.instance.ActivateMenu(MainMenuManager.MenuType.PLAYERSELECTIONMENU);

        }
        else
        {
            InstantiateMenuObject(cloneObject, new Vector3(0, 0.1f, 0), new Vector3(0, 0, -180));
            gameSelections.selectedPlayer = holdingObject;
            
        }
    }

    private void CreateHoldingObject()
    {

    }

    private void InstantiateMenuObject(GameObject selectedObject,Vector3 pos, Vector3 lookDirection)
    {
        if(GameObject.Find(selectedObject.name) != null)
        {
            DestroyImmediate(selectedObject,true);
        }
        else
        {
            GameObject selectedObjIns = Instantiate(selectedObject, pos, Quaternion.LookRotation(lookDirection));
            selectedObjIns.SetActive(true);
        }
    }
}
