using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement instance { get; private set; }
    private AudioSource audioSrc;
    public bool isMuted = false;

    private void Awake()
    {
        instance = this; 
        audioSrc = GetComponent<AudioSource>();
        
    }

    public void PlaySound(AudioClip _sound)
    {

        if (!audioSrc.isPlaying)
        {
            audioSrc.PlayOneShot(_sound);
        }
        
    }

    public void MuteSounds()
    {
        //if the game is not muted, mute sounds. Otherwise, unmute sounds
        if (isMuted == false)
        {
            AudioListener.volume = 0;
            isMuted = true;
        }
        else
        {
            AudioListener.volume = 1;
            isMuted = false;
        }
    }


}


