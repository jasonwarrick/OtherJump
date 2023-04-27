using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPod : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] BoxCollider2D bc;
    [SerializeField] AudioSource podAudio;
    
    [SerializeField] float respawnTime = 5f;
    float timeCounter = 0f;
    public bool reset = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetComponent<PlayerMovement>().IncJumpPods(gameObject)) { // Only disable the pod if it was added successfully
                podAudio.Play();
                SetBox();
            }        
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
