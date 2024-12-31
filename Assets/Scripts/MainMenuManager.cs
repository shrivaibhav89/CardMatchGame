using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button newGameButton;
    public Button loadgameButton;
    public TMP_Dropdown griddWidth;
    public TMP_Dropdown gridHeight;
    public TMP_Text compatibilityMessage;
    public GameObject mainMenuPanel;


    public void OnEnable()
    {
        newGameButton.onClick.AddListener(OnNewGame);
        loadgameButton.onClick.AddListener(OnLoadGame);
        griddWidth.onValueChanged.AddListener(CheckGridCompatibility);
        gridHeight.onValueChanged.AddListener(CheckGridCompatibility);
    }



    public void OnDisable()
    {
        newGameButton.onClick.RemoveListener(OnNewGame);
        loadgameButton.onClick.RemoveListener(OnLoadGame);
        griddWidth.onValueChanged.RemoveListener(CheckGridCompatibility);
        gridHeight.onValueChanged.RemoveListener(CheckGridCompatibility);
    }

    private void CheckGridCompatibility(int arg0)
    {
        int width = int.Parse(griddWidth.options[griddWidth.value].text);
        int height = int.Parse(gridHeight.options[gridHeight.value].text);
        if (width * height % 2 != 0)
        {
            newGameButton.interactable = false;
            ShowCompatibilitymessage();
        }
        else
        {
            HideCompatibilitymessage();
            newGameButton.interactable = true;
        }

    }

    private void ShowCompatibilitymessage()
    {
        compatibilityMessage.gameObject.SetActive(true);
        compatibilityMessage.text = "Grid is not compatible";
    }

    private void HideCompatibilitymessage()
    {
        compatibilityMessage.gameObject.SetActive(false);
        newGameButton.interactable = true;
    }

    private void OnNewGame()
    {
        int width = int.Parse(griddWidth.options[griddWidth.value].text);
        int height = int.Parse(gridHeight.options[gridHeight.value].text);
        GameManager.instance.StartNewGame(width,height);
        mainMenuPanel.SetActive(false);

    }
    private void OnLoadGame()
    {
        GameManager.instance.LoadGame();
        mainMenuPanel.SetActive(false);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }
}
