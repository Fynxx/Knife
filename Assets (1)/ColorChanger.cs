using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public RoundManager rm;

    public Color startingColor;
    public Color recordColor;
    public Color deadColor;
    public Color lerpingColor;
    public Color backgroundColor;

    public GameObject background;

	// Use this for initialization
	void Start () {
        rm = GameObject.Find("GameManager").GetComponent<RoundManager>();
        background = GameObject.Find("backgroundRayCollider");
        background.GetComponent<Renderer>().material.color = startingColor;
	}

    // Update is called once per frame
    void Update()
    {
        background.GetComponent<Renderer>().material.color = lerpingColor;
        if (rm.currentRound == round.Playing || rm.currentRound == round.Fresh){
            ChangeBackgroundColor(backgroundColor, startingColor);
        }

        if (rm.counter > rm.score){
            ChangeBackgroundColor(startingColor, recordColor);
        }
        if (rm.currentRound == round.Killed || rm.currentRound == round.Ended){
            ChangeBackgroundColor(backgroundColor, deadColor);
        }

	}

    void ChangeBackgroundColor(Color from, Color to){
        lerpingColor = Color.Lerp(from, to, (Time.time*.2f));
    }
}
