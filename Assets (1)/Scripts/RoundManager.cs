using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum round {Fresh, Playing, Reset, Ended, Killed, Settings};

public class RoundManager : MonoBehaviour {

	public round currentRound;
	public enum danger {None, Knife, Shuriken, Grater};
	public danger currentDanger;

	public float time;
	public float counter;
	public float score;

	public Text timerLabel;
	public bool isDead;
	public bool inSettings;

	public Knife dangerKnife;
	public Spawner dangerSpawner;

	private float _nextDangerTimer;
	private float _nextDangerShot;
	private float _nextDangerResetValue;
	private PlayerData data;

	// Use this for initialization
	void Start () {
		timerLabel = GameObject.Find ("TimerText").GetComponent<Text> ();
		isDead = false;
		currentDanger = danger.None;
		dangerKnife = GameObject.Find ("Knife").GetComponent<Knife> ();
		dangerSpawner = GameObject.Find ("ShurikenSpawner").GetComponent<Spawner> ();
		_nextDangerResetValue = 2.0f;
		_nextDangerShot = _nextDangerResetValue;
		Load ();
	}
	
	// Update is called once per frame
	void Update () {
		ChangeRound ();
		Rounds ();
        //		DangerSwitcher ();
	}

	public void ChangeRound(){
		TouchPhase touch;
		Ray ray;
		RaycastHit hit;

		if (Input.touchCount > 0) {
			
			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
			touch = Input.GetTouch (0).phase;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.tag == "Background"){
					if (inSettings == false) {
						switch (touch) {
						case TouchPhase.Began:
							currentRound = round.Reset;
							isDead = false;
							break;
						case TouchPhase.Moved:
						case TouchPhase.Stationary:
							if (isDead == false) {
								currentRound = round.Playing;
							} else {
								currentRound = round.Killed;
							}
							break;
						case TouchPhase.Ended:
							currentRound = round.Ended;
							break;
						default:
							currentRound = round.Fresh;
							break;
						}
					}
				}
			}
		}
	}

	public void Rounds(){ 
		switch (currentRound) {
		case round.Fresh:
			counter = 0;
			break;
		case round.Reset:
			counter = 0;
			_nextDangerTimer = 0;
			_nextDangerShot = _nextDangerResetValue;
			currentDanger = danger.None;
			currentRound = round.Playing;
			dangerSpawner.weapon.transform.position = new Vector3 (0, 7, 0);
			break;
		case round.Playing:
			time = Time.deltaTime;
			counter = counter + time;
			Mathf.RoundToInt (counter);
			break;
		case round.Ended:
		case round.Killed:
			Handheld.Vibrate ();
			if (counter > score) {
				score = counter;
				Save ();
			}
			break;
		case round.Settings:
			break;
		default:
			break;
		}
	}

	public void DangerSwitcher(){
		if (currentRound == round.Playing) {
			_nextDangerTimer = _nextDangerTimer + time;
		
			switch (currentDanger) {
			case danger.None:
				if (_nextDangerTimer > _nextDangerShot) {
					currentDanger = (danger)UnityEngine.Random.Range (0, 4);
					_nextDangerShot -= 0.1f;
					_nextDangerTimer = 0;
				}

				break;
			case danger.Knife:
				dangerKnife.KnifeRotation ();
				if (_nextDangerTimer > _nextDangerShot) {
					currentDanger = danger.Shuriken;
					_nextDangerShot -= 0.1f;
					_nextDangerTimer = 0;
				}
				break;
			case danger.Shuriken:
				if (_nextDangerTimer > _nextDangerShot) {
					currentDanger = (danger)UnityEngine.Random.Range (1, 4);
					_nextDangerShot -= 0.1f;
					_nextDangerTimer = 0;
				}
				break;
			case danger.Grater:
				if (_nextDangerTimer > _nextDangerShot) {
					currentDanger = (danger)UnityEngine.Random.Range (1, 4);
					_nextDangerShot -= 0.1f;
					_nextDangerTimer = 0;
				}
				break;
			default:
				break;
			}
		}
	}

	public void Settings(){
		inSettings = !inSettings;

		if (inSettings == true) {
			currentRound = round.Settings;
		}
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		data = new PlayerData ();
		data.score = score;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close();

			score = data.score;
		}
	}

	public void Reset(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);

			data.score = 0;
			score = 0;
			bf.Serialize (file, data);
			file.Close ();
		}
	}
		
}

[Serializable]
class PlayerData
{
	public float score;
}
