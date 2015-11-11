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
	//moving
	int moveX = 0;
	int moveY = 0;
	bool moved = false;

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
		//if player clicks on sky & there IS an active airplane,
		//the airplane teleports to that location
		else if (!(x == airplane.x && y == airplane.y) && airplane.active == true) {
			//set old cube to white
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.white;
			//set new cube to yellow (active)
			allCubes[x, y].GetComponent<Renderer>().material.color = Color.yellow;
			//update airplane location
			airplane.x = x;
			airplane.y = y;
		}
		//if airplane leaves depot, change depot back to black
		if (!(airplane.x == depotX && airplane.y == depotY)) {				
			allCubes[15,0].GetComponent<Renderer>().material.color = Color.black;
		}
	}
	
	//check key input for plane direction
	//plane can't move past boundaries of array
	public void CheckInput() {
		if (Input.GetKey("up") && airplane.y < 8) {
			moveY++;
			moved = true;
		}
		else if (Input.GetKey("down") && airplane.y > 0) {
			moveY--;
			moved = true;
		}
		else if (Input.GetKey("right") && airplane.x < 15) {
			moveX++;
			moved = true;
		}
		else if (Input.GetKey("left") && airplane.x > 0) {
			moveX--;
			moved = true;
		}
	}

	public void MovePlane () {
		if (airplane.active == true && moved == true) {
			//set cubes to white (or depot to black)
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.white;
			allCubes[depotX, depotY].GetComponent<Renderer>().material.color = Color.black;
			//update plane location
			airplane.x += moveX;
			airplane.y += moveY;
			//set plane to yellow
			allCubes[airplane.x, airplane.y].GetComponent<Renderer>().material.color = Color.yellow;
			//reset movement data
			moveX = 0;
			moveY = 0;
			moved = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//check for input every frame
		CheckInput();
		if (Time.time >= timeToAct) {
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
			MovePlane();
			timeToAct += turnLength;
		}
	}
}

