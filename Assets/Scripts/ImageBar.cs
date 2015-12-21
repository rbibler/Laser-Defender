using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageBar : MonoBehaviour {

	public Image barImage; 
	
	public void UpdateImage(float fillAmount) {
		barImage.fillAmount = fillAmount;
	}
}
