using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;

namespace CookLibrary.VSGame
{
    class MenuSoundButton
    {
        SASimpleSprite SoundOnButton;
        SASimpleSprite SoundOffButton;
        bool ifSoundOn { get { return SAMusicManager.IfPlaySound; } }
        public MenuSoundButton()
        {
            SoundOnButton = new SASimpleSprite("Images/button",new Rectangle(300,10,160,74),new Vector2(160,645),Color.White);
            SoundOffButton = new SASimpleSprite("Images/button", new Rectangle(300,100,160,74), new Vector2(160,645), Color.White);
        }
        public bool IfCollide(Rectangle rect)
        {
            if (SoundOnButton.IfCollide(rect))
            {
                return true;
            }
            return false;
        }
        public void Switch()
        {
            if (ifSoundOn)
            {
                SAMusicManager.EnableSoundEffect(false);
            }
            else
            {
                SAMusicManager.EnableSoundEffect(true);
            }
        }
        public void MoveUp(int speed)
        {
            SoundOnButton.position.Y -= speed;
            SoundOffButton.position.Y -= speed;
        }
        public void SetPosY(int y)
        {
            SoundOnButton.position.Y = SoundOffButton.position.Y = y;
        }
        public void Draw()
        {
            if (ifSoundOn)
            {
                SoundOnButton.Draw();
            }
            else
            {
                SoundOffButton.Draw();
            }
        }
    }
}
