using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour {
public GameObject cubePrefab;
int numCubes = 16;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numCubes; i++) {
			Instantiate(cubePrefab, new Vector3(i*2, 0, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
