using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangers : MonoBehaviour {

	public RoundManager roundManager;
	public GameObject bloodSplatter;

	public void AssignScript(){
		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
		bloodSplatter = GameObject.Find ("BloodSplatter");
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			roundManager.isDead = true;
			if (roundManager.currentRound == round.Playing) {
//				bloodSplatter.transform.position = other.transform.position;
				Instantiate(bloodSplatter, other.transform.position, Quaternion.identity);
			}
		}
	}
}
