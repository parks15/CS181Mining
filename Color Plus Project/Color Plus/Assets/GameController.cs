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
	bool keyPressed = false;
	int row = 0;
	public static bool rowWasFull = false;
	//random cubes
	public GameObject randomCube;
	bool cubeGenerated = false;
	int destinationX;
	bool freeSpotFound;
	int blackCubeX;
	int blackCubeY;
	//cube traits
	bool active = false;
	float growFactor = 1.2f;
	float shrinkFactor = 1.0f;
	//timing
	public Text timerUI;
	float timerLength = 60.0f;
	float gameLength = 60.0f;
	float turnLength = 2.0f;
	float timeToAct = 0;
	//scoring
	public Text scoreUI;
	public static int score = 0;
	Color centerColor;
	Color topColor;
	Color bottomColor;
	Color leftColor;
	Color rightColor;
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
		timerUI.text = "Time left:";
		timerUI.color = Color.blue;
		//make a cube grid
		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*1.3f - 6, y*1.3f - 4, 10), Quaternion.identity);
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

	//this method deals with random cube generation & destruction 
	public void GenerateRandomCube() {
		//if random cube from last turn still exists,
		if (cubeGenerated) {
			//destroy it,
			Destroy(randomCube);
			//subtract from score,
			score -= noMovePoints;
			//assign random black cube to a free spot
			freeSpotFound = false;
			while (freeSpotFound == false) {
				blackCubeX = Random.Range(0, gridWidth);
				blackCubeY = Random.Range(0, gridHeight);
				if (allCubes[blackCubeX, blackCubeY].GetComponent<Renderer>().material.color != Color.white) {
					freeSpotFound = false;
				}
				else {
					freeSpotFound = true;
				}
			}
			allCubes[blackCubeX, blackCubeY].GetComponent<Renderer>().material.color = Color.black;
			cubeGenerated = false;
		}
		//create a new random cube
		randomCube = (GameObject) Instantiate(cubePrefab, new Vector3(5, 0, 0), Quaternion.identity);
		randomCube.GetComponent<Renderer>().material.color = cubeColors[Random.Range(0, 5)];
		cubeGenerated = true;
	}

	public void CheckKeyboardInput() {
		//if key is pressed, assign row number to variable, indicate that key was pressed & the row 
		if (cubeGenerated && Input.GetKeyDown(KeyCode.Alpha1)) {
			if (CheckForFullRow(4)) {
				rowWasFull = true;
				Application.LoadLevel(4);
			}
			else {
				keyPressed = true; 
				row = 4;
			}
		}
		else if (cubeGenerated && Input.GetKeyDown(KeyCode.Alpha2)) {
			if (CheckForFullRow(3)) {
				rowWasFull = true;
				Application.LoadLevel(4);
			}
			else {
				keyPressed = true; 
				row = 3;
			}
		}
		else if (cubeGenerated && Input.GetKeyDown(KeyCode.Alpha3)) {
			if (CheckForFullRow(2)) {
				rowWasFull = true;
				Application.LoadLevel(4);
			}
			else {
				keyPressed = true; 
				row = 2;
			}
		}
		else if (cubeGenerated && Input.GetKeyDown(KeyCode.Alpha4)) {
			if (CheckForFullRow(1)) {
				rowWasFull = true;
				Application.LoadLevel(4);
			}
			else {
				keyPressed = true; 
				row = 1;
			}
		}
		else if (cubeGenerated && Input.GetKeyDown(KeyCode.Alpha5)) {
			if (CheckForFullRow(0)) {
				rowWasFull = true;
				Application.LoadLevel(4);
			}
			else {
				keyPressed = true; 
				row = 0;
			}
		}
	}

	public bool CheckForFullRow (int row) {
		//if all cubes in row are not white, don't move random cube
		//! indicate somehow on player screen that row is full
		for (int x = 0; x < gridWidth; x++) {
			if (allCubes[x, row].GetComponent<Renderer>().material.color == Color.white) {
				return false;
			}
		}
		return true;
	}

	public void MoveRandomCube() {
		CheckKeyboardInput();
		//if number key is pressed and a random cube has been generated,
		if (keyPressed && cubeGenerated) {
			freeSpotFound = false;
			while (freeSpotFound == false) {
				//generate random x-coordinate for random cube color
				destinationX = Random.Range(0, gridWidth);
				//if random spot is not a white cube,
				if (allCubes[destinationX, row].GetComponent<Renderer>().material.color != Color.white) {
					//haven't found a free spot; continue loop
					freeSpotFound = false;
				}
				//if random spot is a white cube,
				else {
					//free spot has been found; end loop
					freeSpotFound = true;
				}
			}
			//color of random cube is assigned to random x position within row
			allCubes[destinationX, row].GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
			//random cube is destroyed
			Destroy(randomCube);
			//things reset
			cubeGenerated = false;
			keyPressed = false;
			destinationX = 0;
		}
	}

	public void ProcessClickedCube (GameObject clickedCube, int x, int y) {
		//If the player clicks an inactive cube, it activates/grows
		if (active == false && allCubes[x, y].GetComponent<Renderer>().material.color != Color.white && allCubes[x, y].GetComponent<Renderer>().material.color != Color.black) {
			active = true;
			activeX = x;
			activeY = y;
			allCubes[activeX, activeY].transform.localScale = new Vector3 (growFactor, growFactor, growFactor); 
		}
		//if player clicks on adjacent white cube,
		else if (allCubes[x, y].GetComponent<Renderer>().material.color == Color.white && (x == activeX + 1 || x == activeX - 1 || y == activeY + 1 || y == activeY - 1)) {
			//adjacent cube changes color
			allCubes[x, y].GetComponent<Renderer>().material.color = allCubes[activeX, activeY].GetComponent<Renderer>().material.color;
			//old cube deactivates/shrinks
			allCubes[activeX, activeY].GetComponent<Renderer>().material.color = Color.white;
			allCubes[activeX, activeY].transform.localScale = new Vector3 (shrinkFactor, shrinkFactor, shrinkFactor); 
			//coordinates & active status reset
			activeX = 0;
			activeY = 0;
			active = false;
		}
		//if player clicks an active cube,
		else if (active && x == activeX && y == activeY) {
			//cube shrinks
			allCubes[activeX, activeY].transform.localScale = new Vector3 (shrinkFactor, shrinkFactor, shrinkFactor); 
			//coordinates & active status reset
			activeX = 0;
			activeY = 0;
			active = false;
		}
	}

	//there is probably a much cleaner way to do this 
	//maybe by attributing a closed/open (black/white) bool to each non-color cube, instead of checking color each time
	public void CheckForPlus () {
		for (int x = 1; x < gridWidth - 1; x++) {
			for (int y = 1; y < gridHeight - 1; y++) {
				//if center cube is not white or black,
				if (allCubes[x,y].GetComponent<Renderer>().material.color != Color.white && allCubes[x,y].GetComponent<Renderer>().material.color != Color.black) {
					//assign colors of cubes to their respective names
					centerColor = allCubes[x,y].GetComponent<Renderer>().material.color;
					topColor = allCubes[x, y + 1].GetComponent<Renderer>().material.color;
					bottomColor = allCubes[x, y - 1].GetComponent<Renderer>().material.color;
					leftColor = allCubes[x - 1, y].GetComponent<Renderer>().material.color;
					rightColor = allCubes[x + 1, y].GetComponent<Renderer>().material.color;
					//if right cube is same color as center,
					if (allCubes[x + 1, y].GetComponent<Renderer>().material.color == centerColor) {
						//if left cube is same color as center,
						if (allCubes[x - 1, y].GetComponent<Renderer>().material.color == centerColor) {
							//if top cube is same color as center,
							if (allCubes[x, y + 1].GetComponent<Renderer>().material.color == centerColor) {
								//if bottom cube is same color as center,
								if (allCubes[x, y - 1].GetComponent<Renderer>().material.color == centerColor) {
									//color all cubes black
									allCubes[x,y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x + 1, y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x - 1, y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x, y + 1].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x, y - 1].GetComponent<Renderer>().material.color = Color.black;
									//add to score
									score += oneColorPoints;
								}
							}
						}
					}
					//this part checks for a + with 5 different colors 
					//if color of right cube is not black, white, center color, top color, bottom color, or left color
					else if (allCubes[x + 1, y].GetComponent<Renderer>().material.color != Color.white && allCubes[x + 1, y].GetComponent<Renderer>().material.color != Color.black && allCubes[x + 1, y].GetComponent<Renderer>().material.color != centerColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != topColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != bottomColor && allCubes[x + 1, y].GetComponent<Renderer>().material.color != leftColor) {
						//if color of left cube is not black, white, center color, top color, or bottom color
						if (allCubes[x - 1, y].GetComponent<Renderer>().material.color != Color.white && allCubes[x - 1, y].GetComponent<Renderer>().material.color != Color.black && allCubes[x - 1, y].GetComponent<Renderer>().material.color != centerColor && allCubes[x - 1, y].GetComponent<Renderer>().material.color != topColor && allCubes[x - 1, y].GetComponent<Renderer>().material.color != bottomColor) {
							//if color of top cube is not black, white, center color, or bottom color
							if (allCubes[x, y + 1].GetComponent<Renderer>().material.color != Color.white && allCubes[x, y + 1].GetComponent<Renderer>().material.color != Color.black && allCubes[x, y + 1].GetComponent<Renderer>().material.color != centerColor && allCubes[x, y + 1].GetComponent<Renderer>().material.color != bottomColor) {
								//if color of bottom cube is not black, white, or center color
								if (allCubes[x, y - 1].GetComponent<Renderer>().material.color != Color.white && allCubes[x, y - 1].GetComponent<Renderer>().material.color != Color.black && allCubes[x, y - 1].GetComponent<Renderer>().material.color != centerColor) {
									//color all cubes black
									allCubes[x,y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x + 1, y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x - 1, y].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x, y + 1].GetComponent<Renderer>().material.color = Color.black;
									allCubes[x, y - 1].GetComponent<Renderer>().material.color = Color.black;
									//add to score
									score += fiveColorPoints;
								}
							}
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		//timer runs from beginning of game
		timerLength -= Time.deltaTime;
		timerUI.text = "Time left: " + timerLength;
		//check keyboard input every frame
		MoveRandomCube();
		//if game time exceeds length of game, load Game Over screen
		if (Time.time > gameLength) {
			Application.LoadLevel(4);
		}
		//inside turn:
		if (Time.time > timeToAct) {
			GenerateRandomCube();
			CheckForPlus();
			scoreUI.text = "Score: " + score;
			timeToAct += turnLength;
		}
	}
}

/*
Source for size transform: http://forum.unity3d.com/threads/changing-the-size-of-gameobject.132598/
Source for button/level info: 
Thanks to Isaiah for helping with keyboard input
*/
