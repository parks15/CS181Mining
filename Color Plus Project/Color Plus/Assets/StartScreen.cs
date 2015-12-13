using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreen : MonoBehaviour {
	public Canvas startScreen;
	public Button toGameScreen;
	public Button toInstructionScreen;
	public Button toAboutScreen;
	public Text titleText;
	private Color[] titleColors;
	float switchColorTime = 1.0f;
	float timeToSwitch = 1.0f;
	int numColors = 5;

	// Use this for initialization
	void Start () {
		startScreen = startScreen.GetComponent<Canvas> (); 
		titleColors = new Color[numColors];
		titleColors[0] = Color.red;
		titleColors[1] = Color.yellow;
		titleColors[2] = Color.green;
		titleColors[3] = Color.blue;
		titleColors[4] = Color.magenta;
		titleText.text = "Color Plus";
	}
	
	public void LoadGame () {
		Application.LoadLevel(3);
	}

	public void LoadInstructionScreen () {
		Application.LoadLevel(1);
	}

	public void LoadAboutScreen () {
		Application.LoadLevel(2);
	}

	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad > timeToSwitch) {
			titleText.color = titleColors[Random.Range(0, numColors)];
			timeToSwitch += switchColorTime;
		}
	}
}
