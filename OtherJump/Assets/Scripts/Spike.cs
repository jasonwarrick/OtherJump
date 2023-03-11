using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public delegate void DeathAction();
    public static event DeathAction PlayerDeath; // Create the player death event

    // Start is called before the first frame update
    void Start() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && PlayerDeath != null) {
            PlayerDeath();
        }
    }
}
