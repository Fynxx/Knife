using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
	public AudioSource audioSource;
	public AudioClip nextClip;
	public AudioClip buttonPress;
	public AudioClip dead;
	public AudioClip waveClear;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	public void ButtonPressAudio(){
		nextClip = buttonPress;
		audioSource.PlayOneShot(nextClip);
	}

	public void DeadAudio(){
		nextClip = dead;
        audioSource.PlayOneShot(nextClip);
	}

	public void WaveClear(){
		nextClip = waveClear;
        audioSource.PlayOneShot(nextClip); 
	}
}
