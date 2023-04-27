using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    LevelManager levelManager;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioManager.Spike();
            levelManager.ResetLevel();
        }
    }
}
