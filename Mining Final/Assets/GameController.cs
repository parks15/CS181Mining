using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bronzePrefabCube;
	public GameObject silverPrefabCube;
	public GameObject goldPrefabCube;
	//timing
	float doSomethingTime = 0.0f;
	float spawnTime = 3.0f;
	//# of cubes
	public static int bronzeCount = 0;
	public static int silverCount = 0;
	public static int goldCount = 0;
	//scoring
	public static int bronzePoints = 1;
	public static int silverPoints = 10;
	public static int goldPoints = 100;
	public static int score = 0;

	// Use this for initialization
	void Start () {
		doSomethingTime += spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= doSomethingTime) {
			if (bronzeCount == 2 && silverCount == 2 && goldCount < 1) {
				//spawn gold
				Instantiate(goldPrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				goldCount ++;
			}
			else if (bronzeCount < 4) {
				//spawn bronze
				Instantiate(bronzePrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				bronzeCount ++;
			}
			else if (bronzeCount >= 4) {
				//spawn silver
				Instantiate(silverPrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				silverCount ++;
			}
		doSomethingTime += spawnTime;
		print(GameController.score);
		}
	}	
}

