using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
namespace CookLibrary
{
    class PKDialog
    {
        SASimpleSprite background;
         int currentSpeed = 0;
        const int ACC = 2;
        const int MAXSPEED = 60;

        public bool IfUpFinished { get { return background.position.Y+background.Size.Y<=0;} }
        public bool IfDownFinished { get { return background.position.Y >= 0; } }
        public PKDialog()
        {
            background = new SASimpleSprite("Images/pk");
            ResetUpFinished();
        }
        public void MoveUp()
        {
            background.position.Y += currentSpeed; 
            currentSpeed-=ACC;
        }
        public void BeginToMoveIn()
        {
            this.currentSpeed = MAXSPEED;
        }
        public void BeginToMoveOut()
        {
            this.currentSpeed = -15;
        }
        public void ResetUpFinished()
        {
            background.position.Y = -800;
            currentSpeed = 0;
        }
        public void ResetDownFinished()
        {
            background.position.Y = 0;
            currentSpeed = 0;
        }
        public void Draw()
        {
            background.Draw();
        }
    }
}
