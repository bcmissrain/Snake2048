using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
namespace CookLibrary.DoubleGame
{
    class WinDialog
    {
        public enum Choice 
        {
            Leave,
            Rate
        }
        //0 continue 1quit
        SASimpleSprite[] buttons;
        SASimpleSprite background;
        public Choice choice;
         int currentSpeed = 0;
        const int ACC = 2;
        const int MAXSPEED = 50;

        public bool IfUpFinished { get { return background.position.Y+background.Size.Y<=0;} }
        public bool IfDownFinished { get { return background.position.Y >= 0; } }
        public WinDialog()
        {
            background = new SASimpleSprite("Images/doubleWin");
            this.choice = Choice.Leave;
            buttons = new SASimpleSprite[2];
            buttons[0] = new SASimpleSprite("Images/doubleWin", new Rectangle(80, 480, 125, 55), new Vector2(80, 480), Color.White);
            buttons[1] = new SASimpleSprite("Images/doubleWin", new Rectangle(275, 480, 125, 55), new Vector2(275, 480), Color.White);
            ResetUpFinished();
        }
        public void MoveUp()
        {
            background.position.Y += currentSpeed; 
            foreach(SASimpleSprite ss in buttons)
            {
                ss.position.Y  += currentSpeed;
            }
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
            background.position.Y = -640;
            buttons[0].position.Y =-160;
            buttons[1].position.Y = -160;
            currentSpeed = 0;
        }
        public void ResetDownFinished()
        {
            background.position.Y = 0;
            buttons[0].position.Y = 480;
            buttons[1].position.Y = 480;
            currentSpeed = 0;
        }
        public int ifCollide(Rectangle rect)
        {
            for (int i = 0; i < 2; i++)
            {
                if (buttons[i].IfCollide(rect))
                {
                    this.choice = (Choice)i;
                    return i;    
                }
            }
            return -1;
        }
        public void Draw()
        {
            background.Draw();
        }
    }
}
