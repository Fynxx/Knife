using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public RoundManager roundManager;

	public enum state { SetLocation, Idle, Spawn, Wait };
	public state currentState;

	public Collectable item;
	public Vector3 spawnLocation;
	public float coolDown;

	public bool isAlive;

	protected float randomX;
	protected float randomY;
	protected float oneTenthHeight;
	protected float oneSixthWidth;
	protected float coolDownMin;
	protected float coolDownMax;   
    
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();

		oneTenthHeight = Screen.height / 10;
        oneSixthWidth = Screen.width / 6;
        
		coolDownMin = 1f;
		coolDownMax = 2f;      

		currentState = state.SetLocation;
	}

	//void Update () {
	//	if (roundManager.currentRound == round.Playing)
	//	{
	//		switch (currentState)
	//		{
	//			case state.SetLocation:
	//				randomX = Random.Range(1, 5);
	//				randomY = Random.Range(1, 9);
	//				spawnLocation = new Vector3(Screen.width - oneSixthWidth * randomX, Screen.height - oneTenthHeight * randomY, 10);
	//				spawnLocation = Camera.main.ScreenToWorldPoint(spawnLocation);
	//				coolDown = Random.Range(coolDownMin, coolDownMax);

	//				item.gameObject.SetActive(false);

	//				currentState = state.Idle;
	//				break;
	//			case state.Idle:
	//				coolDown -= Time.deltaTime;
	//				if (coolDown <= 0){
	//					currentState = state.Spawn;
	//				}
	//				break;
	//			case state.Spawn:
	//				item.gameObject.SetActive(true);
	//				isAlive = true;
	//				item.transform.position = spawnLocation;
	//				currentState = state.Wait;
	//				break;
	//			case state.Wait:
	//				if (isAlive == false){
	//					currentState = state.SetLocation;
	//				}
	//				break;
	//			default:
	//				break;
	//		}
	//	}
	//}
}
