using Assets.scripts.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfectionDistributionAction : MonoBehaviour, IWorldAction {


    public float infectionPower = 0.1f;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void worldUpdate(WorldController controller, WorldSector sector, int locX, int locY)
    {
        // increase Infection
        controller.adjustInfectSector(locX,locY,infectionPower);
    }


}
