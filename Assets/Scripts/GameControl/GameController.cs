﻿using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameMode currentGameMode;
    [SerializeField] private UiController uiController;
    [SerializeField] private StatsSaver statsSaver;

    private GameObject playerBall;
    private GameObject target;
    private UnityEvent scoreEvent = new UnityEvent();
    private int currentPlayerScore = 0;

    public void Restart()
    {
        currentPlayerScore = 0;
        uiController.UpdateScore(currentPlayerScore);

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

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
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
            ThrowData data = new ThrowData(currentGameMode.initialVelocity, currentGameMode.VelocityModifier, Physics2D.gravity, currentGameMode.launchOffsetPerLevel, currentGameMode.yBallSpawnCoordinates);
            playerBallComponent.Init(OnPlayerScored,OnPlayerFailed,scoreEvent,data);
        }
    }

    private void RandomizeSpawnPosition()
    {
        float ballSpawnX = Random.Range(currentGameMode.ballSpawnCoordinates.x, currentGameMode.ballSpawnCoordinates.y);
        float ballSpawnY = currentGameMode.yBallSpawnCoordinates;
        float flagSpawnX = Random.Range(currentGameMode.flagSpawnCoordinates.x, currentGameMode.flagSpawnCoordinates.y);
        float flagSpawnY = currentGameMode.yFlagSpawnCoordinates;

        playerBall.transform.position = new Vector2(ballSpawnX, ballSpawnY);
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
