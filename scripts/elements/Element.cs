using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogameFallingSand.scripts.elements
{
    public class Element
    {
        #region Display Properties
        protected GraphicsDevice _graphics { get; }
        public Texture2D texture { get; set; }
        public Color color = Color.Magenta;
        public string name { get; set; }
        #endregion

        #region Gameplay Properties
        protected Tilemap Tilemap { get;}
        public int X { get; set; }
        public int Y { get; set; } // position in tilemap
        public float Health { get; set; }
        public float Density { get; set; }
        public float Temprature { get; set; }
        public float Hardness { get; set; }
        public float MeltingPoint { get; set; }
        public float FreezingPoint { get; set; }
        public float Flammability { get; set; }
        public float Conductivity { get; set; }
        public bool IsLiquid { get; set; }
        public bool HasUpdated { get; set; } // så att den inte updateras flera gånger
        #endregion

        public Element(GraphicsDevice GD, Tilemap tm, int xPos, int yPos) 
        {
            _graphics = GD;
            Tilemap = tm;
            texture = new Texture2D(_graphics, 1, 1);
            X = xPos;
            Y = yPos;
            texture.SetData(new Color[] { color });
        }

        public virtual void ResetUpdateFlag()
        {
            HasUpdated = false;
        }

        public virtual void Update()
        {
            if (HasUpdated) return;
        }

        public void MoveTo(int newX, int newY)
        {
            Tilemap.SwapPosition(this, newX, newY);
        }

    }
}
