using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;
    public static CardRevalEvent cardRevalEvent = new CardRevalEvent(null);
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
    public class CardRevalEvent : UnityEngine.Events.UnityEvent<CardTile>
    {
        public CardTile cardTile;
        public CardRevalEvent(CardTile CardTile)
        {
            this.cardTile = CardTile;
        }
    }
}
