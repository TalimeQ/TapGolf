using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public void Init()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
