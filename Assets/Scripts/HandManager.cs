using Cards;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 3f;
    public float cardSpacing = -120f;
    public float verticalSpacing = 30f;
    public List<GameObject> cardsInHand = new List<GameObject>();

    void Start()
    {

    }

    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        newCard.GetComponent<CardDisplay>().cardData = cardData;

        UpdateHand();
    }

    private void Update()
    {
        UpdateHand();
    }

    private void UpdateHand()
    {
        int cardCount = cardsInHand.Count;
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }
        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float normalizedPos = (2f * i / (cardCount - 1) - 1f);
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f)); ;
            float verticalOffset = verticalSpacing * (1 - normalizedPos * normalizedPos);

            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
