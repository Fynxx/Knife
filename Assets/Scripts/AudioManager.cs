using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
	public AudioSource audioSource;
    public AudioClip menuProgress;
    public AudioClip menuBack;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
    
    public void MenuProgress(){
        float ran = Random.Range(1f, 1.5f);
        audioSource.pitch = ran;
        audioSource.PlayOneShot(menuProgress);
    }
    
    public void MenuBack()
    {
        float ran = Random.Range(1f, 1.5f);
        audioSource.pitch = ran;
        audioSource.PlayOneShot(menuBack);
    }
    
}
