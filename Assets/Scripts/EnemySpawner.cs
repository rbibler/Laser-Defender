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
		SpawnUntilFull();
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		if(AllAreEnemiesDead()) {
			SpawnUntilFull();
		}
	}
	
	bool AllAreEnemiesDead() {
		foreach(Transform childPositionGameObject in transform) {
			if(childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
	
	void Move() {
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
	
	void SpawnUntilFull() {
		Transform nextFree = NextFreePosition();
		if(nextFree) {
			SpawnEnemyAtTransform(nextFree);
			Invoke("SpawnUntilFull", 1.0f);
		} else {
			CancelInvoke();
		}
		
		
	}
	
	Transform NextFreePosition() {
		foreach(Transform child in this.transform) {
			if(child.childCount == 0) {
				return child;
			}			
		}
		return null;
	}
	
	void InstantiateEnemies() {
		foreach(Transform child in this.transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.position, new Quaternion(0,0,0,0)) as GameObject;
			enemy.transform.parent = child;				
		}
		movingRight = true;
		transform.position = new Vector3(0,0,0);
	}
	
	void SpawnEnemyAtTransform(Transform spawnTransform) {
		if(!spawnTransform) {
			return;
		}
		GameObject enemy = Instantiate (enemyPrefab, spawnTransform.position, new Quaternion(0,0,0,0)) as GameObject;
		enemy.transform.parent = spawnTransform;		
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}
}
