using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {

	public GameObject player;
	public RoundManager roundManager; 
	public Renderer rend;
	public Color color;
	public float alpha;
	public Quaternion rotation;

	public AudioSource audioSource;
    public AudioClip whoosh;


	// Use this for initialization
	void Start () {
//		player = GameObject.Find ("FingerSprite");
//		roundManager = GameObject.Find ("GameManager").GetComponent<RoundManager> ();
		audioSource = GetComponent<AudioSource>();
		rend = GetComponent<Renderer>();
		RandomRotation ();
		rotation = Quaternion.identity;
		transform.position = new Vector3 (transform.position.x, transform.position.y, 5);
		float ran = Random.Range(1f, 1.5f);
		audioSource.pitch = ran;
		audioSource.PlayOneShot(whoosh);
    }

	void RandomRotation(){
		Vector3 euler = transform.eulerAngles;
		euler = new Vector3(0, 0, Random.Range(-90f, 90f));
		transform.eulerAngles = euler;
	}
}
