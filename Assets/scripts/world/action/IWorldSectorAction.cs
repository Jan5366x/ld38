using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.world
{
    interface IWorldDataAction {
        void worldUpdate(WorldController controller, WorldSector sector);
    }
}
