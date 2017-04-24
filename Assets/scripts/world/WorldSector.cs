using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.world
{
    public class WorldSector
    {
        private WorldData _worldData;

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
                if (WorldData == null)
                    return null;

                return WorldData.GetWorldObject();
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
