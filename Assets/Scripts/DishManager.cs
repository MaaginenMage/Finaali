using Cards;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class DishManager : MonoBehaviour
{
    public List<Dish> allDishes;              // Set in Inspector
    public TextMeshProUGUI dishNameText;
    public TextMeshProUGUI requirementsText;

    public HandManager handManager;           // Your script that draws cards

    private Dish currentDish;
    private List<Card.CardType> remainingRequirements;

    public AudioClip ding;
    public AudioClip buzz;

    void Start()
    {
        LoadNewDish();
    }

    public void LoadNewDish()
    {
        if (allDishes.Count == 0)
        {
            Debug.LogWarning("No dishes available!");
            return;
        }

        // Pick a random dish
        currentDish = allDishes[Random.Range(0, allDishes.Count)];

        // Copy the requirements (so original ScriptableObject stays untouched)
        remainingRequirements = new List<Card.CardType>(currentDish.requiredTypes);

        UpdateUI();
        if (handManager != null)
            handManager.DrawNewHand();
    }

    private void UpdateUI()
    {
        dishNameText.text = currentDish.dishName;

        // Clear and rewrite requirement list
        requirementsText.text = "";
        foreach (var req in remainingRequirements)
        {
            requirementsText.text += req.ToString() + "\n";
        }
    }

    public bool TryAddCard(Card card)
    {
        if (remainingRequirements.Contains(card.cardType))
        {
            remainingRequirements.Remove(card.cardType);
            UpdateUI();

            if (remainingRequirements.Count == 0)
            {
                ScoreManager score = FindFirstObjectByType<ScoreManager>();
                if (score != null)
                    score.AddPoint();
                Debug.Log("Dish Completed!");
                LoadNewDish();
            }
            handManager.audioSource.PlayOneShot(ding);
            return true;    // success
        }

        handManager.audioSource.PlayOneShot(buzz);
        return false;       // wrong type
    }
}
