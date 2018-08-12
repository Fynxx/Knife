using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public Text scoreLabelPlayLeft;
    public Text scoreLabelPlayRight;
    public Text scoreLabelEnded;
    public Text highScoreLabelFresh;
    //public Text highScoreLabelEnded;
	public Text highScoreLabelMenu;
    public Text lowerLabel;

    public Image dangerNorth;
    public Image dangerEast;
    public Image dangerSouth;
    public Image dangerWest;

	public Slider multiplierBar;
       
    public string[] lowerMessages;
    public CanvasGroup fresh;
    public CanvasGroup playing;
    public CanvasGroup ended;
    public CanvasGroup settings;
    //public CanvasGroup hold;

    public Canvas menu;
    public Canvas game;
    public Canvas settingsC;

    public Toggle flipUi;

    public RoundManager roundManager;
    public StateManager stateManager;
    //public ShurikenSpawner spawner;
    //public PeakNShoot peakNShoot;
    public Multiplier multiplier;

    public GameObject player;
    public GameObject raycastBlockerAd;
	public GameObject panel;

    void Start()
    {
        scoreLabelPlayLeft = GameObject.Find("ScoreLabelPlayLeft").GetComponent<Text>();
        //scoreLabelPlayRight = GameObject.Find("ScoreLabelPlayRight").GetComponent<Text>();
        scoreLabelEnded = GameObject.Find("ScoreLabelEnded").GetComponent<Text>();
        highScoreLabelFresh = GameObject.Find("HighScoreLabelFresh").GetComponent<Text>();
        //highScoreLabelEnded = GameObject.Find("HighScoreLabelEnded").GetComponent<Text>();
		highScoreLabelMenu = GameObject.Find("HighScoreLabelMenu").GetComponent<Text>();
		//lowerLabel = GameObject.Find("LowerLabel").GetComponent<Text>();

        //dangerNorth = GameObject.Find("DangerNorth").GetComponent<Image>();
        //dangerEast = GameObject.Find("DangerEast").GetComponent<Image>();
        //dangerSouth = GameObject.Find("DangerSouth").GetComponent<Image>();
        //dangerWest = GameObject.Find("DangerWest").GetComponent<Image>();

        fresh = GameObject.Find("RoundFresh").GetComponent<CanvasGroup>();
        playing = GameObject.Find("RoundPlaying").GetComponent<CanvasGroup>();
        ended = GameObject.Find("RoundEnded").GetComponent<CanvasGroup>();
        //hold = GameObject.Find("RoundHold").GetComponent<CanvasGroup>();
        //settings = GameObject.Find("GameSettings").GetComponent<CanvasGroup>();

        menu = GameObject.Find("MenuCanvas").GetComponent<Canvas>();
        game = GameObject.Find("GameCanvas").GetComponent<Canvas>();
        settingsC = GameObject.Find("SettingsCanvas").GetComponent<Canvas>();

        flipUi = GameObject.Find("FlipUiToggle").GetComponent<Toggle>();

        raycastBlockerAd = GameObject.Find("RaycastBlockerAdSlider");

        roundManager = GetComponent<RoundManager>();
        stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        //spawner = GameObject.Find("ShurikenSpawner").GetComponent<ShurikenSpawner>();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
		multiplier = GameObject.Find("FingerTarget").GetComponent<Multiplier>();

        player = GameObject.Find("FingerTarget");

		panel = GameObject.Find("Panel");

        lowerMessages = new string[3];
        lowerMessages[0] = "Touch, Hold and Avoid the Shuriken!";
        lowerMessages[1] = " ";
        lowerMessages[2] = "Touch and Hold to play again!";
    }

    void Update()
    {
        CanvasSwitcher();
        CanvasGroupChanger();
        //DangerIndicator();
        //ScoreUiFlipper();
        //		testText.text = roundManager.currentDanger.ToString ();
        scoreLabelPlayLeft.text = roundManager.score.ToString();//C# tostring formatting
        //scoreLabelPlayRight.text = roundManager.score.ToString();//C# tostring formatting
        scoreLabelEnded.text = roundManager.score.ToString();//C# tostring formatting
		highScoreLabelFresh.text = roundManager.highscore.ToString();
		//highScoreLabelEnded.text = roundManager.highscore.ToString("D3");
		if (roundManager.highscore > 0)
		{
			highScoreLabelMenu.text = roundManager.highscore.ToString();
		}
    }

    void CanvasGroupChanger()
    {
        switch (roundManager.currentRound)
        {
            case round.Fresh:
                lowerLabel.text = lowerMessages[0];

                fresh.alpha = 1;
                playing.alpha = 0;
                ended.alpha = 0;
                //settings.alpha = 0;
                raycastBlockerAd.SetActive(false);
				player.SetActive(true);
				player.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .2f, .5f));
				panel.SetActive(false);
                break;
			case round.Holding:
				lowerLabel.text = lowerMessages[0];

                fresh.alpha = 0;
                playing.alpha = 0;
                ended.alpha = 1;
                //settings.alpha = 0;
                raycastBlockerAd.SetActive(false);
                player.SetActive(true);
				panel.SetActive(false);
				break;
            case round.Playing:
                lowerLabel.text = lowerMessages[1];

                fresh.alpha = 0;
                playing.alpha = 1;
                ended.alpha = 0;
                //settings.alpha = 0;
                raycastBlockerAd.SetActive(false);
                player.SetActive(true);
				panel.SetActive(true);
                break;
            case round.Ended:
            case round.Killed:
                lowerLabel.text = lowerMessages[2];

                fresh.alpha = 0;
                playing.alpha = 0;
                ended.alpha = 1;
                //settings.alpha = 0;
                raycastBlockerAd.SetActive(false);
                player.SetActive(false);
				panel.SetActive(false);
                break;
            case round.Settings:
                lowerLabel.text = null;

                fresh.alpha = 0;
                playing.alpha = 0;
                ended.alpha = 0;
                //settings.alpha = 1;
                raycastBlockerAd.SetActive(true);
				player.SetActive(false);
				panel.SetActive(false);
                break;
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
        roundManager.currentRound = round.Killed;
    }
}