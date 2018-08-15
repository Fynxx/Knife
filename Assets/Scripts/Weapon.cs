using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public RoundManager roundManager;
    public WaveManager waveManager;
    public float speed;
	//public float speedMultiplier;
    
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();      
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		speed = 7;
	}

	void Update () {
		
	}
}
