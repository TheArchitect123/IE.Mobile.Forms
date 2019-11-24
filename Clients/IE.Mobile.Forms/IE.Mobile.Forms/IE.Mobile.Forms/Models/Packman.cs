using IE.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace IE.Mobile.Forms.Models
{
    internal class Packman
    {
        public double XPosition { get; set; }
        public double YPosition { get; set; }

        public PackmanDirection Direction { get; set; }
        public MovementDirection Motion { get; set; }
    }
}
