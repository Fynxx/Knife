using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour {

	public RoundManager roundManager;

	public float countDown;   

	public Image multiplierBar;

	private void Start()
	{
		multiplierBar = GameObject.Find("MultiplierBar").GetComponent<Image>();
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
	}

	void Update () {
		multiplierBar.fillAmount = countDown;

		if (countDown <= 0)
        {
            countDown = 0;
        }
        else
        {
			countDown -= (Time.deltaTime * .25f);
        }
	}

	public void AddMultiplier(float amount){
		countDown = countDown + amount;
	}
}
