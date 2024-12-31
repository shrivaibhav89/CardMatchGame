using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Button exitToMainMenuButton;

    private void OnEnable()
    {
        exitToMainMenuButton.onClick.AddListener(OnExitToMainMenuButtonClicked);
    }
    private void OnDisable()
    {
        exitToMainMenuButton.onClick.RemoveListener(OnExitToMainMenuButtonClicked);
    }

    private void OnExitToMainMenuButtonClicked()
    {
        HideGameOverPanel();
        GameManager.instance.ExitToMainMenu();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}
