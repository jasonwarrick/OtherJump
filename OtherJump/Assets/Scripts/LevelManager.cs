using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{   
    [SerializeField] Canvas pausedCanvas;
    PlayerMovement playerMovement;

    bool paused = true;
    
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        PauseGame();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    void PauseGame() {
        paused = !paused;
        pausedCanvas.enabled = paused;
        FindObjectOfType<PlayerMovement>().enabled = !paused;

        if (paused) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }
}
