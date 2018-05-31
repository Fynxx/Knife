using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUp : MonoBehaviour {

    public const float lifeValue = 3f;
    public const float pickupTimerValue = .75f;
    //public const float 

	public enum state { Idle, OnField, PickedUp, Reset };
	public state currentState;

	public PowerUp currentPowerup;
	public Shield shield;

    public float pickupTimer;
    public Player player;
    public RoundManager roundManager;
    public float oneTenthHeight;
    public float oneSixthWidth;
    public float powerUpLife;

    public GameObject playerSprite;
	//public GameObject GOshield;
       
	public bool isPickingUp;

    private void Awake()
    {
        player = GameObject.Find("FingerTarget").GetComponent<Player>();
        playerSprite = GameObject.Find("FingerSprite");
		//GOshield = GameObject.Find("Shield");
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		shield = GameObject.Find("ShieldPickUp").GetComponent<Shield>();
        powerUpLife = lifeValue;      
    }

	void Start () {
        
        pickupTimer = pickupTimerValue;
        oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;
		currentState = state.Idle;
		currentPowerup = shield;
        //topRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight, 10);
        //topLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight, 10);
        //bottomRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight * 9, 10);
        //bottomLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight * 9, 10);
        //transform.position = Camera.main.ScreenToWorldPoint(bottomRight);
	}

	void OnTriggerStay2D(Collider2D other)
    {
		print(isPickingUp);
		if (other.tag == "Player")
        {
			isPickingUp = true;
			//print("picking up");
		} else {
			//print("powerup reset");
			isPickingUp = false;
			currentState = state.Reset;
		}
    }
	
	void Update () {
		print(currentState);
		switch (currentState)
		{
			case state.Idle:
			    //PickUpPowerUp(shield);

                break;
			case state.OnField:
				//PickUpPowerUp(shield);
				//LifeTime();
				//print(isPickingUp);
				//if (isPickingUp){
					
				//	pickupTimer -= Time.deltaTime;
    //                if (pickupTimer < 0)
    //                {
    //                    currentState = state.PickedUp;
    //                }
				//}

				break;
			case state.PickedUp:

				break;
			case state.Reset:
				//GOshield.transform.parent = null;
				//GOshield.transform.position = new Vector3(100, 0, 0);
			    //currentState = state.Idle;
                break;
			default:
				break;
		}
        //Powerup spawnt ook al is er een opgepakt.
	}
   

    void OnTriggerExit2D(Collider2D other)
	{
        //pickupTimer = pickupTimerValue;
	}

    public void LifeTime()
    {
        powerUpLife -= Time.deltaTime;
        if (powerUpLife < 0)
        {
			currentState = state.Reset;
            powerUpLife = lifeValue;
        }
		if (currentState != state.OnField)
        {
            transform.position = new Vector3(100, 0, 0);
        }
    }

	public void PickUpPowerUp(PowerUp power)
    {
		currentPowerup = power;
    }

	public void Shield(){
		currentPowerup = shield;
	}
	public void SlowMo(){
		
	}
	public void Reduce(){
		
	}

}
