using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    public float staticCameraRegion;

    public float[] cameraMovementRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float posX = transform.position.x;
            float distanceFromPlayer = player.transform.position.x - transform.position.x;

            float distanceOutsideRegion = Mathf.Abs(distanceFromPlayer) - staticCameraRegion;

            if (distanceOutsideRegion > 0) {
                float distanceToMove = distanceOutsideRegion * (distanceFromPlayer / Mathf.Abs(distanceFromPlayer));
                float newX = transform.position.x + distanceToMove;
                transform.position = new Vector3(Mathf.Min(Mathf.Max(cameraMovementRange[0], newX), cameraMovementRange[1]), transform.position.y, transform.position.z);
            }
    }
}
