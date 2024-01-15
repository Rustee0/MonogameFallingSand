using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameFallingSand.scripts.elements;
using MonogameFallingSand.scripts.elements.Solid;

namespace MonogameFallingSand.scripts
{
    public class Tilemap
    {
        public Element[,] tilemap;

        public int gridSize;
        public int tileSize;

        private bool LeftToRight = true;
        public int ElementCount = 0;

        public Tilemap(int sizeOfGrid, int sizeOfTiles) 
        {
            gridSize = sizeOfGrid;
            tileSize = sizeOfTiles;
            tilemap = new Element[(int)(gridSize / tileSize), (int)(gridSize / tileSize)];
        }

        public Element GetElementAtIndex(int x, int y)
        {
            return tilemap[x, y];
        }

        public void SetElementAtIndex(int x, int y,Element element)
        {
            tilemap[x, y] = element;
        }

        public void SwapPosition(Element element, int newX, int newY)
        {
            if (element.X == newX && element.Y == newY) return;

            Element toSwap = GetElementAtIndex(newX, newY);
            SetElementAtIndex(element.X, element.Y, toSwap);

            if (toSwap != null)
            {
                toSwap.X = element.X;
                toSwap.Y = element.Y;
            }

            SetElementAtIndex(newX, newY, element);
            element.X = newX;
            element.Y = newY;
        }

        public void Update()
        {
            if(LeftToRight)
            {
                for (int x = 0; x < tilemap.GetLength(0); x++)
                {
                    for (int y = tilemap.GetLength(1) - 1; y >= 0; y--)
                    {
                        Element element = tilemap[x, y];
                        if (element != null)
                        {
                            element.Update();
                            element.ResetUpdateFlag();
                        }
                    }
                }
                LeftToRight = !LeftToRight;
                return;
            }

            for (int x = tilemap.GetLength(0) - 1; x >= 0; x--)
            {
                for (int y = tilemap.GetLength(1) - 1; y >= 0; y--)
                {
                    Element element = tilemap[x, y];
                    if (element != null)
                    {
                        element.Update();
                        element.ResetUpdateFlag();
                    }
                }
            }
            LeftToRight = !LeftToRight;
        }

        public void Draw(SpriteBatch sb)
        {
            ElementCount = 0;
            foreach (Element e in tilemap)
            {
                if(e != null)
                {
                    sb.Draw(e.texture, new Rectangle(e.X * tileSize, e.Y * tileSize, tileSize, tileSize), e.color);
                    ElementCount++;
                }
            }
        }
    }
}
