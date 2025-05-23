using UnityEngine;

public class BossBulletwork : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    public GameObject Spark;
    private BossCannon bossCannon;
    private float timer = 0;
    void Start()
    {
        bossCannon = FindFirstObjectByType<BossCannon>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger || other.gameObject.CompareTag("Boss"))
            return;
        if (other.gameObject.CompareTag("Player"))
        {
            if(!FindFirstObjectByType<Health>().isImmune && other.GetComponent<Dash>().isDashing == false)
            {
                FindFirstObjectByType<Health>().TakeDamage(bossCannon.GetCurrentDamage());
            }
        }
        Destroy(gameObject);
        GameObject SparkEffect = Instantiate(Spark, transform.position, Quaternion.identity);
        Destroy(SparkEffect, 2f);

    }
}
