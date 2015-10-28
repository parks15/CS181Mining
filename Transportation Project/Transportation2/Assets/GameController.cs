using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
public GameObject cubePrefab;
int gridWidth = 16;
int gridHeight = 9;
private GameObject[,] allCubes;
public Airplane airplane;

	// Use this for initialization
	void Start () {
		allCubes = new GameObject[gridWidth, gridHeight];
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y <gridHeight; y++) {
				allCubes[x,y] = (GameObject) Instantiate(cubePrefab, new Vector3(x*2 - 14, y*2 - 8, 10), Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
			}	
		}
		airplane = new Airplane();
		airplane.x = 0;
		airplane.y = 8;
		allCubes[0,8].GetComponent<Renderer>().material.color = Color.red;
	}

	public void ProcessClickedCube (GameObject clickedCube, int x, int y) {
		//If the player clicks an inactive airplane it should highlight
		if (x == airplane.x && y == airplane.y && airplane.active == false) {
			airplane.active = true;
			clickedCube.GetComponent<Renderer>().material.color = Color.yellow;
		}
		//if the player clicks an active airplane, it should unhiglight 
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
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
