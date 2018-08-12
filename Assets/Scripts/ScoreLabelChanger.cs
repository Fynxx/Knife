using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelChanger : MonoBehaviour {

	public Player player;
	public RoundManager roundManager;

	public Vector3 location1;
	public Vector3 location2;
	public Vector3 location3;

	public Text two;
	public Text four;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (roundManager.currentRound == round.Playing)
		{
			switch (player.multiplier)
			{
				case 1:
					transform.position = location1;
					two.gameObject.SetActive(false);
					four.gameObject.SetActive(false);
					break;
				case 2:
					transform.position = location2;
					two.gameObject.SetActive(true);
					four.gameObject.SetActive(false);
					break;
				case 4:
					transform.position = location3;
					two.gameObject.SetActive(false);
					four.gameObject.SetActive(true);
					break;
				default:
					transform.position = location1;
					break;
			}
		} else {
			transform.position = location1;
            two.gameObject.SetActive(false);
            four.gameObject.SetActive(false);
		}
	}
}
