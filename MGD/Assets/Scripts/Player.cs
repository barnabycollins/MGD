﻿using System.Collections;
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
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
    }

    void LateUpdate() {
        checkShoot();
        if (currentJumpHeight < hitboxJumpHeight) {
            objectsAtDepth = depthScript.findItemsWithDepth(depth);
            foreach (GameObject enemy in objectsAtDepth) {
                float enemyX = enemy.transform.position.x;
                if (enemy != gameObject) {
                    if (firingNow) {
                        if ((facingRight && enemyX > transform.position.x) || !facingRight && enemyX < transform.position.x) {
                            enemy.GetComponent<EnemyControl>().Die();
                        }
                    }

                    if (Mathf.Abs(enemyX - transform.position.x) < hitboxX) {
                        Debug.Log("ouch");
                    }
                }
            }
        }
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

        xPos = xPos + inputX * playerSpeedX;
        depth = Mathf.Max(Mathf.Min(depth + inputY * playerSpeedY, 1), 0);
        yPos = depthScript.updateY(gameObject, depth) + depthOffset;

        transform.localPosition = new Vector2(0, currentJumpHeight);

        sr.sortingOrder = depthScript.depthToLayerOrder(depth);
        laserBeamRenderer.sortingOrder = depthScript.depthToLayerOrder(depth) - 1;

        playerObject.transform.position = new Vector2(xPos, yPos);
    }

    void checkShoot() {
        float fireInput = Input.GetAxis("Fire1");

        firingNow = fireInput != 0.0f;

        laserBeam.SetActive(firingNow);
    }
}