using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai;
using Samurai.Sprites;
using Microsoft.Xna.Framework;
using CookLibrary.Menu;
using Microsoft.Phone.Tasks;

namespace CookLibrary
{
    class MenuScreen:SAScreen
    {
        enum Status 
        {
            Normal,
            PK,
            PKIn,
            PKOut,
            About,
            AboutIn,
            AboutOut
        }

        #region Buttons
        SASimpleSprite rateButton;
        SASimpleSprite aboutButton;
        SASimpleSprite tipSprite;
        SoundButton soundButton;
        MusicButton musicButton;
        SASimpleSprite playButton0;
        SASimpleSprite playButton1;
        SASimpleSprite playButton2;
        SASimpleSprite playButton3;
        PKDialog pkDialog;
        AboutDialog aboutDialog;
        #endregion

        SASimpleSprite background;
        SASimpleSprite frame;
        Status status;
        public MenuScreen(ChangeScreenDelegate changeScreenDelegate) : base(changeScreenDelegate) { }

        public override void LoadContent()
        {
            background = new SASimpleSprite("Images/menuBack");
            frame = new SASimpleSprite("Images/menuFrame");
            rateButton = new SASimpleSprite("Images/button",new Rectangle(0,0,120,120),new Vector2(0,0),Color.White);
            aboutButton = new SASimpleSprite("Images/button", new Rectangle(120,0,110,110), new Vector2(370,690), Color.White);
            playButton0 = new SASimpleSprite("Images/button", new Rectangle(10,130, 280, 80), new Vector2(100, 320), Color.White);
            playButton1 = new SASimpleSprite("Images/button", new Rectangle(10, 570, 280, 80), new Vector2(100, 420), Color.White);
            playButton2 = new SASimpleSprite("Images/button", new Rectangle(10, 224, 280, 80), new Vector2(100, 520), Color.White);
            playButton3 = new SASimpleSprite("Images/button", new Rectangle(10, 319, 280, 80), new Vector2(100, 620), Color.White);
            tipSprite = new SASimpleSprite("Images/button", new Rectangle(0,480,290,80), new Vector2(0,0),Color.White);
            soundButton = new SoundButton();
            musicButton = new MusicButton();
            if (SAMusicManager.CanPlayMusic())
            {
                SAMusicManager.PlaySong("Sounds/music");
            }

            pkDialog = new PKDialog();
            aboutDialog = new AboutDialog();
        }
        public override void SetupInput()
        {
            SAInput.EnableBackButton(OnBack);
            SAInput.EnableGesture(Microsoft.Xna.Framework.Input.Touch.GestureType.Tap,OnTap);
        }
        public override void Init()
        {
            status = Status.Normal;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            switch (status)
            { 
                case Status.Normal:
                    UpdateNormal();
                    break;
                case Status.AboutIn:
                    UpdateAboutIn();
                    break;
                case Status.AboutOut:
                    UpdateAboutOut();
                    break;
                case Status.PKIn:
                    UpdatePKIn();
                    break;
                case Status.PKOut:
                    UpdatePKOut();
                    break;
                case Status.About:
                    UpdateAbout();
                    break;
                case Status.PK:
                    UpdatePK();
                    break;
            }
        }
        #region Update
        public void UpdateNormal()
        { 
            
        }
        public void UpdatePK()
        {

        }
        public void UpdatePKIn()
        {
            pkDialog.MoveUp();
            if (pkDialog.IfDownFinished)
            {
                pkDialog.ResetDownFinished();
                status = Status.PK;
            }
        }
        public void UpdatePKOut()
        {
            pkDialog.MoveUp();
            if (pkDialog.IfUpFinished)
            {
                pkDialog.ResetUpFinished();
                status = Status.Normal;
            }
        }
        public void UpdateAbout()
        {

        }
        public void UpdateAboutIn()
        {
            aboutDialog.MoveUp();
            if (aboutDialog.IfDownFinished)
            {
                aboutDialog.ResetDownFinished();
                status = Status.About ;
            }
        }
        public void UpdateAboutOut()
        {
                aboutDialog.MoveUp();
                if (aboutDialog.IfUpFinished)
                {
                    aboutDialog.ResetUpFinished();
                    status = Status.Normal;
                }
        }
        #endregion
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            background.Draw();
            playButton0.Draw();
            playButton1.Draw();
            playButton2.Draw();
            playButton3.Draw();
            frame.Draw();
            musicButton.Draw();
            soundButton.Draw();
            rateButton.Draw();
            aboutButton.Draw();
            if (!GameData.gRate)
            {
                tipSprite.Draw();
            }
            pkDialog.Draw();
            aboutDialog.Draw();
        }

        #region 事件处理
        public void OnBack()
        {
            if (status == Status.Normal)
            {
                SAGlobal.game.Exit();
            }
            else if (status == Status.PK)
            {
                status = Status.PKOut;
                pkDialog.BeginToMoveOut();
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
            else if (status == Status.About)
            {
                status = Status.AboutOut;
                aboutDialog.BeginToMoveOut();
                SAMusicManager.PlaySoundEffect("Sounds/Coin");
            }
        }
        private void OnTap(Microsoft.Xna.Framework.Input.Touch.GestureSample gestureSample)
        {
            Rectangle tempRect = new Rectangle((int)(gestureSample.Position.X), (int)(gestureSample.Position.Y), 1, 1);
            #region Normal
            if (status == Status.Normal)
            {
                if (playButton0.IfCollide(tempRect))
                {
                    ChangeScreenTo(ScreenType.SingleGame);
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (playButton1.IfCollide(tempRect))
                {
                    ChangeScreenTo(ScreenType.DoubleGame);
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (playButton2.IfCollide(tempRect))
                {
                    ChangeScreenTo(ScreenType.LRGame);
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (playButton3.IfCollide(tempRect))
                {
                    ChangeScreenTo(ScreenType.VSGame);
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (soundButton.IfCollide(tempRect))
                {
                    soundButton.Switch();
                    return;
                }
                if (aboutButton.IfCollide(tempRect))
                {
                    status = Status.AboutIn;
                    aboutDialog.BeginToMoveIn();
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    return;
                }
                if (musicButton.IfCollide(tempRect))
                {
                    musicButton.Switch();
                    return;
                }
                if (rateButton.IfCollide(tempRect))
                {
                    SAMusicManager.PlaySoundEffect("Sounds/Coin");
                    MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
                    marketplaceReviewTask.Show();
                    GameData.SaveRate();
                    GameData.gRate = true;
                    return;
                }
            }
            #endregion
            #region PK
            else if (status == Status.PK)
            {
                OnBack();
            }
            #endregion
            #region About
            else if (status == Status.About)
            {
                if (aboutDialog.weiboButton.IfCollide(tempRect))
                {
                    aboutDialog.CallWeibo();
                    return;
                }
                else 
                {
                    OnBack();
                }
            }
            #endregion
        }
        #endregion
    }
}
