using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    [SerializeField] Canvas pausedCanvas;
    PlayerMovement playerMovement;

    int currentBuildIndex;
    bool paused = true;
    
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        PauseGame();
        Spike.PlayerDeath += ResetLevel;
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.N)) {
            NextLevel();
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

    void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SetLevel(int index) {
        if (index > SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(index);
            currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    void NextLevel() {
        if (currentBuildIndex + 1 > SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(0);
            return;
        } else {
            SceneManager.LoadScene(currentBuildIndex + 1);
            Debug.Log(currentBuildIndex + 1);
        }
        
        // currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
