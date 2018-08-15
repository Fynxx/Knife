using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;

public class Shuriken : Weapon {

	public enum State { inactive, active };
	public State state;
	public GameObject player;
	float ran;

	void Start () {    
		player = GameObject.Find("FingerTarget");
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		ran = Random.Range(-.5f, .5f);
	}

	void Update()
	{
		Spin();
		speed = waveManager.speed + ran;
		if (roundManager.currentRound == round.Playing && gameObject.activeSelf == true)
		{
			transform.Translate(Vector3.down * (Time.deltaTime * speed), Space.World);
		}
		//transform.position = new Vector3(-3, 6, 0);
	}		

	void Spin(){
		if (roundManager.currentRound == round.Playing) {//currentState == starState.Shoot && 
			transform.Rotate (Vector3.back, Time.deltaTime * 1500);
		}
	}

	void OnBecameInvisible()
    {
		gameObject.SetActive(false);
		waveManager.starsOnField.Remove(this);
		//state = State.inactive;
    }   

	//void PeakAndShoot()
    //{
    //    switch (currentState)
    //    {
    //        case Shuriken.starState.Initiation:
    //            peakTimer = 0;
    //            shootTimer = 0;
				////TapticManager.Impact(ImpactFeedback.Heavy);
    //            currentState = Shuriken.starState.SetLocation;
    //            break;
    //        case Shuriken.starState.SetLocation:
    //            SetPosition();
    //            //currentState = Shuriken.starState.Wait;
    //            //nextState = Shuriken.starState.Peak;
				////TapticManager.Impact(ImpactFeedback.Light);
				//currentState = Shuriken.starState.Peak;
    //            breathingTime = peakTimer;
    //            break;
    //        case Shuriken.starState.Peak:
    //            Peak();
    //            if (starPosition == peakLocation)
    //            {
    //                //currentState = Shuriken.starState.Wait;
    //                //nextState = Shuriken.starState.Shoot;
				//	currentState = Shuriken.starState.Shoot;
    //                breathingTime = shootTimer;
    //            }
    //            break;
    //        case Shuriken.starState.Shoot:
    //            Shoot();
    //            break;
    //        case Shuriken.starState.Wait:
    //            Wait();
    //            break;
    //        case Shuriken.starState.Reset:
				////peakNShoot.AddToScore(1);
				////peakSpeed = peakSpeed + 0.05f;
				//shootSpeed = shootSpeed + 0.25f;
    //            currentState = Shuriken.starState.SetLocation;
    //            break;
    //        default:
    //            break;
    //    }
    //}

	//void SetPosition()
  //  {
		////int returnValue = peakNShoot.SetDirection();
		////nextDirection = (direction)returnValue;
  //      //nextDirection = (direction)Random.Range(0, 4);
		//float spawnMargin = Random.Range(-1f, 1f);
		//float randomSpawn = Random.Range(0f, 1f);
	 //   peakTime = 1f;
		//playerLocation = player.transform.position;

		//switch (nextDirection)
    //    {
    //        case direction.North:
				////spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomSpawn, 1.2f, 0.5f));
				//spawnLocation = new Vector3((player.transform.position.x + spawnMargin), 7, 0);
    //            peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y - peakOffset), spawnLocation.z);
				//endlocation = peakLocation.y;
    //            break;
    //        case direction.East:
				////spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(1.3f, randomSpawn, 0.5f));
				//spawnLocation = new Vector3(4.5f, (player.transform.position.y + spawnMargin), 0);
    //            peakLocation = new Vector3((spawnLocation.x - peakOffset), spawnLocation.y, spawnLocation.z);
				//endlocation = peakLocation.x;
    //            break;
    //        case direction.South:
				////spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(randomSpawn, -.2f, 0.5f));
				//spawnLocation = new Vector3((player.transform.position.x + spawnMargin), -7, 0);
    //            peakLocation = new Vector3(spawnLocation.x, (spawnLocation.y + peakOffset), spawnLocation.z);
				//endlocation = peakLocation.y;
    //            break;
    //        case direction.West:
				////spawnLocation = Camera.main.ViewportToWorldPoint(new Vector3(-.3f, randomSpawn, 0.5f));
				//spawnLocation = new Vector3(-4.5f, (player.transform.position.y + spawnMargin), 0);
    //            peakLocation = new Vector3((spawnLocation.x + peakOffset), spawnLocation.y, spawnLocation.z);
				//endlocation = peakLocation.x;
    //            break;
    //        default:
    //            break;
    //    }
    //    transform.position = spawnLocation;
    //}

	//void Peak()
  //  {
		//peakDirection = (peakLocation - spawnLocation).normalized;
		////shootDirection = (playerLocation - peakLocation).normalized;
		////peakSpeed = (peakSpeed / 2);

		////transform.DOKill(false);
		////transform.DOMove(peakLocation, peakTime);
		////transform.DORotate(new Vector3(0, 0, -3), peakTime);

		//transform.Translate(peakDirection * Time.deltaTime * peakSpeed, Space.World);
        
		////transform.position = spawnLocation + new Vector3(Mathf.Sin(Time.time), 0, 0);
        

		//peakTime -= Time.deltaTime;
		//if (peakTime <= 0){
		//	breathingTime = .5f;
		//	//currentState = starState.Wait;
		//	//nextState = starState.Shoot;
		//}
    //}

	//void Shoot()
  //  {
		//transform.Translate(peakDirection * (Time.deltaTime * shootSpeed), Space.World);
    //}

	//void Wait()
    //{
    //    if (nextState == Shuriken.starState.Peak)
    //    {
    //        breathingTime -= Time.deltaTime;
    //        if (breathingTime <= 0)
    //        {
    //            currentState = nextState;
    //        }
    //    }
        
    //    if (nextState == Shuriken.starState.Shoot)
    //    {
    //        breathingTime -= Time.deltaTime;
    //        if (breathingTime <= 0)
    //        {
    //            currentState = nextState;
    //        }
    //    }
    //}
}
