using UnityEngine;

public class ItemID : MonoBehaviour
{
    public int id;
    public Item item;
    private void Awake()
    {
        id = item.itemID;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
