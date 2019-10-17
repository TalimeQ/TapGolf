using UnityEngine;

[CreateAssetMenu(fileName ="New GameMode", menuName = "GameMode")]
public class GameMode : ScriptableObject
{
    [Header("Player")]
    public GameObject playerPrefab;
    public Vector2 ballSpawnCoordinates;
    public float yBallSpawnCoordinates;
    [Space(10)]

    [Header("Hole")]
    public GameObject targetPrefab;
    public Vector2 flagSpawnCoordinates;
    public float yFlagSpawnCoordinates;
    [Space(10)]

    [Header("Throw Values")]
    public float launchOffsetPerLevel;
    public Vector2 initialVelocity;
    public Vector2 VelocityModifier;

}
