using UnityEngine;
using System.Collections;

public class BigBoom : Projectile {

	public int life;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(new Vector3(0f, 0f, 15f));
	
	}
	
	public override void Hit() {
		life--;
		if(life < 0) {
			Destroy (gameObject);
		}
	}
}
