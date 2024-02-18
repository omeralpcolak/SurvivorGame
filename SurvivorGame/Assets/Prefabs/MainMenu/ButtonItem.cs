using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonItem : MonoBehaviour
{   
    public GameSelections gameSelections;
    public ButtonPanel buttonPanel;

    public GameObject holdingObject; //map or character.
    public GameObject cloneObject;

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

    public void OnClick()
    {
        buttonPanel.SetClickableOtherButton();
        button.interactable = false;

    }
}
