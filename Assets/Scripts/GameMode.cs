using UnityEngine;

[CreateAssetMenu(fileName ="New GameMode", menuName = "GameMode")]
public class GameMode : ScriptableObject
{
    public GameObject playerPrefab;
    public Vector2 minBallSpawnCoordinates;
    public Vector2 maxBallSpawnCoordinates;

    public GameObject targetPrefab;
    public Vector2 minflagSpawnCoordinates;
    public Vector2 maxflagSpawnCoordinates;

    public float launchOffsetPerLevel;
}
