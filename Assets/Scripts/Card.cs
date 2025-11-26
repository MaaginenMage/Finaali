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
            Technique,
            Equipment,
            Tool
        }
    }
}
