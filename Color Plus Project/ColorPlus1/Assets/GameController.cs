using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	float timeToAct;
	float turnLength = 1.0f;
	int score = 0;
	public Text scoreUI;

	// Use this for initialization
	void Start () {

		timeToAct = turnLength;

		scoreUI.text = "THE GAME IS ABOUT TO BEGIN";
		scoreUI.color = Color.green;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > timeToAct) {
			
		}
	
	}
}
