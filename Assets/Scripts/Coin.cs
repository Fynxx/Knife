using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

	public Spawner spawner;
	public RoundManager roundManager;
       
	public int point;

	protected GameObject coin;

	void Start()
	{
		coin = this.gameObject;
		spawner = GameObject.Find("GenericSpawner").GetComponent<Spawner>();
		roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
	}

	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			roundManager.score++;
			spawner.isAlive = false;
			Die(coin);         
		}
	}
}
