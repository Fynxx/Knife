using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;

public class Shuriken : MonoBehaviour {

	public RoundManager roundManager;
    public PeakNShoot peakNShoot;
    public GameObject bloodSplatter;

	public const float peakOffset = 2.3f;

	public enum direction { North, East, South, West }
	public direction nextDirection;

	public enum starState { Initiation, SetLocation, Peak, Shoot, Wait, Reset };
	public SpriteRenderer sprenderer;
	public Sprite rotating;
	public Sprite still;
	public starState currentState;
	public starState nextState;

	public GameObject player;

	public float peakTimer;
	public float shootTimer;
	public float breathingTime;
	public float peakSpeed;
	public float shootSpeed;

	public Vector3 starPosition;
    public Vector3 spawnLocation;
    public Vector3 peakLocation;  
	public Vector3 shootDirection;


	// Use this for initialization
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
        bloodSplatter = GameObject.Find("BloodSplatterPivot");

		player = GameObject.Find("FingerTarget");
		sprenderer = GetComponent<SpriteRenderer> ();
		currentState = starState.Initiation;
//		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Spin ();

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
				TapticManager.Impact(ImpactFeedback.Light);
                currentState = Shuriken.starState.SetLocation;
                break;
            case Shuriken.starState.SetLocation:
                SetPosition();
                //currentState = Shuriken.starState.Wait;
                //nextState = Shuriken.starState.Peak;
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
				peakNShoot.AddToScore(1);
                peakTimer = peakTimer - 0.02f;
                shootTimer = shootTimer - 0.04f;
                currentState = Shuriken.starState.SetLocation;
                break;
            default:
                break;
        }
    }

	void SetPosition()
    {
        nextDirection = (direction)Random.Range(0, 4);
		float spawnMargin = Random.Range(-1.5f, 1.5f);

		switch (nextDirection)
        {
            case direction.North:
				spawnLocation = new Vector3((player.transform.position.x + spawnMargin), 7, 0);
                peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y - peakOffset), spawnLocation.z);
                break;
            case direction.East:
				spawnLocation = new Vector3(4.5f, (player.transform.position.y + spawnMargin), 0);
                peakLocation = new Vector3((spawnLocation.x - peakOffset), spawnLocation.y, spawnLocation.z);
                break;
            case direction.South:
				spawnLocation = new Vector3((player.transform.position.x + spawnMargin), -7, 0);
                peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y + peakOffset), spawnLocation.z);
                break;
            case direction.West:
				spawnLocation = new Vector3(-4.5f, (player.transform.position.y + spawnMargin), 0);
                peakLocation = new Vector3((spawnLocation.x + peakOffset), spawnLocation.y, spawnLocation.z);
                break;
            default:
                break;
        }
        transform.position = spawnLocation;
    }

	void Peak()
    {
        peakSpeed = 2f;
        float peakTime = .15f;
        //float distCovered = (Time.time - startTime) * peakSpeed;
        //float fracJourney = distCovered / journeyLength;
        //Vector3 temp = Vector3.Lerp(spawnLocation, peakLocation, peakSpeed);
        //star.transform.position = temp;
		transform.DOKill(false);
        transform.DOMove(peakLocation, peakTime);
        transform.DORotate(new Vector3(0, 0, -1), peakTime);
        shootDirection = (peakLocation - spawnLocation).normalized;      
    }

	void Shoot()
    {
        shootSpeed = 20f;
        transform.Translate(shootDirection * (Time.deltaTime * shootSpeed), Space.World);
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
