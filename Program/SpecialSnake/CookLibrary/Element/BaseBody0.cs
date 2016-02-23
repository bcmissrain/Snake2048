using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Samurai;
using Samurai.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary.DoubleGame
{
    enum BodyDirection
    {
        Left = 0,
        Right = 3,
        Up = 1,
        Down = 2
    };

    class BaseBody
    {
        public const int DISTANCE = 70;
        public BodyDirection direction;
        public BodyDirection laseDirection;
        protected Texture2D bodyTexture;
        protected Rectangle sourceRectU;
        protected Rectangle sourceRectL;
        public Vector2 position;
        public virtual Vector2 Position { get { return new Vector2(World1.WORLD_X + position.X * World1.SNAKE_WIDTH, World1.WORLD_Y + position.Y * World1.SNAKE_WIDTH); } }
        public virtual Vector2 NextPosition
        {
            get
            {
                Vector2 v = Vector2.Zero;
                switch (direction)
                {
                    case BodyDirection.Left:
                        v = new Vector2(position.X - 1, position.Y);
                        break;
                    case BodyDirection.Right:
                        v = new Vector2(position.X + 1, position.Y);
                        break;
                    case BodyDirection.Up:
                        v = new Vector2(position.X, position.Y - 1);
                        break;
                    case BodyDirection.Down:
                        v = new Vector2(position.X, position.Y + 1);
                        break;
                }
                if (v.X >= 20)
                {
                    v.X = 0;
                }
                else if (v.X < 0)
                { 
                    v.X = 19;
                }
                if (v.Y >= 20)
                {
                    v.Y = 0;
                }
                else if (v.Y < 0)
                {
                    v.Y = 19;
                }
                return v;
            }
        }

        public BaseBody() { }
        public BaseBody(string sourceName, Rectangle sourceRectU, Rectangle sourceRectL)
        {
            this.bodyTexture = SAGlobal.Content.Load<Texture2D>(sourceName);
            this.position = Vector2.Zero;
            this.sourceRectU = sourceRectU;
            this.sourceRectL = sourceRectL;
            this.direction = BodyDirection.Left;
            this.laseDirection = this.direction;
        }
        public virtual void Init(BaseBody forePart)
        {
            this.laseDirection = forePart.direction;
            this.position = forePart.position;
            this.direction = forePart.direction;
        }
        public virtual void Update(BaseBody forePart)
        {
            this.laseDirection = this.direction;
            this.position = forePart.position;
            this.direction = forePart.direction;
        }
        public virtual void Draw()
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
        public virtual bool IfHeadCollide(BaseBody bb)
        {
            switch (direction)
            {
                case BodyDirection.Left:
                    break;
                case BodyDirection.Right:
                    break;
                case BodyDirection.Up:
                    break;
                case BodyDirection.Down:
                    break;
            }
            return false;
        }
    }
}
