using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
	public Canvas SummaryScreen;
	public Button toStartScreen;
	public Text resultUI;
	public Text finalScoreUI;

	// Use this for initialization
	void Start () {
		//if row was full, player lost
		if (GameController.rowWasFull == true) {
			resultUI.color = Color.red;
			resultUI.text = "The row was full. You lost!";
		}
		//if score was less than/equal to 0, player lost
		else if (GameController.score == 0) {
			resultUI.color = Color.red;
			resultUI.text = "Your score was too low. You lost!";
		}
		//if score was higher than 0, player won
		else {
			resultUI.color = Color.green;
			resultUI.text = "You won!";
		}
		//show final score
		finalScoreUI.color = Color.blue;
		finalScoreUI.text = "Final score: " + GameController.score;
	}
	
	public void LoadStartScreen () {
		Application.LoadLevel(0);
	}

	// Update is called once per frame
	void Update () {
	
	}
}

//http://answers.unity3d.com/questions/198878/restart-current-level.html
