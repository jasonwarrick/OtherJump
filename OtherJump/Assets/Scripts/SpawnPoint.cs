using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    // Start is called before the first frame update
    void Start() {
        SpawnPlayer();
    }

    void SpawnPlayer() {
        GameObject playerInstance = Instantiate(player, gameObject.transform);
    }
}
