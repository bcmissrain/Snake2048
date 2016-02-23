using CookLibrary.DoubleGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.VSGame
{
    public enum ElementType
    {
        SnakeBody = 0,
        SnakeHead = 1,
        SnakeTail = 2,
        Ground = 3,
        Food = 4
    };

    class World1
    {
        //起点
        public const int WORLD_X = 40;
        public const int WORLD_Y = 200;
        //场地规格
        public const int WORLD_WIDTH = 20;
        public const int WORLD_HEIGHT = 20;
        //蛇身（一个格子单位）规格
        public const int SNAKE_WIDTH = 20;
        public static int ScoreDelta { get { return snake0.Length < snake1.Length ? snake0.Length : snake1.Length; } }
        public static ElementType[,] worldMap;
        public static Snake snake0;
        public static Snake snake1;
        public int winnerId = -1;
        public static List<Food> foods;
        public bool IfHaveFood { get { return foods.Count == 0 ? false : true; } }
        //Texture2D floor;
        public World1()
        {
            worldMap = new ElementType[WORLD_WIDTH, WORLD_HEIGHT];
            foods = new List<Food>();
            //1.生成地图
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                for (int j = 0; j < WORLD_HEIGHT; j++)
                {
                    worldMap[i, j] = ElementType.Ground;
                }
            }
            snake0 = new Snake("Images/Snake1", BodyDirection.Right, new Vector2(1, 4), new Vector2(0, 4));
            snake1 = new Snake("Images/Snake0", BodyDirection.Left,new Vector2(18,16),new Vector2(19,16));
            worldMap[(int)snake0.head.position.X, (int)snake0.head.position.Y] = ElementType.SnakeHead;
            worldMap[(int)snake1.head.position.X, (int)snake1.head.position.Y] = ElementType.SnakeHead;
            winnerId = -1;
            //2.初始化蛇后修改世界数组
            //worldMap[(int)(snake0.head.position.X), (int)(snake0.head.position.Y)] = ElementType.SnakeHead;
            //worldMap[(int)(snake.tail.position.X), (int)(snake.tail.position.Y)] = ElementType.SnakeTail;
            //floor = SAGlobal.Content.Load<Texture2D>("Images/floor");
        }
        public void Update()
        {
            if (!IfHaveFood)
            {
                AddNewFood();
            }
            snake0.Update(this);
            snake1.Update(this);
        }
        public void Draw()
        {
            #region 测试使用
            //for (int i = 0; i < 20; i++)
            //{
            //    for (int j = 0; j < 20; j++)
            //    {
            //        if (worldMap[i, j] == ElementType.Ground)
            //        {
            //            SAGlobal.spriteBatch.Draw(floor, new Vector2(WORLD_X + i * 20, WORLD_Y + j * 20), Color.White);
            //        }
            //    }
            //}
            #endregion
            snake0.Draw();
            snake1.Draw();
            foreach (Food f in foods)
            {
                f.Draw();
            }
        }
        public void AddNewFood()
        {
            Food f = Restaurant.GetFood();
            f.position = GetEmptyPosition();
            //3.添加食物后修改世界数组
            worldMap[(int)(f.position.X), (int)(f.position.Y)] = ElementType.Food;
            foods.Add(f);
        }
        public void TurnTo(BodyDirection direction,int index)
        {
            //TODEL
            if (index == 0)
            {
                snake0.nextDirection = direction;
            }
            else
            {
                snake1.nextDirection = direction;   
            }
        }
        public Vector2 GetEmptyPosition()
        {
            Vector2 emptyPos = new Vector2(-1, -1);//no empty
            int max_empty_num = WORLD_WIDTH * WORLD_HEIGHT - snake0.Length-snake1.Length;//-1 head -1 tail 
            int randIndex = SAGlobal.random.Next(0, max_empty_num);
            int temp_counter = 0;
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                for (int j = 0; j < WORLD_HEIGHT; j++)
                {
                    if (worldMap[i, j] == ElementType.Ground)
                    {
                        if (temp_counter == randIndex)
                        {
                            return emptyPos = new Vector2(i, j);
                        }
                        else
                        {
                            temp_counter++;
                        }
                    }
                }
            }
            return emptyPos;
        }
        /// <summary>
        /// 判断是否自己咬自己
        /// </summary>
        /// <returns></returns>
        public int IfCollide()
        {
            Vector2 tempPos0 = snake0.head.position;
            Vector2 tempPos1 = snake1.head.position;
            //遍历看是否碰到身体
            for (int i = 0; i < snake0.bodys.Count; i++)
            {
                if (tempPos0 == snake0.bodys[i].position)
                {
                    winnerId = 1;
                    return 0;
                }
            }
            for (int i = 0; i < snake1.bodys.Count; i++)
            {
                if (tempPos1 == snake1.bodys[i].position)
                {
                    winnerId = 0;
                    return 1;
                }
            }
            return -1;
        }
        public int WinnerId()
        {
            if (winnerId != -1)
            {
                return winnerId;
            }
            if (snake0.bodys.Count() >= ScorePainter0.MAX_LENGTH)
            {
                return 0;
            }
            if (snake1.bodys.Count() >= ScorePainter1.MAX_LENGTH)
            {
                return 1;
            }
            return -1;
        }
    }
}
