using UnityEngine;

[CreateAssetMenu(fileName ="New GameMode", menuName = "GameMode")]
public class GameMode : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject targetPrefab;
    public float launchOffsetPerLevel;
}
