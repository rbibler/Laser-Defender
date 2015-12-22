using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public EnemyFormation[] formations = new EnemyFormation[3];
	public WaveNotice noticeText;


	private int waveCount;
	private EnemyFormation currentFormation;
	private int currentFormationIndex;
	// Use this for initialization
	void Start () {
		currentFormation = Instantiate(formations [0], transform.position, Quaternion.identity) as EnemyFormation;
		currentFormation.transform.parent = transform;
	}
	
	public void StartSpawning() {
		currentFormation.totalSpawned = 0;
		currentFormation.SpawnUntilFull();
	}

	public void NextWave() {
		waveCount++;
		noticeText.StartAnimation();
		GameObject.FindObjectOfType<ScoreKeeper>().UpdateScore(waveCount * 400);
		GameObject.FindObjectOfType<PlayerControl>().AddHealth(.25f);
		Invoke ("SpawnNextWave", 1.0f);
	}

	private void SpawnNextWave() {
		currentFormationIndex++;
		if (currentFormationIndex >= formations.Length) {
			currentFormationIndex = 0;
		}
		currentFormation = Instantiate(formations [currentFormationIndex], transform.position, Quaternion.identity) as EnemyFormation;
		currentFormation.transform.parent = transform;
		StartSpawning ();
	}

}
