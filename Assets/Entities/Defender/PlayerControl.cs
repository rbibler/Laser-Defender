using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float xVel;
	public float padding;
	public Projectile laser;
	public BigBoom bigBoom;
	public float laserVelY;
	public float firingRate;
	public float health = 100f;
	public float maxHealth = 1000;
	public float aliveMultiplier = 13.0f;
	public float healthRechargeRate;
	public float boomTimeRequired;

	public ImageBar healthBar;
	public ImageBar powerUpBar;

	public AudioClip fireClip;
	public AudioClip exploadClip;
	public BGAudioPlayer musicPlayer;
						
	private Vector3 pos;
	private Quaternion rotation;
	private float xMin;
	private float xMax;
	private bool inPosition = false;
	private Animator animator;
	private LevelManager levelManager;
	private float boomTime;
	private float timeKeyDown;
	private float keyDownRequired = .1f;
	
	
	
	// Use this for initialization
	void Start () {
		pos = this.transform.position;
		rotation = transform.rotation;
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
		xMax = Camera.main.ViewportToWorldPoint (new Vector3(1, 0, distance)).x;
		xMin += padding;
		xMax -= padding;
		animator = GetComponent<Animator>();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		BGAudioPlayer.instance.RampToLoudness ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!inPosition) {
			return;
		}
		HandleInput ();
		RechargeHealth ();
		this.transform.position = pos;
		GameObject.FindObjectOfType<ScoreKeeper> ().UpdateScore (Time.deltaTime * aliveMultiplier);
		powerUpBar.UpdateImage (boomTime / boomTimeRequired);
		
	}

	void Fire() {
		if(!inPosition) {
			return;
		}
		Projectile newLaser = Instantiate (laser, pos, Quaternion.identity) as Projectile;
		newLaser.rigidbody2D.velocity = new Vector2(0, laserVelY);	
		AudioSource.PlayClipAtPoint(fireClip, transform.position);
	}
	
	void HandleInput() {
		if(!inPosition) {
			return;
		}
		float deltaX = 0;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			deltaX = -xVel;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			deltaX = xVel;
		}
		deltaX *= Time.deltaTime;
		pos.x += deltaX;
		pos.x = Mathf.Clamp (pos.x, xMin, xMax);
		if (Input.GetKeyUp (KeyCode.Space)) {
			if(boomTime >= boomTimeRequired) {
				Boom();
			} else {
				Fire ();
			}
			boomTime = 0;
			timeKeyDown = -1;
		} else if(Input.GetKeyDown (KeyCode.Space)) {
			timeKeyDown = Time.timeSinceLevelLoad;
		} else if (Input.GetKey (KeyCode.Space)) {
			if(Time.timeSinceLevelLoad - timeKeyDown >= keyDownRequired) {
				boomTime += Time.deltaTime;
			}
		}
	}
	
	public void TakeHit(Projectile proj) {
		if(!inPosition) {
			return;
		}
		health -= proj.GetDamage(transform.position);
		healthBar.UpdateImage(health / maxHealth);
		if(health <= 0) {
			Death();
		}
		proj.Hit();
	}

	void OnTriggerEnter2D(Collider2D col) {
		Projectile proj = col.gameObject.GetComponent<Projectile>();
		TakeHit(proj);
	}

	void Death() {
		GameObject.FindObjectOfType<UILifeManager>().RemoveLife();
		levelManager.FadeToBlack ();
		Destroy (gameObject);
		AudioSource.PlayClipAtPoint (exploadClip, pos);
		BGAudioPlayer.instance.FadeToQuiteness ();
	}
	
	public void AddHealth(float healthPercentToAdd) {
		print ("In addhealth");
		float targetHealth = health + (maxHealth * healthPercentToAdd);
		StartCoroutine(FillUpHealthBar(targetHealth, .01f, .01f));
	}
	
	private IEnumerator FillUpHealthBar(float targetHealth, float fillRate, float fillSpeed) {
		health += maxHealth * fillRate;
		print ("Filling health. At: " + health + " Target: " + targetHealth);
		if(health < targetHealth) {
			yield return new WaitForSeconds(fillSpeed);
			StartCoroutine(FillUpHealthBar(targetHealth, fillRate, fillSpeed));
		}
	}
	
	public void EndArrivalAnimation() {
		inPosition = true;
		health = 0;
		ResetHealthBar();
		animator.enabled = false;
	}
	
	private void ResetHealthBar() {
		health += maxHealth / 10;
		healthBar.UpdateImage(health/maxHealth);
		InvokeRepeating("ResetHealthBar", .08f, .25f);
		if(health >= maxHealth) {
			CancelInvoke();
			GameObject.FindObjectOfType<EnemySpawner>().StartSpawning();
		}
	}

	private void RechargeHealth() {
		health += Time.deltaTime * healthRechargeRate;
		if (health > maxHealth) {
			health = maxHealth;
		}
		healthBar.UpdateImage(health/maxHealth);
	}

	private void Boom() {
		BigBoom boom = Instantiate (bigBoom, pos, Quaternion.identity) as BigBoom;
		boom.rigidbody2D.velocity = new Vector2(0, laserVelY);	
		AudioSource.PlayClipAtPoint(fireClip, transform.position);
	}
}	
