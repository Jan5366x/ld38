using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class AutoGroundName : MonoBehaviour {

    private Vector3 lastLocation;

	// Use this for initialization
	void Awake () {
        autoName();
    }
	
	// Update is called once per frame
	void Update () {
        autoName();
    }

    private void autoName() {
        if (!transform.position.Equals(lastLocation)) {
            name = "G-" + (int)transform.position.x + "-" + (int)-transform.position.y;

            lastLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
