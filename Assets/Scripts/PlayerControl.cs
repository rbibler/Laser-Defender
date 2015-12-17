using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float xVel;
	public float padding;
		
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
	}
}	
