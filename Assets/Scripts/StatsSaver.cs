using UnityEngine;

public class StatsSaver : MonoBehaviour
{

    [SerializeField] private string scoreSaveKeyName = "BestScore";

    public void TrySaveBestScore(int scoreToSave)
    {
       if(PlayerPrefs.HasKey(scoreSaveKeyName))
       {
           int currentBestScore = PlayerPrefs.GetInt(scoreSaveKeyName);
           if(scoreToSave > currentBestScore)
           {
                PlayerPrefs.SetInt(scoreSaveKeyName, scoreToSave);
           }
       }
    }

    public int GetBestScore()
    {
        if (PlayerPrefs.HasKey(scoreSaveKeyName))
        {
            return PlayerPrefs.GetInt(scoreSaveKeyName);
        }
        else
        {
            return 0;
        }
    }
}
