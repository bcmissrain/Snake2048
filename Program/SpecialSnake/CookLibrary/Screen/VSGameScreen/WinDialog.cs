using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
namespace CookLibrary.VSGame
{
    class WinDialog
    {
        SASimpleSprite[] backgrounds;
         int currentSpeed = 0;
        const int ACC = 2;
        const int MAXSPEED = 50;

        public bool IfUpFinished { get { return backgrounds[0].position.Y+backgrounds[0].Size.Y<=0;} }
        public bool IfDownFinished { get { return backgrounds[0].position.Y >= 0; } }
        public WinDialog()
        {
            backgrounds = new SASimpleSprite[2];
            backgrounds[0] = new SASimpleSprite("Images/vsWin0");
            backgrounds[1] = new SASimpleSprite("Images/vsWin1");
            ResetUpFinished();
        }
        public void MoveUp()
        {
            backgrounds[0].position.Y += currentSpeed;
            backgrounds[1].position.Y += currentSpeed; 
            currentSpeed-=ACC;
        }
        public void BeginToMoveIn()
        {
            this.currentSpeed = MAXSPEED;
        }
        public void BeginToMoveOut()
        {
            this.currentSpeed = -10;
        }
        public void ResetUpFinished()
        {
            backgrounds[0].position.Y = backgrounds[1].position.Y = -640;
            currentSpeed = 0;
        }
        public void ResetDownFinished()
        {
            backgrounds[0].position.Y = backgrounds[1].position.Y = 0;
            currentSpeed = 0;
        }
        public void Draw(int id)
        {
            if (id != -1)
            {
                backgrounds[id % 2].Draw();
            }
        }
    }
}
