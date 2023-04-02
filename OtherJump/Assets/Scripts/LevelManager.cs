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
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        Spike.PlayerDeath += ResetLevel;
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            SetLevel(currentBuildIndex + 1);
        }
    }

    void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetLevel(int index) {
        if (index < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(index);
            currentBuildIndex = index;
        } else {
            SceneManager.LoadScene(0);
            currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void NextLevel() {
        if (currentBuildIndex + 1 > SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(0);
            return;
        } else {
            SceneManager.LoadScene(currentBuildIndex + 1);
            Debug.Log(currentBuildIndex + 1);
        }
    }
}
