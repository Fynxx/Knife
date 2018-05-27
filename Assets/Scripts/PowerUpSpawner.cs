using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

	public enum powerups { Shield, Slowmo, MinOne };
	public powerups nextPowerUp;

	public Shield shield;

	public enum state { Idle, OnField, PickedUp, Reset};
	public state currentSpawnState;

	public float oneTenthHeight;
    public float oneSixthWidth;

    public RoundManager roundManager;

	public PowerUp powerUp;

    private int width;
    private int height;
    public int powerUpCounter;
    public int powerUpMultiplier;

	public int coolDown;
	public int coolDownMin = 1;
	public int coolDownMax = 2;

    private Vector3 nextPowerUpPosition;
    public Vector3[] powerUpPositions;

	private void Awake()
	{
        width = Screen.width;
        height = Screen.height;

        oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;
	}

	void Start () {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        //powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();
		currentSpawnState = state.Reset;
		nextPowerUp = powerups.Shield;
		powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();
        //nextPowerUpPosition = Camera.main.ScreenToWorldPoint(topRight);
        //transform.position = nextPowerUpPosition;

        //powerUpPositions = new Vector3[4];
        //powerUpPositions[0] = topRight;
        //powerUpPositions[1] = topLeft;
        //powerUpPositions[2] = bottomRight;
        //powerUpPositions[3] = bottomLeft;

        //PowerUpMultiplier();
	}
	
	void Update () 
    {
        if (roundManager.isPlaying)
        {
			switch (currentSpawnState)
			{
				case state.Idle:
					if (coolDown == 0){
						currentSpawnState = state.OnField;
					}
				    break;
				case state.OnField:
					coolDown = 0;
					powerUp.Shield();
                    PlacePowerUp();
					powerUp.currentState = PowerUp.state.OnField;
                    break;
				case state.PickedUp:

                    break;
				case state.Reset:
					coolDown = Random.Range(coolDownMin, coolDownMax);
					currentSpawnState = state.Idle;
                    break;
				default:
					print("CurrentSpawnState is set to Default, this should not happen");
					break;
			}
        }
	}

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
		powerUp.currentState = PowerUp.state.OnField;
        powerUp.transform.position = nextPowerUpPosition;
        //Instantiate(powerUp, nextPowerUpPosition, Quaternion.identity);
    }

    void PowerUpMultiplier()
    {
        powerUpMultiplier = Random.Range(5, 15);
    }

    public void AddToCounter()
    {
        powerUpCounter++;
    }
}
