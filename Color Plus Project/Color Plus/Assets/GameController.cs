using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	//cube grid info
	public GameObject cubePrefab;
	private GameObject[,] allCubes;
	int gridWidth = 8;
	int gridHeight = 5;
	int activeX = 0;
	int activeY = 0;
	//color array info
	private Color[] cubeColors;
	int numColors = 5;
	//random cube
	public GameObject randomCube;
	bool cubeGenerated = false;
	//cube traits
	bool active = false;
	float growFactor = 1.2f;
	float shrinkFactor = 1.0f;
	//timing
	public Text turnUI;
	float gameLength = 60.0f;
	float turnLength = 2.0f;
	float timeToAct = 0;
	//scoring
	public Text scoreUI;
	int score = 0;
	int oneColorPoints = 10;
	int fiveColorPoints = 5;
	int noMovePoints = 1;

	// Use this for initialization
	void Start () {
		timeToAct += turnLength;
		//make green score UI
		scoreUI.text = "Score: " + score;
		scoreUI.color = Color.green;
		//make blue timer UI
		turnUI.text = "Time left:";
		turnUI.color = Color.blue;
		//make a cube grid
		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*1.3f - 6, y*1.3f - 4, 20), Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
				allCubes[x,y].GetComponent<Renderer>().material.color = Color.white;
			}
		}
		//make an array of all cube colors
		cubeColors = new Color[numColors];
		cubeColors[0] = Color.red;
		cubeColors[1] = Color.yellow;
		cubeColors[2] = Color.green;
		cubeColors[3] = Color.blue;
		cubeColors[4] = Color.magenta;
	}
	
	public void ProcessClickedCube (GameObject clickedCube, int x, int y) {
		//If the player clicks an inactive cube, it activates/grows
		if (active == false) {
			active = true;
			activeX = x;
			activeY = y;
			allCubes[activeX, activeY].transform.localScale = new Vector3 (growFactor, growFactor, growFactor); 
		}
		//if player clicks on adjacent cube, 
		else if (x > activeX || x < activeX || y > activeY || y < activeY) {
		//adjacent cube changes color
			allCubes[x, y].GetComponent<Renderer>().material.color = allCubes[activeX, activeY].GetComponent<Renderer>().material.color;
		//old cube deactivates/shrinks
			allCubes[activeX, activeY].GetComponent<Renderer>().material.color = Color.white;
			allCubes[activeX, activeY].transform.localScale = new Vector3 (shrinkFactor, shrinkFactor, shrinkFactor); 
		//coordinates & status of active cube reset
			activeX = 0;
			activeY = 0;
			active = false;
		}
		//if player clicks an active cube, it deactivates/shrinks
		else if (active == true) {
			active = false;
			allCubes[activeX, activeY].transform.localScale = new Vector3 (shrinkFactor, shrinkFactor, shrinkFactor); 
		}
	}

	public void GenerateRandomCube() {
		//if random cube from last turn still exists, destroy it & subtract from score
		if (cubeGenerated) {
			Destroy(randomCube);
			score -= noMovePoints;
		}
		//create a new random cube
		randomCube = (GameObject) Instantiate(cubePrefab, new Vector3(5, 0, 0), Quaternion.identity);
		randomCube.GetComponent<Renderer>().material.color = cubeColors[Random.Range(0, 5)];
		cubeGenerated = true;
	}

	public void CheckKeyboardInput() {
		//if number key is pressed and a random cube has been generated, 
		//assign random cube color to random column in given row
		if (Input.GetKeyDown(KeyCode.Alpha1) && cubeGenerated) {
			allCubes[Random.Range(0, 8), 4].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			Destroy(randomCube);
			cubeGenerated = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && cubeGenerated) {
			allCubes[Random.Range(0, 8), 3].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			Destroy(randomCube);
			cubeGenerated = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && cubeGenerated) {
			allCubes[Random.Range(0, 8), 2].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			Destroy(randomCube);
			cubeGenerated = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && cubeGenerated) {
			allCubes[Random.Range(0, 8), 1].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			Destroy(randomCube);
			cubeGenerated = false;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha5) && cubeGenerated) {
			allCubes[Random.Range(0, 8), 0].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			Destroy(randomCube);
			cubeGenerated = false;
		}
	}

	/* 
	public void CheckForPlus () {
		if (+ of one color) {
			score += oneColorPoints;
		}
		else if (+ of five colors) {
			score += fiveColorPoints;
		}
	} 
	*/

	// Update is called once per frame
	void Update () {
		CheckKeyboardInput();
		/*
		if (Time.time > gameLength) {
			Application.LoadLevel(End);
		}
		*/
		if (Time.time > timeToAct) {
			GenerateRandomCube();
			//CheckForPlus();
			scoreUI.text = "Score: " + score;
			timeToAct += turnLength;
			print(score);
		}
	}
}


//source for size transform: http://forum.unity3d.com/threads/changing-the-size-of-gameobject.132598/
//thanks to Isaiah for helping with keyboard input
