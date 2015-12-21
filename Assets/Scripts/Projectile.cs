using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage = 100f;
	
	
	public void SetDamage(float damage) {
		this.damage = damage;
	}
	
	public float GetDamage(Vector3 position) {
		return damage * Vector3.Distance (transform.position, position);
	}
	
	public virtual void Hit() {
		Destroy(gameObject);
	}
	
	void OnDrawGizmos() {
		Gizmos.DrawWireSphere(gameObject.collider2D.transform.position, this.gameObject.collider2D.bounds.size.x / 2);
	}
	
}
