using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOrder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = (int)Random.Range(1f, 1000.0f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
