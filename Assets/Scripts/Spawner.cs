using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public RoundManager roundManager;
	public Multiplier multiplier;

	public enum state { SetLocation, Idle, Spawn, Wait };
	public state currentState;

	public GameObject item;
	public Vector3 spawnLocation;

	public float coolDown;
	public float coolDownMin;
    public float coolDownMax;   

	public bool isAlive;
	public bool isAllowedToSpawn;
	public bool hasRequirements;

	protected float randomX;
	protected float randomY;
	protected float oneTenthHeight;
	protected float oneSixthWidth;

    
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();

		oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;
        
		//coolDownMin = 1f;
		//coolDownMax = 2f;      

		currentState = state.SetLocation;
		isAllowedToSpawn = true;
	}

	void Update () {
		if (hasRequirements){
			if (multiplier.coins == 8){
				Spawn();
			}
		} else {
			Spawn();
		}


	}

	void Spawn()
	{
		float randomLoc = Random.Range(.2f, .8f);
		if (roundManager.currentRound == round.Reset){
			isAllowedToSpawn = true;
		}
		if (roundManager.currentRound == round.Playing && isAllowedToSpawn == true)
        {
            switch (currentState)
            {
                case state.SetLocation:
					
                    randomX = Random.Range(1, 5);
                    randomY = Random.Range(1, 9);
					spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomLoc, randomLoc, 10));
                    //spawnLocation = new Vector3(Screen.width - oneSixthWidth * randomX, Screen.height - oneTenthHeight * randomY, 10);
                    //spawnLocation = Camera.main.ScreenToWorldPoint(spawnLocation);
                    coolDown = Random.Range(coolDownMin, coolDownMax);
                    isAllowedToSpawn = true;
                    item.SetActive(false);

                    currentState = state.Idle;
                    break;
                case state.Idle:
                    coolDown -= Time.deltaTime;
                    if (coolDown <= 0)
                    {
                        currentState = state.Spawn;
                    }
                    break;
                case state.Spawn:
                    item.SetActive(true);
                    isAlive = true;
                    item.transform.position = spawnLocation;
                    isAllowedToSpawn = false;
                    currentState = state.Wait;
                    break;
                case state.Wait:
                    if (isAlive == false)
                    {
                        currentState = state.SetLocation;
                    }
                    break;
                default:
                    break;
            }
        }
	}
}
