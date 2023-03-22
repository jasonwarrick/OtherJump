using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{   
    PlayerMovement playerMovement;

    int currentBuildIndex;
    
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        Spike.PlayerDeath += ResetLevel;
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            NextLevel();
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
    }
}
