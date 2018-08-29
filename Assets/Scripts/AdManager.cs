using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
	#if UNITY_IOS
	private string gameId = "2608860";
	#endif
	#if UNITY_ANDROID
	private string gameId = "2608861";
	#endif

    void Awake()
    {
		Advertisement.Initialize(gameId);
	}
}
