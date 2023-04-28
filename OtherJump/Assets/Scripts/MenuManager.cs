using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas; // Index: 0
    [SerializeField] Canvas pausedCanvas; // Index: 2
    [SerializeField] TextMeshProUGUI levelSelection;

    [SerializeField] Canvas[] canvases;

    [SerializeField] GameObject pauseFirstButton; // All menu navigation code adapted from: https://www.youtube.com/watch?v=SXBgBmUcTe0
    
    bool paused = false;
    int sceneCount;
    int currentSelection = 0;

    // Start is called before the first frame update
    void Start() {
        pausedCanvas.enabled = false;
        currentSelection = int.Parse(levelSelection.text);
        sceneCount = SceneManager.sceneCountInBuildSettings;

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            ToggleCanvas(0); // Show main menu if in the correct scene
        } else {
            ToggleCanvas(-1); // Hide all canvases if in any other scene
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        paused = !paused;
        FindObjectOfType<PlayerMovement>().enabled = !paused;

        if (paused) {
            Debug.Log("PauseGame");
            Time.timeScale = 0;
            ToggleCanvas(1); // Show paused canvas

            // Clear current selected object (necessary for some reason)
            EventSystem.current.SetSelectedGameObject(null);
            // Set the desired game object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        } else {
            ToggleCanvas(-1); // Hide paused canvas
            Time.timeScale = 1;
        }
    }

    public void ToggleCanvas(int index) {
        if (index >= 0) {
            for (int i = 0; i < canvases.Length; i++) {
                if (i == index) {
                    canvases[i].enabled = true;
                } else {
                    canvases[i].enabled = false;
                }
            }
        } else { // If -1 is passed in, disable all canvases
            for (int i = 0; i < canvases.Length; i++) {
                canvases[i].enabled = false;
            }
        }
        
    }

    public void Options() {
        Debug.Log("Options");
    }

    public void Exit() {
        Application.Quit();
    }

    public void Quit() {
        paused = !paused;
        Time.timeScale = 1;
        ToggleCanvas(0); // Hide paused canvas
        Debug.Log("quit");
    }

    public void ChangeLevelSelection(bool up) {
        if (up && currentSelection + 1 < sceneCount) {
            currentSelection++;
            levelSelection.text = currentSelection.ToString();
        }

        if (!up && currentSelection - 1 > 0) {
            currentSelection--;
            levelSelection.text = currentSelection.ToString();
        }
    }
}
