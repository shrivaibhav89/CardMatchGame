using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardTile : MonoBehaviour
{
    public CardType cardType;
    public Image itemImage;
    public Button button;
    [SerializeField] private Sprite cardBg;
    private bool isRevealed = false;

    private void OnEnable()
    {
        button.onClick.AddListener(RevealCard);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(RevealCard);
    }
    public void OnCLick()
    {
        RevealCard();
    }
    private void RevealCard()
    {
        if (isRevealed || GameManager.instance.gameState != GameManager.GameState.Idle)
        {
            return;
        }
        isRevealed = true;
        GameEventsManager.cardRevalEvent.Invoke(this);
        itemImage.sprite = cardType.cardImage;
    }
    public void ResetCard()
    {
        isRevealed = false;
        itemImage.sprite = cardBg;
    }
    public void HideCard()
    {
        button.interactable = false;
        itemImage.sprite = null;
    }
}
