using UnityEngine.Events;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] BallThrow throwComponent;
    [SerializeField] BallCollision collisionComponent;

    public void Init(UnityAction scoreCallback, UnityAction loseCallback, UnityEvent scoreListener)
    {
        throwComponent.Init(scoreListener);
        collisionComponent.Init(scoreCallback, loseCallback, throwComponent);
    }

    public void Reset()
    {
        collisionComponent.OnResetRequest();
        throwComponent.OnResetRequest();
    }
}
