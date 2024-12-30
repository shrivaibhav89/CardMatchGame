using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Idle,
        MatchingCard,
        GameOver
    }
    public GridGenrator gridGenrator;
    public int score = 0;
    [SerializeField] private int turns = 0;
    public CardTile firstCard;
    public CardTile secondCard;

    private int cardMatchScore = 5;
    public GameState gameState = GameState.Idle;
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnEnable()
    {
        GameEventsManager.cardRevalEvent.AddListener(OnCardReveal);
    }
    private void OnDisable()
    {
        GameEventsManager.cardRevalEvent.RemoveListener(OnCardReveal);
    }

    private void OnCardReveal(CardTile cardTile)
    {
        if (firstCard != null && secondCard != null)
        {
            return;
        }
        turns++;
        if (firstCard == null)
        {
            firstCard = cardTile;
        }
        else
        {
            secondCard = cardTile;
            CheckMatch();
        }
    }
    private IEnumerator CheckMatchCards()
    {
        yield return new WaitForSeconds(1);
        if (firstCard.cardType == secondCard.cardType)
        {
            firstCard.HideCard();
            secondCard.HideCard();
            AddScore(cardMatchScore);
        }
        else
        {
            firstCard.ResetCard();
            secondCard.ResetCard();
        }
        firstCard = null;
        secondCard = null;
        gameState = GameState.Idle;
    }
    public void CheckMatch()
    {
        gameState = GameState.MatchingCard;
        StartCoroutine(CheckMatchCards());
    }

    public void AddScore(int matchScore)
    {
        score += matchScore;
    }


    private void Start()
    {
        gridGenrator.GenrateCardGrid();
    }
}
