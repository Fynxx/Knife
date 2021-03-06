﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Screenshot : MonoBehaviour
{

	public RoundManager roundManager;
	string filePath;
	public string shareText;
	public bool highscore;
	//public string[] shareText;

	void Start()
	{
		//TakeScreenshot();
		roundManager = GetComponent<RoundManager>();
	}

	public void TakeScreenshot()
	{
        roundManager.currentState = State.Screenshot;
		//roundManager.inactiveState = RoundManager.InactiveState.Screenshot;
		StartCoroutine(TakeScreen());
		//roundManager.activeState = RoundManager.ActiveState.Dieing;
		//roundManager.inactiveState = RoundManager.InactiveState.Dead;
	}

	private IEnumerator TakeScreen()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.height, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.height, Screen.height), 0, 0);
		ss.Apply();

		filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		// To avoid memory leaks
		Destroy(ss);

		new NativeShare().AddFile(filePath).Share();
		//print(shareText); 
		//roundManager.activeState = RoundManager.ActiveState.Dieing;
        roundManager.currentState = State.Dead;
	}

	//public void ShareTextSelector()
	//{
	//	if (highscore){
	//		shareText = ("I got a new high score of " + roundManager.score + " in #Schuriken! @FriedCandle");
	//	} else {
	//		shareText = ("I dodged " + roundManager.score + " weapons before I got hit by a " + roundManager.waveManager.currentWeapon + " #Schuriken @FriedCandle");
 //       }
	//}
}
