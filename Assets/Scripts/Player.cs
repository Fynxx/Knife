using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class Player : MonoBehaviour {

	public RoundManager roundManager;
	public GameObject bloodSplatter;
	public Transform powerupIndicator;
    public Vector3 start;
    public Vector3 end;
    public float speed;

	public int hitPoints;

    public PowerUp powerUp;
	public Spawner spawner;
	public GameObject fingerSprite;

	void Start(){
		Application.targetFrameRate = 60;
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		bloodSplatter = GameObject.Find("BloodSplatterPivot");
		spawner = GameObject.Find("PowerUpSpawner").GetComponent<Spawner>();
		fingerSprite = GameObject.Find("FingerSprite");
	}

	void Update () {
		FingerTarget ();
        speed = 1;

		if (hitPoints == 1){
			spawner.isAllowedToSpawn = true; 
			powerupIndicator.parent = null;
            powerupIndicator.position = new Vector3(100, 0, 0);
		}
		else {
			spawner.isAllowedToSpawn = false;
			powerupIndicator.position = fingerSprite.transform.position;
            powerupIndicator.parent = fingerSprite.transform;
		}

		//if (powerupIndicator != null){
		//	powerupIndicator.position = fingerSprite.transform.position;
		//	powerupIndicator.parent = fingerSprite.transform;
		//} else {
		//	powerupIndicator.parent = null;
		//	powerupIndicator.position = new Vector3(100, 0, 0);
		//}
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
            hitPoints--;
            //roundManager.isDead = true;
            if (hitPoints == 0)
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
