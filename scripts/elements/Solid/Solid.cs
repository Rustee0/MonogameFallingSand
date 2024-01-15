using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MonogameFallingSand.scripts.elements.Solid
{
    public class Solid : Element
    {
        public Solid(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) : base(GD, tm, xPos, yPos)
        {
            IsLiquid = false;
        }
    }
}
