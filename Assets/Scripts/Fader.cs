using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		//animator.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartFadeOut() {
		animator.enabled = true;
		animator.Play ("Fade_out", -1, 0f);
	}
}
