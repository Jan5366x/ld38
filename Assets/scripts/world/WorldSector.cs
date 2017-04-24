using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.world
{
    public class WorldSector
    {
        private WorldData _worldData;
        private WorldObject _worldObject;

        public WorldData WorldData
        {
            get
            {
                return _worldData;
            }

            set
            {
                _worldData = value;
            }
        }

        public WorldObject WorldObject
        {
            get
            {
                return _worldObject;
            }

            set
            {
                _worldObject = value;
            }
        }

        public bool IsInfected
        {
            get
            {
                WorldData worldData = WorldData;

                if (worldData == null)
                    return false;

                return worldData.IsInfected;
            }
        }
    }
}
