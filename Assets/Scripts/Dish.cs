using System.Collections.Generic;
using UnityEngine;
using Cards;

[CreateAssetMenu(fileName = "New Dish", menuName = "Dish")]
public class Dish : ScriptableObject
{
    public string dishName;
    public List<Card.CardType> requiredTypes = new List<Card.CardType>();
}
