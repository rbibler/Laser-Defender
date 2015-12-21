using UnityEngine;
using System.Collections;

public class BGAudioPlayer : MonoBehaviour {

	public static BGAudioPlayer instance = null;
	public PlayerControl player;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
	
	public void FadeToQuiteness() {
		print ("fading " + instance.gameObject.GetInstanceID());
		if (instance.audio.volume >= .2f) {
			instance.audio.volume -= .05f;
			Invoke ("FadeToQuiteness", .1f);
		} else {
			instance.audio.volume = .2f;
		}
	}

	public void RampToLoudness() {
		print ("ramping: " + audio.volume + " "  + instance.gameObject.GetInstanceID());
		if(instance.audio.volume < 1f) {
			instance.audio.volume += .05f;
			Invoke ("RampToLoudness", .1f);
		}
	}
}
