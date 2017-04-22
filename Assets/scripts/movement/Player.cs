using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speedFactor = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * speedFactor;
		var y = Input.GetAxis("Vertical") * Time.deltaTime * speedFactor;

		transform.Translate(x, y, 0);
	}
}
