using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    public List<ButtonItem> buttonItems;

    private void Start()
    {
        buttonItems.ForEach(x => x.Init(this));
    }

    public void SetClickableOtherButton()
    {
        ButtonItem notClickableButton = buttonItems.Find(x => !x.isClickable);
        Destroy(notClickableButton.cloneObjectInst);
        notClickableButton.GetComponent<Button>().interactable = true;
    }
}
