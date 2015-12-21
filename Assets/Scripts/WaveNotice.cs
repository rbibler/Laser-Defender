using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveNotice : MonoBehaviour {


	public Text text;
	private Animator animator;

	void Awake() {
		GetComponent<Animator> ().enabled = false;
		text.enabled = false;
		animator = GetComponent<Animator> ();
	}

	public void StartAnimation() {
		text.enabled = true;
		animator.enabled = true;
		Invoke ("StopAnimation", 2f);
	}

	private void StopAnimation() {
		animator.enabled = false;
		animator.Play ("Text_swoop", -1, 0);
		text.enabled = false;
	}
}
