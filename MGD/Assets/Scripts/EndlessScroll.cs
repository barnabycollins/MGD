using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessScroll : MonoBehaviour
{
    public GameObject mainCamera;
    public int myIndex;

    private float myWidth;

    // Start is called before the first frame update
    void Start()
    {
        myWidth = this.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float normalisedCameraX = mainCamera.transform.position.x / myWidth;
        float posX = (Mathf.Floor((normalisedCameraX + 1 - myIndex)/2) * 2 + myIndex) * myWidth;
        
        transform.position = new Vector2(posX, transform.position.y);
    }
}
