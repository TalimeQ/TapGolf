using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] BallThrow throwComponent;
    [SerializeField] BallCollision collisionComponent;

    public void Init()
    {
        throwComponent.Init();
        collisionComponent.Init();
    }

    public void Reset()
    {

    }
}
