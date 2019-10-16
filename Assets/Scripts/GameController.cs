using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameMode currentGameMode;
    [SerializeField] UiController uiController;
    [SerializeField] StatsSaver statsSaver;

    private GameObject playerBall;
    private GameObject target;
    private UnityEvent scoreEvent = new UnityEvent();
    private int currentPlayerScore = 0;

    public void Restart()
    {
        currentPlayerScore = 0;

        Ball playerBallComponent = playerBall.GetComponent<Ball>();
        if (playerBallComponent != null)
        {
            playerBallComponent.Reset();
        }
        RandomizeSpawnPosition();
    }

    private void Start ()
    {
        Init();
	}

    private void Init()
    {
        ControllerInit();
        SpawnObjects();
        InitializePrefabs();
        RandomizeSpawnPosition();
    }

    private void ControllerInit()
    {
        currentPlayerScore = 0;
    }

    private void SpawnObjects()
    {
        playerBall = Instantiate(currentGameMode.playerPrefab);
        target = Instantiate(currentGameMode.targetPrefab);
    }

    private void InitializePrefabs()
    {
        Ball playerBallComponent = playerBall.GetComponent<Ball>();
        if(playerBallComponent != null)
        {
            ThrowData data = new ThrowData(currentGameMode.initialVelocity, currentGameMode.VelocityModifier, Physics2D.gravity);
            playerBallComponent.Init(OnPlayerScored,OnPlayerFailed,scoreEvent,data);
        }
    }

    private void RandomizeSpawnPosition()
    {
        float ballSpawnX = UnityEngine.Random.Range(currentGameMode.minBallSpawnCoordinates.x, currentGameMode.maxBallSpawnCoordinates.x);
        float ballSpawnY = UnityEngine.Random.Range(currentGameMode.minBallSpawnCoordinates.y, currentGameMode.maxBallSpawnCoordinates.y);
        float flagSpawnX = UnityEngine.Random.Range(currentGameMode.minflagSpawnCoordinates.x, currentGameMode.maxflagSpawnCoordinates.x);
        float flagSpawnY = UnityEngine.Random.Range(currentGameMode.minflagSpawnCoordinates.y, currentGameMode.maxflagSpawnCoordinates.y);

        playerBall.transform.position = new Vector2(ballSpawnX,ballSpawnY);
        target.transform.position = new Vector2(flagSpawnX, flagSpawnY);

        Rigidbody2D rigidbodyComponent = playerBall.GetComponent<Rigidbody2D>();
        if(rigidbodyComponent != null)
        {
            rigidbodyComponent.velocity = Vector2.zero;
        }
    }

    private void OnPlayerFailed()
    {
        statsSaver.TrySaveBestScore(currentPlayerScore);
        int bestScore = statsSaver.GetBestScore();
        uiController.ToggleLoseScreen(currentPlayerScore,bestScore);
    }

    private void OnPlayerScored()
    {
        RandomizeSpawnPosition();
        IncreaseScore();
        scoreEvent.Invoke();
    }

    private void IncreaseScore()
    {
        currentPlayerScore++;
        uiController.UpdateScore(currentPlayerScore);
    }
}
