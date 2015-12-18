using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10;
	public float height = 5;
	public float xVel = 1;

	private float xMin;
	private float xMax;
	private bool movingRight = true;


	// Use this for initialization
	void Start () {
		InstantiateEnemies();
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;
	}
	
	// Update is called once per frame
	void Update () {
		if (movingRight) {
			transform.position += Vector3.right * xVel * Time.deltaTime;
		} else {
			transform.position += Vector3.left * xVel * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (width / 2);
		float leftEdgeofFormation = transform.position.x - (width / 2);
		if (rightEdgeOfFormation >= xMax) {
			movingRight = false;
		} else if (leftEdgeofFormation < xMin) {
			movingRight = true;
		}
	}
	
	void InstantiateEnemies() {
		foreach(Transform child in this.transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, new Quaternion(0,0,0,0)) as GameObject;
			enemy.transform.parent = child;				
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}
}
