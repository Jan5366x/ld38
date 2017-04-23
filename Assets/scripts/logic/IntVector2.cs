using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.logic
{

    public struct IntVector2
    {
        public int x, y;


        public IntVector2(int[] xy)
        {
            x = xy[0];
            y = xy[1];
        }

    }
}
