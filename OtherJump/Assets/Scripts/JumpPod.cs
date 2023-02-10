using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPod : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] BoxCollider2D bc;
    
    [SerializeField] float respawnTime = 5f;
    float timeCounter = 0f;
    bool reset = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerMovement>().IncJumpPods();
            SetBox();
            reset = true;
        }
    }

    void Update() {
        if (reset) {
            timeCounter += Time.deltaTime;

            if (timeCounter >= respawnTime) {
                SetBox();
                reset = false;
            }
        }
    }

    void SetBox() {
        sr.enabled = reset;
        bc.enabled = reset;
        timeCounter = 0f;
    }
}
