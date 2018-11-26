using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Shield : PowerUp
{
	public Multiplier multiplier;
	public GameObject shield;
	public GameObject GOShield;
    
	public Image shieldPickupBar;
    
	void Start()
	{
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();
        shield = this.gameObject;
		shieldPickupBar = GameObject.Find("ShieldPickupBar").GetComponent<Image>();
		GOShield = GameObject.Find("Shield");
		//shield = GameObject.Find("Shield");
		//pickupTimer = 1.25f;

	}

	void OnEnable()
    {
        lifeTime = 4;
		pickupTimer = 1;
    }
       
	void Update()
	{
		shieldPickupBar.fillAmount = pickupTimer;
		//print(spawner.currentState);
        if (roundManager.currentState == State.Playing)
        {
            if (lifeTime > 0)
            {
				lifeTime -= Time.deltaTime;

            }
            else if (lifeTime < 0)
            {
				//spawner.isAlive = false;
				Die(shield);
            }
        }
	}
   
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
			pickupTimer -= (Time.deltaTime * .75f) * Input.GetTouch(0).pressure;
			//print("picking up");   

			if (pickupTimer < 0 )//|| Input.GetTouch(0).pressure > 5
            {
				//spawner.isAlive = false;
				player.hitPoints = 2;
				player.powerupIndicator = GOShield.transform;
				//spawner.isAllowedToSpawn = true;
				multiplier.coins -= 8;
				Die(shield);
            }
        }

    }
}
