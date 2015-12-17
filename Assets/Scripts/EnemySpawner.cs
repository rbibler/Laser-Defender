using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	// Use this for initialization
	void Start () {
		InstantiateEnemies();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void InstantiateEnemies() {
		foreach(Transform child in this.transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, 
			                                new Quaternion(0,0,0,0)) as GameObject;
			enemy.transform.parent = child;				
		}
	}
}
