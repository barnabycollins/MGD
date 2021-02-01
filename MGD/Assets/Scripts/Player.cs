using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeedX;
    public float playerSpeedY;

    public GameObject depthCoordinator;
    public float depthOffset;
    public GameObject playerObject;
    public GameObject laserBeam;

    public float jumpHeight;
    public float jumpSpeed;

    public float hitboxX;
    public float hitboxJumpHeight;

    public GameObject gameController;

    public float fireCooldown;
    public float shotLength;
    private float lastFireTime;

    private float inputX;
    private float inputY;
    private float jump;

    private bool isJumping;
    private bool facingRight;
    private bool firingNow;
    private float currentJumpHeight;
    private float jumpTime;
    private float jumpLength;

    private float xPos;
    private float yPos;

    private SpriteRenderer sr;
    private Animator animator;
    private SpriteRenderer laserBeamRenderer;

    private ObjectDepth depthScript;
    private List<GameObject> objectsAtDepth;

    private float depth;
    private float initialScale;

    private GameControlScript gameControl;
    private float levelLength;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        depthScript = depthCoordinator.GetComponent<ObjectDepth>();
        depth = 0.5f;
        initialScale = transform.localScale.x;
        currentJumpHeight = 0.0f;
        isJumping = false;
        jumpLength = Mathf.PI / jumpSpeed;
        laserBeamRenderer = laserBeam.GetComponent<SpriteRenderer>();
        gameControl = gameController.GetComponent<GameControlScript>();
        levelLength = gameControl.levelLength;

        lastFireTime = -100;        
    }

    // Update is called once per frame
    void FixedUpdate() {
        MovePlayer();
    }

    void LateUpdate() {
        checkShoot();
        checkCollisions();
    }

    void MovePlayer() {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        jump = Input.GetAxis("Jump");

        float now = Time.time;

        if (!isJumping && jump > 0.0f) {
            isJumping = true;
            jumpTime = now;
        }
        else if (isJumping) {
            if ((now - jumpTime) < jumpLength) {
                currentJumpHeight = jumpHeight * Mathf.Sin(jumpSpeed * (Time.time - jumpTime));
            }
            else {
                currentJumpHeight = 0.0f;
                isJumping = false;
            }
        }

        bool isMoving = false;

        if (inputX > 0.0f) {
            transform.localScale = new Vector2(initialScale, initialScale);
            facingRight = true;
            isMoving = true;
        }
        else if (inputX < 0.0f) {
            transform.localScale = new Vector2(-initialScale, initialScale);
            facingRight = false;
            isMoving = true;
        }

        if (inputY != 0.0f) {
            isMoving = true;
            depth += inputY * playerSpeedY;
        }

        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isJumping", isJumping);

        xPos = Mathf.Min(Mathf.Max(-22, xPos + inputX * playerSpeedX), levelLength + 11);
        depth = Mathf.Max(Mathf.Min(depth + inputY * playerSpeedY, 1), 0);
        yPos = depthScript.updateY(gameObject, depth) + depthOffset;

        transform.localPosition = new Vector2(0, currentJumpHeight);

        sr.sortingOrder = depthScript.depthToLayerOrder(depth);
        laserBeamRenderer.sortingOrder = depthScript.depthToLayerOrder(depth) - 1;

        playerObject.transform.position = new Vector2(xPos, yPos);
    }

    void checkShoot() {
        float fireInput = Input.GetAxis("Fire1");

        float timeNow = Time.time;
        float timeSinceFiring = timeNow - lastFireTime;
        
        if (timeSinceFiring > shotLength) {
            if (fireInput != 0.0f && timeSinceFiring > fireCooldown) {
                lastFireTime = timeNow;
                firingNow = true;
            }
            else {
                firingNow = false;
            }
        }
        gameControl.updateFireCooldown(Mathf.Min(timeSinceFiring/fireCooldown));
        laserBeam.SetActive(firingNow);
    }

    void checkCollisions() {
        if (currentJumpHeight < hitboxJumpHeight) {
            objectsAtDepth = depthScript.findItemsWithDepth(depth);
            foreach (GameObject enemy in objectsAtDepth) {
                float enemyX = enemy.transform.position.x;
                if (enemy.name == "Virus(Clone)") {
                    if (firingNow) {
                        if ((facingRight && enemyX > transform.position.x) || !facingRight && enemyX < transform.position.x) {
                            enemy.GetComponent<EnemyControl>().Die();
                        }
                    }

                    if (Mathf.Abs(enemyX - transform.position.x) < hitboxX) {
                        bool isAlive = gameControl.updateHealth(-2);

                        if (!isAlive) {
                            Debug.Log("dead ah jeez");
                        }
                    }
                }
                else if (enemy.name == "Glasses" && Mathf.Abs(enemyX - transform.position.x) < hitboxX) {
                    Debug.Log("EPIC WIN BRUDDA");
                }
            }
        }
    }
}
