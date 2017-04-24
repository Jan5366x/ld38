using Assets.scripts;
using Assets.scripts.world;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// World Controller
/// </summary>
public class WorldController : MonoBehaviour {


    public static byte INFECTED = 1;
    public static byte NORMAL = 0;

    public Transform[] worldElements;
    private WorldSector[,] sectors = new WorldSector[GameProperties.WORLD_SIZE, GameProperties.WORLD_SIZE];
    private List<byte[,]> infectionSpriteData = new List<byte[,]>();

    private bool init = false;

    private float updateTimer = 0.0f;

    // Use this for initialization
    void Awake () {


        // handle compex infection data
        if (GameProperties.COMPLEX_INFECTION) 
            setupSpriteData();

        // read world data
        readWorldData();
      
        // TEST: testInfect();
    }
	
	// Update is called once per frame
	void Update () {


        // TODO DIRTY timing hotfix for testing
        if (!init) Init();


        if (updateTimer <= 0 )
        {

            // run logic
            worldUpdate();

            // reset timer
            updateTimer = GameProperties.WORLD_UPDATE_DELAY;
        }
        else
        {
            // update timer
            updateTimer -= Time.deltaTime;
        }

	}

    private void Init() {
        // read world data
        readWorldData();

        init = true;
    }


    private byte getDataValue(string entry) {
        if (entry.Equals("0"))
        {
            return NORMAL;
        } else {
            return INFECTED;
        }
    }


    public bool isSpriteDataKnown(byte[,] request) {
        foreach (byte[,] data in infectionSpriteData)
        {

            bool match = true;
            for (byte x = 0; x <= 2; x++)
            {
                for (byte y = 0; y <= 2; y++)
                {
                    if (request[x, y] != data[x, y])
                    {
                        match = false;
                    }
                }
            }

            if (match)
                return true;
        }

        return false;
    }

    public byte[,] requestAlternativInfectionSprite(byte[,] request) {
        int bestQuality = 0;
        byte[,] bestResult = new byte[3, 3];


        foreach (byte[,] data in infectionSpriteData)
        {
            int quality = 0;
            for (byte x = 0; x <= 2; x++) {
                for (byte y = 0; y <= 2; y++)
                {
                    if (/*request[x, y]  == NORMAL &&*/ request[x, y] == data[x, y]) {
                        quality++;
                    }
                }
            }

            if (quality > bestQuality) {
                bestQuality = quality;
                bestResult = data;
            }
        }


            // no result
            return bestResult;
    }

    private void setupSpriteData() {


        Debug.Log(Application.dataPath);


        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Misc/Resources/infection/complex/");
        FileInfo[] info = dir.GetFiles("*.png");
        foreach (FileInfo file in info) {
            string[] rawData = file.Name.ToLower().Replace(".png","").Replace("infection_","").Split('_');

            if (rawData.GetUpperBound(0) == 7)
            {
                byte[,] newData = new byte[3, 3];
                newData[0, 0] = getDataValue(rawData[0]);
                newData[1, 0] = getDataValue(rawData[1]);
                newData[2, 0] = getDataValue(rawData[2]);

                newData[0, 1] = getDataValue(rawData[3]);
                newData[1, 1] = INFECTED;
                newData[2, 1] = getDataValue(rawData[4]);

                newData[0, 2] = getDataValue(rawData[5]);
                newData[1, 2] = getDataValue(rawData[6]);
                newData[2, 2] = getDataValue(rawData[7]);

                infectionSpriteData.Add(newData);
            }
            else {
                Debug.LogWarning("Invalid infection sprite file " + file.Name + " detected!");
            }
        }


           
    }


    /// <summary>
    /// Get Texture Name By Data
    /// Note: no error handling!
    /// </summary>
    public static string GetTextureNameByData(string textureName, byte[,] data) {
        return textureName + "_" + data[0,0] + "_" + data[1, 0] + "_" + data[2, 0] + "_" + data[0, 1] + "_" + data[2, 1] + "_" + data[0, 2] + "_" + data[1, 2] + "_" + data[2, 2];
    }

    public byte[,] GetInfectionData(int locX, int locY) {
        byte[,] result = new byte[3, 3];


        byte pointerX = 0;
        for (int x = locX - 1; x <= locX + 1; x++) {
            byte pointerY = 0;

            for (int y = locY - 1; y <= locY + 1; y++)
            {
                // check world boundrys
                if (!(x >= 0 && x <= GameProperties.WORLD_SIZE && y >= 0 && y <= GameProperties.WORLD_SIZE))
                {
                    result[pointerX, pointerY] = NORMAL;
                    continue;
                }

                
                WorldSector sector = sectors[x, y];

                // no data no infection ;-)
                if (sector == null) {
                    result[pointerX, pointerY] = NORMAL;
                    continue;
                }


                // read sector information
                if (sector.IsInfected)
                {
                    result[pointerX, pointerY] = INFECTED;
                } else {
                    result[pointerX, pointerY] = NORMAL;
                }
               

                pointerY++;
            }
            pointerX++;
        }


        // process result

        if (isSpriteDataKnown(result)) {
            return result;
        } else {
            return requestAlternativInfectionSprite(result);
        }
        
   
    }

    public void adjustInfectSector(int locX, int locY, float power)
    {

        List<WorldSector> processedSectors = new List<WorldSector>();

        for (int angle = 0; angle < 360; angle++ ) {
            for (int distance = 0; distance <= GameProperties.INFECTION_RANGE; distance++)
            {
                int posX = locX + (int)(Mathf.Cos(angle) * distance);
                int posY = locY + (int)(Mathf.Sin(angle) * distance);

                if (!(posX >= 0 && posX <= GameProperties.WORLD_SIZE && posY >= 0 && posY <= GameProperties.WORLD_SIZE))
                    continue;

                WorldSector sector = sectors[posX, posY];
                if (sector == null)
                    continue;


                if (processedSectors.Contains(sector))
                {
                    continue;
                }
                else
                {
                    processedSectors.Add(sector);
                }


                WorldData worldData = sector.WorldData;

                if (worldData == null)
                    continue;


                bool wasInfected = worldData.IsInfected;

                worldData.Infection += ((((GameProperties.INFECTION_RANGE - distance) * 100) / GameProperties.INFECTION_RANGE) * power) / 100;

                // state change ?!
                bool stateChange = wasInfected != worldData.IsInfected;


                // force update for all nearby sectors
                worldData.updateState(this, posX, locY);



                /* old code from complex code (don't delete yet!)
                if (stateChange)
                {
                    for (int x = posX - 1; x <= posX + 1; x++)
                    {
                        for (int y = posX - 1; y <= posY + 1; y++)
                        {
                            WorldSector updateSector = sectors[x, y];
                            if (updateSector == null)
                                continue;

                            WorldData updateWorldData = updateSector.WorldData;

                            if (updateWorldData == null)
                                continue;

                            updateWorldData.updateState(this, x, y);

                        }
                    }
                }

    */
            }
        }
    }



    private void worldUpdate() {

        // Debug.Log("World Update!");

        for (int x = 0; x < GameProperties.WORLD_SIZE; x++)
        {
            for (int y = 0; y < GameProperties.WORLD_SIZE; y++)
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
        Debug.Log("Start reading world of size " + GameProperties.WORLD_SIZE);

        for (int x = 0; x < GameProperties.WORLD_SIZE; x++)
        {
            for (int y = 0; y < GameProperties.WORLD_SIZE; y++)
            {
                GameObject currentGameObject = GameObject.Find(getGroundName(x,y));

                if (currentGameObject == null)
                    continue;

              //  Debug.Log("world ground game object " + x + "-" + y + "detected");

                WorldData data = currentGameObject.GetComponent<WorldData>();

                if (data == null)
                    continue;

              //  Debug.Log("world data " + x + "-" + y + "detected");


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

    public static string getGroundName(int x, int y) {
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
                data.Infection = GameProperties.INFECTION_MAX;
                data.updateState(this,x, y);
            }
        }
    }
}
