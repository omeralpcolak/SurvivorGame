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
    private bool anyButtonClicked = false;

    private void Start()
    {
        buttonItems.ForEach(x => x.Init(this));
    }

    
    public void SetClickableOtherButton()
    {

        if (!anyButtonClicked)
        {
            anyButtonClicked = true;
            return;
        }

        ButtonItem notClickableButton = buttonItems.Find(x => !x.isClickable);
        if (notClickableButton != null)
        {
            notClickableButton.isClickable = true;
            notClickableButton.GetComponent<Button>().interactable = true;
            Destroy(notClickableButton.cloneObjectInst);
        }
        else
        {
            Debug.Log("All buttons are clickable.");
        }
    }
}
