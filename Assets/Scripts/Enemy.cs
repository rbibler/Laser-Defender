using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float health = 100f;
	public Projectile projectile;
	public float initialProjVelocity = -10f;
	private float timeSinceLastFire = 2.8f;
	public float minTimeToFire =  .5f;
	public float maxTimeToFire = 3f;
	public float projectileDamage = 50f;
	
	public void Update() {
		timeSinceLastFire += Time.deltaTime;
		if(timeSinceLastFire >= Random.Range (minTimeToFire, maxTimeToFire)) {
			Fire();
		}
	}
	
	void Fire() {
		Projectile proj = Instantiate (projectile, transform.position, Quaternion.identity) as Projectile;
		proj.rigidbody2D.velocity = new Vector3(0, initialProjVelocity, 0);
		timeSinceLastFire = 0;
		proj.SetDamage(projectileDamage);
	}

	public void OnTriggerEnter2D(Collider2D col) {
		TakeHit(col.gameObject.GetComponent<Projectile>());
	}
	
	private void TakeHit(Projectile laser) {
		health -= laser.GetDamage ();
		CheckHealth();
		laser.Hit();
	}
	
	private void CheckHealth() {
		if(health <= 0) {
			Destroy (gameObject);
		}
	}
}
