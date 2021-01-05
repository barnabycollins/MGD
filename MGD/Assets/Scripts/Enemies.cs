using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float enemyLikelihood;

    private List<GameObject> enemies;

    public GameObject enemyPrefab;

    public Sprite[] enemySprites;
    private int numSprites;

    public GameObject depthCoordinator;
    public float enemyDeathTime;

    // Start is called before the first frame update
    void Start() {
        enemies = new List<GameObject>();
        numSprites = enemySprites.Length;
    }

    void FixedUpdate() {
        if (Random.value < enemyLikelihood) {
            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            enemies.Add(newEnemy);

            // Configure enemy before activating
            SpriteRenderer sr = newEnemy.GetComponent<SpriteRenderer>();
            sr.sprite = enemySprites[Random.Range(0, numSprites)];
            
            EnemyControl controlScript = newEnemy.GetComponent<EnemyControl>();
            controlScript.depth = Random.value;
            controlScript.depthCoordinator = depthCoordinator;
            controlScript.deathTime = enemyDeathTime;

            // Activate enemy
            newEnemy.SetActive(true);
        }
    }
}
