using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai.Sprites
{
    /// <summary>
    /// Sprite�Ļ���
    /// ��������texture
    /// </summary>
    public abstract class SASprite
    {
        public Texture2D texture;
        public virtual Color color { get; set; }
        public Vector2 position;

        public virtual Vector2 Size { get { return new Vector2(rectangle.Width, rectangle.Height); } } //��ȡSprite�Ĵ�С
        public virtual Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height); } }//��ȡSprite����ײ����
        public virtual Rectangle sourceRectangle { get; set; } //��ȡ����ͼ��λ��

        public SASprite() { }
        public SASprite(Texture2D texture) : this(texture, Vector2.Zero) { }
        public SASprite(Texture2D texture, Vector2 positon) : this(texture, (int)positon.X, (int)positon.Y) { }
        public SASprite(Texture2D texture, int pos_x, int pos_y)
        {
            this.texture = texture;
            this.position = new Vector2(pos_x, pos_y);
            this.color = Color.White;
        }
        //�ж��Ƿ����ཻ����
        public virtual bool IfCollide(Rectangle rect)
        {
            return rectangle.Intersects(rect);
        }
        //�ⲿ���õ�Draw�ӿ�
        public void Draw()
        {
            Draw(SAGlobal.spriteBatch);
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
