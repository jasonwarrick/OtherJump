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
    [SerializeField] List<GameObject> jumpPods = new List<GameObject>(); // Int that gets increased for every available additional jump
    [SerializeField] GameObject[] podSprites = new GameObject[3];
    [SerializeField] int maxPods = 3;
    bool isFacingRight = true;
    public float additionalVel = 0f;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckSize = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask spikeLayer;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem playerTrail;

    void Start() {
        UpdatePodSprites();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }
        else if (Input.GetButtonDown("Jump") && jumpPods.Count > 0) {
            Jump();
            jumpPods[0].GetComponent<JumpPod>().reset = true;
            jumpPods.RemoveAt(0);
            UpdatePodSprites();
            Debug.Log("Used");
        }

        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) {
        //     rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * floatingPower);
        // }

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

    void UpdatePodSprites() {
        for (int i = 0; i < 3; i++) {
            if (i < jumpPods.Count) {
                Debug.Log("set");
                podSprites[i].SetActive(true);
            } else {
                podSprites[i].SetActive(false);
            }
            
        }
    }

    bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckSize, groundLayer);
    }

    public bool IncJumpPods(GameObject pod) {
        if (jumpPods.Count + 1 <= maxPods) { // If the max pods count won't be reached, tell the pod that it was picked up successfully, and add it to the list
            jumpPods.Add(pod);
            UpdatePodSprites();
            Debug.Log("Picked");
            return true;
        }

        return false;
    }

    void Flip() {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Die() {
        Debug.Log("Player is dead");
    }
}
