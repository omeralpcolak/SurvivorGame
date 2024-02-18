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

    private void Start()
    {
        buttonItems.ForEach(x => x.Init(this));
    }

    public void SetClickableOtherButton()
    {
        ButtonItem notClickableButton = buttonItems.Find(x => !x.isClickable);
        notClickableButton.isClickable = true;
        notClickableButton.GetComponent<Button>().interactable = true;
        Destroy(notClickableButton.cloneObjectInst);
        
    }
}
