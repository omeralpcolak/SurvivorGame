using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum Type
{
    Map,
    Character
}

public class ButtonPanel : MonoBehaviour
{
    public Type type;
    public List<ButtonItem> buttonItems;
    private bool anyButtonClicked;
    private ButtonItem currentClickedButton;


    private void Start()
    {
        buttonItems.ForEach(x => x.Init(this));
    }

    
    public void SetClickableOtherButton()
    {

        bool anyButtonClickable = false;

        foreach (ButtonItem buttonItem in buttonItems)
        {
            if (buttonItem.isClickable && buttonItem != currentClickedButton)
            {
                anyButtonClickable = true;
                break;
            }
        }

        if (!anyButtonClickable && currentClickedButton != null)
        {
            currentClickedButton = null;
            anyButtonClicked = false;
            return;
        }

        foreach (ButtonItem buttonItem in buttonItems)
        {
            if (!buttonItem.isPurchased)
            {
                buttonItem.isClickable = false;
                buttonItem.GetComponent<Button>().interactable = false;
                continue;
            }

            if (!buttonItem.isClickable && buttonItem != currentClickedButton)
            {
                buttonItem.isClickable = true;
                buttonItem.GetComponent<Button>().interactable = true;
                Destroy(buttonItem.cloneObjectInst);
                break;
            }
        }



    }
}
