using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public int maxInFormation;
	public int totalSpawned;
	public float width = 10;
	public float height = 5;
	public float xVel = 1;
	public GameObject enemyPrefab;

	private bool movingRight = true;
	private bool spawnInvoked = false;
	private bool spawned = false;
	private float xMin;
	private float xMax;

	// Use this for initialization
	void Start () {
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		if(AllAreEnemiesDead() && !spawnInvoked) {
			spawnInvoked = true;
			totalSpawned = 0;
			GameObject.FindObjectOfType<EnemySpawner>().NextWave();
		}
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

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}

	public void ClearAll() {
		spawned = false;
		foreach(Transform child in transform) {
			Destroy (child.gameObject);
		}
	}
	
	bool AllAreEnemiesDead() {
		if (!spawned) {
			return false;
		}
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
	
	public void SpawnUntilFull() {
		spawned = true;
		Transform nextFree = NextFreePosition();
		if(nextFree && totalSpawned < maxInFormation) {
			SpawnEnemyAtTransform(nextFree);
			Invoke("SpawnUntilFull", .5f);
		} else {
			spawnInvoked = false;
			CancelInvoke();
		}
	}
	
	Transform NextFreePosition() {
		foreach(Transform child in transform) {
			if(child.childCount == 0) {
				return child;
			}			
		}
		return null;
	}
	
	void SpawnEnemyAtTransform(Transform spawnTransform) {
		if(!spawnTransform) {
			return;
		}
		totalSpawned++;
		GameObject enemy1 = Instantiate (enemyPrefab, spawnTransform.position, Quaternion.identity) as GameObject;
		enemy1.transform.parent = spawnTransform;		
	}
}
