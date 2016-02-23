using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
namespace CookLibrary.DoubleGame
{
    class MenuDialog
    {
        public enum Choice 
        {
            Continue,
            Retry,
            Leave
        }
        //0 continue 1retry 2Leave
        SASimpleSprite[] buttons;
        MenuSoundButton soundButton;
        SASimpleSprite background;
        public Choice choice;
         int currentSpeed = 0;
        const int ACC = 2;
        const int MAXSPEED = 50;

        public bool IfUpFinished { get { return background.position.Y+background.Size.Y<=0;} }
        public bool IfDownFinished { get { return background.position.Y >= 0; } }
        public MenuDialog()
        {
            background = new SASimpleSprite("Images/gameMenu");
            this.choice = Choice.Continue;
            buttons = new SASimpleSprite[3];
            buttons[1] = new SASimpleSprite("Images/gameMenu", new Rectangle(80,230,320,80), new Vector2(80, 230), Color.White); //Retry
            buttons[2] = new SASimpleSprite("Images/gameMenu", new Rectangle(80,330,320,80), new Vector2(80, 330), Color.White);//Leave
            buttons[0] = new SASimpleSprite("Images/gameMenu", new Rectangle(80, 430, 320, 80), new Vector2(80, 430), Color.White);//Continue
            soundButton = new MenuSoundButton();
            soundButton.SetPosY(528);
            ResetUpFinished();
        }
        public void MoveUp()
        {
            background.position.Y += currentSpeed; 
            foreach(SASimpleSprite ss in buttons)
            {
                ss.position.Y  += currentSpeed;
            }
            soundButton.MoveUp(-currentSpeed);
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
            buttons[0].position.Y =-410;
            buttons[1].position.Y = -310;
            buttons[2].position.Y = -210;
            soundButton.SetPosY(-112);
            currentSpeed = 0;
        }
        public void ResetDownFinished()
        {
            background.position.Y = 0;
            buttons[0].position.Y = 230;
            buttons[1].position.Y = 330;
            buttons[2].position.Y = 430;
            soundButton.SetPosY(528);
            currentSpeed = 0;
        }
        public int IfCollide(Rectangle rect)
        {
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i].IfCollide(rect))
                {
                    this.choice = (Choice)i;
                    return i;    
                }
            }
            return -1;
        }
        public bool IfCollideSound(Rectangle rect)
        {
            return soundButton.IfCollide(rect);
        }
        public void SwitchSound()
        {
            soundButton.Switch();
        }
        public void Draw()
        {
            background.Draw();
            foreach (SASimpleSprite s in buttons)
            {
                s.Draw();
            }
            soundButton.Draw();
        }
    }
}
