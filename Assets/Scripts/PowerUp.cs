using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PowerUp : Collectable {

    public const float lifeValue = 3f;
    public const float pickupTimerValue = .75f;

	public float lifeTime;
    public float pickupTimer;

	public GameObject powerUpIcon;

	protected GameObject playerSprite;
	protected Player player;
    protected RoundManager roundManager;
	public Spawner spawner;

    private void Awake()
    {
        player = GameObject.Find("FingerTarget").GetComponent<Player>();
        playerSprite = GameObject.Find("FingerSprite");
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();  
		spawner = GameObject.Find("PowerUpSpawner").GetComponent<Spawner>();
    }

	void Start () {
        
	}

	public float PickingUp(float pickupTime){
		pickupTime -= Time.deltaTime;
		return pickupTime;
	}
    

}
