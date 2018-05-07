using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text counterLabel;
    public Text counterLabelBig;
    public Text lowerLabel;
    public Text scoreLabel;

    public Image dangerNorth;
    public Image dangerEast;
    public Image dangerSouth;
    public Image dangerWest;

    public string[] lowerMessages;
    public CanvasGroup fresh;
    public CanvasGroup playing;
    public CanvasGroup ended;
    public CanvasGroup settings;

    public RoundManager roundManager;
    public Spawner spawner;

    // Use this for initialization
    void Start()
    {
        counterLabel = GameObject.Find("TimerText").GetComponent<Text>();
        lowerLabel = GameObject.Find("LowerText").GetComponent<Text>();
        counterLabelBig = GameObject.Find("TimerTextBig").GetComponent<Text>();
        scoreLabel = GameObject.Find("score").GetComponent<Text>();

        dangerNorth = GameObject.Find("DangerNorth").GetComponent<Image>();
        dangerEast = GameObject.Find("DangerEast").GetComponent<Image>();
        dangerSouth = GameObject.Find("DangerSouth").GetComponent<Image>();
        dangerWest = GameObject.Find("DangerWest").GetComponent<Image>();

        fresh = GameObject.Find("RoundFresh").GetComponent<CanvasGroup>();
        playing = GameObject.Find("RoundPlaying").GetComponent<CanvasGroup>();
        ended = GameObject.Find("RoundEnded").GetComponent<CanvasGroup>();
        settings = GameObject.Find("GameSettings").GetComponent<CanvasGroup>();

        roundManager = GetComponent<RoundManager>();
        spawner = GameObject.Find("ShurikenSpawner").GetComponent<Spawner>();

        lowerMessages = new string[3];
        lowerMessages[0] = "Press anywhere to start.";
        lowerMessages[1] = "Keep holding your finger down.";
        lowerMessages[2] = "Your score is displayed above.";
    }

    // Update is called once per frame
    void Update()
    {
        CanvasGroupChanger();
        DangerIndicator();
        //		testText.text = roundManager.currentDanger.ToString ();
        counterLabel.text = roundManager.counter.ToString("f2");//C# tostring formatting
        counterLabelBig.text = roundManager.counter.ToString("f2");//C# tostring formatting
        scoreLabel.text = roundManager.score.ToString("f2");
    }

    void CanvasGroupChanger()
    {
        switch (roundManager.currentRound)
        {
            case round.Fresh:
                lowerLabel.text = lowerMessages[0];
                counterLabel.enabled = false;

                fresh.alpha = 1;
                playing.alpha = 0;
                ended.alpha = 0;
                settings.alpha = 0;
                break;
            case round.Playing:
                lowerLabel.text = lowerMessages[1];
                counterLabel.enabled = true;

                fresh.alpha = 0;
                playing.alpha = 1;
                ended.alpha = 0;
                settings.alpha = 0;
                break;
            case round.Ended:
            case round.Killed:
                lowerLabel.text = lowerMessages[2];
                counterLabel.enabled = true;

                fresh.alpha = 0;
                playing.alpha = 0;
                ended.alpha = 1;
                settings.alpha = 0;
                break;
            case round.Settings:
                lowerLabel.text = null;
                counterLabel.enabled = true;

                fresh.alpha = 0;
                playing.alpha = 0;
                ended.alpha = 0;
                settings.alpha = 1;
                break;
            default:
                break;
        }
    }

    void DangerIndicator()
    {
        if (roundManager.currentRound == round.Playing)
        {
            switch (spawner.spawnDirection)
            {
                case Spawner.Direction.North:
                    dangerNorth.enabled = true;
                    dangerEast.enabled = false;
                    dangerSouth.enabled = false;
                    dangerWest.enabled = false;
                    break;
                case Spawner.Direction.East:
                    dangerNorth.enabled = false;
                    dangerEast.enabled = true;
                    dangerSouth.enabled = false;
                    dangerWest.enabled = false;
                    break;
                case Spawner.Direction.South:
                    dangerNorth.enabled = false;
                    dangerEast.enabled = false;
                    dangerSouth.enabled = true;
                    dangerWest.enabled = false;
                    break;
                case Spawner.Direction.West:
                    dangerNorth.enabled = false;
                    dangerEast.enabled = false;
                    dangerSouth.enabled = false;
                    dangerWest.enabled = true;
                    break;
                default:
                    break;
            }
        }
    }
}