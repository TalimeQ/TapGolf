using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Triggered!");
        bool isHole = col.gameObject.layer == 9;
        if (isHole)
        {
            Debug.Log("isHole!");
            gameObject.layer = 8;
        }
    }
}
