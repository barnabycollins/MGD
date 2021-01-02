using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public int playerSpeed = 10;

    public float yUpper;
    public float yLower;

    private float inputX;
    private float inputY;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
    }

    void MovePlayer() {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        bool isMoving = false;

        if (inputX > 0.0f) {
            sr.flipX = false;
            isMoving = true;
        }
        else if (inputX < 0.0f) {
            sr.flipX = true;
            isMoving = true;
        }

        float moveY = 0.0f;

        if (inputY != 0.0f) {
            isMoving = true;

            if (inputY < 0.0f && transform.position.y > yLower) {
                moveY = inputY;
            }
            else if (inputY > 0.0f && transform.position.y < yUpper) {
                moveY = inputY;
            }
        }

        animator.SetBool("isMoving", isMoving);

        rb.velocity = new Vector2(inputX, moveY) * playerSpeed;
    }
}
