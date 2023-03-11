using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D playerRB;

    [SerializeField] float waitTime = 1f; // How long the pad waits to retract
    float counter = 0f;
    [SerializeField] float force = 1f; // How much force the pad propels the player with
    bool launched = false;

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("supposed to be launching rn");
        if (other.gameObject.tag == "Player") {
            launched = true;
            playerRB = other.gameObject.GetComponent<Rigidbody2D>(); // Grab the player's rigidbody to make the code easier
            playerRB.velocity = new Vector2(playerRB.velocity.x, 0f); // Reset the player's y-velocity so it applies a consistent force
            playerRB.AddForce(new Vector2(0f, force)); // Add the force
            animator.SetTrigger("bounce");
        }
    }

    void Update() {
        if (launched == true) { // For starting the retract animation
            counter += Time.deltaTime;

            if (counter >= waitTime) {
                counter = 0f;
                launched = false;
                animator.SetTrigger("retract");
            }
        } else { // For starting the idle animation
            counter += Time.deltaTime;

            if (counter >= waitTime) {
                counter = 0f;
                animator.SetTrigger("idle");
            }
        }
    }
}
