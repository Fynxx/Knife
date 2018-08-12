using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

	public int waveIndex;
	public int amountStars;   
    
	// Use this for initialization
	void Start () {
		//waveLocations = new Vector3[pooledAmount];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//public void WaveSelector(int index){
		//waveIndex = index;
		//switch (waveIndex)
		//{
			//case 1:
			//	waveLocations[0] = new Vector3(1, 1, 0);
   //             waveLocations[1] = new Vector3(2, 1, 0);
   //             waveLocations[2] = new Vector3(3, 1, 0);
   //             waveLocations[3] = new Vector3(4, 1, 0);
   //             waveLocations[4] = new Vector3(4, 2, 0);
   //             waveLocations[5] = new Vector3(6, 5, 0);
   //             waveLocations[6] = new Vector3(5, 5, 0);
   //             waveLocations[7] = new Vector3(4, 5, 0);
   //             waveLocations[8] = new Vector3(3, 5, 0);
   //             waveLocations[9] = new Vector3(3, 6, 0);
			//	amountStars = waveLocations.Count;
   //             FixWaveLocations();
			//	break;
			//case 2:
   //             waveLocations[0] = new Vector3(5, 1, 0);
   //             waveLocations[1] = new Vector3(6, 1, 0);
   //             waveLocations[2] = new Vector3(4, 2, 0);
   //             waveLocations[3] = new Vector3(3, 3, 0);
   //             waveLocations[4] = new Vector3(5, 5, 0);
   //             waveLocations[4] = new Vector3(4, 6, 0);
   //             waveLocations[5] = new Vector3(1, 7, 0);
   //             waveLocations[6] = new Vector3(2, 7, 0);
   //             waveLocations[7] = new Vector3(3, 7, 0);
			//	amountStars = waveLocations.Count;
   //             FixWaveLocations();
			//	break;
			//default:
				//break;
	//	}
	//}
    

	//void FixWaveLocations()
 //   {
 //       for (int i = 0; i < amountStars; i++)
 //       {
 //           waveLocations[i] = new Vector3((waveLocations[i].x - 3.5f), (waveLocations[i].y + 6.5f), 0);
 //       }
 //   }
	//void ResetWaveArray()
 //   {
	//	for (int i = 0; i < amountStars; i++)
 //       {
 //           waveLocations[i] = new Vector3(0, 0, 0);
 //       }
 //   }

	//public void WaveOne()
  //  {
		////ResetWaveArray();
  //      waveLength = 10;
  //      waveLocations[0] = new Vector3(1, 1, 0);
  //      waveLocations[1] = new Vector3(2, 1, 0);
  //      waveLocations[2] = new Vector3(3, 1, 0);
  //      waveLocations[3] = new Vector3(4, 1, 0);
  //      waveLocations[4] = new Vector3(4, 2, 0);
  //      waveLocations[5] = new Vector3(6, 5, 0);
  //      waveLocations[6] = new Vector3(5, 5, 0);
  //      waveLocations[7] = new Vector3(4, 5, 0);
  //      waveLocations[8] = new Vector3(3, 5, 0);
  //      waveLocations[9] = new Vector3(3, 6, 0);
  //      FixWaveLocations();
  //  }

  // public void WaveTwo()
  //  {
		////ResetWaveArray();
    //    waveLength = 10;
    //    waveLocations[0] = new Vector3(5, 1, 0);
    //    waveLocations[1] = new Vector3(6, 1, 0);
    //    waveLocations[2] = new Vector3(4, 2, 0);
    //    waveLocations[3] = new Vector3(3, 3, 0);
    //    waveLocations[4] = new Vector3(5, 5, 0);
    //    waveLocations[5] = new Vector3(4, 6, 0);
    //    waveLocations[6] = new Vector3(1, 7, 0);
    //    waveLocations[7] = new Vector3(2, 7, 0);
    //    waveLocations[9] = new Vector3(3, 7, 0);
    //    FixWaveLocations();
    //}
}
