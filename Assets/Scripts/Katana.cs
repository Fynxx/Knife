using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon {
    
	public enum State { inactive, active };
	public State state;

	public AudioSource audioSource;
    public AudioClip whoosh;

	// Use this for initialization
	void Start () {
		//state = State.active;
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		audioSource = GetComponent<AudioSource>();
		speed = waveManager.speed;
	}
	
	// Update is called once per frame
	void Update () {
		speed = waveManager.speed;
        if (roundManager.currentState == global::State.Playing && state == State.active){
			transform.Translate(Vector3.down * (Time.deltaTime * speed), Space.World);
		}
	}

	void OnBecameVisible()
	{
		float ran = Random.Range(1f, 1.5f);
        audioSource.pitch = ran;
		audioSource.PlayOneShot(whoosh);
	}

	void OnBecameInvisible()
    {
		state = State.inactive;
        //gameObject.SetActive(false);
    }   
}
