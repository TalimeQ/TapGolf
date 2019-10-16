using UnityEngine;

[CreateAssetMenu(fileName ="New GameMode", menuName = "GameMode")]
public class GameMode : ScriptableObject
{
    [Header("Player")]
    public GameObject playerPrefab;
    public Vector2 minBallSpawnCoordinates;
    public Vector2 maxBallSpawnCoordinates;
    [Space(10)]

    [Header("Hole")]
    public GameObject targetPrefab;
    public Vector2 minflagSpawnCoordinates;
    public Vector2 maxflagSpawnCoordinates;
    [Space(10)]

    [Header("Throw Values")]
    public float launchOffsetPerLevel;
    public Vector2 initialVelocity;
    public Vector2 VelocityModifier;

}
