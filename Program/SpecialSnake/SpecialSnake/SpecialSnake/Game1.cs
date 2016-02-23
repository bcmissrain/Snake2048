using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using Samurai;
using CookLibrary;

namespace SpecialSnake
{
    /// <summary>
    /// 这是游戏的主类型
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        MyDirector director;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // Windows Phone 的默认帧速率为 30 fps。
            TargetElapsedTime = TimeSpan.FromTicks(333333);
            // 延长锁定时的电池寿命。
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            SAGraphicManager.SetVertical(graphics);
            graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: 在此处添加初始化逻辑

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            director = new MyDirector(this, graphics, spriteBatch);
        }

        protected override void UnloadContent()
        {
            director.UnloadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            director.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            director.Draw(gameTime);
            base.Draw(gameTime);
        }
        protected override void OnActivated(object sender, EventArgs args)
        {
            director.OnActivated();
        }
    }
}
