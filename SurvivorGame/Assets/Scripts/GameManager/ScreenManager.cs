using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenManager : MonoBehaviour
{
    public enum Screen
    {
        MAINMENU,
        SKILLUPGRADEMENU,
        RANDOMSKILLMENU,
        NOMENU
    }

    private GameObject currentMenu;

    public GameObject mainMenu;
    public GameObject skillUpgradeMenu;
    public GameObject randomSkillMenu;
    public GameObject noMenu;

    private void Start()
    {
        currentMenu = mainMenu;
    }

    public void ChangeScreen(Screen screen)
    {
        currentMenu.GetComponent<CanvasGroup>().DOFade(0, 1f);
        currentMenu.SetActive(false);

        switch (screen)
        {
            case Screen.MAINMENU:
                currentMenu = mainMenu;
                break;
            case Screen.SKILLUPGRADEMENU:
                currentMenu = skillUpgradeMenu;
                break;
            case Screen.RANDOMSKILLMENU:
                currentMenu = randomSkillMenu;
                break;
            case Screen.NOMENU:
                currentMenu = noMenu;
                break;
        }

        currentMenu.GetComponent<CanvasGroup>().DOFade(1, 1f);
        currentMenu.SetActive(true);
    }
}
