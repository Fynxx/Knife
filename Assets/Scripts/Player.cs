using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 start;
    public Vector3 end;
    public float speed;
    public PowerUp powerUp;

	void Update () {
		FingerTarget ();
        speed = 1;
	}

	void FingerTarget(){
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
            //transform.position = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
            start = transform.position;
            end = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position = Vector3.Lerp(start, end, speed);
		}
	}
}
