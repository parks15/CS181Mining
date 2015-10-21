using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
public GameObject Airplane;
int[,] numAirplanes = 16;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numAirplanes; i++) {
			Airplane[x,y] = Instantiate(cubePrefab, new Vector3(i*2, 0, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
