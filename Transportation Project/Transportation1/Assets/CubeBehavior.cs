using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		GameObject[] allCubes = GameObject.FindGameObjectsWithTag("Clickable Cube");
		foreach (GameObject oneCube in allCubes) {
			oneCube.GetComponent<Renderer>().material.color = Color.white;
		}
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
