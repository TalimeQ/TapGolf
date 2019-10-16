using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour {

    [SerializeField] private GameObject loseScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ToggleLoseScreen()
    {
        loseScreen.SetActive(!loseScreen.activeInHierarchy);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.SetText("" + newScore);
    }
}
