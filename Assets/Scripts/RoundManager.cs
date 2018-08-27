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
using UnityEngine.EventSystems;

public enum State { Active, Inactive };

public class RoundManager : MonoBehaviour {

	public State currentState;

    public enum ActiveState { Reset, Playing, Holding, Dieing, Continue };
    public ActiveState activeState;

    public enum InactiveState { Start, Paused, Dead, Ended, Continue };
    public InactiveState inactiveState;

    public StateManager stateManager;
	public Player player;
	public Multiplier multiplier;
	public WaveManager waveManager;
	public AudioManager audioManager;
	//public enum danger {None, Knife, Shuriken, Grater};
	//public danger currentDanger;

	//public float time;
	public float resetTimer;
	public float holdTimer;
	public float pauseTimer;

	public int score;
	public int highscore;
    public int lives;
	public int adMultiplier;
	public int timesPlayed;
	//public int hitPoints;
	public int multiplierWhenDied;

	public Text scoreLabel;
	public bool isDead;
	public bool inSettings;
    public bool inGame;
	public bool heldForLongEnough;
	public bool fillHoldTimer;

	public bool showAdButton;
	public bool adShown;
    
	//public ShurikenSpawner dangerSpawner;
	//public PeakNShoot peakNShoot;

	//private float _nextDangerTimer;
	//private float _nextDangerShot;
	private PlayerData data;
	//private Scene thisScene;

	void Start () {
        //scoreLabel = GameObject.Find ("ScoreLabel").GetComponent<Text> ();
		isDead = false;
		//currentDanger = danger.None;
		currentState = State.Inactive;
		//dangerSpawner = GameObject.Find ("ShurikenSpawner").GetComponent<ShurikenSpawner> ();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		//_nextDangerShot = _nextDangerResetValue;
		Load ();
		adMultiplier = 5;
		//thisScene = SceneManager.GetActiveScene();
	}
	
	void Update () {
		ChangeState ();
        States();
		WatchAd();
		//		DangerSwitcher ();
	}

	public void ChangeState(){
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
						switch (touch)
						{
							case TouchPhase.Began:
								if (!IsPointerOverUIObject())
								{
    								TapticManager.Impact(ImpactFeedback.Light);
    								audioManager.ButtonPressAudio();
									if (inactiveState == InactiveState.Paused || inactiveState == InactiveState.Continue)
    								{
    									currentState = State.Active;
    									ResetGame();
    								}
    								else
    								{
    									currentState = State.Active;
    									activeState = ActiveState.Reset; // ResetGame and FullReset
    								}
    						    }
								break;
							case TouchPhase.Moved:
							case TouchPhase.Stationary:
								break;
							case TouchPhase.Ended:
								currentState = State.Inactive;
								break;
							default:
								//currentState = State.Fresh;
								break;
							}
					}
				}
			}
		}

		if (Input.touchCount > 1){
			activeState = ActiveState.Reset;
		}
	}

	public void States()
	{
		if (currentState == State.Active)
		{
			switch (activeState)
			{
				case ActiveState.Reset:
					FullReset();
                    ResetGame();
					//activeState = ;
					break;
				case ActiveState.Holding:
					Holding(ActiveState.Playing);
					break;
				case ActiveState.Playing:
					//playing shit            
					break;
				default:
					activeState = ActiveState.Reset;
					break;
			}
		}
		else if (currentState == State.Inactive)
		{      
			if (activeState == ActiveState.Playing){
				inactiveState = InactiveState.Paused;
			} else if (activeState == ActiveState.Dieing){
				inactiveState = InactiveState.Dead;
			} else if (activeState == ActiveState.Continue){
				inactiveState = InactiveState.Continue;
			}
			switch (inactiveState)
			{
				case InactiveState.Start:
					break;
				case InactiveState.Paused:
                    pauseTimer -= Time.deltaTime;

                    if (pauseTimer < 0)
                    {
						inactiveState = InactiveState.Dead;
                    }
                    break;
				case InactiveState.Ended:
					break;
				case InactiveState.Dead:
					KillPlayer();
					if (score > highscore)
                    {
                        highscore = score;
                        Save();
                    }
					break;
				default:
					break;
			}
		}      
	}

	public void KillPlayer(){
        multiplierWhenDied = player.multiplier;
        currentState = State.Inactive;
        activeState = ActiveState.Dieing;
        inactiveState = InactiveState.Dead;
	}

	public void ResetGame(){
		AnalyticsEvent.Custom("new_round", new Dictionary<string, object>
        {
            { "current_score", score },
            { "time_elapsed", Time.timeSinceLevelLoad },
            { "times_played", timesPlayed},
            { "multiplier_when_died", multiplierWhenDied}
        });
        player.hitPoints = 1;
        //multiplier.countDown = 0;
        //multiplier.coins = 0;
        //adMultiplier--;
        //resetTimer = 10f;
        holdTimer = 1f;
        heldForLongEnough = false;
        fillHoldTimer = false;
		if (timesPlayed > 0)
        {
            waveManager.ResetField();
        }
        timesPlayed++;
		activeState = ActiveState.Holding;
        waveManager.currentStep = WaveManager.step.ChooseWeapon;
    }

    public void FullReset(){
        score = 0;
		pauseTimer = 10f;
		adShown = false;
		waveManager.ResetSpeed();
	}

	public void Holding(ActiveState nextState){
		holdTimer -= (Time.deltaTime);
        if (holdTimer <= 0f)
        {
            //heldForLongEnough = true;
			activeState = nextState;
        }
	}

	public void ResetButton(){
		ResetGame();
		FullReset();
		currentState = State.Inactive;
		inactiveState = InactiveState.Start;
	}

	public void WatchAd(){
		if (score > 10 && !adShown){
			showAdButton = true;
		} else {
			showAdButton = false;
		} 
	}

	private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    

	//public void Settings(){
	//	inSettings = !inSettings;

	//	if (inSettings) {
	//		currentState = State.Settings;
 //       } else {
 //           currentState = State.Fresh;
 //       }
	//}

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
			data.xp = 0;
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
	public int xp;
}
