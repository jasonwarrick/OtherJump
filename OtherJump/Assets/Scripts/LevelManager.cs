using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{   
    PlayerMovement playerMovement;
    [SerializeField] MenuManager menuManager;
    [SerializeField] TextMeshProUGUI levelText;

    int currentBuildIndex;
    
    // Start is called before the first frame update
    void Start() {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            SetLevel(currentBuildIndex + 1);
        }
    }

    public void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetLevel(int index) {
        if (index < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(index);
            currentBuildIndex = index;
        } else {
            SceneManager.LoadScene(0);
            menuManager.ToggleCanvas(0);
            currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void PlayLevel() {
        int levelNum;
        // attempt to parse the value using the TryParse functionality of the integer type
        int.TryParse(levelText.text, out levelNum);

        SceneManager.LoadScene(levelNum);
        currentBuildIndex = levelNum;
    }

    public void NextLevel() {
        SetLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
