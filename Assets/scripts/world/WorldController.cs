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

        // read world data
        readWorldData();


        // TODO replace
        testFill();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private void readWorldData() {

        for (int x = 0; x < sectors.GetUpperBound(0); x++)
        {
            for (int y = 0; y < sectors.GetUpperBound(1); y++)
            {
                GameObject currentGameObject = GameObject.Find(getGroundName(x,y));

                if (currentGameObject == null)
                    continue;

                WorldData data = currentGameObject.GetComponent<WorldData>();

                if (data == null)
                    continue;

                // set world data
                sectors[x, y].WorldData = data;

                // TODO set world object
                //   sectors[x, y].WorldOBject = ???
				// TODO: NOT TESTED
				foreach (Transform child in currentGameObject.transform) {
					WorldObject worldObject = child.GetComponent("WorldObject") as WorldObject;
					if (worldObject != null) {
						sectors [x, y].WorldObject = worldObject;
						break;
					}
				}
            }
        }
    }

    private string getGroundName(int x, int y) {
        return "G-" + x + "-" + y;
    }

    /// <summary>
    /// TODO delete this
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
