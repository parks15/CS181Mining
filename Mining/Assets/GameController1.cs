using UnityEngine;
using System.Collections;

public class GameController1 : MonoBehaviour {

	float spawnFrequency = 3f;
	float timeToAct = 0f;

	// Use this for initialization
	void Start () {
		timeToAct += spawnFrequency;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > timeToAct) {
			// do something cool like spawn a cube
			print ("Spawn da cube");
			print (Time.time);

			timeToAct += spawnFrequency;
		}
	
	}
}
