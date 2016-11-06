using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SoundEngine : MonoBehaviour {

    public static SoundEngine instance = null;

    public AudioClip audioClick;
    public AudioClip audioDrag;
    public AudioClip audioWin;

    private AudioSource audioSource;

    void Awake() {
        if( instance == null ) {
            instance = this;
        } else  if( instance != this ) {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip, 0.7f);
    }
}
