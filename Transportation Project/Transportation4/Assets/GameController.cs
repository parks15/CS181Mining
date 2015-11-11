using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	//cube grid & array info
	public GameObject cubePrefab;
	int gridWidth = 16;
	int gridHeight = 9;
	private GameObject[,] allCubes;
	public Airplane airplane;
	//timing info
	float timeToAct = 0f;
	float turnLength = 1.5f;
	//cargo & scoring info
	public int cargo = 0;
	public int cargoCapacity = 90;
	public int score = 0;
	//start & depot positions
	int startX = 0;
	int startY = 8;
	int depotX = 15;
	int depotY = 0;

	// Use this for initialization
	void Start () {
		timeToAct += turnLength;
		//spawn a 16 by 9 grid of cubes 
		//assign them to an array
		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*2 - 14, y*2 - 8, 10), Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
			}
		}
		//make the first cube a red airplane
		airplane = new Airplane();
		airplane.x = startX;
		airplane.y = startY;
		allCubes[startX,startY].GetComponent<Renderer>().material.color = Color.red;
		airplane.targetX = startX;
		airplane.targetY = startY;
		//there's a black cube in the bottom left
		allCubes[depotX,depotY].GetComponent<Renderer>().material.color = Color.black;
	}

	public void ProcessClickedCube (GameObject clickedCube, int x, int y) {
		//If the player clicks an inactive airplane it should turn yellow
		if (x == airplane.x && y == airplane.y && airplane.active == false) {
			airplane.active = true;
			clickedCube.GetComponent<Renderer>().material.color = Color.yellow;
		}
		//if the player clicks an active airplane, it should turn red again
		else if (x == airplane.x && y == airplane.y && airplane.active == true) {
			airplane.active = false;
			clickedCube.GetComponent<Renderer>().material.color = Color.red;
		}
		//if player clicks on sky & there's no active airplane, nothing happens
		//if player clicks on sky & plane is active, update target location
		if (airplane.active) {
			airplane.targetX = x;
			airplane.targetY = y;
		}
	}

	public void MoveAirplane() {
		int nextX = airplane.x;
		int nextY = airplane.y;
		//move 1 unit in target direction
		if (airplane.targetX > airplane.x) {
			nextX++;
		}
		else if (airplane.targetX < airplane.x) {
			nextX--;
		}
		if (airplane.targetY > airplane.y) {
			nextY++;
		}
		else if (airplane.targetY < airplane.y) {
			nextY--;
		}
		//set old cube to black if plane is in the depot
		if (airplane.x == depotX && airplane.y == depotY) {
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.black;
		}
		//or set old cube to white
		else {
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.white;
		}
		//update airplane location
		airplane.x = nextX;
		airplane.y = nextY;
		//if airplane is active, set to yellow
		if (airplane.active) {
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.yellow;
		}
		//or red if not active
		else {
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= timeToAct) {
			MoveAirplane();
			//check if airplane is in starting position
			//if so, add 10 cargo, but not above max capacity
			if (airplane.x == startX && airplane.y == startY && airplane.cargo < cargoCapacity) {
				airplane.cargo += 10;
				print ("Airplane cargo is now "+airplane.cargo);
			}
			//check if airplane is at the depot
			//if so, print score, reset cargo
			if (airplane.x == depotX && airplane.y == depotY) {
				score += airplane.cargo;
				airplane.cargo = 0;
				print ("Score: "+score);
			}
			timeToAct += turnLength;
		}
	}
}

