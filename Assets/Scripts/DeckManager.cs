using Cards;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    public int startingHand = 6;
    public int maxHand = 14;
    public int currentHand;

    private HandManager hand;
    public AudioClip draw;

    private void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");

        allCards.AddRange(cards);

        hand = FindFirstObjectByType<HandManager>();
        maxHand = hand.maxHand;
    }

    private void Update()
    {
        if (hand != null)
        {
            currentHand = hand.cardsInHand.Count;
        }
    }

    public void DrawCardFromButton()
    {

        Debug.Log("Button pressed!");
        if (hand == null)
            hand = FindFirstObjectByType<HandManager>();

        DrawCard(hand);
    }


    public void DrawCard(HandManager handManager)
    {
        handManager.audioSource.PlayOneShot(draw);
        if (allCards.Count == 0 || handManager.cardsInHand.Count >= maxHand)
        {
            return;
        }
        Card nextCard = allCards[Random.Range(0, allCards.Count)];
        handManager.AddCardToHand(nextCard);
    }
}