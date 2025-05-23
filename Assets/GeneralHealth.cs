using System;
using UnityEngine;

public class GeneralHealth : MonoBehaviour
{
    public int Health = 20;
    public event Action<int> OnHealthChanged;
    public ShowHp showHp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damage)
    {
        Health -= damage;
        showHp.updateHealth(); 
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        OnHealthChanged?.Invoke(Health);
    }

}
