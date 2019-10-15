using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{

    [SerializeField] private DotPool dotPool;

    private Vector2 startLaunchVelocity = new Vector2(0f, 2f);
    private Vector2 startPosition = Vector2.zero;
    private Vector2 gravity = (Vector2)Physics.gravity;
    private Vector2 launchVelocity;
    private Vector2 velocityModifier = new Vector2(1f, 2f);

    private bool launched = false;
    private Rigidbody2D rigidBody;
    private int levelLauncModifier = 1;
    private int DotsToShow = 20;
    private float dotTimeStep = 0.1f;


    public void Init()
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }

    private void Update()
    {
        ParseInputs();
    }

    private void ParseInputs()
    {
        if (!launched)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                InputStart();
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                CalculateLaunchVelocity();
                GenerateTrajectory();
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Launch();
            }
        }
    }

    private void InputStart()
    {
        launchVelocity = startLaunchVelocity;
        startPosition = (Vector2)transform.position;
    }

    private void GenerateTrajectory()
    {
        dotPool.ResetPool();
        startPosition = (Vector2)transform.position;
        for (int i = 0; i < DotsToShow; i++)
        {
            GameObject trajectoryDot = dotPool.GetObjectFromPool();
            trajectoryDot.transform.position = CalculatePosition(dotTimeStep * i);
            trajectoryDot.SetActive(true);
        }
    }

    private void Launch()
    {
        dotPool.ResetPool();
        rigidBody.velocity = launchVelocity;
        launched = true;
    }

    private void CalculateLaunchVelocity()
    {
        launchVelocity += levelLauncModifier * Time.deltaTime * 0.3f * velocityModifier;
    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return gravity * elapsedTime * elapsedTime * 0.5f +
                   launchVelocity * elapsedTime + startPosition;
    }

}
