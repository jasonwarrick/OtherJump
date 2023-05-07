using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas mainCanvas; // Index: 0
    [SerializeField] Canvas pausedCanvas; // Index: 2
    [SerializeField] TextMeshProUGUI levelSelection;
    [SerializeField] AudioMixer musicMixer;
    [SerializeField] AudioMixer SFXMixer;

    [SerializeField] Canvas[] canvases;
    
    bool paused = false;
    bool music = true;
    bool SFX = true;
    int sceneCount;
    int currentSelection = 0;

    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.None;
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
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            ToggleCanvas(1); // Show paused canvas
        } else {
            Cursor.lockState = CursorLockMode.Locked;
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

    public void Exit() {
        Application.Quit();
    }

    public void Quit() {
        paused = !paused;
        Time.timeScale = 1;
        ToggleCanvas(0); // Hide paused canvas
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
