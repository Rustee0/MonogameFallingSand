using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFallingSand.scripts.elements.Liquid
{
    public class Water : Liquid
    {
        public Water(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) : base(GD, tm, xPos, yPos)
        {
            color = Color.CadetBlue;
            /*color.R += (byte)Game1.Random.Next(-5, 5);
            color.G += (byte)Game1.Random.Next(-5, 5);
            color.B += (byte)Game1.Random.Next(-5, 5);*/
            texture.SetData(new Color[] { color });
            name = "Water";
            Density = 1000;
        }

        public override void Update()
        {
            base.Update();
            int width = Tilemap.tilemap.GetLength(0);
            int height = Tilemap.tilemap.GetLength(1);
            if (TryMove(0, 1, width, height))
                return;
            if (Game1.Random.Next(2) == 0)
                if (TryMove(1, 1, width, height))
                    return;
                else
                    if (TryMove(-1, 1, width, height))
                    return;
                else
                    if (TryMove(-1, 1, width, height))
                    return;
                else
                    if (TryMove(1, 1, width, height))
                    return;

            if ((Game1.Random.Next(2)) == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (i != 0 && TryMove(i, 0, width, height))
                        return;
                }
            }

            if ((Game1.Random.Next(2)) == 0)
            {
                for (int i = -7; i < 0; i++)
                {
                    if (i != 0 && TryMove(i, 0, width, height))
                        return;
                }
            }
        }

        protected override bool TryMove(int dirX, int dirY, int width, int height)
        {
            return base.TryMove(dirX, dirY, width, height);
        }
    }
}
