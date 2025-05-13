using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulletwork : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public GameObject Spark;
    private float timer = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        Destroy(gameObject);
        Debug.Log(other.name);
        GameObject SparkEffect = Instantiate(Spark, transform.position, Quaternion.identity);
        Destroy(SparkEffect, 2f);

    }
}
