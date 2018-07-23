using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;

public class Shuriken : MonoBehaviour {

	public RoundManager roundManager;
    public PeakNShoot peakNShoot;
	public PowerUpSpawner powerUpSpawner;
    public GameObject bloodSplatter;

	public const float peakOffset = 1f;

	public enum direction { North, East, South, West }
	public direction nextDirection;

	public enum starState { Initiation, SetLocation, Peak, Shoot, Wait, Reset };
	public SpriteRenderer sprenderer;
	public Sprite rotating;
	public Sprite still;
	public starState currentState;
	public starState nextState;

	public GameObject player;
	public GameObject shurikenIndicator;

	public float peakTimer;
	public float shootTimer;
	public float breathingTime;
	public float peakSpeed;
	public float shootSpeed;
	public float endlocation;
	public float peakTime;

	public Vector3 starPosition;
    public Vector3 spawnLocation;
    public Vector3 peakLocation;  
	public Vector3 shootDirection;
	public Vector3 peakDirection;
	public Vector3 shootLocation;
	public Vector3 playerLocation;
   
	// Use this for initialization
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
        bloodSplatter = GameObject.Find("BloodSplatterPivot");

		player = GameObject.Find("FingerTarget");
		shurikenIndicator = GameObject.Find("ShurikenIndicator");
		sprenderer = GetComponent<SpriteRenderer> ();
		currentState = starState.Initiation;
//		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Spin ();
		//print(currentState);
		if (roundManager.currentRound == round.Playing)
        {
            starPosition = transform.position;
            PeakAndShoot();
        }
	}

	void Spin(){
		if (currentState == starState.Shoot && roundManager.currentRound == round.Playing) {
			transform.Rotate (Vector3.back, Time.deltaTime * 900);
			//transform.DOKill(false);
			//transform.DORotate(new Vector3(0, 0, 359), .2f).SetLoops(-1);
			sprenderer.sprite = rotating;
		} else {
			sprenderer.sprite = still;
		}
	}

	void OnBecameInvisible()
    {
        if (currentState == starState.Shoot)
        {
            currentState = starState.Reset;
        }
    }   

	void PeakAndShoot()
    {
        switch (currentState)
        {
            case Shuriken.starState.Initiation:
                peakTimer = 0;
                shootTimer = 0;
				//TapticManager.Impact(ImpactFeedback.Heavy);
                currentState = Shuriken.starState.SetLocation;
                break;
            case Shuriken.starState.SetLocation:
                SetPosition();
                //currentState = Shuriken.starState.Wait;
                //nextState = Shuriken.starState.Peak;
				//TapticManager.Impact(ImpactFeedback.Light);
				currentState = Shuriken.starState.Peak;
                breathingTime = peakTimer;
                break;
            case Shuriken.starState.Peak:
                Peak();
                if (starPosition == peakLocation)
                {
                    //currentState = Shuriken.starState.Wait;
                    //nextState = Shuriken.starState.Shoot;
					currentState = Shuriken.starState.Shoot;
                    breathingTime = shootTimer;
                }
                break;
            case Shuriken.starState.Shoot:
                Shoot();
                break;
            case Shuriken.starState.Wait:
                Wait();
                break;
            case Shuriken.starState.Reset:
				//peakNShoot.AddToScore(1);
                //peakTimer = peakTimer - 0.02f;
                //shootTimer = shootTimer - 0.04f;
                currentState = Shuriken.starState.SetLocation;
                break;
            default:
                break;
        }
    }

	void SetPosition()
    {
        nextDirection = (direction)Random.Range(0, 4);
		//float spawnMargin = Random.Range(-1f, 1f);
		float randomSpawn = Random.Range(0f, 1f);
	    peakTime = 1f;
		peakSpeed = 2f;
		playerLocation = player.transform.position;

		switch (nextDirection)
        {
            case direction.North:
				spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomSpawn, 1.2f, 0.5f));//new Vector3((player.transform.position.x + spawnMargin), 7, 0);
                peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y - peakOffset), spawnLocation.z);
				endlocation = peakLocation.y;
                break;
            case direction.East:
				spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(1.3f, randomSpawn, 0.5f));//new Vector3(4.5f, (player.transform.position.y + spawnMargin), 0);
                peakLocation = new Vector3((spawnLocation.x - peakOffset), spawnLocation.y, spawnLocation.z);
				endlocation = peakLocation.x;
                break;
            case direction.South:
				spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomSpawn, -.2f, 0.5f));//new Vector3((player.transform.position.x + spawnMargin), -7, 0);
                peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y + peakOffset), spawnLocation.z);
				endlocation = peakLocation.y;
                break;
            case direction.West:
				spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(-.3f, randomSpawn, 0.5f));//new Vector3(-4.5f, (player.transform.position.y + spawnMargin), 0);
                peakLocation = new Vector3((spawnLocation.x + peakOffset), spawnLocation.y, spawnLocation.z);
				endlocation = peakLocation.x;
                break;
            default:
                break;
        }
        transform.position = spawnLocation;
    }

	void Peak()
    {
		peakDirection = (peakLocation - spawnLocation).normalized;
		//shootDirection = (playerLocation - peakLocation).normalized;
		//peakSpeed = (peakSpeed / 2);

		//transform.DOKill(false);
		//transform.DOMove(peakLocation, peakTime);
		//transform.DORotate(new Vector3(0, 0, -3), peakTime);

		transform.Translate(peakDirection * Time.deltaTime * peakSpeed, Space.World);
        
		//transform.position = spawnLocation + new Vector3(Mathf.Sin(Time.time), 0, 0);
        

		peakTime -= Time.deltaTime;
		if (peakTime <= 0){
			breathingTime = .5f;
			currentState = starState.Wait;
			nextState = starState.Shoot;
		}
    }

	void Shoot()
    {
        shootSpeed = 20f;
		transform.Translate(peakDirection * (Time.deltaTime * shootSpeed), Space.World);
    }

	void Wait()
    {
        if (nextState == Shuriken.starState.Peak)
        {
            breathingTime -= Time.deltaTime;
            if (breathingTime <= 0)
            {
                currentState = nextState;
            }
        }
        
        if (nextState == Shuriken.starState.Shoot)
        {
            breathingTime -= Time.deltaTime;
            if (breathingTime <= 0)
            {
                currentState = nextState;
            }
        }
    }
}
