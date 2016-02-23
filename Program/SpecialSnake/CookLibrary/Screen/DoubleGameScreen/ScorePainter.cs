using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.DoubleGame
{
    class ScorePainter
    {
        SpriteFont font;
        Vector2 currentPos;
        Vector2 bestPos;
        public ScorePainter()
        {
            this.font = SAGlobal.Content.Load<SpriteFont>("Font/ScoreFont");
            this.currentPos = new Vector2(70,80);
            this.bestPos = new Vector2(285,80);
        }
        public void Draw()
        {
            SAGlobal.spriteBatch.DrawString(font, GameData.gBestDoubleScore.ToString(), bestPos, Color.White);
            SAGlobal.spriteBatch.DrawString(font, GameData.gCurrentScore.ToString(), currentPos, Color.White);
        }
    }
}
