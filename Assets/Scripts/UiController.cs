using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour {

    [SerializeField] private LosePanel loseScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ToggleLoseScreen(int currentScore, int bestScore)
    {
        loseScreen.gameObject.SetActive(true);
        loseScreen.SetFinalScore(currentScore, bestScore);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("" + newScore);
    }
}
