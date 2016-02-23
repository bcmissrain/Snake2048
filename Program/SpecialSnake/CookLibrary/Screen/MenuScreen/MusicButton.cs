using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;

namespace CookLibrary.Menu
{
    class MusicButton
    {
        SASimpleSprite musicOnButton;
        SASimpleSprite musicOffButton;
        bool ifMusicOn{get{return SAMusicManager.IfPlayingSong();}}
        public MusicButton()
        {
            musicOnButton = new SASimpleSprite("Images/button",new Rectangle(300,320,102,102),new Vector2(380,0),Color.White);
            musicOffButton = new SASimpleSprite("Images/button", new Rectangle(300,460,102,102), new Vector2(380,0), Color.White);
        }
        public bool IfCollide(Rectangle rect)
        {
            if (musicOnButton.IfCollide(rect))
            {
                return true;
            }
            return false;
        }
        public void Switch()
        {
            if (ifMusicOn)
            {
                SAMusicManager.StopSong();
            }
            else
            {
                SAMusicManager.PlaySong("Sounds/music");
            }
        }
        public void Draw()
        {
            if (ifMusicOn)
            {
                 musicOnButton.Draw();
            }
            else
            {
                musicOffButton.Draw();
            }

        }
    }
}
