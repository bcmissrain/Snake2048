using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai.Sprites;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CookLibrary
{
    class LoadingScreen:SAScreen
    {
        LogoSprite logoSprite;
        bool ifLoadAllResource;
        const int MAX_COUNT = 96;
        int counter;

        public LoadingScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }

        #region 游戏流程
        public override void LoadContent()
        {
            SAGraphicManager.GetImage("Images/Logo");
            ifLoadAllResource = false;
            counter = 0;
            new Thread(new ThreadStart(LoadAllResources)).Start();
        }

        public override void Init()
        {
            logoSprite = new LogoSprite();
        }
        public override void SetupInput()
        {
            SAInput.EnableBackButton(onBackButton);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            logoSprite.Update();
            counter++;
            if (counter > MAX_COUNT && ifLoadAllResource)
            {
                ChangeScreenTo(ScreenType.Menu);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            logoSprite.Draw(SAGlobal.spriteBatch);
        }

        private void LoadAllResources()
        {
            #region 加载音效
            SAMusicManager.LoadSoundEffect("Sounds/Coin");
            SAMusicManager.LoadSoundEffect("Sounds/Die");
            SAMusicManager.LoadSoundEffect("Sounds/Win");
            SAMusicManager.LoadSoundEffect("Sounds/eat");
            SAMusicManager.LoadSoundEffect("Sounds/eye");
            #endregion

            #region 加载图像
            SAGraphicManager.GetImage("Images/about");
            SAGraphicManager.GetImage("Images/askContinue");
            SAGraphicManager.GetImage("Images/button");
            SAGraphicManager.GetImage("Images/doubleBack");
            SAGraphicManager.GetImage("Images/doubleFrame");
            SAGraphicManager.GetImage("Images/doubleInfo");
            SAGraphicManager.GetImage("Images/doubleWin");
            SAGraphicManager.GetImage("Images/floor");
            SAGraphicManager.GetImage("Images/gameMenu");
            SAGraphicManager.GetImage("Images/menuBack");
            SAGraphicManager.GetImage("Images/menuFrame"); 
            SAGraphicManager.GetImage("Images/pk");
            SAGraphicManager.GetImage("Images/singleBack");
            SAGraphicManager.GetImage("Images/singleFrame");
            SAGraphicManager.GetImage("Images/singleInfo");
            SAGraphicManager.GetImage("Images/singleWin");
            SAGraphicManager.GetImage("Images/snake");
            SAGraphicManager.GetImage("Images/Snake0");
            SAGraphicManager.GetImage("Images/Snake1");
            #endregion

            ifLoadAllResource = true;
        }
        #endregion

        #region 响应事件
        private void onBackButton()
        {
            SAGlobal.game.Exit();
        }
        #endregion
    }
}
