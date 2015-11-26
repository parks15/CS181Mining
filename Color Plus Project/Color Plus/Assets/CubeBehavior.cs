using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour {
	public int x, y;
	public GameController aGameController;
	public bool colored;
	// Use this for initialization
	void Start () {
		aGameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
	}
	
	void OnMouseDown() {
		aGameController.ProcessClickedCube(this.gameObject, x, y);
	}

	// Update is called once per frame
	void Update () {

	}
}
