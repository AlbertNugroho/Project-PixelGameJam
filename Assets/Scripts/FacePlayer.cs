using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceTarget();
    }
    private void FaceTarget()
    {
        Vector2 direction = (Target.transform.position - transform.position).normalized;
        transform.right = direction;

        Vector3 scale = transform.localScale;
        scale.y = direction.x < 0 ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        transform.localScale = scale;
    }

}
