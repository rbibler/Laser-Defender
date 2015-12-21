using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	public static float score = 0;
	public Text scoreText;
	
	void Start() {
		UpdateScore (0);
	}
	
	
	public void UpdateScore(float updateAmount) {
		score += updateAmount;
		scoreText.text = "" + (int) (score / 1);
	}
}
