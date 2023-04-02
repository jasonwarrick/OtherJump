using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        if (FindObjectsOfType<DontDestroy>().Length > 1) { // Delete the extra Manager object when you return to the main menu
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
