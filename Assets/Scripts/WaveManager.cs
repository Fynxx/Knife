using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public enum step { ChooseWeapon, ChooseKatanaSide, ChooseShurikenWave, FireKatana, SetShurikenWave, FireShuriken, AddScore, Reset };
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

	public int pooledAmount = 25;
	public int waveLength;

	public float speed;
	public const float initialSpeed = 5;

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
						currentStep = step.ChooseShurikenWave;
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
				case step.ChooseShurikenWave:
					int wave = Random.Range(0, 10);
					if (wave > 5)
					{
						WaveOne();
						currentStep = step.SetShurikenWave;
					}
					else
					{
						WaveTwo();
						currentStep = step.SetShurikenWave;
					}
					//waves.WaveSelector(shurikenWave);
					break;
				case step.FireKatana:
					if (katana.state == Katana.State.inactive)
					{
						currentStep = step.AddScore;
					}
					break;
				case step.SetShurikenWave:
					SetWave();
					currentStep = step.FireShuriken;
					break;
				case step.FireShuriken:
					if (!stars[waveLength - 1].gameObject.activeSelf)
					{
						currentStep = step.AddScore;
					}
					break;
				case step.AddScore:
					AddToScore(1);
					//katana.CalculateSpeed();
					//star.CalculateSpeed();
					//speed = speed - .1f;
					currentStep = step.Reset;
					break;
				default:
					currentStep = step.ChooseWeapon;
					break;
			}
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

	void ResetWaveArray()
	{
		for (int i = 0; i < pooledAmount; i++)
		{
			waveLocations[i] = new Vector3(0, 0, 0);
		}
	}

	//void Reset()
	//{
	//	for (int i = 0; i < pooledAmount; i++)
	//	{
	//		currentStar = activeStars[0];
	//		inactiveStars.Add(currentStar);
	//		inactiveStars[i].transform.position = waveLocations[i];
	//		activeStars.Remove(currentStar);
	//	}
	//}
	void FixWaveLocations()
	{
		for (int i = 0; i < pooledAmount; i++)
		{
			waveLocations[i] = new Vector3((waveLocations[i].x - 3.5f), (waveLocations[i].y + 6.5f), 0);
		}
	}
	void WaveOne()
	{
		ResetWaveArray();
		waveLength = 10;
		waveLocations[0] = new Vector3(1, 1, 0);
		waveLocations[1] = new Vector3(2, 1, 0);
		waveLocations[2] = new Vector3(3, 1, 0);
		waveLocations[3] = new Vector3(4, 1, 0);
		waveLocations[4] = new Vector3(4, 2, 0);
		waveLocations[5] = new Vector3(6, 5, 0);
		waveLocations[6] = new Vector3(5, 5, 0);
		waveLocations[7] = new Vector3(4, 5, 0);
		waveLocations[8] = new Vector3(3, 5, 0);
		waveLocations[9] = new Vector3(3, 6, 0);
		FixWaveLocations();
	}

	void WaveTwo()
	{
		ResetWaveArray();
		waveLength = 8;
		waveLocations[0] = new Vector3(5, 1, 0);
		waveLocations[1] = new Vector3(6, 1, 0);
		waveLocations[2] = new Vector3(4, 2, 0);
		waveLocations[3] = new Vector3(3, 3, 0);
		waveLocations[4] = new Vector3(4, 6, 0);
		waveLocations[5] = new Vector3(1, 7, 0);
		waveLocations[6] = new Vector3(2, 7, 0);
		waveLocations[7] = new Vector3(3, 7, 0);
		FixWaveLocations();
	}
}


