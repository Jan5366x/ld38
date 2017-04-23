using Assets.scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData : MonoBehaviour {
   

    public Sprite normalSprite;
    public Sprite infectedSprite;

    public bool canGetInfected = true;

    public float infection = 0f;
    private bool lastInfectionState = false;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void updateState()
    {
        if (!canGetInfected)
            return;


        if (lastInfectionState == IsInfected) {
            // we don't have to update the sprite since there are no changes
            return;
        }


        // change sprite if required
        if (IsInfected)
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

        // update last state helper
        lastInfectionState = IsInfected;

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
            updateState();
        }
    }
}
