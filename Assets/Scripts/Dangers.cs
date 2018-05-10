using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class Dangers : MonoBehaviour {

	public RoundManager roundManager;
	public GameObject bloodSplatter;

	public void AssignScript(){
		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
		bloodSplatter = GameObject.Find ("BloodSplatter");
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
            roundManager.hitPoints--;
			//roundManager.isDead = true;
            if (roundManager.hitPoints == 0) {
//				bloodSplatter.transform.position = other.transform.position;
                roundManager.isDead = true;
				Instantiate(bloodSplatter, other.transform.position, Quaternion.identity);
				TapticManager.Notification(NotificationFeedback.Error);
			}
		}
	}
}
//if (roundManager.currentRound == round.Playing)