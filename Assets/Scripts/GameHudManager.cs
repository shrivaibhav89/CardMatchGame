using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHudManager : MonoBehaviour
{
      [SerializeField] private TMP_Text scoreText;
      [SerializeField] private TMP_Text turnsText;
      [SerializeField] private Button exitButton;
      [SerializeField] private TMP_Text streakText;

      public void OnEnable()
      {
            exitButton.onClick.AddListener(OnExitButtonClicked);
      }

      public void OnDisable()
      {
            exitButton.onClick.RemoveListener(OnExitButtonClicked);
      }

      private void OnExitButtonClicked()
      {
            GameManager.instance.ExitToMainMenu();
      }

      public void UpdateHudData()
      {
            scoreText.text = GameManager.instance.score.ToString("00");
            turnsText.text = GameManager.instance.turns.ToString("00");
            streakText.text = $"x{GameManager.instance.streakMultiplier}";
      }
}
