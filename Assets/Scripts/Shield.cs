using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PowerUp
{

	// Use this for initialization
	void Start()
	{

	}



	void Update()
	{
		if (currentState == state.PickedUp)
		{
			pickupTimer -= Time.deltaTime;

			if (pickupTimer <= 0)
			{
				currentState = state.PickedUp;
				roundManager.hitPoints = 2;
				transform.position = new Vector3(100, 0, 0);
			}
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
