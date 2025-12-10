using UnityEngine;
using Cards;
using System.Collections.Generic;

public class PrepSlot : MonoBehaviour
{
    public Dish currentDish;
    public DishManager dishManager;
    private List<Card.CardType> remainingRequirements;

    private void Start()
    {
        if (currentDish != null)
            remainingRequirements = new List<Card.CardType>(currentDish.requiredTypes);
    }

    public void PlaceCard(Card card, GameObject cardObj)
    {
        if (dishManager == null)
            dishManager = FindFirstObjectByType<DishManager>(); // fallback

        // Ask DishManager if this card is correct
        bool success = dishManager.TryAddCard(card);

        if (success)
        {
            Debug.Log("Correct card for dish!");

            // Remove from hand visually
            HandManager hand = FindFirstObjectByType<HandManager>();
            hand.RemoveCard(cardObj);
            Destroy(cardObj);
        }
        else
        {
            Debug.Log("Wrong ingredient! Returning card...");
            ReturnCardToHand(cardObj);
        }
    }

    private void CheckDishCompletion()
    {
        if (remainingRequirements.Count == 0)
        {
            Debug.Log($"Dish {currentDish.dishName} completed!");
        }
    }

    private void ReturnCardToHand(GameObject cardObj)
    {
        // You can animate it or simply snap back
        cardObj.transform.localPosition = Vector3.zero;
    }
}