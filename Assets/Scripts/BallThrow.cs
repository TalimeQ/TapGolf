using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BallThrow : MonoBehaviour
{

    [SerializeField] private DotPool dotPool;

    private ThrowData throwData;
    private Vector2 startPosition = Vector2.zero;
    private Vector2 launchVelocity;

    private bool launched = false;
    private Rigidbody2D rigidBody;
    private int levelLaunchModifier = 1;
    private int dotsToShow = 60;
    private float dotTimeStep = 0.05f;

    public bool Launched {  get { return launched; } }

    public void Init(UnityEvent onScoreCallback, ThrowData initialData)
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        onScoreCallback.AddListener(() => launched = false);
        throwData = initialData;
    }

    public void OnResetRequest()
    {
        rigidBody.velocity = Vector2.zero;
        launched = false;
    }

    private void Update()
    {
        ParseInputs();
    }

    private void ParseInputs()
    {
        if (!launched && !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InputStart();
            }
            if (Input.GetButton("Fire1"))
            {
                CalculateLaunchVelocity();
                GenerateTrajectory();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                Launch();
            }
        }
    }

    private void InputStart()
    {
        launchVelocity = throwData.startLaunchVelocity;
        startPosition = (Vector2)transform.position;
    }

    private void GenerateTrajectory()
    {
        dotPool.ResetPool();
        startPosition = (Vector2)transform.position;
        for (int i = 0; i < dotsToShow; i++)
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
        launchVelocity += levelLaunchModifier * Time.deltaTime * 0.3f * throwData.velocityModifier;
    }

    private Vector2 CalculatePosition(float elapsedTime)
    {
        return throwData.gravity * elapsedTime * elapsedTime * 0.5f +
                   launchVelocity * elapsedTime + startPosition;
    }

}
public struct ThrowData
{
    public ThrowData(Vector2 startLaunchVelocity, Vector2 velocityModifier, Vector2 gravity)
    {
        this.startLaunchVelocity = startLaunchVelocity;
        this.velocityModifier = velocityModifier;
        this.gravity = gravity;
    }

    public Vector2 startLaunchVelocity;
    public Vector2 velocityModifier;
    public Vector2 gravity;
}