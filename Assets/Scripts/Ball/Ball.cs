using UnityEngine.Events;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private BallThrow throwComponent;
    [SerializeField] private BallCollision collisionComponent;

    public void Init(UnityAction scoreCallback, UnityAction loseCallback, UnityEvent scoreListener, ThrowData initialData)
    {
        throwComponent.Init(scoreListener, initialData);
        collisionComponent.Init(scoreCallback, loseCallback, throwComponent);
    }

    public void Reset()
    {
        collisionComponent.OnResetRequest();
        throwComponent.OnResetRequest();
    }
}
