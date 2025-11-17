using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string cardName;
        public CardType cardType;

        public Sprite cardImg;
        public Sprite cardBaseImg;
        
        public enum CardType
        {
            Flavor,
            Vegetable,
            Meat,
            Side,
            CookingTechnique,
            Tool
        }
    }
}
