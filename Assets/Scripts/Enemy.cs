using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col) {
		print (col.gameObject.tag);
		if(col.gameObject.tag == "Laser") {
			Destroy (gameObject);
			Destroy (col.gameObject);
		}
	}
}
