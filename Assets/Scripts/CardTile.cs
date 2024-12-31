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
    public bool isRevealed = false;
    public bool isExposed = false;
    public AnimationCurve flipCurve;

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
        if (isRevealed )
        {
            return;
        }
        StartCoroutine(FlipCard());
        SoundManager.instance.PlaySound(SoundManager.instance.cardFlipSound);
        isRevealed = true;
        GameEventsManager.cardRevalEvent.Invoke(this);

    }
    public void ResetCard()
    {

        isRevealed = false;
        StartCoroutine(FLipCardBack());

    }
    public void HideCard()
    {
        isExposed = true;
        button.interactable = false;
        itemImage.sprite = null;
    }

    private IEnumerator FLipCardBack()
    {
        float duration = 0.1f; // Duration of the flip
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);
         
         // i want to multiply the duration by animation curve


        while (elapsedTime < duration)
        {
            transform.rotation  = Quaternion.Lerp(startRotation, endRotation, flipCurve.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration / 2)
            {
                itemImage.sprite = cardBg;
            }
            yield return null;
        }
        transform.rotation = endRotation;
    }

    private IEnumerator FlipCard()
    {
        float duration = 0.3f; // Duration of the flip
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 180, 0);

        while (elapsedTime < duration)
        {
            transform.rotation  = Quaternion.Lerp(startRotation, endRotation, flipCurve.Evaluate(elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= duration / 2)
            {
                itemImage.sprite = cardType.cardImage;
            }
            yield return null;
        }
        transform.rotation = endRotation;
    }
}
