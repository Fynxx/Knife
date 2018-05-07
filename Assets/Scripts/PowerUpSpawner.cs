using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    public float oneTenthHeight;
    public float oneSixthWidth;

    public RoundManager roundManager;

    public PowerUp powerUp;

    private int width;
    private int height;
    public int powerUpCounter;
    public int powerUpMultiplier;

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
        powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();

        //nextPowerUpPosition = Camera.main.ScreenToWorldPoint(topRight);
        //transform.position = nextPowerUpPosition;

        //powerUpPositions = new Vector3[4];
        //powerUpPositions[0] = topRight;
        //powerUpPositions[1] = topLeft;
        //powerUpPositions[2] = bottomRight;
        //powerUpPositions[3] = bottomLeft;

        PowerUpMultiplier();
	}
	
	void Update () 
    {
        //if (roundManager.isPlaying)
        //{
        //    if (powerUp.isPickedUp == false)
        //    {
        //        if (powerUpCounter >= powerUpMultiplier)
        //        {
        //            powerUpCounter = 1;
        //            PlacePowerUp();
        //        }
        //    }
        //}
	}

    void SetRandomPosition()
    {
        int randomizerW = Random.Range(1, 5);
        int randomizerH = Random.Range(1, 9);
        nextPowerUpPosition = new Vector3(Screen.width - oneSixthWidth * randomizerW, Screen.height - oneTenthHeight * randomizerH, 10);
    }

    void PlacePowerUp()
    {
        if (!powerUp.isPickedUp)
        {
            SetRandomPosition();
            nextPowerUpPosition = Camera.main.ScreenToWorldPoint(nextPowerUpPosition);
            powerUp.isAlive = true;
            powerUp.transform.position = nextPowerUpPosition;
            //Instantiate(powerUp, nextPowerUpPosition, Quaternion.identity);
        }
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
