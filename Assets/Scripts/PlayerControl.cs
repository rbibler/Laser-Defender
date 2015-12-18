using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float xVel;
	public float padding;
	public Projectile laser;
	public float laserVelY;
	public float firingRate;
	public float health = 100f;
		
	private Vector3 pos;
	private float xMin;
	private float xMax;
	
	// Use this for initialization
	void Start () {
		pos = this.transform.position;
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3(1, 0, distance)).x;
		xMin += padding;
		xMax -= padding;
	}
	
	// Update is called once per frame
	void Update () {
		HandleInput();
		this.transform.position = pos;
	}

	void Fire() {
		Projectile newLaser = Instantiate (laser, this.transform.position, Quaternion.identity) as Projectile;
		newLaser.rigidbody2D.velocity = new Vector2(0, laserVelY);	
	}
	
	void HandleInput() {
		float deltaX = 0;
		if(Input.GetKey (KeyCode.LeftArrow)) {
			deltaX = -xVel;
		} else if(Input.GetKey (KeyCode.RightArrow)) {
			deltaX = xVel;
		}
		deltaX *= Time.deltaTime;
		pos.x += deltaX;
		pos.x = Mathf.Clamp (pos.x, xMin, xMax);
		if(Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		Projectile proj = col.gameObject.GetComponent<Projectile>();
		TakeHit(proj);
	}
	
	void TakeHit(Projectile proj) {
		health -= proj.GetDamage();
		if(health <= 0) {
			Destroy(gameObject);
		}
		proj.Hit();
	}
}	
