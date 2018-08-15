using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public enum step { ChooseWeapon, ChooseKatanaSide, FireKatana, SetShurikenWave, FireShuriken, AddScore, Reset };
	//public enum step { ChooseWeapon, Katana, Shuriken, Fire, AddScore, Reset };
	public step currentStep;
	public enum Weapons { Shuriken, Katana };
	public Weapons currentWeapon;
	public bool katanaLeft;
	public int shurikenWave;

	//public enum state { SetWave, Shoot, Reset };

	public Katana katana;

	public Shuriken star;
	public Shuriken currentStar;
	//public List<Shuriken> inactiveStars;
	//public List<Shuriken> activeStars;
	public List<Shuriken> stars;
	public bool waveIsOver;
	public bool waveIsSet;

	public RoundManager roundManager;
	public Player player;
	public Waves waves;

	public Vector3[] waveLocations;

	public int pooledAmount = 5;
	public int waveLength;

	public float speed;
	public const float initialSpeed = 5;
	public float wallStartLocation;
	public const float initialWallStartLocation = -2.2f;
	float ranY;
	public List<Shuriken> starsOnField;

	void Start()
	{
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		waves = GetComponent<Waves>();
		waveIsOver = true;
		waves.amountStars = pooledAmount;
		waveLocations = new Vector3[pooledAmount];
		//Create stars - put in available stars list
		for (int i = 0; i < pooledAmount; i++)
		{
			Shuriken obj = Instantiate(star);
			obj.gameObject.SetActive(false);
			stars.Add(obj);
			//obj.currentState = Shuriken.starState.inactive;
		}
		wallStartLocation = initialWallStartLocation;
		speed = initialSpeed;
	}

	void Update()
	{
		WeaponSwitcher();
	}

	void WeaponSwitcher()
	{
		if (roundManager.currentRound == round.Playing)
		{
			switch (currentStep)
			{
				case step.ChooseWeapon:
					int ran = Random.Range(0, 10);
					if (ran < 5)
					{
						currentStep = step.ChooseKatanaSide;
					}
					else
					{
						currentStep = step.SetShurikenWave;
					}
					break;
				case step.ChooseKatanaSide:
					int side = Random.Range(0, 10);
					if (side < 5)
					{
						katanaLeft = true;
						katana.transform.localScale = new Vector3(-1, 1, 1);
						katana.transform.position = new Vector3(-1.5f, 7, 0);
						katana.state = Katana.State.active;
						currentStep = step.FireKatana;
					}
					else
					{
						katanaLeft = false;
						katana.transform.localScale = new Vector3(1, 1, 1);
						katana.transform.position = new Vector3(1.5f, 7, 0);
						katana.state = Katana.State.active;
						currentStep = step.FireKatana;
					}
					break;
				case step.SetShurikenWave:
					SetShurikenWall();
					currentStep = step.FireShuriken;
					break;
				case step.FireKatana:
					if (katana.state == Katana.State.inactive)
					{
						currentStep = step.AddScore;
					}
					break;
				case step.FireShuriken:
					//if (!stars[pooledAmount-1].gameObject.activeSelf)
					//{
					//	currentStep = step.AddScore;
					//}

					if (starsOnField.Count == 0){
						currentStep = step.AddScore;
					}
					break;
				case step.AddScore:
					AddToScore(1);
					wallStartLocation = initialWallStartLocation;
					currentStep = step.Reset;
					break;
				default:
					currentStep = step.ChooseWeapon;
					break;
			}
		}
	}

	public void SetShurikenWall(){
		for (int i = 0; i < pooledAmount; i++)
        {
			ranY = Random.Range(6f, 10f);
            currentStar = stars[i];
			currentStar.transform.position = new Vector3(wallStartLocation, ranY, 0);
            currentStar.gameObject.SetActive(true);         
            wallStartLocation = wallStartLocation + 0.9f;
			starsOnField.Add(currentStar);
        }
	}

	public void ResetField()
	{
		katana.transform.position = new Vector3(-1.5f, 7, 0);
		//katana.gameObject.SetActive(false);
		for (int i = 0; i < pooledAmount; i++)
		{
			stars[i].transform.position = new Vector3(0, 0, 0);
			stars[i].gameObject.SetActive(false);
		}
		speed = initialSpeed;
	}

	public void AddToScore(int amount)
	{
		roundManager.score = roundManager.score + (amount * player.multiplier);
		speed = speed + (player.multiplier * .05f);
	}

	public void AddXP(){
		roundManager.totalXP = roundManager.totalXP + (roundManager.score * 100);
	}

	void SetWave()
	{
		for (int i = 0; i < waveLength; i++)
		{
			currentStar = stars[i];
			if (currentStar.gameObject.activeSelf == false)
			{
				stars[i].transform.position = waveLocations[i];
				currentStar.gameObject.SetActive(true);
			}
		}
	}



	public void ClearWeapons()
	{
		for (int i = 0; i < pooledAmount; i++)
		{
			stars[i].gameObject.SetActive(false);
		}
		katana.gameObject.SetActive(false);
	}
}


