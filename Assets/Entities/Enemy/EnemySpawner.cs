using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10;
	public float height = 5;
	public float xVel = 1;
	public WaveNotice noticeText;

	private float xMin;
	private float xMax;
	private bool movingRight = true;
	private bool spawned = false;
	private bool spawnInvoked;
	private int totalSpawned;
	private int maxInFormation;
	private static int waveCount;
	// Use this for initialization
	void Start () {
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x;
		maxInFormation = transform.childCount;
	}
	
	public void StartSpawning() {
		SpawnUntilFull();
	}
	
	public void ClearAll() {
		spawned = false;
		foreach(Transform child in transform) {
			Destroy (child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		if(AllAreEnemiesDead() && !spawnInvoked) {
			spawnInvoked = true;
			totalSpawned = 0;
			waveCount++;
			noticeText.StartAnimation();
			GameObject.FindObjectOfType<ScoreKeeper>().UpdateScore(waveCount * 400);
			print ("About to fill health");
			GameObject.FindObjectOfType<PlayerControl>().AddHealth(.25f);
			Invoke ("SpawnUntilFull", 2.0f);
		}
	}
	
	bool AllAreEnemiesDead() {
		if(!spawned) {
			return false;
		}
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
		totalSpawned++;
		GameObject enemy = Instantiate (enemyPrefab, spawnTransform.position, new Quaternion(0,0,0,0)) as GameObject;
		enemy.transform.parent = spawnTransform;		
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, 0));
	}
}
