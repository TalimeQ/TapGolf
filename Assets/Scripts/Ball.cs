using UnityEngine.Events;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] BallThrow throwComponent;
    [SerializeField] BallCollision collisionComponent;

    public void Init(UnityAction scoreCallback)
    {
        throwComponent.Init();
        collisionComponent.Init(scoreCallback);
    }

    public void Reset()
    {
        collisionComponent.OnResetRequest();
        throwComponent.OnResetRequest();
    }
}
