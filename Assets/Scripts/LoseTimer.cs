using UnityEngine;
using System.Collections;

public class LoseTimer : MonoBehaviour {

	private LevelManager levelManager;

	void Start() {
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeSinceLevelLoad >= 2.0f) {
			levelManager.LoadGame();
		}
	}
}
