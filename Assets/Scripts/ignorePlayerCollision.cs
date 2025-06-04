using UnityEngine;

public class ignorePlayerCollision : MonoBehaviour
{
    Collider2D circleCollider2D;

    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circleCollider2D = GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(circleCollider2D, target.GetComponent<Collider2D>(), true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
