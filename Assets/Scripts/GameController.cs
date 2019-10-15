﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameMode currentGameMode;

    private GameObject playerBall;
    private GameObject target;
    private int currentLevel = 0;

    public int CurrentLevel { get { return currentLevel; } }

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
        currentLevel = 0;
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
            playerBallComponent.Init();
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

        Debug.Log(playerBall.transform.position + "   " + target.transform.position);
    }

    private void OnPlayerFailed()
    {

    }

    private void OnPlayerScored()
    {
        currentLevel++;
    }
}