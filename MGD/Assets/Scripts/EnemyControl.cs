using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float depth;
    private float posY;
    private float startScale;
    public float rollSpeed;

    public GameObject depthCoordinator;

    public float deathTime;
    private ObjectDepth depthScript;
    private bool dead;
    private float timeOfDeath;

    // Start is called before the first frame update
    void Start() {
        depthScript = depthCoordinator.GetComponent<ObjectDepth>();

        posY = depthScript.updateY(gameObject, depth);
        GetComponent<SpriteRenderer>().sortingOrder = depthScript.depthToLayerOrder(depth);
        
        dead = false;

        transform.position = new Vector2(0, posY);
        startScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update() {
        if (dead) {
            float timeUntilRemoval = deathTime - (Time.time - timeOfDeath);
            transform.localScale = timeUntilRemoval / deathTime * startScale * new Vector2(1, 1);
            if (timeUntilRemoval <= 0) {
                depthScript.remove(gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void Die() {
        if (!dead) {
            dead = true;
            timeOfDeath = Time.time;
        }
    }
}
