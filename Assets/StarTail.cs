using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTail : MonoBehaviour {

	public ShurikenSpawner shurikenSpawner;
	public GameObject star;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = star.transform.position;

		if (shurikenSpawner.spawnDirection == ShurikenSpawner.Direction.North)
		{
			transform.eulerAngles = new Vector3(0, 0, 90);
		}
		if (shurikenSpawner.spawnDirection == ShurikenSpawner.Direction.East)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
		if (shurikenSpawner.spawnDirection == ShurikenSpawner.Direction.South)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
		if (shurikenSpawner.spawnDirection == ShurikenSpawner.Direction.West)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
	}
}
