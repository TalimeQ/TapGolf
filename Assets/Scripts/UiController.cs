using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour {

    [SerializeField] private LosePanel loseScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ToggleLoseScreen()
    {
        loseScreen.gameObject.SetActive(!loseScreen.gameObject.activeInHierarchy);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("" + newScore);
    }
}
