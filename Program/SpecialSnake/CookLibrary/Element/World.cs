using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.SingleGame
{
    public enum ElementType
    {
        SnakeBody = 0,
        SnakeHead = 1,
        SnakeTail = 2,
        Ground = 3,
        Food = 4
    };

    class World
    {
        //起点
        public const int WORLD_X = 40;
        public const int WORLD_Y = 200;
        //场地规格
        public const int WORLD_WIDTH = 20;
        public const int WORLD_HEIGHT = 20;
        //蛇身（一个格子单位）规格
        public const int SNAKE_WIDTH = 20;
        
        public static ElementType[,] worldMap;
        public static Snake snake;
        public static List<Food> foods;
        public bool IfHaveFood { get { return foods.Count == 0 ? false : true; } }
        //Texture2D floor;
        public World()
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
            snake = new Snake();
            worldMap[(int)snake.head.position.X, (int)snake.head.position.Y] = ElementType.SnakeHead;
            //2.初始化蛇后修改世界数组
            //worldMap[(int)(snake.head.position.X), (int)(snake.head.position.Y)] = ElementType.SnakeHead;
            //worldMap[(int)(snake.tail.position.X), (int)(snake.tail.position.Y)] = ElementType.SnakeTail;
            //floor = SAGlobal.Content.Load<Texture2D>("Images/floor");
        }
        public void Update()
        {
            if (!IfHaveFood)
            {
                AddNewFood();
            }
            snake.Update(this);
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
            snake.Draw();
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
        public void TurnTo(BodyDirection direction)
        {
            snake.nextDirection = direction;
        }
        public Vector2 GetEmptyPosition()
        {
            Vector2 emptyPos = new Vector2(-1, -1);//no empty
            int max_empty_num = WORLD_WIDTH * WORLD_HEIGHT - snake.Length;//-1 head -1 tail 
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

        public bool IfCollide()
        {
            Vector2 tempPos = snake.head.position;
            //遍历看是否碰到身体
            for (int i = 0; i < snake.bodys.Count; i++)
            {
                if (tempPos == snake.bodys[i].position)
                {
                    return true;
                }
            }

            if (tempPos == snake.tail.position)
            {
                return true;
            }
            return false;
        }
    }
}
