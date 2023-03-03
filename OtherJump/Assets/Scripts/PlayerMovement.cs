using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour // Basic player controller code taken from: https://www.youtube.com/watch?v=K1xZ-rycYY8&t=255s
{
    float horizontal;
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16f;
    [SerializeField] float floatingPower = 0.5f;
    [SerializeField] float groundSlide = 0.2f; // Rate at which the player speeds up and slows down right after button press and release
    int jumpPods = 0; // Int that gets increased for every available additional jump
    bool isFacingRight = true;
    public float additionalVel = 0f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckSize = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem playerTrail;

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && jumpPods > 0) {
            Jump();
            jumpPods -= 1;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * floatingPower);
        }

        Flip();
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
    }

    void FixedUpdate() {
        AnimatePlayer();
        Vector2 inputSpeed = new Vector2(horizontal * speed + additionalVel, rb.velocity.y);
        rb.velocity = Vector2.Lerp(rb.velocity, inputSpeed, groundSlide);

        if (rb.velocity.x < -1.5f || rb.velocity.x > 1.5f || rb.velocity.y < -1.5f || rb.velocity.y > 1.5f) {
            playerTrail.Play();
        } else {
            playerTrail.Stop();
        }
    }

    void AnimatePlayer() {
        if (IsGrounded()) {
            if (rb.velocity.x < -0.1f || rb.velocity.x > 0.1f) {
                animator.SetTrigger("Run");
            } else { 
                animator.SetTrigger("Idle");
            }
        } else {
            if (rb.velocity.y > 0) {
                animator.SetTrigger("Jump");
            } else if (rb.velocity.y < 0) {
                animator.SetTrigger("Fall");
            }
        }
    }

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
    }

    public void IncJumpPods() {
        jumpPods += 1;
    }

    void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
