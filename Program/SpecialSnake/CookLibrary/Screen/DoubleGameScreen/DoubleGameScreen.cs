using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Phone.Tasks;
using CookLibrary.DoubleGame;

namespace CookLibrary
{
    class DoubleGameScreen:SAScreen
    {
        enum Status
        { 
            Normal,
            LoseIn,
            Lose,
            LoseOut,
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
        SASimpleSprite cheatButton;
        SASimpleSprite cheatFigure;
        SASimpleSprite rudderButton0;
        SASimpleSprite rudderButton1;
        SASimpleSprite speedUpButton;
        SASimpleSprite speedDownButton;
        PowerBanner powerBanner;
        ScorePainter scorePainter;
        Rectangle[] directionRect0s; //Up Down Left Right
        Rectangle[] directionRect1s; //Up Down Left Right

        SASimpleSprite doubleInfo;
        LoseDialog loseDialog;
        MenuDialog menuDialog;
        WinDialog winDialog;
        #endregion

        #region 彩蛋
        public static bool ifCheat;
        #endregion

        World1 world;
        public DoubleGameScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }
        public override void LoadContent()
        {
            background = new SASimpleSprite("Images/doubleBack");
            frameSprite = new SASimpleSprite("Images/doubleFrame");
            doubleInfo = new SASimpleSprite("Images/doubleInfo");
            directionRect0s = new Rectangle[4];
            directionRect0s[0] = new Rectangle(0, 615, 178, 88);
            directionRect0s[1] = new Rectangle(0, 707, 178, 88);
            directionRect0s[2] = new Rectangle(0, 615, 88, 178);
            directionRect0s[3] = new Rectangle(92, 615, 88, 178);
            directionRect1s = new Rectangle[4];
            directionRect1s[0] = new Rectangle(300, 615, 178, 88);
            directionRect1s[1] = new Rectangle(300, 707, 178, 88);
            directionRect1s[2] = new Rectangle(300, 615, 88, 178);
            directionRect1s[3] = new Rectangle(392, 615, 88, 178);

            rudderButton0 = new SASimpleSprite("Images/doubleFrame",new Rectangle(0,615,180,180),new Vector2(0,615),Color.White);
            rudderButton1 = new SASimpleSprite("Images/doubleFrame", new Rectangle(300, 615, 180, 180), new Vector2(300, 615), Color.White);
            speedUpButton = new SASimpleSprite("Images/doubleFrame", new Rectangle(204,740,72,60), new Vector2(204,740), Color.White);
            speedDownButton = new SASimpleSprite("Images/doubleFrame", new Rectangle(204,608,72,60), new Vector2(204,608), Color.White);
            scorePainter = new ScorePainter();
            cheatButton = new SASimpleSprite("Images/button", new Rectangle(300, 210, 64, 64), new Vector2(208, 95), Color.White);
            cheatFigure = new SASimpleSprite("Images/button", new Rectangle(370, 210,26, 62), new Vector2(227, 20), Color.White);
            menuDialog = new MenuDialog();
            loseDialog = new LoseDialog();
            winDialog = new WinDialog();
        }
        public override void Init()
        {
            this.status = Status.Info;
            world = new World1();
            powerBanner = new PowerBanner(World1.snake0);
            GameData.ReadDoubleScore();
            GameData.gCurrentScore = 0;
            ifCheat = false;
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
                case Status.LoseIn:
                    UpdateLoseIn();
                    break;
                case Status.LoseOut:
                    UpdateLoseOut();
                    break;
                case Status.Lose:
                    UpdateLose();
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
            //游戏失败
            if (!ifCheat)
            {
                if (world.IfCollide())
                {
                    LoseGame();
                }
                else
                {
                    if (IfWin())
                    {
                        WinGame();
                    }
                }
            }
            else
            {
                if (IfWin())
                {
                    WinGame();
                }
                else
                {
                    //Lose
                    if (World1.snake0.Length >= 400)
                    {
                        LoseGame();
                    }
                }
            }
        }
        private bool IfWin()
        {
            return GameData.gCurrentScore >= GameData.WIN_SCORE;
        }
        public void WinGame()
        {
            this.status = Status.WinIn;
            winDialog.BeginToMoveIn();
            SAMusicManager.PlaySoundEffect("Sounds/Win");
            GameData.SaveDoubleRecord();
        }
        public void LoseGame()
        {
            this.status = Status.LoseIn;
            loseDialog.BeginToMoveIn();
            SAMusicManager.PlaySoundEffect("Sounds/Die");
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
        public void UpdateLose()
        {

        }
        public void UpdateLoseIn()
        {
            loseDialog.MoveUp();
            if (loseDialog.IfDownFinished)
            {
                loseDialog.ResetDownFinished();
                status = Status.Lose;
            }
        }
        public void UpdateLoseOut()
        {
            loseDialog.MoveUp();
            if (loseDialog.IfUpFinished)
            {
                loseDialog.ResetUpFinished();

                if (loseDialog.choice == LoseDialog.Choice.Continue)
                {
                    status = Status.Normal;
                    ReInit();
                }
                else if(loseDialog.choice == LoseDialog.Choice.Leave)
                {
                    ChangeScreenTo(ScreenType.Menu);
                }
            }
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

                if (winDialog.choice == WinDialog.Choice.Leave)
                {
                    ChangeScreenTo(ScreenType.Menu);
                }
                else if (winDialog.choice == WinDialog.Choice.Rate)
                {
                    MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                    ChangeScreenTo(ScreenType.Menu);
                }
            }
        }
        #endregion
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            background.Draw();
            world.Draw();
            winDialog.Draw();
            loseDialog.Draw();
            menuDialog.Draw();
            frameSprite.Draw();
            if (ifCheat)
            { 
                cheatFigure.Draw();
            }
            if (GameData.gRate || ifCheat)
            {
                cheatButton.Draw();
            }
            #region 测试用
            //rudderButton.Draw();
            //speedUpButton.Draw();
            //speedDownButton.Draw();
            #endregion
            scorePainter.Draw();
            powerBanner.Draw();
            if (status == Status.Info)
            {
                doubleInfo.Draw();
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
                if (speedUpButton.IfCollide(temp))
                {
                    World1.snake0.SpeedUp();
                    World1.snake1.SpeedUp();
                    return;
                }
                if (speedDownButton.IfCollide(temp))
                {
                    World1.snake0.SpeedDown();
                    World1.snake1.SpeedDown();
                    return;
                }
                if (cheatButton.IfCollide(temp))
                {
                    ifCheat = !ifCheat;
                    if (ifCheat)
                    {
                        SAMusicManager.PlaySoundEffect("Sounds/eye");
                    }
                    else
                    {
                        SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    }
                }
            }
            #endregion
            else if (status == Status.Info)
            {
                status = Status.Normal;
                SAMusicManager.PlaySoundEffect("Sounds/Coin");                
            }
            #region Info
            #endregion
            #region Lose
            else if (status == Status.Lose)
            {
                int choice = loseDialog.ifCollide(temp);
                if (choice != -1)
                {
                    status = Status.LoseOut;
                    loseDialog.BeginToMoveOut();
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                }
            }
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
                int choice = winDialog.ifCollide(temp);
                if (choice != -1)
                {
                    status = Status.WinOut;
                    winDialog.BeginToMoveOut();
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                }
            }
            #endregion
        }
        private void OnTouch(Microsoft.Xna.Framework.Input.Touch.TouchCollection touchCollection)
        {
            if (status == Status.Normal)
            {
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
            else if (status == Status.Lose)
            {
                status = Status.LoseOut;
                loseDialog.BeginToMoveOut();
                loseDialog.choice = LoseDialog.Choice.Continue;
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
                winDialog.choice = WinDialog.Choice.Leave;
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
