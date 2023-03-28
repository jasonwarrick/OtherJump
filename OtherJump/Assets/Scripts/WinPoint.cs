using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPoint : MonoBehaviour
{
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        levelManager.NextLevel();
    }
}
