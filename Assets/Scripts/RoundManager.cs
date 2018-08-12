using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TapticPlugin;

public enum round {Fresh, Holding, Playing, Reset, Ended, Killed, Settings };

public class RoundManager : MonoBehaviour {

	public round currentRound;
    public StateManager stateManager;
	public Player player;
	public Multiplier multiplier;
	public WaveManager waveManager;
	//public enum danger {None, Knife, Shuriken, Grater};
	//public danger currentDanger;

	public float time;
	public float resetTimer;
	public float holdTimer;

	public int score;
	public int highscore;
    public int lives;
	public int adMultiplier;
	public int timesPlayed;
	//public int hitPoints;

	public Text scoreLabel;
	public bool isDead;
	public bool inSettings;
    public bool inGame;
	public bool heldForLongEnough;
	public bool fillHoldTimer;
    
	//public ShurikenSpawner dangerSpawner;
	//public PeakNShoot peakNShoot;

	//private float _nextDangerTimer;
	//private float _nextDangerShot;
	private PlayerData data;
	private Scene thisScene;

	void Start () {
        //scoreLabel = GameObject.Find ("ScoreLabel").GetComponent<Text> ();
		isDead = false;
		//currentDanger = danger.None;
        currentRound = round.Fresh;
		//dangerSpawner = GameObject.Find ("ShurikenSpawner").GetComponent<ShurikenSpawner> ();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		//_nextDangerShot = _nextDangerResetValue;
		Load ();
		adMultiplier = 5;
		thisScene = SceneManager.GetActiveScene();
	}
	
	void Update () {
		ChangeRound ();
        Rounds();
		//		DangerSwitcher ();
		//print(currentRound);
		//print();
		//print(currentRound);
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
								if (currentRound == round.Holding)
								{
									holdTimer -= (Time.deltaTime);
                                    if (holdTimer <= 0f)
                                    {
                                        heldForLongEnough = true;
                                        currentRound = round.Playing;
                                    }
								}

								if (isDead == false ) {
									if (heldForLongEnough == true)
									{
										currentRound = round.Playing;
									} else {
										currentRound = round.Holding;
									}
    							} else {
    								currentRound = round.Killed;
    							}
							break;
						case TouchPhase.Ended:
								if (currentRound == round.Playing)//|| currentRound == round.Holding
                                {
									if (score == 0 && !isDead)
									{
										currentRound = round.Holding;
										TapticManager.Notification(NotificationFeedback.Warning);
									}
									else
									{
										currentRound = round.Ended;
										if (!isDead)
										{
											TapticManager.Notification(NotificationFeedback.Error);
										}
                                    }
                                }
								if (currentRound == round.Holding){
									fillHoldTimer = true;
								}
							break;
						default:
							    currentRound = round.Fresh;
							break;
						}
                    } 
				}
			}
		}

		if (Input.touchCount > 1){
			currentRound = round.Reset;
		}
	}

	public void Rounds(){
		switch (currentRound)
		{
			case round.Fresh:
				score = 0;
				break;
			case round.Reset:
				if (score > 0) { 
    				AnalyticsEvent.Custom("new_round", new Dictionary<string, object>
    				{
    					{ "current_score", score },
    					{ "time_elapsed", Time.timeSinceLevelLoad },
    					{ "times_played", timesPlayed}
    				});
    		    }
                player.hitPoints = 1;            
                multiplier.countDown = 0;
                multiplier.coins = 0;
                adMultiplier--;
                resetTimer = 10f;
                holdTimer = 1f;
                heldForLongEnough = false;
                fillHoldTimer = false;            
                if (timesPlayed > 0)
                {
                    waveManager.ResetField();
                }
                timesPlayed++;
				score = 0;
				currentRound = round.Holding;
			break;
		case round.Holding:
				if (fillHoldTimer)
				{
					holdTimer += (Time.deltaTime * 2);
					if (holdTimer >= 1f)
					{
						holdTimer = 1;
					}
				}
			break;
		case round.Playing:
                time = 0;
			break;
		case round.Ended:
		case round.Killed:
    			if (score > highscore) {
    				highscore = score;
    				Save ();
    			}
				if (adMultiplier <= 0){
					//Advertisement.Show();
					adMultiplier = 5;
					currentRound = round.Fresh;
				}

				//resetTimer -= Time.deltaTime;
				//if (resetTimer <= 0){
				//	currentRound = round.Fresh;
				//	stateManager.currentState = state.Menu;
				//}
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

	private void OnApplicationQuit()
	{
		AnalyticsEvent.Custom("times_played", new Dictionary<string, object>
                {
                    { "times_played", timesPlayed }
                });
	}

	private void OnApplicationPause()
	{
		AnalyticsEvent.Custom("times_played", new Dictionary<string, object>
                {
                    { "times_played", timesPlayed }
                });
		timesPlayed = 0;
	}

}

[Serializable]
class PlayerData
{
	public int score;
}
