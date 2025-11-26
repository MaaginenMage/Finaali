using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardBase;
    public Image cardImage;
    public TMP_Text typeText;
    public TMP_Text nameText;

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        typeText.text = cardData.cardType.ToString();
        nameText.text = cardData.cardName;

        if (cardData.cardBaseImg != null)
            cardBase.sprite = cardData.cardBaseImg;

        if (cardData.cardImg != null)
            cardImage.sprite = cardData.cardImg;
    }
}