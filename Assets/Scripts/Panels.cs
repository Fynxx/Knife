using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour {

	public WaveManager waveManager;
	public int highscore;
	public int objective;

	public enum state { setup, chooseNext, check, setNext };
	public state currentState;

	public List<GameObject> AllPanels;
	public List<GameObject> UnlockedPanels;

	public GameObject nextPanel;
	public GameObject currentPanel;

	public Vector3 restSpot;
	public Vector3 publicSpot;

	public bool panel1unlocked;
	public bool panel2unlocked;
	public bool panel3unlocked;
	public bool panel4unlocked;
	public bool panel5unlocked;
	public bool panelSecretUnlocked;

	public const int panel2 = 20;
	public const int panel3 = 40;
	public const int panel4 = 90;
	public const int panel5 = 120;
	public const int panelSecret = 130;

	public Text pageNumber;

	void Awake()
	{
		waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
		pageNumber = GameObject.Find("Pagenumber").GetComponent<Text>();
		restSpot = new Vector3(-10, 0, -9);
		publicSpot = new Vector3(0, 0, -9);
		for (int i = 1; i < AllPanels.Count; i++)
		{
			AllPanels[i].transform.position = restSpot;
		}
    }

    void Start () {
		//CheckUnlockedPanels();
        //UnlockedPanels[0].transform.position = publicSpot;
    }
    
    // Update is called once per frame
    void Update () {
		highscore = waveManager.roundManager.highscore;

		switch (currentState)
		{
			case state.setup:
				CheckUnlockedPanels();
				currentPanel = UnlockedPanels[0].gameObject;
                currentPanel.transform.position = publicSpot;
				currentState = state.chooseNext;
				break;
			case state.chooseNext:
				nextPanel = UnlockedPanels[Random.Range(0, UnlockedPanels.Count)];
				currentState = state.check;
				break;
			case state.check:
				if (nextPanel == currentPanel)
                {
					currentState = state.chooseNext;
				} else {
					currentState = state.setNext;
				}
				break;
			case state.setNext:
				if (waveManager.wavesAmount >= waveManager.nextPanel)
                {
                    currentPanel.transform.position = restSpot;
                    currentPanel = nextPanel;
                    currentPanel.transform.position = publicSpot;
					waveManager.wavesAmount = 0;
                    currentState = state.chooseNext;
				}
				break;
			default:
				currentState = state.setup;
				break;
		}
		PageNumber();
	}

	//void PanelChanger(){
	//	if (highscore > panel2){
	//		nextPanel = UnlockedPanels[Random.Range(0, UnlockedPanels.Count)];
	//		if (nextPanel == currentPanel){
	//			nextPanel = UnlockedPanels[Random.Range(0, UnlockedPanels.Count)];
	//			print("same one chosen");            
	//		} else {
	//			//TurnOffAllPanels();
	//			print("new panel chosen");
	//			currentPanel.transform.position = restSpot;
	//			currentPanel = nextPanel;
	//			currentPanel.transform.position = publicSpot;
	//		}
	//	}
	//}

	void PageNumber(){         
		if (currentPanel == UnlockedPanels[0]){
			pageNumber.text = "1-5";
		} else if (currentPanel == UnlockedPanels[1]){
			pageNumber.text = "2-5";
		} else if (currentPanel == UnlockedPanels[2])
        {
            pageNumber.text = "3-5";
		} else if (currentPanel == UnlockedPanels[3])
        {
            pageNumber.text = "4-5";
		} else if (currentPanel == UnlockedPanels[4])
        {
            pageNumber.text = "5-5";
		} else if (currentPanel == UnlockedPanels[5])
        {
            pageNumber.text = "?-5";
		}

	}

	void TurnOffAllPanels(){
		for (int i = 0; i < UnlockedPanels.Count; i++)
		{
			//UnlockedPanels[i].tran
		}
	}
    

	public void CheckUnlockedPanels()
	{
		if (highscore > panelSecret)
        {
			UnlockedPanels.Clear();
			UnlockedPanels.Add(AllPanels[0]);
			panel1unlocked = true;
			UnlockedPanels.Add(AllPanels[1]);
			panel2unlocked = true;
			UnlockedPanels.Add(AllPanels[2]);
			panel3unlocked = true;
			UnlockedPanels.Add(AllPanels[3]);
			panel4unlocked = true;
			UnlockedPanels.Add(AllPanels[4]);
			panel5unlocked = true;
            UnlockedPanels.Add(AllPanels[5]);
            panelSecretUnlocked = true;

		} else if (highscore > panel5)
        {
			UnlockedPanels.Clear();
			UnlockedPanels.Add(AllPanels[0]);
            panel1unlocked = true;
            UnlockedPanels.Add(AllPanels[1]);
            panel2unlocked = true;
            UnlockedPanels.Add(AllPanels[2]);
            panel3unlocked = true;
            UnlockedPanels.Add(AllPanels[3]);
            panel4unlocked = true;
            UnlockedPanels.Add(AllPanels[4]);
            panel5unlocked = true;
		} else if (highscore > panel4)
        {
			UnlockedPanels.Clear();
			UnlockedPanels.Add(AllPanels[0]);
            panel1unlocked = true;
            UnlockedPanels.Add(AllPanels[1]);
            panel2unlocked = true;
            UnlockedPanels.Add(AllPanels[2]);
            panel3unlocked = true;
            UnlockedPanels.Add(AllPanels[3]);
            panel4unlocked = true;
		} else if (highscore > panel3)
        {
			UnlockedPanels.Clear();
			UnlockedPanels.Add(AllPanels[0]);
            panel1unlocked = true;
            UnlockedPanels.Add(AllPanels[1]);
            panel2unlocked = true;
            UnlockedPanels.Add(AllPanels[2]);
            panel3unlocked = true;
		} else if (highscore > panel2)
        {
			UnlockedPanels.Clear();
			UnlockedPanels.Add(AllPanels[0]);
            panel1unlocked = true;
            UnlockedPanels.Add(AllPanels[1]);
            panel2unlocked = true;
        } else if (!panel1unlocked){
			UnlockedPanels.Clear();
            UnlockedPanels.Add(AllPanels[0]);
            panel1unlocked = true;
        }

    }
}
