using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AboutScreen : MonoBehaviour {
	public Canvas aboutScreen;
	public Button toStartScreen;

	// Use this for initialization
	void Start () {
		aboutScreen = aboutScreen.GetComponent<Canvas>();
	}

	public void LoadStartScreen () {
		Application.LoadLevel(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
