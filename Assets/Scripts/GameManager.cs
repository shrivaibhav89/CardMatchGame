using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
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
        if (firstCard.cardType == secondCard.cardType)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.cardMatchSound);
        }
        else
        {
            SoundManager.instance.PlaySound(SoundManager.instance.cardMismatchSound);
        }
        yield return new WaitForSeconds(1);
        if (firstCard.cardType == secondCard.cardType)
        {
            //SoundManager.instance.PlaySound(SoundManager.instance.cardMatchSound);
            firstCard.HideCard();
            secondCard.HideCard();
            AddScore(cardMatchScore);
        }
        else
        {
            //  SoundManager.instance.PlaySound(SoundManager.instance.cardFlipSound);
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
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Turns", turns);
        SaveGridData();
    }
    public void SaveGridData()
    {
        GridData gridData = new GridData();
        gridData.gridWidth = gridGenrator.gridWidth;
        gridData.gridHeight = gridGenrator.gridHeight;
        for (int i = 0; i < gridGenrator.cardGrid.transform.childCount; i++)
        {
            CardTile tile = gridGenrator.cardGrid.transform.GetChild(i).GetComponent<CardTile>();
            CellData cellData = new CellData
            {
                index = i,
                isExposed = tile.isExposed,
                cardType = tile.cardType.cardId
            };
            gridData.cells.Add(cellData);
        }

        string json = JsonUtility.ToJson(gridData, true);
        File.WriteAllText(Application.persistentDataPath + "/gridData.json", json);
    }

    // Method to load grid data
    public async void LoadGridData()
    {
        gameState = GameState.GameOver;
        string path = Application.persistentDataPath + "/gridData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GridData gridData = JsonUtility.FromJson<GridData>(json);
            // gridGenrator.gridWidth = gridData.gridWidth;
            // gridGenrator.gridHeight = gridData.gridHeight;
            gridGenrator.CreateGridForLoadgame(gridData.gridWidth,gridData.gridHeight);
            await Task.Delay(100); // Delay to allow grid to be generated before loading data

            for (int i = 0; i < gridData.cells.Count; i++)
            {
                CellData cellData = gridData.cells[i];
                CardTile tile = gridGenrator.tiles[cellData.index];
                tile.isExposed = cellData.isExposed;
                tile.cardType = Array.Find(gridGenrator.cardTypes, ct => ct.cardId == cellData.cardType);
                tile.ResetCard(); // Reset the card to update its appearance
                if (tile.isExposed)
                {
                    tile.HideCard();
                }
               // tile.transform.SetSiblingIndex(cellData.position);
            }
            gameState = GameState.Idle;
        }
    }

    public void StartNewGame(int gridWidth,int gridHeight)
    {
        gridGenrator.gridWidth = gridWidth;
        gridGenrator.gridHeight = gridHeight;
        gridGenrator.GenrateCardGrid();
        score = 0;
        turns = 0;
        gameState = GameState.Idle;
        SaveGame();
    }
    public void LoadGame()
    {
        score = PlayerPrefs.GetInt("Score");
        turns = PlayerPrefs.GetInt("Turns");
        LoadGridData();
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGridData();
        }
    }

    [Serializable]
    public class GridData
    {
        public int gridWidth;
        public int gridHeight;
        public List<CellData> cells = new List<CellData>();
    }

    [Serializable]
    public class CellData
    {
        public int index;
        public bool isExposed;
        public int cardType;
    }
}
