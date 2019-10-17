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

    private Rigidbody2D rigidBody;
    private Camera mainCamera;

    private bool launched = false;
    private int dotsToShow = 60;
    private float dotTimeStep = 0.05f;
    private float levelLaunchModifier = 1.0f;
    private float normalizedY;

    public bool Launched {  get { return launched; } }

    public void Init(UnityEvent onScoreCallback, ThrowData initialData)
    {
        if (rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
        onScoreCallback.AddListener(OnPlayerScored);
        throwData = initialData;
        mainCamera = Camera.main;
        Vector3 startPos = new Vector3(0, initialData.ballspawnY, 0);
        normalizedY = mainCamera.WorldToViewportPoint(startPos).y;
    }

    public void OnResetRequest()
    {
        ResetVelocity();
        levelLaunchModifier = 1.0f;
        launched = false;
    }

    private void Update()
    {
        ParseInputs();
    }

    private void OnPlayerScored()
    {
        launched = false;
        levelLaunchModifier += throwData.offsetChangePerLevel;
        ResetVelocity();
    }

    private void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = 0.0f;
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
        
       Vector2 CalculatedPosition = throwData.gravity * elapsedTime * elapsedTime * 0.5f +
                   launchVelocity * elapsedTime + startPosition;

        Vector3 normalizedScreenPos = mainCamera.WorldToViewportPoint(CalculatedPosition);

        if (normalizedScreenPos.x > 1 && (normalizedScreenPos.y >= normalizedY && normalizedScreenPos.y <= 1))
        {
            Launch();
        }

        return CalculatedPosition;
    }

}
public struct ThrowData
{
    public ThrowData(Vector2 startLaunchVelocity, Vector2 velocityModifier, Vector2 gravity, float offsetChangePerLevel, float ballspawnY)
    {
        this.startLaunchVelocity = startLaunchVelocity;
        this.velocityModifier = velocityModifier;
        this.gravity = gravity;
        this.offsetChangePerLevel = offsetChangePerLevel;
        this.ballspawnY = ballspawnY;
    }

    public Vector2 startLaunchVelocity;
    public Vector2 velocityModifier;
    public Vector2 gravity;
    public float offsetChangePerLevel;
    public float ballspawnY;
}