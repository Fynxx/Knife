﻿using System.Collections;
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

public enum State { Reset, Start, Playing, Paused, Holding, Dead, Continue, Screenshot };

public class RoundManager : MonoBehaviour {

	public State currentState;

    //public enum ActiveState { Reset, Playing, Holding, Dieing, Continue, Screenshot };
    //public ActiveState activeState;

    //public enum InactiveState { Start, Paused, Dead, Ended, Continue, Screenshot };
    //public InactiveState inactiveState;

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
	public const int adMultiplierReset = 6;
	public int adMultiplier;
	public int timesPlayed;
	//public int hitPoints;
	public int multiplierWhenDied;
	public int panelsUnlocked;
	public int remainderUnlock;

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

	public bool isObjectiveCompleted;

	public bool initialReset;

	public bool gameIsSaved;
    
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
        currentState = State.Start;
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
		adMultiplier = adMultiplierReset;
		//InitialReset();
		//thisScene = SceneManager.GetActiveScene();
	}
	
	void Update () {
		ChangeState ();
        States();
		//ShowAdButton();
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
                                    if (currentState == State.Paused || currentState == State.Continue)
    								{
    									//currentState = State.Active;
    									ResetGame();
    								}
    								else
    								{
                                        currentState = State.Start;
                                        currentState = State.Reset; // ResetGame and FullReset
    								}
    						    }
								break;
							case TouchPhase.Moved:
							case TouchPhase.Stationary:
								break;
							case TouchPhase.Ended:
                                currentState = State.Dead;                        
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
            currentState = State.Reset;
		}
	}

	public void States()
	{
        switch(currentState){
            case State.Reset:
                FullReset();
                ResetGame();
                break;
            case State.Holding:
                Holding();
                break;
            case State.Paused:
                pauseTimer -= Time.deltaTime;

                if (pauseTimer < 0)
                {
                    currentState = State.Dead;
                }
                break;
            case State.Dead:
                //KillPlayer(); 
                if (score > highscore)
                {
                    highscore = score;
                    if (!gameIsSaved)
                    {
                        Save();
                        gameIsSaved = true;
                    }
                    if (!reviewShown)
                    {
                        panelManager.CheckUnlockedPanels();
                        RateBox.Instance.Show();
                        print("review shown");
                        reviewShown = true;
                    }
                    screenshot.highscore = true;
                }
                else
                {
                    panelManager.CheckUnlockedPanels();
                    screenshot.highscore = false;
                    if (!gameIsSaved)
                    {
                        Save();
                        adMultiplier--;
                        gameIsSaved = true;
                    }
                    if (adMultiplier <= 0)
                    {
                        Advertisement.Show();
                        adMultiplier = adMultiplierReset;
                    }
                }
                break;
        }
		//if (currentState == State.Active)
		//{
		//	switch (activeState)
		//	{
		//		case ActiveState.Reset:
		//			FullReset();
  //                  ResetGame();
		//			//activeState = ;
		//			break;
		//		case ActiveState.Holding:
		//			Holding();
		//			break;
		//		case ActiveState.Playing:
		//			//playing shit            
		//			break;
		//		default:
		//			activeState = ActiveState.Reset;
		//			break;
		//	}
		//}
		//else if (currentState == State.Inactive)
		//{      
			//if (activeState == ActiveState.Playing){
			//	inactiveState = InactiveState.Paused;
			//} else if (activeState == ActiveState.Dieing){
			//	inactiveState = InactiveState.Dead;
			//} else if (activeState == ActiveState.Continue){
			//	inactiveState = InactiveState.Continue;
			//}
			//switch (inactiveState)
			//{
				//case InactiveState.Start:
				//	break;
				//case InactiveState.Paused:
    //                pauseTimer -= Time.deltaTime;

    //                if (pauseTimer < 0)
    //                {
				//		inactiveState = InactiveState.Dead;
    //                }
    //                break;
				//case InactiveState.Ended:
				//	break;
				//case InactiveState.Screenshot:
					////screenshot.TakeScreenshot();
					////active- and inactive state are set back in screenshot.cs

     //               //activeState = ActiveState.Dieing;
     //               //inactiveState = InactiveState.Dead;
     //               break;
     //           case InactiveState.Dead:
					////KillPlayer(); 
					//if (score > highscore)
					//{
					//	highscore = score;
					//	if (!gameIsSaved){
					//		Save();
					//		gameIsSaved = true;
     //                   }
					//	if (!reviewShown)
					//	{
					//		panelManager.CheckUnlockedPanels();
					//		RateBox.Instance.Show();
					//		print("review shown");
					//		reviewShown = true;
					//	}
					//	screenshot.highscore = true;
					//}
					//else
					//{
					//	panelManager.CheckUnlockedPanels();
					//	screenshot.highscore = false;
					//	if (!gameIsSaved)
     //                   {
     //                       Save();
					//		adMultiplier--;
     //                       gameIsSaved = true;
     //                   }
					//	if (adMultiplier <= 0)
     //                   {
     //                       Advertisement.Show();
     //                       adMultiplier = adMultiplierReset;
     //                   }
					//} 
            //        break;
            //    default:
            //        break;
            //}
        //}      
    }

    public void KillPlayer(){
		remainderUnlock = ((panelManager.currentObjective - score) +1);
		panelManager.UnlockNewPanel();
        multiplierWhenDied = player.multiplier;
        currentState = State.Dead;
        //activeState = ActiveState.Dieing;
        //inactiveState = InactiveState.Dead;
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

		gameIsSaved = false;

		panelManager.currentObjective = panelManager.nextObjective;
		isObjectiveCompleted = false;
        currentState = State.Holding;
        waveManager.currentStep = WaveManager.step.ChooseWeapon;
    }

    public void FullReset(){
        score = 0;
		pauseTimer = 10f;
		adShown = false;
		waveManager.ResetSpeed();

	}

	public void Holding(){
		holdTimer -= (Time.deltaTime);
        if (holdTimer <= 0f)
        {
            //heldForLongEnough = true;
            currentState = State.Playing;
        }
	}

	public void ResetButton(){
		ResetGame();
		FullReset();
		//currentState = State.Inactive;
        currentState = State.Start;
	}

	public void ShowAdButton(){
		if (!isObjectiveCompleted && !adShown && remainderUnlock < 20){
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
