using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.LRGame
{
    class ScorePainter0
    {
        public const int MAX_LENGTH = 10;
        SASimpleSprite[] scores;
        public ScorePainter0()
        {
            scores = new SASimpleSprite[MAX_LENGTH];
            for (int i = 0; i < MAX_LENGTH; i++)
            {
                scores[i] = new SASimpleSprite("Images/button", new Rectangle(410,210,20,40), new Vector2(12,200+i*40), Color.White);
            }
        }
        public void Draw(int length)
        {
            if(length>MAX_LENGTH)
            {
                length = MAX_LENGTH;
            }
            for (int i = MAX_LENGTH-1; i >= length ;i--)
            {
                scores[i].Draw();
            }
        }
    }

    class ScorePainter1
    {
        public const int MAX_LENGTH = 10;
        SASimpleSprite[] scores;
        public ScorePainter1()
        {
            scores = new SASimpleSprite[MAX_LENGTH];
            for (int i = 0; i < MAX_LENGTH; i++)
            {
                scores[i] = new SASimpleSprite("Images/button", new Rectangle(410,210,20,40), new Vector2(448,560-i*40), Color.White);
            }
        }
        public void Draw(int length)
        {
            if(length>MAX_LENGTH)
            {
                length = MAX_LENGTH;
            }
            for (int i = MAX_LENGTH-1; i >= length ;i--)
            {
                scores[i].Draw();
            }
        }
    }

}
