using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{
	public GameObject GOshield;
    
	void Start()
	{
        GOshield = GameObject.Find("Shield");
	}
       
	void Update()
	{
		switch (currentState)
        {
			case state.Idle:
				PickUpPowerUp(shield);
				pickupTimer = pickupTimerValue;
				break;
            case state.OnField:
				LifeTime();          
                if (isPickingUp)
                {
                    pickupTimer -= Time.deltaTime;
                    if (pickupTimer <= 0)
                    {
						roundManager.hitPoints = 2;
                        currentState = state.PickedUp;
						//print("picking up done");
                    }
                }
                break;
            case state.PickedUp:
				//print("picked up");
			    
                transform.position = new Vector3(100, 0, 0);
				GOshield.transform.position = playerSprite.transform.position;
                GOshield.transform.parent = playerSprite.transform;

				if (roundManager.hitPoints == 1)
                {
                    //GOshield.transform.parent = null;
                    //GOshield.transform.position = new Vector3(100, 0, 0);
                    currentState = state.Reset;
                }
                break;
			case state.Reset:
                GOshield.transform.parent = null;
                GOshield.transform.position = new Vector3(100, 0, 0);
                currentState = state.Idle;
                break;
            default:
                print("Shield current state is default, this should not happen.");
                break;
        } 

	}

	void OnField()
	{

	}

	void PickingUp()
	{

	}

	void PickedUp()
    {

    }

	void Unavailable()
    {

    }

	void Drop(){
		
	}
}
