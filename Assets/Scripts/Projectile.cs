using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage = 100f;
	
	
	public void SetDamage(float damage) {
		this.damage = damage;
	}
	
	public float GetDamage() {
		return damage;
	}
	
	public void Hit() {
		Destroy(gameObject);
	}
	
}
