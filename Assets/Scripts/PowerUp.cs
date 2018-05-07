using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    public const float lifeValue = 10f;
    public const float pickupTimerValue = 1f;

    public float pickupTimer;
    public Player player;
    public RoundManager roundManager;
    public float oneTenthHeight;
    public float oneSixthWidth;
    public float powerUpLife;

    public bool isAlive;
    public bool isPickedUp;

    public GameObject playerSprite;
    public GameObject shield;

    private void Awake()
    {
        player = GameObject.Find("FingerTarget").GetComponent<Player>();
        playerSprite = GameObject.Find("FingerSprite");
        shield = GameObject.Find("Shield");
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        powerUpLife = lifeValue;
    }

	void Start () {
        
        pickupTimer = pickupTimerValue;
        //oneTenthHeight = Screen.height / 10;
        //oneSixthWidth = Screen.width / 6;
        //topRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight, 10);
        //topLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight, 10);
        //bottomRight = new Vector3(Screen.width - oneSixthWidth, Screen.height - oneTenthHeight * 9, 10);
        //bottomLeft = new Vector3(Screen.width - oneSixthWidth * 5, Screen.height - oneTenthHeight * 9, 10);
        //transform.position = Camera.main.ScreenToWorldPoint(bottomRight);
	}
	
	void Update () {
        //if (isAlive == true)
        //{
        //    LifeTime();
        //} 
        //else 
        //{
        //    powerUpLife = lifeValue;
        //}
        //if (roundManager.life == 2)
        //{
        //    shield.transform.position = playerSprite.transform.position;
        //    shield.transform.parent = playerSprite.transform;
        //}
        //else if (roundManager.life == 1)
        //{
        //    shield.transform.parent = null;
        //    shield.transform.position = new Vector3(100, 0, 0);
        //    isPickedUp = false;
        //}
        //if (!roundManager.isPlaying)
        //{
        //    transform.position = new Vector3(100, 0, 0);
        //}


        //Powerup spawnt ook al is er een opgepakt.
	}

    void OnTriggerStay2D(Collider2D other)
	{
        if (other.tag == "Player")
        {
            pickupTimer -= Time.deltaTime;

            if (pickupTimer <= 0)
            {
                isPickedUp = true;
                roundManager.hitPoints = 2;
                isAlive = false;
                transform.position = new Vector3(100, 0, 0);
            }
        }
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
            isAlive = false;
            powerUpLife = lifeValue;
        }
        if (!isAlive)
        {
            transform.position = new Vector3(100, 0, 0);
        }
    }

	public void PickUpPowerUp(PowerUp power)
    {

    }

}

public class Shield : PowerUp {


}
