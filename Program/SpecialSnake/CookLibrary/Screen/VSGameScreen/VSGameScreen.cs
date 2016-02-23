using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Phone.Tasks;
using CookLibrary.VSGame;

namespace CookLibrary
{
    class VSGameScreen:SAScreen
    {
        enum Status
        { 
            Normal,
            WinIn,
            Win,
            WinOut,
            MenuIn,
            Menu,
            MenuOut,
            Info
        }
        Status status;

        #region 背景
        SASimpleSprite background;
        SASimpleSprite frameSprite;
        #endregion

        #region 控件
        SASimpleSprite rudderButton0;
        SASimpleSprite rudderButton1;
        SASimpleSprite speedUpButton0;
        SASimpleSprite speedDownButton0;
        SASimpleSprite speedUpButton1;
        SASimpleSprite speedDownButton1;
        Rectangle[] directionRect0s; //Up Down Left Right
        Rectangle[] directionRect1s; //Up Down Left Right
        PowerBanner0 powerBanner0;
        PowerBanner1 powerBanner1;
        ScorePainter0 scorePainter0;
        ScorePainter1 scorePainter1;

        SASimpleSprite vsInfo;
        MenuDialog menuDialog;
        WinDialog winDialog;
        #endregion

        World1 world;
        public VSGameScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }
        public override void LoadContent()
        {
            background = new SASimpleSprite("Images/vsBack");
            frameSprite = new SASimpleSprite("Images/vsFrame");
            vsInfo = new SASimpleSprite("Images/vsInfo");
            rudderButton0 = new SASimpleSprite("Images/vsFrame",new Rectangle(150,10,180,180),new Vector2(150,10),Color.White);
            rudderButton1 = new SASimpleSprite("Images/vsFrame", new Rectangle(150, 610, 180, 180), new Vector2(150, 610), Color.White);
            speedUpButton1 = new SASimpleSprite("Images/vsFrame", new Rectangle(0,650,120,100), new Vector2(0,650), Color.White);
            speedDownButton1 = new SASimpleSprite("Images/vsFrame", new Rectangle(360,650,120,100), new Vector2(360,650), Color.White);
            speedUpButton0 = new SASimpleSprite("Images/vsFrame", new Rectangle(360, 50, 120, 100), new Vector2(360, 50), Color.White);
            speedDownButton0 = new SASimpleSprite("Images/vsFrame", new Rectangle(0, 50, 120, 100), new Vector2(0, 50), Color.White);
            scorePainter0 = new ScorePainter0();
            scorePainter1 = new ScorePainter1();

            directionRect0s = new Rectangle[4];
            directionRect0s[0] = new Rectangle(150, 10, 178, 88);
            directionRect0s[1] = new Rectangle(150, 102, 178, 88);
            directionRect0s[2] = new Rectangle(150, 10, 88, 178);
            directionRect0s[3] = new Rectangle(242, 10, 88, 178);
            directionRect1s = new Rectangle[4];
            directionRect1s[0] = new Rectangle(150, 610, 178, 88);
            directionRect1s[1] = new Rectangle(150, 702, 178, 88);
            directionRect1s[2] = new Rectangle(150, 610, 88, 178);
            directionRect1s[3] = new Rectangle(242, 610, 88, 178);

            menuDialog = new MenuDialog();
            winDialog = new WinDialog();
        }
        public override void Init()
        {
            this.status = Status.Info;
            world = new World1();
            powerBanner0 = new PowerBanner0(World1.snake0);
            powerBanner1 = new PowerBanner1(World1.snake1);
            GameData.ReadDoubleScore();
            GameData.gCurrentScore = 0;
        }
        public override void SetupInput()
        {
            SAInput.EnableBackButton(OnBackButton);
            SAInput.EnableGesture(Microsoft.Xna.Framework.Input.Touch.GestureType.Tap,OnTap);
            SAInput.EnableTouchCollection(OnTouch);
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            switch (status) 
            {
                case Status.Normal:
                    UpdateNormal();
                    break;
                case Status.Menu:
                    UpdateMenu();
                    break;
                case Status.MenuIn:
                    UpdateMenuIn();
                    break;
                case Status.MenuOut:
                    UpdateMenuOut();
                    break;
                case Status.Win:
                    UpdateWin();
                    break;
                case Status.WinIn:
                    UpdateWinIn();
                    break;
                case Status.WinOut:
                    UpdateWinOut();
                    break;
            }
        }
        #region 部分更新
        public void UpdateNormal()
        {
            world.Update();
            if (world.IfCollide()!=-1)
            {
                WinGame();
            }
            else
            {
                if (IfWin() != -1)
                {
                    WinGame();
                }
            }
        }
        private int IfWin()
        {
            if (World1.snake0.bodys.Count() >= 10)
            {
                return 0;
            }
            if (World1.snake1.bodys.Count() >= 10)
            {
                return 1;
            }
            return -1;
        }
        public void WinGame()
        {
            this.status = Status.WinIn;
            winDialog.BeginToMoveIn();
            SAMusicManager.PlaySoundEffect("Sounds/Win");
            GameData.SaveDoubleRecord();
        }

        public void UpdateMenuIn()
        {
            menuDialog.MoveUp();
            if (menuDialog.IfDownFinished)
            {
                menuDialog.ResetDownFinished();
                status = Status.Menu;
            }
        }
        public void UpdateMenuOut()
        {
            menuDialog.MoveUp();
            if (menuDialog.IfUpFinished)
            {
                menuDialog.ResetUpFinished();

                if (menuDialog.choice == MenuDialog.Choice.Continue)
                {
                    status = Status.Normal;
                }
                else if (menuDialog.choice == MenuDialog.Choice.Retry)
                {
                    status = Status.Normal;
                    ReInit();
                }
                else if (menuDialog.choice == MenuDialog.Choice.Leave)
                {
                    ChangeScreenTo(ScreenType.Menu);
                }
            }
        }
        public void UpdateMenu()
        {
        }
        public void UpdateWin()
        {

        }
        public void UpdateWinIn()
        {
            winDialog.MoveUp();
            if (winDialog.IfDownFinished)
            {
                winDialog.ResetDownFinished();
                status = Status.Win;
            }
        }
        public void UpdateWinOut()
        {
            winDialog.MoveUp();
            if (winDialog.IfUpFinished)
            {
                winDialog.ResetUpFinished();
                status = Status.Normal;
                ReInit();
            }
        }
        #endregion
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            background.Draw();
            world.Draw();
            winDialog.Draw(world.WinnerId());
            menuDialog.Draw();
            frameSprite.Draw();
            #region 测试用
            //rudderButton.Draw();
            //speedUpButton.Draw();
            //speedDownButton.Draw();
            #endregion
            scorePainter0.Draw(World1.snake0.bodys.Count());
            scorePainter1.Draw(World1.snake1.bodys.Count());
            powerBanner0.Draw();
            powerBanner1.Draw();
            if (status == Status.Info)
            {
                vsInfo.Draw();
            }
        }
        public void Replay()
        {
            Init();
        }
        #region 响应事件
        private void OnTap(Microsoft.Xna.Framework.Input.Touch.GestureSample gestureSample)
        {
            Rectangle temp = new Rectangle((int)(gestureSample.Position.X), (int)(gestureSample.Position.Y), 1, 1);
            #region Normal
            if (status == Status.Normal) 
            {
            }
            #endregion
            else if (status == Status.Info)
            {
                status = Status.Normal;
                SAMusicManager.PlaySoundEffect("Sounds/Coin");                
            }
            #region Info
            #endregion
            #region Menu
            else if (status == Status.Menu) 
            {
                int choice = menuDialog.IfCollide(temp);
                if (choice != -1)
                {
                    status = Status.MenuOut;
                    menuDialog.BeginToMoveOut();
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (menuDialog.IfCollideSound(temp))
                {
                    menuDialog.SwitchSound();
                }
            }
            #endregion
            #region Win
            else if (status == Status.Win)
            {
                status = Status.WinOut;
                winDialog.BeginToMoveOut();
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
            #endregion
        }
        private void OnTouch(Microsoft.Xna.Framework.Input.Touch.TouchCollection touchCollection)
        {
            if (status == Status.Normal)
            {
                //为了防止连续变向！！！
                bool ifControl0 = false;
                bool ifControl1 = false;
                foreach (TouchLocation tl in touchCollection)
                {
                    Rectangle temp = new Rectangle((int)(tl.Position.X), (int)(tl.Position.Y), 1, 1);
                    if (tl.State == TouchLocationState.Pressed)
                    {
                        #region Snake0
                        if (rudderButton0.IfCollide(temp))
                        {
                            if (!ifControl0)
                            {
                                if (World1.snake0.CanMoveLeft)
                                {
                                    if (directionRect0s[2].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Left, 0);
                                        ifControl0 = true;
                                    }
                                    if (directionRect0s[3].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Right, 0);
                                        ifControl0 = true;
                                    }
                                }
                                else
                                {
                                    if (directionRect0s[0].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Up, 0);
                                        ifControl0 = true;
                                    }
                                    if (directionRect0s[1].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Down, 0);
                                        ifControl0 = true;
                                    }
                                }
                            }
                        }
                        #endregion
                        #region Snake1
                        if (rudderButton1.IfCollide(temp))
                        {
                            #region
                            if (!ifControl1)
                            {
                                if (World1.snake1.CanMoveLeft)
                                {
                                    if (directionRect1s[2].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Left, 1);
                                        ifControl1 = true;
                                    }
                                    if (directionRect1s[3].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Right, 1);
                                        ifControl1 = true;
                                    }
                                }
                                else
                                {
                                    if (directionRect1s[0].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Up, 1);
                                        ifControl1 = true;
                                    }
                                    if (directionRect1s[1].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Down, 1);
                                        ifControl1 = true;
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }

                    if (tl.State == TouchLocationState.Released)
                    {                    
                        #region 加速
                        if (speedUpButton0.IfCollide(temp))
                        {
                            World1.snake0.SpeedUp();
                        }
                        if (speedUpButton1.IfCollide(temp))
                        {
                            World1.snake1.SpeedUp();
                        }
                        if (speedDownButton0.IfCollide(temp))
                        {
                            World1.snake0.SpeedDown();
                        }
                        if (speedDownButton1.IfCollide(temp))
                        {
                            World1.snake1.SpeedDown();
                        }
                        #endregion
                    }
                }
            }
        }
        private void OnBackButton()
        {
            if (status == Status.Normal)
            {
                status = Status.MenuIn;
                menuDialog.BeginToMoveIn();
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
            else if (status == Status.Menu)
            {
                status = Status.MenuOut;
                menuDialog.BeginToMoveOut();
                menuDialog.choice = MenuDialog.Choice.Continue;
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
            else if (status == Status.Win)
            {
                status = Status.WinOut;
                winDialog.BeginToMoveOut();
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
            else if(status == Status.Info)
            {
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
                ChangeScreenTo(ScreenType.Menu);
            }
        }
        #endregion
    }
}
