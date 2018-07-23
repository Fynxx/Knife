using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	public RoundManager roundManager;
	public Image holdingBar;

	public float holdingTime;

	// Use this for initialization
	void Start () {
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		holdingBar = GameObject.Find("HoldingBar").GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
		if (roundManager.currentRound == round.Holding)
		{
			holdingTime = roundManager.holdTimer;
			holdingBar.fillAmount = holdingTime;
		}
	}
}
