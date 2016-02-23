using Microsoft.Xna.Framework;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.DoubleGame
{
    class PowerBanner
    {
        Snake snake;
        SASimpleSprite[] banners;
        int Index { get { return snake.MAX_COUNT-Snake.MAX_SPEED; } }
        public PowerBanner(Snake s)
        {
            this.snake = s;
            banners = new SASimpleSprite[8];
            for (int i = 0; i < 8; i++)
            {
                banners[i] = new SASimpleSprite("Images/button", new Rectangle(10+i*15, 410, (8-i)*15, 45), new Vector2(180, 688), Color.White);
            }
        }

        public void Draw()
        {
            banners[Index].Draw();
        }
    }
}
