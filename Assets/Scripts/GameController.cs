using System;
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

    private void OnPlayerFailed()
    {

    }

    private void OnPlayerScored()
    {
        currentLevel++;
    }
}
