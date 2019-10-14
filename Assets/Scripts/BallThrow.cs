using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour {

    [SerializeField] GameObject trajectoryDotPrefab;

    private Vector2 startLaunchVelocity = new Vector2(2f, 8f);
    private Vector2 startPosition = Vector2.zero;
    private Vector2 gravity = (Vector2)Physics.gravity;
    private Vector2 launchVelocity;

    private bool launched = false;
    private Rigidbody2D rigidBody;
    private int levelLauncModifier = 1;
    private int DotsToShow = 10;
    private float dotTimeStep = 0.1f;

    public int LaunchModifier
    {
        get
        {
            return levelLauncModifier;
        }

        set
        {
            levelLauncModifier = value;
        }
    }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            launchVelocity = startLaunchVelocity;
            startPosition = (Vector2)transform.position;
        }
        if(Input.GetKey(KeyCode.Mouse0))
        {
            launchVelocity += levelLauncModifier * Time.deltaTime * 0.3f * Vector2.one;
        }
        if (!launched && Input.GetKeyUp(KeyCode.Mouse0))
        {

                for (int i = 0; i < DotsToShow; i++)
                {
                    GameObject trajectoryDot = Instantiate(trajectoryDotPrefab);
                    trajectoryDot.transform.position = CalculatePosition(dotTimeStep* i);
                }

            Launch();
        }
    }

    private void Launch()
    {
        rigidBody.velocity = launchVelocity;
        launched = true;
    }

    private Vector2 CalculateLanchVelocity()
    {
        
        return Vector2.zero;
    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return gravity * elapsedTime * elapsedTime * 0.5f +
                   launchVelocity * elapsedTime + startPosition;
    }
}
