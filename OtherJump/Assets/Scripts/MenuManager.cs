using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Canvas pausedCanvas;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas levelCanvas;

    [SerializeField] GameObject pauseFirstButton; // All menu navigation code adapted from: https://www.youtube.com/watch?v=SXBgBmUcTe0
    
    bool paused = false;
    
    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(gameObject);
        pausedCanvas.enabled = false;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneAt(0)) {
            Debug.Log("Main menu");
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        paused = !paused;
        pausedCanvas.enabled = paused;
        FindObjectOfType<PlayerMovement>().enabled = !paused;

        if (paused) {
            Time.timeScale = 0;

            // Clear current selected object (necessary for some reason)
            EventSystem.current.SetSelectedGameObject(null);
            // Set the desired game object
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        } else {
            Time.timeScale = 1;
        }
    }

    public void Options() {
        Debug.Log("Options");
    }

    public void Quit() {
        Debug.Log("Quit");
    }
}
