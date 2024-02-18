using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public enum MenuType
    {
        MAINMENU,
        MAPMENU,
        PLAYERSELECTIONMENU
    }
    public static MainMenuManager instance;
    private GameObject currentMenu;

    public GameObject mainMenu;
    public GameObject mapMenu;
    public GameObject playerSelectionMenu;
    

    public GameSelections gameSelections;
    
    void Start()
    {
        instance = this;
        currentMenu = mainMenu;
    }
    
    public void StartTheGameScene()
    {
        currentMenu.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            SceneManager.LoadScene("GameScene");
        });
        
    }

    public void ActivatePlayerSelectionMenu()
    {
        ActivateMenu(MenuType.MAPMENU);
    }
    public void BackToMainMenu()
    {
        ActivateMenu(MenuType.MAINMENU);
    }

    public void ActivateMenu(MenuType menuType)
    {
        currentMenu.GetComponent<CanvasGroup>().DOFade(0, 1f);
        currentMenu.SetActive(false);

        switch (menuType)
        {
            case MenuType.MAINMENU:
                currentMenu = mainMenu;
                break;
            case MenuType.MAPMENU:
                currentMenu = mapMenu;
                break;
            case MenuType.PLAYERSELECTIONMENU:
                currentMenu = playerSelectionMenu;
                break;

        }

        currentMenu.SetActive(true);
        currentMenu.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }
}
