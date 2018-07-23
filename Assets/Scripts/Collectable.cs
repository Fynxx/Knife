using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapticPlugin;

public class Collectable : MonoBehaviour {   

	public void LifeTime(float life){
		life -= Time.deltaTime;
	}

	public void Die(GameObject item){
		TapticManager.Impact(ImpactFeedback.Midium);
		item.SetActive(false);
	}
}
