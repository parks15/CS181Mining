using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstructionScreen : MonoBehaviour {
	public Canvas instructionScreen;
	public Button toStartScreen;

	// Use this for initialization
	void Start () {
		instructionScreen = instructionScreen.GetComponent<Canvas>();
	}

	public void LoadStartScreen () {
		Application.LoadLevel(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
