using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public OreType myType;
	//speed of spinning cube
	float speed = 150f;

	// Use this for initialization
	void Start () {
	}

	 void OnMouseOver() {
	 	//cube spins when mouse hovers
	 	transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}

	void OnMouseDown() {
		Destroy(gameObject);
		if (myType == OreType.Bronze) {
			GameController.bronzeCount --;
			GameController.score += GameController.bronzePoints;
		}
		if (myType == OreType.Silver) {
			GameController.silverCount --;
			GameController.score += GameController.silverPoints;
		}
		if (myType == OreType.Gold) {
			GameController.goldCount --;
			GameController.score += GameController.goldPoints;
		}
		if (myType == OreType.Kryptonite) {
			GameController.kryptoniteCount --;
			GameController.score += GameController.kryptonitePoints;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
