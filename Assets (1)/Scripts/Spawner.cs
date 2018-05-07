using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public enum Direction {SetNorth, SetEast, SetWest, SetSouth, North, East, West,  South, Reset};
	public Direction spawnDirection;
	public Transform weapon;
	public float time;
	public float nextSpawn;
	public float speed;
	public float rmCounter;
	public RoundManager roundManager;

	private bool locationSet;
	private float randomX; 
	private float randomY;
    private float maxXpos;
    private float maxYpos;

	// Use this for initialization
	void Start () {
		locationSet = false;
        maxXpos = 4.5f;
        maxYpos = 7f;
	}
	
	// Update is called once per frame
	void Update () {
		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
		time = Time.deltaTime;
		randomX = Random.Range(-1.5f, 1.5f);
		randomY = Random.Range(-3.5f, 3.5f);
		NextSpawnDirection ();

		rmCounter = roundManager.counter;

		if (roundManager.currentRound == round.Ended || roundManager.currentRound == round.Killed) {
			speed = 0;
		}
	}

	void NextSpawnDirection(){
		if (roundManager.currentRound == round.Playing) {
			switch (spawnDirection) {
			case Direction.SetNorth:
				weapon.transform.position = new Vector3 (randomX, 7, 0);
				spawnDirection = Direction.North;
				break;
			case Direction.SetEast:
				weapon.transform.position = new Vector3 (4.5f, randomY, 0);
                spawnDirection = Direction.East;
				break;
			case Direction.SetWest:
				spawnDirection = Direction.West;
				weapon.transform.position = new Vector3 (-4.5f, randomY, 0);
				break;
			case Direction.SetSouth:
				spawnDirection = Direction.South;
				weapon.transform.position = new Vector3 (randomX, -7, 0);
				break;

			case Direction.North:
				Move (Vector3.down);
                    if (weapon.transform.position.y < -maxYpos) {
					ChooseRandomDirection ();
				}
				break;
			case Direction.East:
				Move (Vector3.left);
                    if (weapon.transform.position.x < -maxXpos) {
					ChooseRandomDirection ();
				}
				break;
			case Direction.West:
				Move (Vector3.right);
                    if (weapon.transform.position.x > maxXpos) {
					ChooseRandomDirection ();
				}
				break;	
			case Direction.South:
				Move (Vector3.up);
                    if (weapon.transform.position.y > maxYpos) {
					ChooseRandomDirection ();
				}
				break;
			default:
				break;
			}
		}
//		if (roundManager.currentRound == round.Ended) {
//			weapon.transform.position = new Vector3 (randomX, 7, 0);
//		}
	}

	void ChooseRandomDirection(){
		spawnDirection = (Direction)Random.Range (0, 4);
	}

	void Move (Vector3 direction){
		speed = 5 + (rmCounter / 10);
		weapon.transform.Translate (direction * (Time.deltaTime * speed), Space.World);
	}
}
