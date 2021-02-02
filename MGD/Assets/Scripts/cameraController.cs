using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public GameObject gameController;
    private GameControlScript gameControl;

    public float staticCameraRegion;

    public float minCameraX;
    private float maxCameraX;
    
    private bool firstFrame = true;
    private float endTime;
    private float endX;
    public float endPanTime;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = gameController.GetComponent<GameControlScript>();
        maxCameraX = gameControl.levelLength;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (gameControl.gameState != "win") {
            float posX = transform.position.x;
            float distanceFromPlayer = player.transform.position.x - transform.position.x;

            float distanceOutsideRegion = Mathf.Abs(distanceFromPlayer) - staticCameraRegion;

            if (distanceOutsideRegion > 0) {
                float distanceToMove = distanceOutsideRegion * (distanceFromPlayer / Mathf.Abs(distanceFromPlayer));
                float newX = transform.position.x + distanceToMove;
                transform.position = new Vector3(Mathf.Min(Mathf.Max(minCameraX, newX), maxCameraX), transform.position.y, transform.position.z);
            }
        }
        else {
            float now = Time.time;
            if (firstFrame) {
                endTime = now;
                endX = transform.position.x;
                firstFrame = false;
            }
            
            float timeSinceEnd = now - endTime;

            if (timeSinceEnd < endPanTime) {
                transform.position = new Vector3(Mathf.Lerp(endX, maxCameraX, timeSinceEnd / endPanTime), transform.position.y, transform.position.z);
            }
        }
    }
}
