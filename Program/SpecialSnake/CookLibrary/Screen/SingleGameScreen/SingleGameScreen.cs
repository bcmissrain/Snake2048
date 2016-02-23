using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Phone.Tasks;
using CookLibrary.SingleGame;

namespace CookLibrary
{
    class SingleGameScreen:SAScreen
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
        SASimpleSprite rudderButton;
        SASimpleSprite speedUpButton;
        SASimpleSprite speedDownButton;
        PowerBanner powerBanner;
        ScorePainter scorePainter;
        Rectangle[] directionRects; //Up Down Left Right
        SASimpleSprite singleInfo;
        LoseDialog loseDialog;
        MenuDialog menuDialog;
        WinDialog winDialog;
        #endregion

        #region 彩蛋
        public static bool ifCheat;
        #endregion

        World world;
        public SingleGameScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }
        public override void LoadContent()
        {
            background = new SASimpleSprite("Images/singleBack");
            frameSprite = new SASimpleSprite("Images/singleFrame");
            singleInfo = new SASimpleSprite("Images/singleInfo");
            directionRects = new Rectangle[4];
            directionRects[0] = new Rectangle(150,610,178,88);
            directionRects[1] = new Rectangle(150,702,178,88);
            directionRects[2] = new Rectangle(150,610,88,178);
            directionRects[3] = new Rectangle(242, 610, 88, 178);
            rudderButton = new SASimpleSprite("Images/singleFrame",new Rectangle(150,610,180,180),new Vector2(150,610),Color.White);
            speedUpButton = new SASimpleSprite("Images/singleFrame", new Rectangle(15,655,90,90), new Vector2(15,655), Color.White);
            speedDownButton = new SASimpleSprite("Images/singleFrame", new Rectangle(375,655,90,90), new Vector2(375,655), Color.White);
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
            world = new World();
            powerBanner = new PowerBanner(World.snake);
            GameData.ReadSingleScore();
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
                    if (World.snake.Length >= 400)
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
            GameData.SaveSingleRecord();
        }
        public void LoseGame()
        {
            this.status = Status.LoseIn;
            loseDialog.BeginToMoveIn();
            SAMusicManager.PlaySoundEffect("Sounds/Die");
            GameData.SaveSingleRecord();
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
            if (ifCheat || GameData.gRate)
            {
                cheatButton.Draw();
            }
            if (ifCheat)
            { 
                cheatFigure.Draw();
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
                singleInfo.Draw();
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
            else if (status == Status.Info)
            {
                status = Status.Normal;
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
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
                foreach (TouchLocation tl in touchCollection)
                {
                    Rectangle temp = new Rectangle((int)(tl.Position.X), (int)(tl.Position.Y), 1, 1);
                    if (rudderButton.IfCollide(temp))
                    {
                        #region
                        if (tl.State == TouchLocationState.Pressed)
                        {
                            if (!ifControl0)
                            {
                                if (World.snake.CanMoveLeft)
                                {
                                    if (directionRects[2].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Left);
                                        ifControl0 = true;
                                    }
                                    if (directionRects[3].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Right);
                                        ifControl0 = true;
                                    }
                                }
                                else
                                {
                                    if (directionRects[0].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Up);
                                        ifControl0 = true;
                                    }
                                    if (directionRects[1].Intersects(temp))
                                    {
                                        world.TurnTo(BodyDirection.Down);
                                        ifControl0 = true;
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    if (tl.State == TouchLocationState.Released) 
                    {
                        if (speedUpButton.IfCollide(temp))
                        {
                            World.snake.SpeedUp();
                        }
                        if (speedDownButton.IfCollide(temp))
                        {
                            World.snake.SpeedDown();
                        }
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
            else if (status == Status.Info)
            {
                ChangeScreenTo(ScreenType.Menu);
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
        }
        #endregion
    }
}
