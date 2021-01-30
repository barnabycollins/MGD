using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesController : MonoBehaviour
{
    public float depthOffset;
    public float distance;

    public GameObject depthCoordinator;

    // Start is called before the first frame update
    void Start()
    {
        ObjectDepth depthScript = depthCoordinator.GetComponent<ObjectDepth>();
        float posY = depthScript.updateY(gameObject, 0.5f) + depthOffset;
        
        transform.position = new Vector2(distance, posY);
    }
}
