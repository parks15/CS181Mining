using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
public GameObject cubePrefab;
int numCubes = 16;
private GameObject[] allCubes;


	// Use this for initialization
	void Start () {
		allCubes = new GameObject[numCubes];
		for (int i = 0; i < numCubes; i++) {
			allCubes[i] = (GameObject) Instantiate(cubePrefab, new Vector3(i*2, 0, 0), Quaternion.identity);
		}
	}

	public void ProcessClickedCube (GameObject clickedCube) {
		foreach (GameObject oneCube in allCubes) {
			oneCube.GetComponent<Renderer>().material.color = Color.white;
		}
		clickedCube.GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
