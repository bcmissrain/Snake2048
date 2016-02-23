using Microsoft.Xna.Framework.Input.Touch;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary
{
    public enum DragDirection
    {
        None,
        Left,
        Right
    }
    public class BaseLevel
    {
        public static Random random { get { return GameManager.random; } set { GameManager.random = value; } }
        
        protected virtual int MaxLevelCount{get{return 0;}}

        public virtual void LoadContent()
        {

        }

        public virtual void Init()
        {

        }

        public virtual void Update(GameManager gameManager)
        {
            UpdateHero(gameManager);
            if (gameManager.status == CookLibrary.GameManager.GameStatus.Play || gameManager.status == CookLibrary.GameManager.GameStatus.Cheat)
            {
                UpdateGoods(gameManager);
                UpdateFloors(gameManager);
                RecycleGoods();
                RecycleFloors();
            }
            UpdateLevel(gameManager);
        }

        protected virtual void UpdateGoods(GameManager gameManager)
        {

        }

        protected virtual void UpdateFloors(GameManager gameManager)
        {

        }

        protected virtual void UpdateHero(GameManager gameManager)
        {

        }

        protected virtual void RecycleFloors()
        {
        }

        protected virtual void RecycleGoods()
        {
        }

        public virtual void UpdateLevel(GameManager gameManager)
        {
        }

        public virtual void Draw()
        {

        }

        public virtual void DrawFront() { }

        public virtual void ChangeToNextLevel(GameManager gameManager) { }
        public virtual void ChangeToGameOver(GameManager gameManager)
        {
            
        }
        protected virtual void GameOver()
        {
            
        }
        public virtual void ChangeToGameWin()
        {
        }
        public virtual void ChangeToGameSPWin()
        {

        }
        public virtual void ChangeToGameMenu()
        {

        }
        public virtual void OnTap(GestureSample gesture) {

        }
        public virtual void OnDrag(GestureSample gesture,DragDirection dragDirection)
        {

        }
        public virtual void OnBackButtonClick()
        {
            ChangeToGameMenu();
        }
        #region ¹¤¾ß
        protected static bool InPercentage(int percent)
        {
            return random.Next(100) < percent ? true : false;
        }
        protected static int GetPercentage()
        {
            return random.Next(100);
        }
        #endregion
    }
}
