using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class Player : MonoBehaviour {

	public RoundManager roundManager;
	public GameObject bloodSplatter;
    public Vector3 start;
    public Vector3 end;
    public float speed;
    public PowerUp powerUp;

	void Start(){
		Application.targetFrameRate = 60;
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		bloodSplatter = GameObject.Find("BloodSplatterPivot");
	}

	void Update () {
		FingerTarget ();
        speed = 1;
	}

	void FingerTarget(){
		if (Input.touchCount > 0 && roundManager.currentRound == round.Playing) {
            //transform.position = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
            start = transform.position;
            end = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            transform.position = Vector3.Lerp(start, end, speed);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            roundManager.hitPoints--;
            //roundManager.isDead = true;
            if (roundManager.hitPoints == 0)
            {
				//              bloodSplatter.transform.position = other.transform.position;
                roundManager.isDead = true;
                Instantiate(bloodSplatter, other.transform.position, Quaternion.identity);
                TapticManager.Notification(NotificationFeedback.Error);
            }
        }
        //      if (other.tag == "Barrier")
        //{
        //  peakNShoot.currentState = PeakNShoot.starState.Reset;
        //}
    }
}
