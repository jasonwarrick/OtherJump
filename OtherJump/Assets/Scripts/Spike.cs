using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    LevelManager levelManager;
    [SerializeField] AudioSource spikeAudio;

    // Start is called before the first frame update
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            spikeAudio.Play();
            levelManager.ResetLevel();
        }
    }
}
