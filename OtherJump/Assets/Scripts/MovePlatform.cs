using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    [SerializeField] Transform startMarker;
    [SerializeField] Transform endMarker;

    // Movement speed in units per second.
    [SerializeField] float speed = 1.0F;

    Rigidbody rb;

    // Time when the movement started.
    float startTime;

    // Total distance between the markers.
    float journeyLength;

    // Flag that is true when the player is touching the platform
    bool playerTouching = false;

    GameObject player;

    void Start() {
        Setup();
    }

    void Setup() {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerTouching = true;
            player = other.gameObject;
            player.transform.parent = gameObject.transform; // Parenting code taken from: https://thiscodedoesthis.com/moving-platform-unity/
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerTouching = false;
            player.transform.parent = null;
        }
    }

    void FixedUpdate() {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

        if (transform.position == endMarker.position) {
            Transform dummy = startMarker;
            startMarker = endMarker;
            endMarker = dummy;

            Setup();
        }
    }
}
