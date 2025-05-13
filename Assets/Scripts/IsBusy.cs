using UnityEngine;

public class IsBusy : MonoBehaviour
{
    public static IsBusy singleton { get; private set; }
    public bool isBusy = false;
    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
