using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class Player : MonoBehaviour {

	public RoundManager roundManager;
	public GameObject bloodSplatter;
	public AudioManager audioManager;

	public Transform powerupIndicator;
	public Transform noPowerup;
    public Vector3 start;
    public Vector3 end;
    public float speed;

	public Vector3 playerPosition;
	public int hitPoints;
	public int multiplier;
	public int effect;

	public float height;
	public float two;
	public float four;
	public TrailRenderer effect2;
	public ParticleSystem effect4;

    public PowerUp powerUp;
	//public Spawner spawner;
	public GameObject fingerSprite;
	//public GameObject multiplierField;

	void Start(){
		Application.targetFrameRate = 60;
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		bloodSplatter = GameObject.Find("BloodSplatterPivot");
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		//spawner = GameObject.Find("PowerUpSpawner").GetComponent<Spawner>();
		fingerSprite = GameObject.Find("FingerSprite");
		noPowerup = GameObject.Find("NoPowerUp").GetComponent<Transform>();
		powerupIndicator = noPowerup;
		two = Screen.height / 3;
		four = two * 2;
		effect2.enabled = false;
		effect4.gameObject.SetActive(false);
		multiplier = 1;
	}

	void Update () {
		FingerTarget ();
        speed = 1;
		//playerPosition = Camera.main.WorldToScreenPoint(transform.position);
		effect4.transform.position = fingerSprite.transform.position;
                      
		if (hitPoints == 1){
			//spawner.isAllowedToSpawn = true;          
            powerupIndicator.position = new Vector3(100, 0, 0);
			if (powerupIndicator.parent != null){
				//spawner.isAllowedToSpawn = false;
				powerupIndicator.parent = null;
			}
		}
		else {
			//spawner.isAllowedToSpawn = false;
			powerupIndicator.position = fingerSprite.transform.position;
            powerupIndicator.parent = fingerSprite.transform;
		}

		if (powerupIndicator == null){
			powerupIndicator = noPowerup;
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
		if (Input.touchCount > 0 && roundManager.currentState == State.Active) {
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
            if (hitPoints == 0)
            {
				roundManager.KillPlayer();
                Instantiate(bloodSplatter, transform.position, Quaternion.identity);
                TapticManager.Notification(NotificationFeedback.Error);
				audioManager.DeadAudio();
            }
        }    
    }

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "Multiplier"){
			multiplier = other.GetComponent<MultiplierField>().multiplier;
			effect = other.GetComponent<MultiplierField>().effect;
			if (effect == 1)
            {
                effect2.enabled = true;
				effect4.gameObject.SetActive(false);
            }
            else if (effect == 2)
            {
				effect2.enabled = false;
                effect4.gameObject.SetActive(true);
            }
		} else {
            effect2.enabled = false;
            effect4.gameObject.SetActive(false);
        }
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Multiplier"){
            multiplier = 1;
			effect2.enabled = false;
            effect4.gameObject.SetActive(false);
        } 
	}
}
