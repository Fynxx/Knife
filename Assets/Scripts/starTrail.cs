using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starTrail : MonoBehaviour {

	public TrailRenderer trail;
	//public PeakNShoot peakNShoot;
	public Shuriken star;

	// Use this for initialization
	void Start () {
		trail = GetComponent<TrailRenderer>();
		//peakNShoot = GameObject.Find("Peaknshoot").GetComponent<PeakNShoot>();
	}
	
	// Update is called once per frame
	void Update () {
		if (star.currentState == Shuriken.starState.Shoot)
		{
			trail.enabled = true;
		} 
		else 
		{
			trail.enabled = false;
		}
	}
}
