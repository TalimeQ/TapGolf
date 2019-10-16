using UnityEngine;
using TMPro;

public class LosePanel : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    public void SetFinalScore(int currentScore, int bestScore)
    {
        currentScoreText.SetText("Score: " + currentScore);
        bestScoreText.SetText("Best: " + bestScore);
    }

    public void OnButtonQuit()
    {
        Application.Quit();
    }

    public void OnButtonRetry()
    {
        GameController.Instance.Restart();
        gameObject.SetActive(false);
    }
}
