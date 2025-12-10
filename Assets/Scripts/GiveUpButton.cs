using UnityEngine;

public class GiveUpButton : MonoBehaviour
{
    public HandManager handManager;
    public DeckManager deckManager;
    public ScoreManager scoreManager;

    public void OnGiveUp()
    {
        // Reset score
        if (scoreManager != null)
            scoreManager.ResetScore();

        // Reset hand
        if (handManager != null)
            handManager.DrawNewHand();
    }
}
