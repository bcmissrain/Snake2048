using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary
{
    public class MyDirector:SADirector
    {
        #region 收费相关
        public static bool IfTrial { get; set; }
        public static void ReadTrial()
        {
            IfTrial = Guide.IsTrialMode;
        }
        #endregion

        public MyDirector(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch) : base(game, graphics, spriteBatch) 
        {
            ReadTrial();
            GameData.ReadRate();
        }
        public override void LoadFirstScreen()
        {
            ChangeScreenTo(ScreenType.Loading);
        }
        public override void ChangeScreenTo(ScreenType screenType)
        {
            switch(screenType)
            {
                case ScreenType.Menu:
                    ChangeScreenTo(screenType, CreateMenuScreen);
                    break;
                case ScreenType.Ready:
                    ChangeScreenTo(screenType, CreateReadyScreen);
                    break;
                case ScreenType.SingleGame:
                    ChangeScreenTo(screenType, CreateSingleGameScreen);
                    break;
                case ScreenType.DoubleGame:
                    ChangeScreenTo(screenType, CreateDoubleGameScreen);
                    break;
                case ScreenType.LRGame:
                    ChangeScreenTo(screenType, CreateLRGameScreen);
                    break;
                case ScreenType.VSGame:
                    ChangeScreenTo(screenType, CreateVSGameScreen);
                    break;
                case ScreenType.Loading:
                    ChangeScreenTo(screenType, CreateLoadScreen);
                    break;
            }
        }
        public override void OnActivated()
        {
            ReadTrial();
            base.OnActivated();
        }
        public SAScreen CreateMenuScreen()
        {
            return new MenuScreen(ChangeScreenTo);
        }
        public SAScreen CreateReadyScreen() 
        {
            return null;
        }
        public SAScreen CreateSingleGameScreen()
        {
            return new SingleGameScreen(ChangeScreenTo);
        }
        public SAScreen CreateDoubleGameScreen()
        {
            return new DoubleGameScreen(ChangeScreenTo);
        }
        public SAScreen CreateVSGameScreen()
        {
            return new VSGameScreen(ChangeScreenTo);
        }
        public SAScreen CreateLRGameScreen()
        {
            return new LRGameScreen(ChangeScreenTo);
        }
        public SAScreen CreateLoadScreen()
        {
            return new LoadingScreen(this.ChangeScreenTo);
        }
    }
}
