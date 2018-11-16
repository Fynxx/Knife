using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public Text scoreLabelPlayLeft;
	public Text multiplierLabel;
    public Text scoreLabelPlayRight;
    public Text scoreLabelEnded;
	public Text scoreLabelCont;
	public Text scoreLabelScreenshot;
    public Text highScoreLabelFresh;
    //public Text highScoreLabelEnded;
	public Text highScoreLabelMenu;
    public Text lowerLabel;
	public Text pagesUnlockedFresh;
	public Text pagesUnlockedEnded;
	public Text objectiveBoxFresh;
	public Text objectiveBoxEnded;
	public Text watchAdPopupTitle;
	//public Text xp;

    public Image dangerNorth;
    public Image dangerEast;
    public Image dangerSouth;
    public Image dangerWest;

	public Image pauseTimerBar;
	public Text pauseTimerLabel;


    public Slider multiplierBar;
       
    public string[] lowerMessages;
    public CanvasGroup start;
    public CanvasGroup playing;
    public CanvasGroup ended;
    public CanvasGroup settings;
    public CanvasGroup pause;
    public CanvasGroup cont;
    public CanvasGroup screen;
	//public CanvasGroup hold;
	public CanvasGroup resetConfirmation;
	public CanvasGroup objectiveCompleteBanner;

    public Canvas menu;
    public Canvas game;
    public Canvas settingsC;

    public Toggle flipUi;

	public GameObject watchAdButton;

    public RoundManager roundManager;
    public StateManager stateManager;
    //public ShurikenSpawner spawner;
    //public PeakNShoot peakNShoot;
    public Multiplier multiplier;
	public Player player;
	public Panels panelManager;

    public GameObject fingerTarget;
    public GameObject raycastBlockerAd;
	public GameObject panels;

	public bool isBannerTurnedOn;
	public float bannerTimer;
	public const float bannerTimerReset = 3f;

    void Start()
    {
        scoreLabelPlayLeft = GameObject.Find("ScoreLabelPlayLeft").GetComponent<Text>();
		multiplierLabel = GameObject.Find("MultiplierLabel").GetComponent<Text>();
        //scoreLabelPlayRight = GameObject.Find("ScoreLabelPlayRight").GetComponent<Text>();
        scoreLabelEnded = GameObject.Find("ScoreLabelEnded").GetComponent<Text>();
		scoreLabelCont = GameObject.Find("ScoreLabelCont").GetComponent<Text>();
		scoreLabelScreenshot = GameObject.Find("ScoreLabelScreenshot").GetComponent<Text>();
        highScoreLabelFresh = GameObject.Find("HighScoreLabelFresh").GetComponent<Text>();
        //highScoreLabelEnded = GameObject.Find("HighScoreLabelEnded").GetComponent<Text>();
		highScoreLabelMenu = GameObject.Find("HighScoreLabelMenu").GetComponent<Text>();
		pagesUnlockedFresh = GameObject.Find("PagesUnlockedFresh").GetComponent<Text>();
		pagesUnlockedEnded = GameObject.Find("PagesUnlockedEnded").GetComponent<Text>();
		objectiveBoxFresh = GameObject.Find("ObjectiveLabelFresh").GetComponent<Text>();
		objectiveBoxEnded = GameObject.Find("ObjectiveLabelEnded").GetComponent<Text>();
		watchAdPopupTitle = GameObject.Find("WatchAdMessage").GetComponent<Text>();
		//lowerLabel = GameObject.Find("LowerLabel").GetComponent<Text>();
		//xp = GameObject.Find("TotalXP").GetComponent<Text>();

        //dangerNorth = GameObject.Find("DangerNorth").GetComponent<Image>();
        //dangerEast = GameObject.Find("DangerEast").GetComponent<Image>();
        //dangerSouth = GameObject.Find("DangerSouth").GetComponent<Image>();
        //dangerWest = GameObject.Find("DangerWest").GetComponent<Image>();

        start = GameObject.Find("RoundFresh").GetComponent<CanvasGroup>();
        playing = GameObject.Find("RoundPlaying").GetComponent<CanvasGroup>();
        ended = GameObject.Find("RoundEnded").GetComponent<CanvasGroup>();
		pause = GameObject.Find("RoundPause").GetComponent<CanvasGroup>();
		cont = GameObject.Find("RoundContinue").GetComponent<CanvasGroup>();
		screen = GameObject.Find("RoundScreenshot").GetComponent<CanvasGroup>();
		objectiveCompleteBanner = GameObject.Find("ObjectiveComplete").GetComponent<CanvasGroup>();
        //hold = GameObject.Find("RoundHold").GetComponent<CanvasGroup>();
        //settings = GameObject.Find("GameSettings").GetComponent<CanvasGroup>();

        menu = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        game = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        settingsC = GameObject.Find("SettingsCanvas").GetComponent<Canvas>();

        flipUi = GameObject.Find("FlipUiToggle").GetComponent<Toggle>();

		watchAdButton = GameObject.Find("WatchAdButton");

        raycastBlockerAd = GameObject.Find("RaycastBlockerAdSlider");

        roundManager = GetComponent<RoundManager>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
        //spawner = GameObject.Find("ShurikenSpawner").GetComponent<ShurikenSpawner>();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();

		fingerTarget = GameObject.Find("FingerTarget");

		panels = GameObject.Find("Panels");
		panelManager = GameObject.Find("PanelManager").GetComponent<Panels>();

        lowerMessages = new string[3];
        lowerMessages[0] = "Touch, Hold and Avoid the Shuriken!";
        lowerMessages[1] = " ";
        lowerMessages[2] = "Touch and Hold to play again!";
		bannerTimer = bannerTimerReset;
    }

    void Update()
    {
        CanvasSwitcher();
        CanvasGroupChanger();
		WatchAdButton();
		ObjectiveComplete();
        //DangerIndicator();
        //ScoreUiFlipper();
        //		testText.text = roundManager.currentDanger.ToString ();
        scoreLabelPlayLeft.text = roundManager.score.ToString();//C# tostring formatting
        //scoreLabelPlayRight.text = roundManager.score.ToString();//C# tostring formatting
        scoreLabelEnded.text = roundManager.score.ToString();//C# tostring formatting
		scoreLabelCont.text = roundManager.score.ToString();//C# tostring formatting
		scoreLabelScreenshot.text = roundManager.score.ToString();//C# tostring formatting
		highScoreLabelFresh.text = roundManager.highscore.ToString();
		multiplierLabel.text = ("x" + player.multiplier.ToString());
		//xp.text = roundManager.totalXP.ToString();
		//highScoreLabelEnded.text = roundManager.highscore.ToString();
		highScoreLabelMenu.text = roundManager.highscore.ToString();
		pauseTimerBar.fillAmount = roundManager.pauseTimer * .1f ;
		pauseTimerLabel.text = roundManager.pauseTimer.ToString("f");
		pagesUnlockedFresh.text = (panelManager.UnlockedPanels.Count+ "/" +(panelManager.AllPanels.Count-1) + " pages unlocked!");
		pagesUnlockedEnded.text = pagesUnlockedFresh.text;
		objectiveBoxEnded.text = ("Dodge " + (panelManager.nextObjective +1) + " weapons to unlock the new page!");
		objectiveBoxFresh.text = ("Dodge " + (panelManager.nextObjective + 1) + " weapons to unlock the new page!");
		watchAdPopupTitle.text = ("Only " + roundManager.remainderUnlock + " more waves!");
    }

    void CanvasGroupChanger()
    {
        switch (roundManager.currentState)
        {
			case State.Active:
				panels.gameObject.SetActive(true);
				fingerTarget.SetActive(true);
				playing.gameObject.SetActive(true);
				start.gameObject.SetActive(false);
				ended.gameObject.SetActive(false);
				pause.gameObject.SetActive(false);            
			    cont.gameObject.SetActive(false);
				if (roundManager.activeState == RoundManager.ActiveState.Holding){
					
				}
				if (roundManager.activeState == RoundManager.ActiveState.Playing)
                {
					playing.gameObject.SetActive(true);
                }
				break;
           
            case State.Inactive:
				panels.gameObject.SetActive(false);
				playing.gameObject.SetActive(false);
                if (roundManager.inactiveState == RoundManager.InactiveState.Start)
                {
					lowerLabel.text = lowerMessages[0];
					start.gameObject.SetActive(true);
                    ended.gameObject.SetActive(false);
                    pause.gameObject.SetActive(false);
                    cont.gameObject.SetActive(false);
					screen.gameObject.SetActive(false);
					fingerTarget.SetActive(false);
					isBannerTurnedOn = false;
					bannerTimer = bannerTimerReset;
                }
                if (roundManager.inactiveState == RoundManager.InactiveState.Dead){
					lowerLabel.text = lowerMessages[2];
					start.gameObject.SetActive(false);
                    ended.gameObject.SetActive(true);
                    pause.gameObject.SetActive(false);
                    cont.gameObject.SetActive(false);
					screen.gameObject.SetActive(false);
					fingerTarget.SetActive(false);
                }

                if (roundManager.inactiveState == RoundManager.InactiveState.Paused)
                {
					start.gameObject.SetActive(false);
                    ended.gameObject.SetActive(false);
					pause.gameObject.SetActive(true);
                    cont.gameObject.SetActive(false);
					screen.gameObject.SetActive(false);
					fingerTarget.SetActive(false);
                }

				if (roundManager.inactiveState == RoundManager.InactiveState.Continue)
                {
					start.gameObject.SetActive(false);
                    ended.gameObject.SetActive(false);
                    pause.gameObject.SetActive(false);
                    cont.gameObject.SetActive(true);
					screen.gameObject.SetActive(false);
					fingerTarget.SetActive(false);
                }
				if (roundManager.inactiveState == RoundManager.InactiveState.Screenshot)
                {
                    start.gameObject.SetActive(false);
                    ended.gameObject.SetActive(false);
                    pause.gameObject.SetActive(false);
					cont.gameObject.SetActive(false);
					screen.gameObject.SetActive(true);
					fingerTarget.SetActive(true);
                }
                //settings.alpha = 0;
                raycastBlockerAd.SetActive(false);
                break;
    //        case State.Settings:
    //            lowerLabel.text = null;

    //            fresh.alpha = 0;
    //            playing.alpha = 0;
    //            ended.alpha = 0;
				//pause.alpha = 0;
    //            //settings.alpha = 1;
    //            raycastBlockerAd.SetActive(true);
				//player.SetActive(false);
				//panel.SetActive(false);
                //break;
    //        case round.Hold:
    //            lowerLabel.text = null;

    //            fresh.alpha = 0;
    //            playing.alpha = 0;
    //            ended.alpha = 0;
    //            hold.alpha = 1;
    //            //settings.alpha = 1;
    //            raycastBlockerAd.SetActive(false);
				//player.SetActive(false);
				//panel.SetActive(false);
                //break;
            default:
                break;
        }
    }

    void CanvasSwitcher()
    {
        switch (stateManager.currentState)
        {
            case state.Game:
                menu.enabled = false;
                game.enabled = true;
                settingsC.enabled = false;
                break;
            case state.Menu:
                menu.enabled = true;
                game.enabled = false;
                settingsC.enabled = false;
                break;
            case state.Settings:
                menu.enabled = false;
                game.enabled = false;
                settingsC.enabled = true;
                break;
            default:
                break;
        }
    }

	void ObjectiveComplete(){
		if (roundManager.isObjectiveCompleted && !isBannerTurnedOn){
			objectiveCompleteBanner.alpha = 1;
			isBannerTurnedOn = true;
        }
		if (isBannerTurnedOn)
		{
			bannerTimer -= Time.deltaTime;
			if (bannerTimer < 0)
			{
				bannerTimer = 0;
				objectiveCompleteBanner.alpha = objectiveCompleteBanner.alpha - (Time.deltaTime);
			}
		}
	}

	void WatchAdButton(){
		if (roundManager.showAdButton){
			watchAdButton.SetActive(true);
		} else {
			watchAdButton.SetActive(false);
		}
	}

	public void CancelAdButton()
    {
		roundManager.adShown = false;
		watchAdButton.SetActive(false);
    }

	// void DangerIndicator()
	// {
	//     if (roundManager.currentRound == round.Playing)
	//     {
	//switch (peakNShoot.nextDirection)
	//        {
	//case PeakNShoot.direction.North:
	//                dangerNorth.enabled = true;
	//                dangerEast.enabled = false;
	//                dangerSouth.enabled = false;
	//                dangerWest.enabled = false;
	//                break;
	//case PeakNShoot.direction.East:
	//                dangerNorth.enabled = false;
	//                dangerEast.enabled = true;
	//                dangerSouth.enabled = false;
	//                dangerWest.enabled = false;
	//                break;
	//case PeakNShoot.direction.South:
	//                dangerNorth.enabled = false;
	//                dangerEast.enabled = false;
	//                dangerSouth.enabled = true;
	//                dangerWest.enabled = false;
	//                break;
	//case PeakNShoot.direction.West:
	//                dangerNorth.enabled = false;
	//                dangerEast.enabled = false;
	//                dangerSouth.enabled = false;
	//                dangerWest.enabled = true;
	//                break;
	//            default:
	//                break;
	//        }
	//    }
	//}

	public void ResetConfirmation()
	{
		resetConfirmation.alpha = 1;
		resetConfirmation.interactable = true;
		resetConfirmation.blocksRaycasts = true;
	}

	public void CancelConfirmation(){
		resetConfirmation.alpha = 0;
		resetConfirmation.interactable = false;
		resetConfirmation.blocksRaycasts = false;
	}
		

	public void ScoreUiFlipper()
    {
        bool righty;
        if (flipUi.isOn)
        {
            righty = true;
        }
        else 
        { 
            righty = false;
        }
        if (righty)
        {
            scoreLabelPlayLeft.enabled = true;
            scoreLabelPlayRight.enabled = false;
        }
        else
        {
            scoreLabelPlayLeft.enabled = false;
            scoreLabelPlayRight.enabled = true;
        }
    }
    public void CloseAd()
    {
        //roundManager.currentState = State.Killed;
    }
}