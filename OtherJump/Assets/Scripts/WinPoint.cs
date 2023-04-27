using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    LevelManager levelManager;
    AudioManager audioManager;

    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            audioManager.Win();
            levelManager.NextLevel();
        }
    }
}
