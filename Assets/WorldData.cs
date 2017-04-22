using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour {

    public Sprite normalSprite;
    public Sprite infectedSprite;

    private bool isInfected = false;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void switchState(bool infected)
    {

        // change sprite
        if (infected)
        {
            if (infectedSprite != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = infectedSprite;
            }

        }
        else
        {
            if (normalSprite != null)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
        }


        // update flag
        IsInfected = infected;
    }

    public bool IsInfected
    {
        get
        {
            return isInfected;
        }

        set
        {
            isInfected = value;
        }
    }
}
