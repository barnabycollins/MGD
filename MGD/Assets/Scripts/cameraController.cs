using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public GameObject gameController;

    public float staticCameraRegion;

    public float minCameraX;
    private float maxCameraX;

    // Start is called before the first frame update
    void Start()
    {
        maxCameraX = gameController.GetComponent<GameControlScript>().levelLength;
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
                transform.position = new Vector3(Mathf.Min(Mathf.Max(minCameraX, newX), maxCameraX), transform.position.y, transform.position.z);
            }
    }
}
