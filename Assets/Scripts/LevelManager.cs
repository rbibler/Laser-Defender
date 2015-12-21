using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadGame() {
		Application.LoadLevel ("Game");
	}

	public void FadeToBlack() {
		Invoke ("LoadTheDarkness", 1.0f);
		GameObject.FindObjectOfType<Fader> ().StartFadeOut ();

	}

	void LoadTheDarkness() {
		Application.LoadLevel ("Lose Screen");
	}

}
