using UnityEngine;

public class BallCollision : MonoBehaviour
{
    private float scoreTime;
    [SerializeField] private float scoreDelay = 2.0f;
    private bool isCounting;

    public void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool isHole = col.gameObject.layer == 9;
        if (isHole)
        {
            TurnOnTimer();
            gameObject.layer = 8;
        }
    }

    private void TurnOnTimer()
    {
        scoreTime = Time.time + scoreDelay;
        isCounting = true;
    }

    private void OnTriggerLeft2D(Collider2D other)
    {
        TurnOffTimer();
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
            Debug.Log("Scored!");
        }
    }
}
