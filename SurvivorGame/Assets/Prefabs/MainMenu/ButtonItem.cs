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
    public bool isPurchased;

    public Sprite icon;
    public string text;

    public int price;
    //public TMP_Text priceText;

    private Button button;
    public Button buyButton;


    //***********************************//
    //********TRYING MY BEST :((*********//
    //***********************************//

    private void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = icon;
        GetComponentInChildren<TMP_Text>().text = text;
        buyButton.GetComponentInChildren<TMP_Text>().text = price.ToString();

        CheckTheStatus();
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

    private void CheckTheStatus()
    {
        isPurchased = gameSelections.isItemPurchased(text);

        if(gameSelections.coin < price)
        {
            buyButton.GetComponent<Button>().interactable = false;
        }

        if (!isPurchased)
        {
            isClickable = false;
            button.interactable = false;
        }

        if (isPurchased)
        {
            isClickable = true;
            button.interactable = true;
            buyButton.gameObject.SetActive(false);
        }

        
    }

    public void BuyButtonOnClick()
    {
        if (!isPurchased)
        {
            gameSelections.PurchaseTheItem(price,text);
            CheckTheStatus();
        }
    }

}
