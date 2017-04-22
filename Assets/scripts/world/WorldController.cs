using Assets.scripts.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// World Controller
/// </summary>
public class WorldController : MonoBehaviour {
    public Transform[] worldElements;
    public int sectorSize = 64;

    private WorldSector[,] sectors = new WorldSector[20,20];

    // Use this for initialization
    void Start () {

        // make sure that the player can't leave the world! >_<
        createWorldCollider();

        // TODO replace
        testFill();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



    private void createWorldCollider() {

    }


    /// <summary>
    ///  test method
    /// </summary>
    private void testFill() {
        for (int x = 0; x < sectors.GetUpperBound(0); x++) {
            for (int y = 0; y < sectors.GetUpperBound(1); y++)
            {
                Instantiate(worldElements[Random.Range(0, worldElements.GetUpperBound(0))], new Vector3(x + 0.5f, -y + 0.5f, 0), new Quaternion());
            }
        }
    }
}
