using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : Collectable
{

	//public Spawner spawner;
	public RoundManager roundManager;
	public Multiplier multiplier;
	//public PeakNShoot peakNShoot;

	public enum state { inactive, active };
	public state currentState;
       
	public int point;

    public float lifeTime;
	public float speed;

	public Image coinLifeBar;

	protected GameObject coin;

	void Start()
	{
		coin = this.gameObject;
		//spawner = GameObject.Find("CoinSpawner").GetComponent<Spawner>();
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();
		coinLifeBar = GameObject.Find("CoinLifeBar").GetComponent<Image>();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		speed = 6f;
	}
    
	void OnEnable()
	{
		lifeTime = 1;
	}

	void Update()
	{
		coinLifeBar.fillAmount = lifeTime;
		//if (roundManager.currentState == State.Active && currentState == state.active) 
		//{
		//	transform.Translate(Vector3.down * (Time.deltaTime * speed), Space.World);
		//}

		//switch (roundManager.currentRound)      
		//{
		//	case round.Playing:
		//		if (lifeTime > 0)
  //              {
  //                  lifeTime -= (Time.deltaTime * .66f);
  //              }
  //              else if (lifeTime < 0)
  //              {
  //   //               spawner.isAlive = false;
		//			//spawner.isAllowedToSpawn = true;
  //                  Die(coin);
		//			multiplier.AddMultiplier(-.125f);
  //              }
		//		break;
		//	case round.Ended:
		//	case round.Killed:
		//		//spawner.isAlive = false;
		//		//spawner.isAllowedToSpawn = true;
		//		Die(coin);
		//		break;
		//	default:
		//		break;
		//}

		//if (roundManager.currentRound == rStateEInactive{
		//	//spawner.isAllowedToSpawn = true;
		//	Die(coin);
		//	currentState = state.inactive;
		//}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			//spawner.isAlive = false;
			//spawner.isAllowedToSpawn = true;
			//multiplier.AddMultiplier(.125f);
			//multiplier.AddCoins(1);
			Die(coin);  
			//peakNShoot.AddToScore(1);
		}
	}
}
