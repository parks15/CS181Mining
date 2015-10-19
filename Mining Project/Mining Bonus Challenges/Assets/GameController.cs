using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public GameObject bronzePrefabCube;
	public GameObject silverPrefabCube;
	public GameObject goldPrefabCube;
	public GameObject kryptonitePrefabCube;
	//timing
	float doSomethingTime = 0.0f;
	float spawnTime = 3.0f;
	//# of cubes
	public static int bronzeCount = 0;
	public static int silverCount = 0;
	public static int goldCount = 0;
	public static int kryptoniteCount = 0;
	private bool recentlySpawnedGold = false;
	//scoring
	public static int bronzePoints = 1;
	public static int silverPoints = 10;
	public static int goldPoints = 100;
	public static int kryptonitePoints = 1000;
	public static int score = 0;

	// Use this for initialization
	void Start () {
		doSomethingTime += spawnTime;
		//print instructions to increase spawn rate
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= doSomethingTime) {
			if (silverCount == 2 && silverCount == goldCount + kryptoniteCount) {
				//spawn kryptonite
				Instantiate(kryptonitePrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				kryptoniteCount ++;
			}
			else if (bronzeCount == 2 && silverCount == 2 && recentlySpawnedGold == false) {
				//spawn gold
				Instantiate(goldPrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				goldCount ++;
				recentlySpawnedGold = true;
			}
			else if (bronzeCount < 4) {
				//spawn bronze
				Instantiate(bronzePrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				bronzeCount ++;
				recentlySpawnedGold = false;
			}
			else if (bronzeCount >= 4) {
				//spawn silver
				Instantiate(silverPrefabCube, new Vector3(Random.Range(-8f, 8f), Random.Range(-3f, 5f), 0), Quaternion.identity);
				silverCount ++;
				recentlySpawnedGold = false;
			}
		doSomethingTime += spawnTime;
		print(GameController.score);
		}
	}
}
