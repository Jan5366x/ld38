using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour {


    public string infectionTextureTitle = "infection";

    public bool canGetInfected = true;

    public float infection = 0f;

    private string currentInfectionTexture = null;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void updateState(WorldController controller, int locX, int locY)
    {
        if (!canGetInfected)
            return;



        GameObject infectionLayer = transform.Find("Infection").gameObject;
        if (infectionLayer == null)
        {
            Debug.LogWarning("Missing infection layer child -Infection-");
            return;
        }

        // update sprite
        SpriteRenderer renderer = infectionLayer.GetComponent<SpriteRenderer>();
       
        
        //  handle infection texture removal
        if (!IsInfected) {
            renderer.sprite = null;
            return;
        }

        byte[,] infectionData = controller.GetInfectionData(locX, locY);
        string systemSprite;

        if (GameProperties.COMPLEX_INFECTION)
        {
            // complex system
            systemSprite = "infection/complex/" + WorldController.GetTextureNameByData(infectionTextureTitle, infectionData);
        }
        else {
            systemSprite = "infection/large_infection"; 
        }

       


        // check if a update is required
        if (currentInfectionTexture != null && currentInfectionTexture.Equals(systemSprite)) {
            return;
        }


        // Debug.Log(locX + "-" + locY + " use texture >" + systemSprite + "<");

        Sprite infectionSprite = Resources.Load<Sprite>(systemSprite);



        if (infectionSprite != null)
        {
            // set texture
            renderer.sprite = infectionSprite;
            // update current texture
            currentInfectionTexture = systemSprite;
        } else {
            Debug.LogWarning(systemSprite + " sprite not found!");
        }
    }

    public bool IsInfected
    {
        get
        {
            return Infection > GameProperties.INFECTION_POINT;
        }
    }

    public float Infection
    {
        get
        {
            return infection;
        }

        set
        {
            infection = Mathf.Clamp(value, GameProperties.INFECTION_MIN, GameProperties.INFECTION_MAX);
        }
    }

    public bool CanPlaceHealer() {
        return canGetInfected && !IsInfected && !HasWorldObject();
    }

    public bool HasWorldObject() {
        foreach (Transform child in transform) {
            if (GetComponent<WorldObject>() != null)
                return true;
        }

        return false;
           
    }
}
