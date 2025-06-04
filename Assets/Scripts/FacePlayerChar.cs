using UnityEngine;

public class FacePlayerChar : MonoBehaviour
{
    public BossCannon cannon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = cannon.Target.transform.position - cannon.armOrigin.position;
        Vector3 clampedDirection = Vector3.ClampMagnitude(direction, 3f);
        transform.position = cannon.armOrigin.position + clampedDirection;
    }
}
