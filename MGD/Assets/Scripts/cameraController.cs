using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    public float staticCameraRegion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float distanceFromPlayer = player.transform.position.x - transform.position.x;

        float distanceOutsideRegion = Mathf.Abs(distanceFromPlayer) - staticCameraRegion;

        if (distanceOutsideRegion > 0) {
            float distanceToMove = distanceOutsideRegion * (distanceFromPlayer / Mathf.Abs(distanceFromPlayer));
            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);
        }
    }
}
