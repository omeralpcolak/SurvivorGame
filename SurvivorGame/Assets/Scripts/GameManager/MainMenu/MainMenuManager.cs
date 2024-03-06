using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public enum MenuType
    {
        MAINMENU,
        MAPMENU,
        PLAYERSELECTIONMENU,
        LEVELMENU
    }
    public static MainMenuManager instance;
    public  CinemachineVirtualCamera cam;

    private GameObject currentMenu;
    public GameObject mainMenu;
    public GameObject mapMenu;
    public GameObject playerSelectionMenu;
    public GameObject levelMenu;
    public GameObject fadeImg;

    public Transform camLookAtPos;

    public TMP_Text coinText;
    public GameObject popUpBubble;

    public GameSelections gameSelections;
    
    void Start()
    {
        instance = this;
        currentMenu = mainMenu;
        UpdateCoinText();
        //PlayerPrefs.DeleteAll();
        fadeImg.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
        {
            fadeImg.SetActive(false);
        });
    }

    public void UpdateCoinText()
    {
        coinText.text = "Coin: " + gameSelections.coin.ToString();
    }

    public void StartTheGameScene()
    {
        GameObject insMap = gameSelections.selectedMap;
        GameObject insChar = gameSelections.selectedPlayer;

        if(insMap == null)
        {
            PopUpBubble("Please select a map for starting the game.",0.5f);
            return;
        }

        if(insChar == null)
        {
            PopUpBubble("Please choose a character for starting the game.",0.5f);
            return;
        }


        if(insMap !=null && insChar != null)
        {
            currentMenu.GetComponent<CanvasGroup>().DOFade(0, 1f).OnComplete(delegate
            {
                SceneManager.LoadScene("GameScene");
            });
        }
        
        
    }

    public void PopUpBubble(string text, float duration)
    {
        popUpBubble.GetComponentInChildren<TMP_Text>().text = text;
        StartCoroutine(PopUpBubbleRtn());

        IEnumerator PopUpBubbleRtn()
        {
            popUpBubble.transform.DOScale(5, duration);
            yield return new WaitForSeconds(duration+0.8f);
            popUpBubble.transform.DOScale(0f, duration);
        }
        
    }

    public void ActivatePlayerSelectionMenu()
    {
        CamMove(new Vector3(0,1,-7));
        ActivateMenu(MenuType.PLAYERSELECTIONMENU);
    }

    public void ActivateLevelMenu()
    {
        ActivateMenu(MenuType.LEVELMENU);
        coinText.gameObject.SetActive(true);
    }

    public void ActivateMapMenu()
    {
        camLookAtPos.transform.DOMoveY(5,1);
        CamMove(new Vector3(0,10,-15));
        ActivateMenu(MenuType.MAPMENU);
    }
    public void BackToMainMenu()
    {
        CamMove(new Vector3(0, 1, -15));
        camLookAtPos.transform.DOMoveY(20, 1);
        ActivateMenu(MenuType.MAINMENU);
        coinText.gameObject.SetActive(false);
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
            case MenuType.LEVELMENU:
                currentMenu = levelMenu;
                break;
            case MenuType.PLAYERSELECTIONMENU:
                currentMenu = playerSelectionMenu;
                break;

        }
        
        currentMenu.SetActive(true);
        currentMenu.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }

    private void CamMove(Vector3 pos)
    {
        cam.transform.DOMove(pos, 1f);
    }

   

}
