using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float depth;
    private float posY;

    public GameObject depthCoordinator;
    private ObjectDepth depthScript;

    // Start is called before the first frame update
    void Start() {
        depthScript = depthCoordinator.GetComponent<ObjectDepth>();

        posY = depthScript.updateY(gameObject, depth);
        GetComponent<SpriteRenderer>().sortingOrder = depthScript.depthToLayerOrder(depth);

        transform.position = new Vector2(0, posY);
    }

    // Update is called once per frame
    void Update() {

    }
}
