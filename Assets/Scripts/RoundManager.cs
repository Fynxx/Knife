using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TapticPlugin;

public enum round {Fresh, Playing, Reset, Ended, Killed, Settings, Hold};

public class RoundManager : MonoBehaviour {

	public round currentRound;
    public StateManager stateManager;
	public Player player;
	//public enum danger {None, Knife, Shuriken, Grater};
	//public danger currentDanger;

	public float time;

	public int score;
	public int highscore;
    public int lives;
	//public int hitPoints;

	public Text scoreLabel;
	public bool isDead;
	public bool inSettings;
    public bool inGame;
    public bool isPlaying;
    
	//public ShurikenSpawner dangerSpawner;
	public PeakNShoot peakNShoot;

	//private float _nextDangerTimer;
	//private float _nextDangerShot;
	private PlayerData data;

	void Start () {
        //scoreLabel = GameObject.Find ("ScoreLabel").GetComponent<Text> ();
		isDead = false;
		//currentDanger = danger.None;
        currentRound = round.Fresh;
		//dangerSpawner = GameObject.Find ("ShurikenSpawner").GetComponent<ShurikenSpawner> ();
		peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		//_nextDangerShot = _nextDangerResetValue;
		Load ();
        isPlaying = false;
	}
	
	void Update () {
		ChangeRound ();
        Rounds();
		//		DangerSwitcher ();
		//print(currentRound);
		//print(Input.GetTouch(0).pressure);
	}

	public void ChangeRound(){
		TouchPhase touch;
		Ray ray;
		RaycastHit hit;

		if (Input.touchCount > 0) 
        {
            if (stateManager.currentState == state.Game)
            {
    			ray = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
    			touch = Input.GetTouch (0).phase;
    			if (Physics.Raycast(ray, out hit))
                {
    				if (hit.transform.tag == "Background")
                    {                    
						switch (touch) {
						case TouchPhase.Began:
    						    TapticManager.Impact(ImpactFeedback.Light);
    							currentRound = round.Reset;
    							isDead = false;
							break;
						case TouchPhase.Moved:
						case TouchPhase.Stationary:
    							if (isDead == false) {
    								currentRound = round.Playing;
                                    isPlaying = true;
    							} else {
    								currentRound = round.Killed;
    							}
							break;
						case TouchPhase.Ended:
                                if (isPlaying == true)
                                {
									if (score < 1 && !isDead)
									{
										currentRound = round.Hold;
										TapticManager.Notification(NotificationFeedback.Warning);
									}
									else
									{
										currentRound = round.Ended;
										isPlaying = false;
										if (!isDead)
										{
											TapticManager.Notification(NotificationFeedback.Error);
										}
                                    }
                                }
							break;
						default:
							    currentRound = round.Fresh;
							break;
						}
                    } else {
                        if (Physics.Raycast(ray, out hit))
                        {
                            if (hit.transform.tag == "Background")
                            {
                                
                            }
                        }
                    }
				}
			}
		}

		if (Input.touchCount > 1){
			currentRound = round.Ended;
		}
	}

	public void Rounds(){ 
		switch (currentRound) {
		case round.Fresh:
			    score = 0;
			break;
		case round.Reset:
				//peakNShoot.Initiation();
				//dangerSpawner.weapon.transform.position = new Vector3(0, 7, 0);
				peakNShoot.EmptyStars();
    			score = 0;
				peakNShoot.starsInGame = 1;
				peakNShoot.starMultiplier = 0;
    			//_nextDangerTimer = 0;
    			//_nextDangerShot = _nextDangerResetValue;
				player.hitPoints = 1;
				peakNShoot.CreateStars();
    			//currentDanger = danger.None;
    			currentRound = round.Playing;
			break;
		case round.Playing:
                time = 0;
			break;
		case round.Ended:
		case round.Killed:
    //        time += Time.deltaTime;
    //        if (time < .01f){
    //            //Handheld.Vibrate();
				//TapticManager.Notification(NotificationFeedback.Error);
            //}

    			if (score > highscore) {
    				highscore = score;
    				Save ();
    			}
				Advertisement.Show();
			break;
		case round.Settings:
			break;
		default:
			break;
		}
	}

	public void Settings(){
		inSettings = !inSettings;

		if (inSettings) {
			currentRound = round.Settings;
        } else {
            currentRound = round.Fresh;
        }
	}

    public void StartGame()
    {
        currentRound = round.Playing;
    }
    public void EndGame()
    {
        currentRound = round.Fresh;
    }


	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		data = new PlayerData ();
        data.score = highscore;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close();

            highscore = data.score;           
		}
	}

	public void Reset(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);

			data.score = 0;
            highscore = 0;
			bf.Serialize (file, data);
			file.Close ();
		}
	}

		
}

[Serializable]
class PlayerData
{
	public int score;
}

//public void DangerSwitcher(){
//  if (currentRound == round.Playing) {
//      _nextDangerTimer = _nextDangerTimer;

//      switch (currentDanger) {
//      case danger.None:
//          if (_nextDangerTimer > _nextDangerShot) {
//              currentDanger = (danger)UnityEngine.Random.Range (0, 4);
//              _nextDangerShot -= 0.1f;
//              _nextDangerTimer = 0;
//          }

//          break;
//      case danger.Knife:
//          dangerKnife.KnifeRotation ();
//          if (_nextDangerTimer > _nextDangerShot) {
//              currentDanger = danger.Shuriken;
//              _nextDangerShot -= 0.1f;
//              _nextDangerTimer = 0;
//          }
//          break;
//      case danger.Shuriken:
//          if (_nextDangerTimer > _nextDangerShot) {
//              currentDanger = (danger)UnityEngine.Random.Range (1, 4);
//              _nextDangerShot -= 0.1f;
//              _nextDangerTimer = 0;
//          }
//          break;
//      case danger.Grater:
//          if (_nextDangerTimer > _nextDangerShot) {
//              currentDanger = (danger)UnityEngine.Random.Range (1, 4);
//              _nextDangerShot -= 0.1f;
//              _nextDangerTimer = 0;
//          }
//          break;
//      default:
//          break;
//      }
//  }
//}
