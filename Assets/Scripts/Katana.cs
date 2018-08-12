using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon {
    
	public enum State { inactive, active };
	public State state;

	// Use this for initialization
	void Start () {
		//state = State.active;
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		speed = waveManager.speed;
	}
	
	// Update is called once per frame
	void Update () {
		speed = waveManager.speed;
		if (roundManager.currentRound == round.Playing && state == State.active){
			transform.Translate(Vector3.down * (Time.deltaTime * speed), Space.World);
		}
	}

	void OnBecameInvisible()
    {
		state = State.inactive;
        //gameObject.SetActive(false);
    }   
}
