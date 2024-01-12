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

    private GameObject currentMenu;

    public GameObject mainMenu;
    public GameObject mapMenu;
    public GameObject playerSelectionMenu;

    public GameSelections gameSelections;
    // Start is called before the first frame update
    void Start()
    {
        currentMenu = mainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log("clicked");
    }

    public void StartTheGameScene()
    {
        SceneManager.LoadScene("GameScene");
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
