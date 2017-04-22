using Assets.scripts.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// World Controller
/// </summary>
public class WorldController : MonoBehaviour {
    private static float UPDATE_DELAY = 500.0f;

    public Transform[] worldElements;
    private WorldSector[,] sectors = new WorldSector[29,29];



    private float updateTimer = 0.0f;

    // Use this for initialization
    void Start () {

        // read world data
        readWorldData();
      
        // TEST: testInfect();
    }
	
	// Update is called once per frame
	void Update () {
      
        if (updateTimer <= 0 )
        {

            // run logic
            worldUpdate();

            // reset timer
            updateTimer = UPDATE_DELAY;
        }
        else
        {
            // update timer
            updateTimer -= Time.deltaTime;
        }

	}

    private void worldUpdate() {

        Debug.Log("World Update!");

        for (int x = 0; x < sectors.GetUpperBound(0); x++)
        {
            for (int y = 0; y < sectors.GetUpperBound(1); y++)
            {

                WorldSector sector = sectors[x, y];

                if (sector == null)
                    continue;

                WorldObject worldObject = sector.WorldObject;

                if (worldObject == null)
                    continue;

                // execute actions
                foreach (IWorldAction action in worldObject.getWorldActions()) {
                    action.worldUpdate(this, sector,x,y);
                }

            }
        }
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

                Debug.Log("world data " + x + "-" + y + "detected");


                // handle lazy load
                WorldSector sector = sectors[x, y];
                if (sector == null)
                {
                    sector = new WorldSector();
                    sectors[x, y] = sector;
                }


                // set world data
                sector.WorldData = data;

                // set world object
				// TODO: NOT TESTED
				foreach (Transform child in currentGameObject.transform) {
					WorldObject worldObject = child.GetComponent<WorldObject>();
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
    private void testInfect() {
        for (int x = 0; x < sectors.GetUpperBound(0); x++) {
            for (int y = 0; y < sectors.GetUpperBound(1); y++)
            {
                WorldSector sector = sectors[x, y];

                if (sector == null)
                    continue;

                WorldData data = sector.WorldData;

                if (data == null)
                    continue;

                // infect
                data.switchState(true);
            }
        }
    }
}
