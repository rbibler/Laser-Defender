using UnityEngine;
using System.Collections;

public class BoomBag : MonoBehaviour {

	public PlayerControl player;

	void OnTriggerEnter2D(Collider2D col) {
		Projectile proj = col.gameObject.GetComponent<Projectile>();
		player.TakeHit(proj);
	}
}
