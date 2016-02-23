using Microsoft.Xna.Framework;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.VSGame
{
    class PowerBanner0
    {
        Snake snake;
        SASimpleSprite[] banners;
        int Index { get { return snake.MAX_COUNT - Snake.MAX_SPEED; } }
        public PowerBanner0(Snake s)
        {
            this.snake = s;
            banners = new SASimpleSprite[8];
            banners[0] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 460, 20), new Vector2(10, 13), Color.White);
            banners[1] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 420, 20), new Vector2(10, 13), Color.White);
            banners[2] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 380, 20), new Vector2(10, 13), Color.White);
            banners[3] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 340, 20), new Vector2(10, 13), Color.White);
            banners[4] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 150, 20), new Vector2(10, 13), Color.White);
            banners[5] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 110, 20), new Vector2(10, 13), Color.White);
            banners[6] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 70, 20), new Vector2(10, 13), Color.White);
            banners[7] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 30, 20), new Vector2(10, 13), Color.White);
        }
        public void Draw()
        {
            banners[Index].Draw();
        }
    }

    class PowerBanner1
    {
        Snake snake;
        SASimpleSprite[] banners;
        int Index { get { return snake.MAX_COUNT - Snake.MAX_SPEED; } }
        public PowerBanner1(Snake s)
        {
            this.snake = s;
            banners = new SASimpleSprite[8];
            banners[0] = new SASimpleSprite("Images/button", new Rectangle(10, 767, 460, 20), new Vector2(10, 767), Color.White);
            banners[1] = new SASimpleSprite("Images/button", new Rectangle(50, 767, 420, 20), new Vector2(50, 767), Color.White);
            banners[2] = new SASimpleSprite("Images/button", new Rectangle(90, 767, 380, 20), new Vector2(90, 767), Color.White);
            banners[3] = new SASimpleSprite("Images/button", new Rectangle(130, 767, 340, 20), new Vector2(130, 767), Color.White);
            banners[4] = new SASimpleSprite("Images/button", new Rectangle(320, 767, 150, 20), new Vector2(320, 767), Color.White);
            banners[5] = new SASimpleSprite("Images/button", new Rectangle(360, 767, 110, 20), new Vector2(360, 767), Color.White);
            banners[6] = new SASimpleSprite("Images/button", new Rectangle(400, 767, 70, 20), new Vector2(400, 767), Color.White);
            banners[7] = new SASimpleSprite("Images/button", new Rectangle(440, 767, 30, 20), new Vector2(440, 767), Color.White);
        }
        public void Draw()
        {
            banners[Index].Draw();
        }
    }
}
