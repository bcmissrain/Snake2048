using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Phone.Tasks;
namespace CookLibrary
{
    class AboutDialog
    {
        public SASimpleSprite weiboButton;
        SASimpleSprite background;
         int currentSpeed = 0;
        const int ACC = 2;
        const int MAXSPEED = 60;

        public bool IfUpFinished { get { return background.position.Y+background.Size.Y<=0;} }
        public bool IfDownFinished { get { return background.position.Y >= 0; } }
        public AboutDialog()
        {
            background = new SASimpleSprite("Images/about");
            weiboButton = new SASimpleSprite("Images/about",new Rectangle(90,660,390,60),new Vector2(90,660),Color.White);
            ResetUpFinished();
        }
        public void MoveUp()
        {
            background.position.Y += currentSpeed;
            weiboButton.position.Y += currentSpeed;
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
            weiboButton.position.Y = -140;
            currentSpeed = 0;
        }
        public void ResetDownFinished()
        {
            background.position.Y = 0;
            weiboButton.position.Y = 660;
            currentSpeed = 0;
        }
        public void Draw()
        {
            background.Draw();
        }

        public void CallWeibo()
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.Uri = new Uri(@"http://weibo.com/samurai54", UriKind.Absolute);
            wbt.Show();
        }
    }
}
