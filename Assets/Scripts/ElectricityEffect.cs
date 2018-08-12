using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float ranx = Random.Range(-2f, 2f);
		float rany = Random.Range(-2f, 2f);
		transform.localPosition = new Vector3(ranx, rany, 0);
	}
}
