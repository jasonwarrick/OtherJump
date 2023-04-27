using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource spikeAudio;
    [SerializeField] AudioSource winAudio;
    
    public void Spike() {
        spikeAudio.Play();
    }

    public void Win() {
        winAudio.Play();
    }
}
