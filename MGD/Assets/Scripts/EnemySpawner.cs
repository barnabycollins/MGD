using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float[] enemyLikelihoods;
    public GameObject gameController;
    private GameControlScript mainControlScript;

    private List<GameObject> enemies;

    public GameObject enemyPrefab;

    public Camera mainCamera;

    public Sprite[] enemySprites;
    private int numSprites;

    public GameObject depthCoordinator;
    private ObjectDepth depthScript;

    public float enemyDeathTime;

    public float minRollSpeed;
    public float maxRollSpeed;

    public float enemyRadius;
    private float enemyCircumference;

    // Start is called before the first frame update
    void Start() {
        enemies = new List<GameObject>();
        numSprites = enemySprites.Length;
        enemyCircumference = 2 * Mathf.PI * enemyRadius;
        mainControlScript = gameController.GetComponent<GameControlScript>();
        depthScript = depthCoordinator.GetComponent<ObjectDepth>();
    }

    void FixedUpdate() {
        if (mainControlScript.gameState != "win") {
            float enemyLikelihood = Mathf.Lerp(enemyLikelihoods[0], enemyLikelihoods[1], mainControlScript.levelProgress);

            if (Random.value < enemyLikelihood) {
                GameObject newEnemy = Instantiate(enemyPrefab, transform);
                enemies.Add(newEnemy);
                
                EnemyControl controlScript = newEnemy.GetComponent<EnemyControl>();

                // Configure enemy before activating
                SpriteRenderer sr = controlScript.sprite.GetComponent<SpriteRenderer>();
                sr.sprite = enemySprites[Random.Range(0, numSprites)];

                controlScript.depth = Random.value;
                controlScript.depthScript = depthScript;
                controlScript.deathTime = enemyDeathTime;
                controlScript.mainCamera = mainCamera;
                controlScript.rollSpeed = Random.Range(minRollSpeed, maxRollSpeed);
                controlScript.circumference = enemyCircumference;
                controlScript.depthOffset = enemyRadius;
                controlScript.gameControl = mainControlScript;

                // Activate enemy
                newEnemy.SetActive(true);
            }
        }
    }
}
