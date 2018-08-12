using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PeakNShoot : MonoBehaviour {

    public enum direction {North, East, South, West}

	public const float peakOffset = 2.3f;

	//public starState currentState;
	//public starState nextState;
	public direction nextDirection;

	public int starMultiplier;
	public int starsInGame;

	public float spawnLocationX;
	public float spawnLocationY;
	public float peakSpeed;
	public float shootSpeed;
	public float breathingTime;
	public float peakTimer;
    public float shootTimer;

	public Vector3 starPosition;
	public Vector3 spawnLocation;
	public Vector3 peakLocation;   
	public Vector3 peakDirection;
	public Vector3 shootDirection;
    
	public GameObject player;
	public GameObject starPrefab;
	public int pooledAmount = 6;
	public List<Shuriken> stars;

	public Shuriken star;
	public RoundManager roundManager;
	public PowerUpSpawner powerUpSpawner;
    
	void Start () 
	{
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		player = GameObject.Find("FingerTarget");
		powerUpSpawner = GameObject.Find("PowerUpSpawner").GetComponent<PowerUpSpawner>();
		//star = GameObject.Find("Shuriken");
		starsInGame = 1;
		stars = new List<Shuriken>();
		CreateStars();   
	}
    
	void Update () 
	{
		if (roundManager.currentRound == round.Playing)
		{
			starPosition = star.transform.position;
			PeakAndShoot();
		}
		//print(currentState);
	}

	public int SetDirection(){
		int ret  = Random.Range(0, 4);
		return ret;
	}

	public void CreateStars(){
		for (int i = 0; i < pooledAmount; i++)
        {
            Shuriken obj = Instantiate(starPrefab).GetComponent<Shuriken>();
            obj.gameObject.SetActive(false);
            stars.Add(obj);
			Initiation(stars[0]);
        }
	}

	public void EmptyStars(){
		for (int i = 0; i < pooledAmount; i++)
        {
            stars[i].gameObject.SetActive(false);
			stars[i].gameObject.transform.position = new Vector3(0, 7, 0);
        }
	}

	public void Initiation(){
		Shuriken inactiveStar = stars.Find((x) => !x.gameObject.activeInHierarchy);
        
		if (inactiveStar != null) Initiation(inactiveStar);
		else print("non ster");
	}

	public void Initiation (Shuriken nextStar) {
		star = nextStar;
		//star.currentState = Shuriken.starState.SetLocation;
		star.gameObject.SetActive(true);
        peakTimer = 0f;
        shootTimer = 0f;
	}

	void PeakAndShoot()
	{
		for (int i = 0; i < stars.Count; i++)
		{
			if (stars[i].gameObject.activeInHierarchy)
			{
				star = stars[i];            
			}
		}
	}

    
	public void AddToScore(int amount)
    {
        roundManager.score = roundManager.score + amount;
		AddStar();
    }

	public void AddStar(){
		starMultiplier++;
        //powerUpSpawner.coolDown--;
        if (starMultiplier == 10 * starsInGame)
        {
            Initiation();
            starMultiplier = 0;
            starsInGame++;
        }
	}
}
