using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public enum Orientation
    {
        Left,
        Right
    }
    interface Enemy
    {

        Orientation orientation { get; set; }
    }
}
