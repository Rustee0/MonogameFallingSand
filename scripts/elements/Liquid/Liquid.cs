using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFallingSand.scripts.elements.Liquid
{
    public class Liquid : Element
    {
        public Liquid(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) : base(GD, tm, xPos, yPos)
        {
            IsLiquid = true;
        }

        public override void Update()
        {
            base.Update();
        }

        protected virtual bool TryMove(int dirX, int dirY, int width, int height)
        {
            int newX = X + dirX;
            int newY = Y + dirY;

            if (newX >= 0 && newX < width && newY < height)
            {
                Element element = Tilemap.GetElementAtIndex(newX, newY);
                if (element == null || (element.IsLiquid && element.Density < Density))
                {
                    Tilemap.SwapPosition(this, newX, newY);
                    return true;
                }
            }
            return false;
        }
    }
}
