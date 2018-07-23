using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplier : MonoBehaviour {

	public RoundManager roundManager;

	public bool isPoweredUp;

	public float countDown;

	public int coins;

	public Image multiplierBar;

	private void Start()
	{
		multiplierBar = GameObject.Find("MultiplierBar").GetComponent<Image>();
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		isPoweredUp = false;
	}

	void Update () {
		multiplierBar.fillAmount = (coins * 0.125f);

		if (countDown == 1){
			isPoweredUp = true;
		} else {
			isPoweredUp = false;
		}
		if (countDown <= 0)
        {
            countDown = 0;
        }
   //     else
   //     {
			//countDown -= (Time.deltaTime * .25f);
        //}

		if (coins > 8){
			coins = 8;
		}
	}

	public void AddMultiplier(float amount){
		//countDown += Time.deltaTime * amount;

		countDown = countDown + amount;
	}

	public void AddCoins(int amount){
		coins = coins + amount;
	}
}
