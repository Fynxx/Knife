using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public RoundManager rm;

	public Color white;
	public Color black;

    public GameObject background;
    
	void Start () {
        rm = GameObject.Find("GameManager").GetComponent<RoundManager>();
        background = GameObject.Find("backgroundRayCollider");
	}
    
    void Update()
    {
        if (rm.currentState == State.Playing || rm.currentState == State.Holding){
			background.GetComponent<Renderer>().material.color = white;
		} else {
			background.GetComponent<Renderer>().material.color = black;
		}      
	}
}
