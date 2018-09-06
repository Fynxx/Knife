using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TapticPlugin;
using PaperPlaneTools;

public enum State { Active, Inactive };

public class RoundManager : MonoBehaviour {

	public State currentState;

    public enum ActiveState { Reset, Playing, Holding, Dieing, Continue, Screenshot };
    public ActiveState activeState;

    public enum InactiveState { Start, Paused, Dead, Ended, Continue, Screenshot };
    public InactiveState inactiveState;

    public StateManager stateManager;
	public Player player;
	public Multiplier multiplier;
	public WaveManager waveManager;
	public AudioManager audioManager;
	public Screenshot screenshot;
	public Panels panelManager;
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
	public int panelsUnlocked;

	public Text scoreLabel;
	public bool isDead;
	public bool inSettings;
    public bool inGame;
	public bool heldForLongEnough;
	public bool fillHoldTimer;

	public bool showAdButton;
	public bool adShown;

	public bool adWatched;
	public bool shared;
	bool reviewShown;

	public bool initialReset;
    
	//public ShurikenSpawner dangerSpawner;
	//public PeakNShoot peakNShoot;

	//private float _nextDangerTimer;
	//private float _nextDangerShot;
	private PlayerData data;
	//private Scene thisScene;

	void Start () {
        //scoreLabel = GameObject.Find ("ScoreLabel").GetComponent<Text> ();
		isDead = false;
		reviewShown = false;
		//currentDanger = danger.None;
		currentState = State.Inactive;
		//dangerSpawner = GameObject.Find ("ShurikenSpawner").GetComponent<ShurikenSpawner> ();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		screenshot = GetComponent<Screenshot>();
		panelManager = GameObject.Find("PanelManager").GetComponent<Panels>();
		//_nextDangerShot = _nextDangerResetValue;
        Load ();
		panelManager.highscore = highscore;
		panelManager.CheckUnlockedPanels();
		adMultiplier = 5;
		//InitialReset();
		//thisScene = SceneManager.GetActiveScene();
	}
	
	void Update () {
		ChangeState ();
        States();
		ShowAdButton();
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
				case InactiveState.Screenshot:
					//screenshot.TakeScreenshot();
					//active- and inactive state are set back in screenshot.cs

                    //activeState = ActiveState.Dieing;
                    //inactiveState = InactiveState.Dead;
                    break;
                case InactiveState.Dead:
					//KillPlayer(); 
                    if (score > highscore)
                    {
                        highscore = score;
                        Save();
						if (!reviewShown)
						{
							panelManager.CheckUnlockedPanels();
                            RateBox.Instance.Show();
                            print("review shown");
                            reviewShown = true;
                        }
                        screenshot.highscore = true;
                    } else {
						panelManager.CheckUnlockedPanels();
                        screenshot.highscore = false;
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
		AnalyticsEvent.Custom("ad", new Dictionary<string, object>
        {
			{ "ad_watched", adWatched }
        });
        adWatched = false;
		AnalyticsEvent.Custom("share", new Dictionary<string, object>
        {
            { "shared", shared }
        });
        shared = false;
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
		reviewShown = false;
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

	public void ShowAdButton(){
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

	//void InitialReset(){
	//	initialReset = data.initialReset;

 //       if (!initialReset){
 //           Reset();

	//		BinaryFormatter bf = new BinaryFormatter();
 //           FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

 //           data = new PlayerData();
	//		data.initialReset = true;
	//		print("Initial Reset successfull.");
 //           bf.Serialize(file, data);
 //           file.Close();
	//	}
	//}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		data = new PlayerData ();
        data.score = highscore;
		data.panelsUnlocked = panelsUnlocked;

		bf.Serialize (file, data);
		file.Close ();
		print("Game Saved.");
	}

	public void Load(){
		if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			highscore = data.score;
			panelsUnlocked = data.panelsUnlocked;
			print("Game Loaded.");
        }
    }

    public void Reset(){
		BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        data = new PlayerData();
        data.score = 0;
		highscore = 0;
		data.xp = 0;
		panelsUnlocked = 0;
		data.panelsUnlocked = 0;

        bf.Serialize(file, data);
        file.Close();
        print("Game Reset.");
		SceneManager.LoadScene(0, LoadSceneMode.Single);

	}   

	private void OnApplicationQuit()
	{
		AnalyticsEvent.Custom("close_game", new Dictionary<string, object>
                {
                    { "times_played", timesPlayed },
			        { "highscore", highscore }
                });
	}

	private void OnApplicationPause()
	{
		AnalyticsEvent.Custom("close_game", new Dictionary<string, object>
                {
                    { "times_played", timesPlayed },
                    { "highscore", highscore }
                });
		timesPlayed = 0;
	}   

	public void AdWatched(){
		adWatched = true;
	}
   
	public void Shared(){
		shared = true;
	}

}

[Serializable]
class PlayerData
{
	public int score;
	public int xp;
	public int panelsUnlocked;
	public bool initialReset;   
}
