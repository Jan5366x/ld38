using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.world.action
{
    public class InfectionSpriteReplaceAction : MonoBehaviour, IWorldAction
    {

        public Sprite normalSprite;
        public Sprite infectedSprite;

        private bool currentState = false;

        public void worldUpdate(WorldController controller, WorldSector sector, int locX, int locY)
        {

            if (sector == null)
                return;

            bool isInfected = sector.IsInfected;

            if (currentState != isInfected) {

                if (isInfected)
                {
                    if (infectedSprite != null)
                    {
                        GetComponent<SpriteRenderer>().sprite = infectedSprite;
                    }
                }
                else
                {
                    if (normalSprite != null)
                    {
                        GetComponent<SpriteRenderer>().sprite = normalSprite;
                    }
                }


                // update flag
                currentState = isInfected;
            }

        }
    }
}
