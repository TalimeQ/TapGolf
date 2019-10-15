using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public void Init()
    {

    }

    private void OnCollsionEnter2D(Collision2D col)
    {
        Debug.Log("Collided!");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered!");
    }
}
