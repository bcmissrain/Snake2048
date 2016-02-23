using Microsoft.Xna.Framework;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary
{
    public class GameManager
    {
        public enum GameStatus
        {
            Play,
            Win,
            Die,
            Cheat
        };

        public static BaseLevel currentLevel;
        public static BaseLevel lastLevel;

        public GameStatus status;
        public static Random random;
        static GameManager()
        {
            random = new Random();
        }
        public GameManager()
        {
            LoadContent();
            Init();
        }

        protected void LoadContent()
        {

        }

        protected void Init()
        {

        }

        public void ReInit()
        {
        }

        public void Update()
        {
            currentLevel.Update(this);
        }

        public void Draw()
        {
            currentLevel.Draw();
        }

        public void DrawFront()
        {
            currentLevel.DrawFront();
        }
        #region 人物功能方法
        #endregion

        #region 人物状态切换
        #endregion

        #region test
        private void ReInitLevel(int level)
        {
            
        }
        #endregion
    }
}
