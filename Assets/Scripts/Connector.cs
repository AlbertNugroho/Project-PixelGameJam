using UnityEngine;

public class Connector : MonoBehaviour
{
    public GrabPlayer grabPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void toggleCollider()
    {
        //grabPlayer.toggleCollider();
    }
    public void ReleasePlayer()
    {
        grabPlayer.ReleasePlayer();
    }
    public void EnableAI()
    {
        grabPlayer.EnableAI();
    }
}
