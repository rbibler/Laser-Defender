using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILifeManager : MonoBehaviour {

	public Image[] lifeIcons = new Image[3];
	
	private int lives = 2;
	
	
	public void RemoveLife() {
		lifeIcons[lives].enabled = false;
		lives--;
		if(lives < 0) {
			RegenerateIcons();
		}
	}
	
	private void RegenerateIcons() {
		lives = 2;
		foreach(Image image in lifeIcons) {
			image.enabled = true;
		}
	}
	
}
