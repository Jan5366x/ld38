using Assets.scripts.world;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfectionDistributionAction : MonoBehaviour, IWorldDataAction {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void worldUpdate(WorldController controller, WorldSector sector)
    {
        throw new NotImplementedException();
    }

}
