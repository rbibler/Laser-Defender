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
	public bool circling = false;
	public float timeSinceLastChange;
	public float minAnimSwitch = 10f;
	public float maxAnimSwitch = 40f;
	public Animator animator;
	public bool inPosition = false;
	public AudioClip boom;
	public AudioClip laserClip;
	
	public int scorePoints = 100;
	private bool invincible = true;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	public void Update() {
		timeSinceLastFire += Time.deltaTime;
		if(timeSinceLastFire >= Random.Range (minTimeToFire, maxTimeToFire) && inPosition) {
			Fire();
		}
		CheckForAnimChange ();
	}
	
	void Fire() {
		Vector3 pos = transform.position;
		pos.z = .25f;
		Projectile proj = Instantiate (projectile, pos, Quaternion.identity) as Projectile;
		proj.rigidbody2D.velocity = new Vector3(0, initialProjVelocity, 0);
		timeSinceLastFire = 0;
		proj.SetDamage(projectileDamage);
		AudioSource.PlayClipAtPoint(laserClip, this.transform.position);
	}

	void CheckForAnimChange() {
		timeSinceLastChange += Time.deltaTime;
		if(timeSinceLastChange >= Random.Range (minAnimSwitch, maxAnimSwitch)) {
			ChangeAnim();
			timeSinceLastChange = 0;
		}
	}

	void ChangeAnim() {
		circling = !circling;
		animator.SetBool ("circling", circling);
		print ("Changing: " + circling);
	}

	public void InPosition() {
		inPosition = true;
		invincible = false;
	}

	public void OnTriggerEnter2D(Collider2D col) {
		TakeHit(col.gameObject.GetComponent<Projectile>());
	}
	
	private void TakeHit(Projectile laser) {
		if(invincible) {
			return;
		}
		health -= laser.GetDamage ();
		CheckHealth();
		laser.Hit();
	}
	
	private void CheckHealth() {
		if(health <= 0) {
			Destroy (gameObject);
			GameObject.FindObjectOfType<ScoreKeeper>().UpdateScore(scorePoints);
			AudioSource.PlayClipAtPoint(boom, this.transform.position);
		}
	}
}
