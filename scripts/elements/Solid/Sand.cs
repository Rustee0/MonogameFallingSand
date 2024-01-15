using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFallingSand.scripts.elements.Solid
{
    public class Sand : Solid
    {
        public Sand(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) : base(GD, tm, xPos, yPos)
        {
            color = Color.Beige;
            color.R += (byte)Game1.Random.Next(-5, 5);
            color.G += (byte)Game1.Random.Next(-5, 5);
            color.B += (byte)Game1.Random.Next(-5, 5);
            texture.SetData(new Color[] { color });
            name = "Sand";
            Density = 1800;
        }

        public override void Update()
        {
            base.Update();
            int width = Tilemap.tilemap.GetLength(0);
            int height = Tilemap.tilemap.GetLength(1);
            if (TryMove(0, width, height))
                return;
            if (Game1.Random.Next(2) == 0)
                if (TryMove(1, width, height))
                    return;
                else
                    if (TryMove(-1, width, height))
                    return;
                else
                    if (TryMove(-1, width, height))
                    return;
                else
                    if (TryMove(1, width, height))
                    return;
        }

        private bool TryMove(int direction, int width, int height)
        {
            int newX = X + direction;
            int newY = Y + 1;

            if (newX >= 0 && newX < width && newY < height)
            {
                Element element = Tilemap.GetElementAtIndex(newX, newY);
                if(element == null || (element.IsLiquid && element.Density < Density))
                {
                    Tilemap.SwapPosition(this, newX, newY);
                    return true;
                }
            }
            return false;
        }

    }
}
