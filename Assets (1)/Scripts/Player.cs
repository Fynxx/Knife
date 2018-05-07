using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		FingerTarget ();
	}

	void FingerTarget(){
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			transform.position = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
		}

	}
}
