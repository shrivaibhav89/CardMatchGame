using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameHudManager : MonoBehaviour
{
   [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text turnsText;

   public void UpdateHudData()
   {
         scoreText.text = GameManager.instance.score.ToString("00");
         turnsText.text = GameManager.instance.turns.ToString("00");
   }
}
