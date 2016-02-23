using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.SingleGame
{
    #region ����
    class Body:BaseBody
    {
        protected Rectangle[] turnSources;

        public Body():base("Images/snake",new Rectangle(0,World.SNAKE_WIDTH,World.SNAKE_WIDTH,World.SNAKE_WIDTH),
           new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
            this.turnSources = new Rectangle[4];
            this.turnSources[0] = new Rectangle(0, 60, 20, 20);
            this.turnSources[1] = new Rectangle(20, 60, 20, 20);
            this.turnSources[2] = new Rectangle(0, 80, 20, 20);
            this.turnSources[3] = new Rectangle(20, 80, 20, 20);
        }

        public Body(string sourceName)
            : base(sourceName, new Rectangle(0, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH),
                new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
            this.turnSources = new Rectangle[4];
            this.turnSources[0] = new Rectangle(0, 60, 20, 20);
            this.turnSources[1] = new Rectangle(20, 60, 20, 20);
            this.turnSources[2] = new Rectangle(0, 80, 20, 20);
            this.turnSources[3] = new Rectangle(20, 80, 20, 20);
        }

        public override void Draw()
        {
            if (this.direction == this.laseDirection)
            {
                switch (direction)
                {
                    case BodyDirection.Left:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectL, Color.White);
                        break;
                    case BodyDirection.Right:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectL, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                        break;
                    case BodyDirection.Up:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectU, Color.White);
                        break;
                    case BodyDirection.Down:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectU, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case BodyDirection.Left:
                        if (laseDirection == BodyDirection.Up)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[1], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Down)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[3], Color.White);
                        }
                        break;
                    case BodyDirection.Right:
                        if (laseDirection == BodyDirection.Up)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[0], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Down)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[2], Color.White);
                        }
                        break;
                    case BodyDirection.Up:
                        if (laseDirection == BodyDirection.Left)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[2], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Right)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[3], Color.White);
                        }
                        break;
                    case BodyDirection.Down:
                        if (laseDirection == BodyDirection.Left)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[0], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Right)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[1], Color.White);
                        }
                        break;
                }
            }
        }
    }

    class Head:BaseBody
    {
        public Head() : base("Images/snake", new Rectangle(0, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
            , new Rectangle(World.SNAKE_WIDTH, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public Head(string sourceName)
            : base(sourceName, new Rectangle(0, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public override void Update(BaseBody forePart)
        {
            
        }
    }

    class Tail :BaseBody
    {
        public Tail() : base("Images/snake", new Rectangle(0, World.SNAKE_WIDTH*2, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
            ,new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH*2, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public Tail(string sourceName)
            : base(sourceName, new Rectangle(0, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }
    }
    #endregion

    static class Restaurant 
    {
        public static Food GetFood()
        {
            return new Food();
        }
    }

    class Food : BaseBody 
    {
        public Food()
        {
            this.bodyTexture = SAGlobal.Content.Load<Texture2D>("Images/snake");
            this.direction = BodyDirection.Up;
            this.sourceRectU = this.sourceRectL = new Rectangle(World.SNAKE_WIDTH * 2, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH);
        }

        public Food(string sourceName)
        {
            this.bodyTexture = SAGlobal.Content.Load<Texture2D>(sourceName);
            this.direction = BodyDirection.Up;
            this.sourceRectU = this.sourceRectL = new Rectangle(World.SNAKE_WIDTH * 2, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH);
        }
    }

    class Snake
    {
        public BodyDirection nextDirection;
        public BaseBody head;
        public BaseBody tail;
        public List<BaseBody> bodys;
        public bool CanMoveLeft { get { return head.direction == BodyDirection.Up || head.direction == BodyDirection.Down; } }
        public bool IfHaveBody { get { return bodys.Count == 0 ? false : true; } }
        public int Length { get { return 2 + bodys.Count; } }
        public int ScoreDelta { get { return MIN_SPEED+1 - MAX_COUNT; } }
        public const int MAX_SPEED = 0;
        public const int MIN_SPEED = 7;
        public const int NORMAL_SPEED = 4;

        public int MAX_COUNT = NORMAL_SPEED;
        int counter;
        
        protected bool ifEat;

        public Snake()
            : this("Images/snake", BodyDirection.Left, new Vector2(18, 15),new Vector2(19 , 15))
        {
        }

        public Snake(string sourceName,BodyDirection direction,Vector2 headPos,Vector2 tailPos)
        {
            this.head = new Head(sourceName);
            head.direction = direction;
            this.tail = new Tail(sourceName);
            this.bodys = new List<BaseBody>();
            this.head.position = headPos;
            this.tail.position = tailPos;
            ifEat = false;
            this.nextDirection = head.direction;
        }

        public void Update(World world)
        {
            if (counter >= MAX_COUNT)
            {
                AutoTurnDirection();
                counter = 0;
                CheckEat(World.foods);
                if (ifEat)
                {
                    Eat();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                counter++;
            }
        }
        public void AutoTurnDirection()
        {
            if ((int)nextDirection + (int)head.direction != 3)
            {
                head.direction = nextDirection;
            }
        }
        public void Draw()
        {
            foreach (BaseBody bb in bodys)
            {
                bb.Draw();
            }
            tail.Draw();
            head.Draw();
        }
        public void Move()
        {
            //������������ɾ��β��
            World.worldMap[(int)(tail.position.X), (int)(tail.position.Y)] = ElementType.Ground; //�����������ͷ

            UpdateTail();
            UpdateBodys();

            //�����������н�δ�ƶ���ͷ�Ŀ�λ������Ϊ����
            World.worldMap[(int)(head.position.X), (int)(head.position.Y)] = ElementType.SnakeBody; //�����������ͷ

            UpdateHead();
            CheckMove();
            //�����������н��Ѿ��ƶ���ͷ��λ������Ϊͷ
            World.worldMap[(int)(head.position.X), (int)(head.position.Y)] = ElementType.SnakeHead; //�����������ͷ
        }
        /// <summary>
        /// ���ȸ���β������
        /// </summary>
        public void UpdateTail()
        {
            if (IfHaveBody)
            {
                tail.Update(bodys.Last());
            }
            else
            {
                tail.Update(head);
            }
        }
        /// <summary>
        /// �ٸ�����������
        /// </summary>
        public void UpdateBodys()
        {
            for (int i = bodys.Count - 1;i>0 ; i--)
            {
                bodys[i].Update(bodys[i - 1]);
            }
            //����head
            if (IfHaveBody)
            {
                bodys[0].Update(head);
            }
        }
        /// <summary>
        /// ������ͷ������
        /// </summary>
        public void UpdateHead()
        {
            switch (head.direction)
            { 
                case BodyDirection.Up:
                    head.position.Y--;
                    break;
                case BodyDirection.Down:
                    head.position.Y++;
                    break;
                case BodyDirection.Left:
                    head.position.X--;
                    break;
                case BodyDirection.Right:
                    head.position.X++;
                    break;
            }
        }

        protected void CheckEat(List<Food> foods)
        {
            for (int i = 0; i < foods.Count; i++)
            {
                if (head.NextPosition == foods[i].position)
                {
                    //ֱ�Ӽ���ͷ�ϼ���
                    foods.RemoveAt(i);
                    BeginEat();
                    break;
                }
            }
        }
        protected virtual void Eat()
        {
            for (int i = 0; i < World.foods.Count; i++)
            {
                //5.ɾ������ʳ���֮��ɾ���������޸���������
                World.worldMap[(int)(World.foods[i].position.X), (int)(World.foods[i].position.Y)] = ElementType.Ground;
            }
            World.foods.Clear();//ɾ������������

            BaseBody temp = new Body();
            temp.Init(head);
            if (IfHaveBody)
            {
                temp.laseDirection = bodys[0].direction;
            }
            else 
            {
                temp.laseDirection = tail.direction;
            }
            this.bodys.Insert(0,temp);
            UpdateHead();
            CheckMove();
            World.worldMap[(int)(head.position.X), (int)(head.position.Y)] = ElementType.SnakeHead; //�����������ͷ
            World.worldMap[(int)(bodys[0].position.X), (int)(bodys[0].position.Y)] = ElementType.SnakeBody;//����������������壬��������β���޸�

            EatUp();
        }
        public void BeginEat()
        {
            ifEat = true;
            SAMusicManager.PlaySoundEffect("Sounds/eat");
            GameData.ScorePlus(ScoreDelta);
        }
        public void EatUp()
        {
            ifEat = false;
        }
        protected void CheckMove()
        {
            if (head.position.X < 0)
            {
                head.position.X  = World.WORLD_WIDTH-1;
            }
            else if (head.position.X > World.WORLD_WIDTH-1)
            {
                head.position.X = 0;
            }
            if (head.position.Y < 0)
            {
                head.position.Y = World.WORLD_HEIGHT - 1;
            }
            else if (head.position.Y > World.WORLD_HEIGHT - 1)
            {
                head.position.Y = 0;
            }
        }
        public Snake InitPos(Vector2 headPos,BodyDirection dir)
        {
            this.head.position = headPos;
            this.head.direction = dir;
            switch (dir)
            { 
                case BodyDirection.Left:
                    this.tail.position = new Vector2(headPos.X + 1, headPos.Y);
                    break;
                case BodyDirection.Right:
                    this.tail.position = new Vector2(headPos.X - 1, headPos.Y);
                    break;
                case BodyDirection.Up:
                    this.tail.position = new Vector2(headPos.X, headPos.Y + 1);
                    break;
                case BodyDirection.Down:
                    this.tail.position = new Vector2(headPos.X, headPos.Y - 1);
                    break;
            }

            return this;
        }

        #region ����
        public void SpeedUp()
        {
            if (!IfMaxSpeed())
            {
                MAX_COUNT--;
            }
        }

        public void SpeedDown()
        {
            if (!IfMinSpeed())
            {
                MAX_COUNT++;
            }
        }
        public bool IfMaxSpeed()
        {
            return MAX_COUNT <= MAX_SPEED;
        }
        public bool IfMinSpeed()
        {
            return MAX_COUNT >= MIN_SPEED;
        }
        #endregion
    }
}
/*
#region 
namespace CookLibrary.SingleGame
{
    #region ����
    class Body : BaseBody
    {
        protected Rectangle[] turnSources;

        public Body()
            : base("Images/snake", new Rectangle(0, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH),
                new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
            this.turnSources = new Rectangle[4];
            this.turnSources[0] = new Rectangle(0, 60, 20, 20);
            this.turnSources[1] = new Rectangle(20, 60, 20, 20);
            this.turnSources[2] = new Rectangle(0, 80, 20, 20);
            this.turnSources[3] = new Rectangle(20, 80, 20, 20);
        }

        public Body(string sourceName)
            : base(sourceName, new Rectangle(0, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH),
                new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
            this.turnSources = new Rectangle[4];
            this.turnSources[0] = new Rectangle(0, 60, 20, 20);
            this.turnSources[1] = new Rectangle(20, 60, 20, 20);
            this.turnSources[2] = new Rectangle(0, 80, 20, 20);
            this.turnSources[3] = new Rectangle(20, 80, 20, 20);
        }

        public override void Draw()
        {
            if (this.direction == this.laseDirection)
            {
                switch (direction)
                {
                    case BodyDirection.Left:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectL, Color.White);
                        break;
                    case BodyDirection.Right:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectL, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
                        break;
                    case BodyDirection.Up:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectU, Color.White);
                        break;
                    case BodyDirection.Down:
                        SAGlobal.spriteBatch.Draw(bodyTexture, Position, sourceRectU, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipVertically, 0);
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case BodyDirection.Left:
                        if (laseDirection == BodyDirection.Up)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[1], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Down)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[3], Color.White);
                        }
                        break;
                    case BodyDirection.Right:
                        if (laseDirection == BodyDirection.Up)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[0], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Down)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[2], Color.White);
                        }
                        break;
                    case BodyDirection.Up:
                        if (laseDirection == BodyDirection.Left)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[2], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Right)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[3], Color.White);
                        }
                        break;
                    case BodyDirection.Down:
                        if (laseDirection == BodyDirection.Left)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[0], Color.White);
                        }
                        else if (laseDirection == BodyDirection.Right)
                        {
                            SAGlobal.spriteBatch.Draw(bodyTexture, Position, turnSources[1], Color.White);
                        }
                        break;
                }
            }
        }
    }

    class Head : BaseBody
    {
        public Head()
            : base("Images/snake", new Rectangle(0, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public Head(string sourceName)
            : base(sourceName, new Rectangle(0, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public override void Update(BaseBody forePart)
        {

        }
    }

    class Tail : BaseBody
    {
        public Tail()
            : base("Images/snake", new Rectangle(0, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }

        public Tail(string sourceName)
            : base(sourceName, new Rectangle(0, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH)
                , new Rectangle(World.SNAKE_WIDTH, World.SNAKE_WIDTH * 2, World.SNAKE_WIDTH, World.SNAKE_WIDTH))
        {
            this.position = position;
        }
    }
    #endregion

    static class Restaurant
    {
        public static Food GetFood()
        {
            return new Food();
        }
    }

    class Food : BaseBody
    {
        public Food()
        {
            this.bodyTexture = SAGlobal.Content.Load<Texture2D>("Images/snake");
            this.direction = BodyDirection.Up;
            this.sourceRectU = this.sourceRectL = new Rectangle(World.SNAKE_WIDTH * 2, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH);
        }

        public Food(string sourceName)
        {
            this.bodyTexture = SAGlobal.Content.Load<Texture2D>(sourceName);
            this.direction = BodyDirection.Up;
            this.sourceRectU = this.sourceRectL = new Rectangle(World.SNAKE_WIDTH * 2, 0, World.SNAKE_WIDTH, World.SNAKE_WIDTH);
        }
    }

    class Snake
    {
        public BodyDirection nextDirection;
        public BaseBody head;
        public BaseBody tail;
        public List<BaseBody> bodys;

        public bool IfHaveBody { get { return bodys.Count == 0 ? false : true; } }
        public int Length { get { return 2 + bodys.Count; } }
        public int ScoreDelta { get { return MIN_SPEED + 1 - MAX_COUNT; } }
        public const int MAX_SPEED = 0;
        public const int MIN_SPEED = 7;
        public const int NORMAL_SPEED = 4;

        public int MAX_COUNT = NORMAL_SPEED;
        int counter;

        protected bool ifEat;

        public Snake()
            : this("Images/snake", BodyDirection.Left, new Vector2(18, 15), new Vector2(19, 15))
        {
        }

        public Snake(string sourceName, BodyDirection direction, Vector2 headPos, Vector2 tailPos)
        {
            this.head = new Head(sourceName);
            head.direction = direction;
            this.tail = new Tail(sourceName);
            this.bodys = new List<BaseBody>();
            this.head.position = headPos;
            this.tail.position = tailPos;
            ifEat = false;
            this.nextDirection = head.direction;
        }

        public void Update(World world)
        {
            if (counter >= MAX_COUNT)
            {
                AutoTurnDirection();
                counter = 0;
                CheckEat(World.foods);
                if (ifEat)
                {
                    Eat();
                }
                else
                {
                    Move();
                }
            }
            else
            {
                counter++;
            }
        }
        public void AutoTurnDirection()
        {
            if ((int)nextDirection + (int)head.direction != 3)
            {
                head.direction = nextDirection;
            }
        }
        public void Draw()
        {
            foreach (BaseBody bb in bodys)
            {
                bb.Draw();
            }
            tail.Draw();
            head.Draw();
        }
        public void Move()
        {
            //������������ɾ��β��
            World.worldMap[(int)(World.snake.tail.position.X), (int)(World.snake.tail.position.Y)] = ElementType.Ground; //�����������ͷ

            UpdateTail();
            UpdateBodys();

            //�����������н�δ�ƶ���ͷ�Ŀ�λ������Ϊ����
            World.worldMap[(int)(World.snake.head.position.X), (int)(World.snake.head.position.Y)] = ElementType.SnakeBody; //�����������ͷ

            UpdateHead();
            CheckMove();
            //�����������н��Ѿ��ƶ���ͷ��λ������Ϊͷ
            World.worldMap[(int)(World.snake.head.position.X), (int)(World.snake.head.position.Y)] = ElementType.SnakeHead; //�����������ͷ
        }
        /// <summary>
        /// ���ȸ���β������
        /// </summary>
        public void UpdateTail()
        {
            if (IfHaveBody)
            {
                tail.Update(bodys.Last());
            }
            else
            {
                tail.Update(head);
            }
        }
        /// <summary>
        /// �ٸ�����������
        /// </summary>
        public void UpdateBodys()
        {
            for (int i = bodys.Count - 1; i > 0; i--)
            {
                bodys[i].Update(bodys[i - 1]);
            }
            //����head
            if (IfHaveBody)
            {
                bodys[0].Update(head);
            }
        }
        /// <summary>
        /// ������ͷ������
        /// </summary>
        public void UpdateHead()
        {
            switch (head.direction)
            {
                case BodyDirection.Up:
                    head.position.Y--;
                    break;
                case BodyDirection.Down:
                    head.position.Y++;
                    break;
                case BodyDirection.Left:
                    head.position.X--;
                    break;
                case BodyDirection.Right:
                    head.position.X++;
                    break;
            }
        }

        protected void CheckEat(List<Food> foods)
        {
            for (int i = 0; i < foods.Count; i++)
            {
                if (head.NextPosition == foods[i].position)
                {
                    //ֱ�Ӽ���ͷ�ϼ���
                    foods.RemoveAt(i);
                    BeginEat();
                    break;
                }
            }
        }
        protected virtual void Eat()
        {
            for (int i = 0; i < World.foods.Count; i++)
            {
                //5.ɾ������ʳ���֮��ɾ���������޸���������
                World.worldMap[(int)(World.foods[i].position.X), (int)(World.foods[i].position.Y)] = ElementType.Ground;
            }
            World.foods.Clear();//ɾ������������

            BaseBody temp = new Body();
            temp.Init(head);
            if (IfHaveBody)
            {
                temp.laseDirection = bodys[0].direction;
            }
            else
            {
                temp.laseDirection = tail.direction;
            }
            this.bodys.Insert(0, temp);
            UpdateHead();
            CheckMove();
            World.worldMap[(int)(World.snake.head.position.X), (int)(World.snake.head.position.Y)] = ElementType.SnakeHead; //�����������ͷ
            World.worldMap[(int)(World.snake.bodys[0].position.X), (int)(World.snake.bodys[0].position.Y)] = ElementType.SnakeBody;//����������������壬��������β���޸�

            EatUp();
        }
        public void BeginEat()
        {
            ifEat = true;
            SAMusicManager.PlaySoundEffect("Sounds/eat");
            GameData.ScorePlus(ScoreDelta);
        }
        public void EatUp()
        {
            ifEat = false;
        }
        protected void CheckMove()
        {
            if (head.position.X < 0)
            {
                head.position.X = World.WORLD_WIDTH - 1;
            }
            else if (head.position.X > World.WORLD_WIDTH - 1)
            {
                head.position.X = 0;
            }
            if (head.position.Y < 0)
            {
                head.position.Y = World.WORLD_HEIGHT - 1;
            }
            else if (head.position.Y > World.WORLD_HEIGHT - 1)
            {
                head.position.Y = 0;
            }
        }
        public Snake InitPos(Vector2 headPos, BodyDirection dir)
        {
            this.head.position = headPos;
            this.head.direction = dir;
            switch (dir)
            {
                case BodyDirection.Left:
                    this.tail.position = new Vector2(headPos.X + 1, headPos.Y);
                    break;
                case BodyDirection.Right:
                    this.tail.position = new Vector2(headPos.X - 1, headPos.Y);
                    break;
                case BodyDirection.Up:
                    this.tail.position = new Vector2(headPos.X, headPos.Y + 1);
                    break;
                case BodyDirection.Down:
                    this.tail.position = new Vector2(headPos.X, headPos.Y - 1);
                    break;
            }

            return this;
        }

        #region ����
        public void SpeedUp()
        {
            if (!IfMaxSpeed())
            {
                MAX_COUNT--;
            }
        }

        public void SpeedDown()
        {
            if (!IfMinSpeed())
            {
                MAX_COUNT++;
            }
        }
        public bool IfMaxSpeed()
        {
            return MAX_COUNT <= MAX_SPEED;
        }
        public bool IfMinSpeed()
        {
            return MAX_COUNT >= MIN_SPEED;
        }
        #endregion
    }
}
#endregion
*/