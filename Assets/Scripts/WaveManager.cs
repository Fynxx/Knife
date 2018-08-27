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
	public AudioManager audioManager;

	public Vector3[] waveLocations;

	public int pooledAmount = 5;
	public int waveLength;

	public float hardReset;

	public float speed;
	public const float initialSpeed = 5;
	public float wallStartLocation;
	public const float initialWallStartLocation = -2.2f;
	float ranY;
	public List<Shuriken> starsOnField;

	public int weaponThreshold;
	public int katanaThreshold;

	void Start()
	{
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
		player = GameObject.Find("FingerTarget").GetComponent<Player>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
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
		hardReset = 4f;
		weaponThreshold = 5;
		katanaThreshold = 5;
	}

	void Update()
	{
		WeaponSwitcher();
	}

	void WeaponSwitcher()
	{
		if (roundManager.activeState == RoundManager.ActiveState.Playing)
		{
			switch (currentStep)
			{
                case step.ChooseWeapon:
					int ran = Random.Range(0, 10);
					wallStartLocation = initialWallStartLocation;
					if (ran < weaponThreshold)
					{
						weaponThreshold = weaponThreshold - 2;
						currentStep = step.ChooseKatanaSide;
					}
					else
					{
						weaponThreshold = weaponThreshold + 2;
						currentStep = step.SetShurikenWave;
					}
					break;
				case step.ChooseKatanaSide:
					int side = Random.Range(0, 10);
					if (side < katanaThreshold)
					{
						katanaLeft = true;
						katana.transform.localScale = new Vector3(-1, 1, 1);
						katana.transform.position = new Vector3(-1.5f, 7, 0);
						katana.state = Katana.State.active;
						katanaThreshold = katanaThreshold - 2;
						currentStep = step.FireKatana;
					}
					else
					{
						katanaLeft = false;
						katana.transform.localScale = new Vector3(1, 1, 1);
						katana.transform.position = new Vector3(1.5f, 7, 0);
						katana.state = Katana.State.active;
						katanaThreshold = katanaThreshold + 2;
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
						currentStep = step.Reset;
					}
					break;
				case step.FireShuriken:
					if (starsOnField.Count == 0){
						currentStep = step.Reset;
					}
					//if (shurikenParent.transform.position.y < scoreLine.transform.position.y){
                    //  currentStep = step.AddScore;
                    //}
					break;
				case step.Reset:
					if (roundManager.activeState == RoundManager.ActiveState.Holding){
						currentStep = step.ChooseWeapon;
					} else {
						currentStep = step.AddScore;
					}
					break;
				case step.AddScore:
					AddToScore(1);               
					currentStep = step.ChooseWeapon;
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
    }

    public void ResetSpeed(){
		speed = initialSpeed;
	}

	public void AddToScore(int amount)
	{
		roundManager.score = roundManager.score + (amount * player.multiplier);
		speed = speed + (player.multiplier * .05f);
		audioManager.WaveClear();
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

	//public void HardReset(){
	//	hardReset -= Time.deltaTime;
	//	if (hardReset < 0){
	//		currentStep = step.Reset;
	//	}
	//}
}


