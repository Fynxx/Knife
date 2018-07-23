using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

	public Shuriken star;
	public GameObject starPrefab;
	public GameObject starGuide;
	public int starsAmount = 6;
    public List<Shuriken> stars;
	public List<GameObject> guides;
	public List<Vector3> positions;

	// Use this for initialization
	void Start () {
		SetSpawnPositions();
		CreateStars(starsAmount);
		CreateGuides(starsAmount);
	}
	
	// Update is called once per frame
	void Update () {
		PlaceStars();
	}

	void SetSpawnPositions(){
		positions.Add(new Vector3(-4, 4, 0));
		positions.Add(new Vector3(-4, 0, 0));
		positions.Add(new Vector3(-4, -4, 0));
		positions.Add(new Vector3(-2, 6, 0));
		positions.Add(new Vector3(4, 4, 0));
		positions.Add(new Vector3(4, 0, 0));
		positions.Add(new Vector3(4, -4, 0));
		positions.Add(new Vector3(-2, -6, 0));
		positions.Add(new Vector3(-2, 6, 0));
	}

	public void CreateStars(int pooledAmount)
	{
		for (int i = 0; i < pooledAmount; i++)
		{
			Shuriken shu = Instantiate(starPrefab).GetComponent<Shuriken>();         
			shu.gameObject.SetActive(false);
			stars.Add(shu);
		}
	}
	public void CreateGuides(int pooledAmount){
		for (int i = 0; i < pooledAmount; i++)
        {
			GameObject gui = new GameObject();
            //gui.gameObject.SetActive(false);
            guides.Add(gui);
        }

	}

	public void PlaceStars(){
		for (int i = 0; i < starsAmount; i++)
		{
			int ran = Random.Range(0, positions.Count); //create random number
			guides[i].transform.position = positions[ran]; //set guide to random location
			stars[i].transform.position = positions[ran]; //set star to same location
			stars[i].gameObject.SetActive(true); //set star active
			positions.Remove(positions[ran]); //removes location from list so no duplicates can occur
		}
	}
}
