using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.world
{
    public interface IWorldAction {
        void worldUpdate(WorldController controller, WorldSector sector, int locX, int locY);
    }
}
