using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftPlatform : MonoBehaviour
{
    [SerializeField] GameObject stage1; // Stage that the player lands on
    [SerializeField] GameObject stage2; // "Half-broken" stage

    [SerializeField] float resetTimer = 5f; // How long it takes the entire platform to reset
    [SerializeField] float lifeTimer = 1f; // How long it takes each stage of the platform to break
    float counter = 0f;
    bool breaking = false;
    bool reset = false;
    
    void Start() {
        stage1.SetActive(true);
        stage2.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) { // Once the player steps on the platform
        if (!breaking) {
            breaking = true;
            Debug.Log("breaking");
        }
    }

    void Update() {
        if (breaking) { // Run this if statement if the platform should be breaking
            counter += Time.deltaTime;

            if (counter >= lifeTimer) {
                if (stage1.activeSelf) { // Break the first stage, set up the second
                    stage1.SetActive(false);
                    stage2.SetActive(true);
                } else { // Break the second stage, start the reset process
                    stage2.SetActive(false);
                    breaking = false;
                    reset = true;
                }

                counter = 0f;
            }
        }

        if (reset) {
            counter += Time.deltaTime;

            if (counter >= resetTimer) {
                stage1.SetActive(true);
                stage2.SetActive(false);
                counter = 0f;
                reset = false;
            }
        }
    }
}
