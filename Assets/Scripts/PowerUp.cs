using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUp : MonoBehaviour {

    public const float lifeValue = 10f;
    public const float pickupTimerValue = 1f;
    //public const float 

	public enum state { OnField, PickingUp, PickedUp, Unavailable };
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
	public GameObject GOshield;

    private void Awake()
    {
        player = GameObject.Find("FingerTarget").GetComponent<Player>();
        playerSprite = GameObject.Find("FingerSprite");
        GOshield = GameObject.Find("Shield");
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		shield = GameObject.Find("ShieldPickUp").GetComponent<Shield>();
        powerUpLife = lifeValue;
    }

	void Start () {
        
        pickupTimer = pickupTimerValue;
        oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;
		currentState = state.Unavailable;
        //topRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight, 10);
        //topLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight, 10);
        //bottomRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight * 9, 10);
        //bottomLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight * 9, 10);
        //transform.position = Camera.main.ScreenToWorldPoint(bottomRight);
	}

	void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag == "Player" && currentState == state.OnField)
        {
			currentState = state.PickingUp;
		} else {
			currentState = state.Unavailable;
		}
    }
	
	void Update () {
		print(currentState);
		switch (currentState)
		{
			case state.OnField:
				Shield();
				LifeTime();

				break;
			case state.PickingUp:
				    pickupTimer--;
                break;
			case state.PickedUp:
    				currentPowerup.transform.position = playerSprite.transform.position;
    				currentPowerup.transform.parent = playerSprite.transform;
				break;
			case state.Unavailable:
    				currentPowerup.transform.parent = null;
                    currentPowerup.transform.position = new Vector3(100, 0, 0);
                break;
			default:
				break;
		}
        //Powerup spawnt ook al is er een opgepakt.
	}
   

    void OnTriggerExit2D(Collider2D other)
	{
        pickupTimer = pickupTimerValue;
	}

    void LifeTime()
    {
        powerUpLife -= Time.deltaTime;
        if (powerUpLife < 0)
        {
			currentState = state.PickedUp;
            powerUpLife = lifeValue;
        }
		if (currentState != state.OnField)
        {
            transform.position = new Vector3(100, 0, 0);
        }
    }

	public void PickUpPowerUp(PowerUp power)
    {

    }

	public void Shield(){
		currentPowerup = shield;
	}
	public void SlowMo(){
		
	}
	public void Reduce(){
		
	}

}
