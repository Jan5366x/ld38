using Assets.scripts;
using Assets.scripts.logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTestWorld : MonoBehaviour {

    public Vector2[] infectorLocations;
    public Transform infector;
    public Transform ground;

   

	// Use this for initialization
	void Start () {
        for (int x = 0; x < GameProperties.WORLD_SIZE; x++) {
            for (int y = 0; y < GameProperties.WORLD_SIZE; y++)
            {
                Transform groundObj = Instantiate(ground, new Vector3(x, -y, 0), new Quaternion());

                groundObj.name = WorldController.getGroundName(x, y);

                foreach (Vector2 location in infectorLocations) {
                    if (x == location.x && y == location.y)
                        Instantiate(infector, groundObj);
                }

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
