using Microsoft.Xna.Framework;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;

namespace CookLibrary
{
    class LogoSprite:SAActionSprite
    {
        int counter;
        public override Rectangle sourceRectangle
        {
            get
            {
                return _sourceRectangles[Math.Abs(index)];
            }
        }
        public LogoSprite()
        {
            this.texture = SAGraphicManager.GetImage("Images/Logo");
            this._sourceRectangles = new Rectangle[8];
            this._sizes = new Vector2[8];
            this.color = Color.White;
            
            for (int i = 0; i < 4; i++)
            {
                this._sourceRectangles[i] = new Rectangle(10,10+i*130,290,120);
                this._sourceRectangles[i+4] = new Rectangle(330,10+i*130,290,120);
                this._sizes[i] = new Vector2(290,120);
                this._sizes[i + 4] = new Vector2(290,120);
            }
            this.position = new Vector2(95,280);
            index = -1;
        }
        public void Update()
        {
            counter++;
            if (counter >= 6)
            {
                index++;
                if (index >= 8)
                {
                    index = -7;
                }

                counter = 0;
            }
        }
    }
}
