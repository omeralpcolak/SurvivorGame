using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public List<ButtonItem> buttonItems;


    public void SetClickableOtherButton()
    {
        ButtonItem deClickableButton = buttonItems.Find(x => !x.isClickable);
        deClickableButton.GetComponent<Button>().interactable = true;
    }
}
