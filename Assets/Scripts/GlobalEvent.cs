using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.EventMgmt
{
    public static class GlobalEvent
    {
        public delegate void AxisInput(float val);

        public static event AxisInput MoveHorizontal;
        public static event AxisInput MoveVertical;
        public static event AxisInput MoveZoom;
    }
}
