using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridGenrator : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 4;
    public CardTile tilePrefab;
    public CardType[] cardTypes;
    public GridLayoutGroup cardGrid;
    public List<CardTile> tiles = new List<CardTile>();


    void Start()
    {
        GenrateCardGrid();
    }
    public void GenrateCardGrid()
    {
        int totalCards = gridWidth * gridHeight;
        List<int> positions = new List<int>();


        for (int i = 0; i < totalCards; i++)
        {
            positions.Add(i);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            int temp = positions[i];
            int randomIndex = UnityEngine.Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        for (int i = 0; i < totalCards / 2; i++)
        {
            CardType cardType = cardTypes[UnityEngine.Random.Range(0, cardTypes.Length)];

            CardTile cardTile1 = Instantiate(tilePrefab, cardGrid.transform);
            cardTile1.gameObject.SetActive(true);
            cardTile1.cardType = cardType;
            tiles.Add(cardTile1);

            // Set position for first card
            int pos1 = positions[i * 2];
            cardTile1.transform.SetSiblingIndex(pos1);

            // Instantiate second card (pair)
            CardTile cardTile2 = Instantiate(tilePrefab, cardGrid.transform);
            cardTile2.gameObject.SetActive(true);
            cardTile2.cardType = cardType;
            tiles.Add(cardTile2);
           
            int pos2 = positions[i * 2 + 1];
            cardTile2.transform.SetSiblingIndex(pos2);
        }


    }
}

[System.Serializable]
public class CardType
{
    public int cardId;
    public Sprite cardImage;
}
