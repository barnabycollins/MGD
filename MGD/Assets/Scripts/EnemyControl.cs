using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float depth;
    private float posY;
    private float initialX;
    private float startScale;
    public float rollSpeed;
    public float depthOffset;

    public Camera mainCamera;

    public GameObject depthCoordinator;

    public float deathTime;
    private ObjectDepth depthScript;
    private bool dead;
    private float timeOfDeath;
    private float spawnTime;

    public float circumference;
    public GameObject sprite;

    private float worldEdgeMargin = 1;

    // Start is called before the first frame update
    void Start() {
        depthScript = depthCoordinator.GetComponent<ObjectDepth>();

        posY = depthScript.updateY(gameObject, depth) + depthOffset;
        initialX = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + worldEdgeMargin;

        sprite.GetComponent<SpriteRenderer>().sortingOrder = depthScript.depthToLayerOrder(depth);
        
        dead = false;

        transform.position = new Vector2(initialX, posY);
        startScale = transform.localScale.x;
        
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if (dead) {
            float timeUntilRemoval = deathTime - (Time.time - timeOfDeath);
            transform.localScale = timeUntilRemoval / deathTime * startScale * new Vector2(1, 1);
            if (timeUntilRemoval <= 0) {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate() {
        if (mainCamera.WorldToViewportPoint(new Vector3(transform.position.x + worldEdgeMargin, transform.position.y, transform.position.z)).x < 0) {
            depthScript.remove(gameObject);
            Destroy(gameObject);
        }
        else {
            float posX = initialX - rollSpeed * (Time.time - spawnTime);
            transform.position = new Vector2(posX, posY);

            sprite.transform.localRotation = Quaternion.Euler(Vector3.forward * (initialX - posX) * 360 / circumference);
        }
    }

    public void Die() {
        depthScript.remove(gameObject);
        if (!dead) {
            dead = true;
            timeOfDeath = Time.time;
        }
    }
}
