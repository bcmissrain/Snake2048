using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;

namespace CookLibrary.Menu
{
    class SoundButton
    {
        SASimpleSprite SoundOnButton;
        SASimpleSprite SoundOffButton;
        bool ifSoundOn{get{return SAMusicManager.IfPlaySound;}}
        public SoundButton()
        {
            SoundOnButton = new SASimpleSprite("Images/button",new Rectangle(10,652,101,101),new Vector2(0,699),Color.White);
            SoundOffButton = new SASimpleSprite("Images/button", new Rectangle(115,652,101,101), new Vector2(0,699), Color.White);
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
