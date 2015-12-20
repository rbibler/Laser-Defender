using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Image healthBarImage; 
	
	public void UpdateHealth(float health) {
		healthBarImage.fillAmount = health;
	}
}
