using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public interface IHandler
    {
        void UpdateHandler( float timeElapsed );
    }
}
