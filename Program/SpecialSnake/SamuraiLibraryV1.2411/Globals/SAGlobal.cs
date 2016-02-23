using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai
{
    public static class SAGlobal
    {
        public static Game game { get; set; }
        //�ڴ�������פ������Դ
        public static ContentManager Content { get; set; }
        //��ҳ��Ϊ��������פ������Դ
        public static ContentManager TemporalContent { get; set; }
        public static SpriteBatch spriteBatch { get; set; }
        public static GraphicsDeviceManager graphics { get; set; }
        public static Random random { get; set; }
        public static void Setup(Game game, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            SAGlobal.game = game;
            SAGlobal.Content = game.Content;
            SAGlobal.TemporalContent = CreateContentManager();
            SAGlobal.spriteBatch = spriteBatch;
            SAGlobal.graphics = graphics;
            SAGlobal.random = new Random();
        }

        public static ContentManager CreateContentManager()
        {
            return new ContentManager(Content.ServiceProvider, Content.RootDirectory);
        }

        public static void CleanTemporalContent()
        {
            if (TemporalContent != null)
            {
                TemporalContent.Unload();
            }
        }
    }
}
