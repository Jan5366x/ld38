using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillWorld : MonoBehaviour {

    public Transform defaultGround;

	// Use this for initialization
	void Start () {

        if (defaultGround == null)
        {
            Debug.Log("No default fill ground prefab!");
            return;
        }

        for (int x = 0; x < GameProperties.WORLD_SIZE; x++)
        {
            for (int y = 0; y < GameProperties.WORLD_SIZE; y++)
            {
                string groundName = WorldController.getGroundName(x, y);
                GameObject currentGround = GameObject.Find(groundName);

                if (currentGround == null) {
                    Transform groundObj = Instantiate(defaultGround, new Vector3(x, -y, 0), new Quaternion());
                    groundObj.name = groundName;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
