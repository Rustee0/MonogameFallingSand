using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFallingSand.scripts.elements.Solid
{
    public class Stone : Solid
    {
        public Stone(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) : base(GD, tm, xPos, yPos)
        {
            color = Color.LightGray;
            color.R += (byte)Game1.Random.Next(-5, 5);
            color.G += (byte)Game1.Random.Next(-5, 5);
            color.B += (byte)Game1.Random.Next(-5, 5);
            texture.SetData(new Color[] { color });
            name = "Stone";
        }
    }
}
