using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	public enum powerups { Shield, Slowmo, Reduce };
	public powerups nextPowerUp;

	public Shield shield;

	public enum state { Idle, Spawned, Reset};
	public state currentSpawnState;

	public float oneTenthHeight;
    public float oneSixthWidth;

    public RoundManager roundManager;

	public PowerUp powerUp;

    private int width;
    private int height;
    public int powerUpCounter;
    public int powerUpMultiplier;
    
	public float coolDown;
	public float coolDownMin;
	public float coolDownMax;

    private Vector3 nextPowerUpPosition;
    public Vector3[] powerUpPositions;

	private void Awake()
	{
        width = Screen.width;
        height = Screen.height;

        oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;

		coolDownMin = 5f;
		coolDownMax = 10f;
	}

	void Start () {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        //powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();
		currentSpawnState = state.Reset;
		nextPowerUp = powerups.Shield;
		//powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();
		shield = GameObject.Find("ShieldPickUp").GetComponent<Shield>();
		powerUp = shield;
        //nextPowerUpPosition = Camera.main.ScreenToWorldPoint(topRight);
        //transform.position = nextPowerUpPosition;

        //powerUpPositions = new Vector3[4];
        //powerUpPositions[0] = topRight;
        //powerUpPositions[1] = topLeft;
        //powerUpPositions[2] = bottomRight;
        //powerUpPositions[3] = bottomLeft;

        //PowerUpMultiplier();
	}
	
	//void Update () 
 //   {
	//	//print(currentSpawnState);
 //       if (roundManager.isPlaying)
 //       {
	//		switch (currentSpawnState)
	//		{
	//			case state.Idle:
	//				coolDown -= Time.deltaTime;
	//				if (powerUp.currentState != PowerUp.state.PickedUp)
	//				{
	//					if (coolDown <= 0f)
	//					{
	//						PlacePowerUp();
	//						currentSpawnState = state.Spawned;
	//					}
	//				}
	//			    break;
	//			case state.Spawned:
	//				coolDown = 0f;
	//				powerUp.Shield();
	//				//powerUp.currentState = PowerUp.state.OnField;
	//				currentSpawnState = state.Reset;
 //                   break;
	//			case state.Reset:
	//				coolDown = Random.Range(coolDownMin, coolDownMax);
	//				currentSpawnState = state.Idle;
 //                   break;
	//			default:
	//				print("CurrentSpawnState is set to Default, this should not happen");
	//				break;
	//		}
 //       }
	//}

	void PickRandomPowerup(){
		nextPowerUp = (powerups)Random.Range(0, 3);
	}

    void SetRandomPosition()
    {
        int randomizerW = Random.Range(1, 5);
        int randomizerH = Random.Range(1, 9);
        nextPowerUpPosition = new Vector3(Screen.width - oneSixthWidth * randomizerW, Screen.height - oneTenthHeight * randomizerH, 10);
    }

	void PlacePowerUp()
	{
		SetRandomPosition();
		nextPowerUpPosition = Camera.main.ScreenToWorldPoint(nextPowerUpPosition);
		//powerUp.isAlive = true;
		powerUp.transform.position = nextPowerUpPosition;
		powerUp.currentState = PowerUp.state.OnField;
		//powerUp.transform.position = nextPowerUpPosition;
		//Instantiate(powerUp, nextPowerUpPosition, Quaternion.identity);
	}
}
