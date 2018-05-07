using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Dangers {

	public bool rotating;
	public GameObject knifePivot;
	public round currentRound;

	private Quaternion startRotation;
	private Quaternion endRotation;

	// Use this for initialization
	void Start () {
		AssignScript ();
		knifePivot = transform.parent.gameObject;
		currentRound = roundManager.currentRound;
		rotating = true;

		startRotation.eulerAngles = new Vector3 (0, 0, 320);
		endRotation.eulerAngles = new Vector3 (0, 0, 90);
	}
	
	// Update is called once per frame
	void Update () {
//		KnifeRotation ();
	}

	public void KnifeRotation(){
//		if (roundManager.currentRound == round.Playing) {
			if (rotating == true) {
				knifePivot.transform.Rotate (Vector3.forward, Time.deltaTime * 100);
			}
			if (knifePivot.transform.eulerAngles.z > endRotation.eulerAngles.z && knifePivot.transform.eulerAngles.z < startRotation.eulerAngles.z) {
				rotating = false;
			} else {
				rotating = true;
			}
//		}
		if (roundManager.currentRound == round.Ended) {
			knifePivot.transform.eulerAngles = startRotation.eulerAngles;
		}
	}

}
