using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonItem : MonoBehaviour
{   
    public GameSelections gameSelections;
    private ButtonPanel ownerPanel;

    public GameObject holdingObject; //map or character.
    public GameObject cloneObject;

    [HideInInspector]public GameObject cloneObjectInst;

    public bool isClickable;

    public Sprite icon;
    public string text;

    private TMP_Text textComponent;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = icon;
        GetComponentInChildren<TMP_Text>().text = text;
    }

    public void Init(ButtonPanel owner)
    {
        ownerPanel = owner;
    }

    public void OnClick()
    {
        ownerPanel.SetClickableOtherButton();
        isClickable = false;
        button.interactable = false;
        cloneObjectInst = Instantiate(cloneObject);

        switch (ownerPanel.type)
        {
            case Type.Map:
                gameSelections.selectedMap = holdingObject;
                break;
            case Type.Character:
                gameSelections.selectedPlayer = holdingObject;
                break;
        }
    }
}
