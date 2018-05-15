using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Dangers {

	public SpriteRenderer sprenderer;
	public Sprite rotating;
	public Sprite still;
	public float speed;

	// Use this for initialization
	void Start () {
		AssignScript ();
		sprenderer = GetComponent<SpriteRenderer> ();
//		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Spin ();
	}

	void Spin(){
		if (roundManager.currentRound == round.Playing) {
			transform.Rotate (Vector3.back, Time.deltaTime * 750);
			sprenderer.sprite = rotating;
		} else {
			sprenderer.sprite = still;
		}
	}
}
