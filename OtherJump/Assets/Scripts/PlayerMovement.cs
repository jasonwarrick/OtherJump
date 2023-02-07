using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour // Basic player controller code taken from: https://www.youtube.com/watch?v=K1xZ-rycYY8&t=255s
{
    float horizontal;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;
    [SerializeField] float floatingPower = 0.5f;
    int jumpPods = 0; // Int that gets increased for every available additional jump

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckSize = 0.2f;
    [SerializeField] LayerMask groundLayer;

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        } else if (Input.GetButtonDown("Jump") && jumpPods > 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpPods -= 1;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * floatingPower);
        }
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
    }

    public void IncJumpPods() {
        jumpPods += 1;
    }
}
