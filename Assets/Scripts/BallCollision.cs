using UnityEngine;
using UnityEngine.Events;

public class BallCollision : MonoBehaviour
{
    [SerializeField] private float scoreDelay = 2.0f;

    private float scoreTime;
    private bool isCounting;
    private BallThrow throwComponent;

    private UnityEvent onPlayerScored = new UnityEvent();
    private UnityEvent onPlayerLost = new UnityEvent();

    public void Init(UnityAction scoreCallback, UnityAction loseCallback, BallThrow throwComponent)
    {
        this.throwComponent = throwComponent;
        onPlayerScored.AddListener(scoreCallback);
        onPlayerLost.AddListener(loseCallback);
    }

    public void OnResetRequest()
    {
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Enter!");
        bool isHole = col.gameObject.layer == 9;
        if (isHole)
        {
            TurnOnTimer();
            gameObject.layer = 8;
        }
        else if(throwComponent.Launched)
        {
            onPlayerLost.Invoke();
        }
    }

    private void TurnOnTimer()
    {
        scoreTime = Time.time + scoreDelay;
        isCounting = true;
    }

    private void OnTriggerLeft2D(Collider2D other)
    {
        Reset();
    }

    private void Reset()
    {
        TurnOffTimer();
        gameObject.layer = 0;
    }

    private void TurnOffTimer()
    {
        isCounting = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        bool shouldScore = isCounting && scoreTime < Time.time;
        if (shouldScore)
        {
            Reset();
            onPlayerScored.Invoke();
        }
    }
}
