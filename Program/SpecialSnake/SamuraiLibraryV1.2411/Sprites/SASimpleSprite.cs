using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.Sprites
{
    /// <summary>
    /// ֻ����һ��ͼ���Sprite
    /// </summary>
    public class SASimpleSprite : SASprite
    {
        public SASimpleSprite() { }
        /// <summary>
        /// ��ָ��λ�û�����������
        /// </summary>
        /// <param name="texture">����</param>
        /// <param name="postion">λ��</param>
        public SASimpleSprite(Texture2D texture, Vector2 postion)
        {
            this.texture = texture;
            this.sourceRectangle = new Rectangle(0, 0, texture.Bounds.Width, texture.Bounds.Height);
            this.position = position;
            this.color = Color.White;
        }
        /// <summary>
        /// ֱ�ӻ�����������
        /// </summary>
        /// <param name="texture">����</param>
        public SASimpleSprite(Texture2D texture)
            : this(texture, Vector2.Zero)
        { }
        /// <summary>
        /// ��ָ��position����ָ����sourceRectangle��Χ�ڵ�����
        /// </summary>
        /// <param name="texture">����</param>
        /// <param name="sourceRectangle">�������</param>
        /// <param name="position">λ��</param>
        /// <param name="color">ָ����ɫ</param>
        public SASimpleSprite(Texture2D texture, Rectangle sourceRectangle, Vector2 position, Color color)
        {
            this.texture = texture;
            this.sourceRectangle = sourceRectangle;
            this.position = position;
            this.color = color;
        }
        public SASimpleSprite(string resource, Vector2 position)
            : this(SAGraphicManager.GetImage(resource), position) { }
        public SASimpleSprite(string resource)
            : this(SAGraphicManager.GetImage(resource)) { }
        public SASimpleSprite(string resource, Rectangle sourceRectangle, Vector2 position, Color color)
            : this(SAGraphicManager.GetImage(resource), sourceRectangle, position, color) { }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color);
        }
    }

    /// <summary>
    /// �򵥵��Ƽ�ʹ������
    /// </summary>
    public class TestSASimpleSprite : SASimpleSprite
    {
        static string _resource;
        static Rectangle _sourceRectangle;
        static TestSASimpleSprite()
        {
            _resource = "Template/TestSASimpleSprite";
            _sourceRectangle = new Rectangle(0, 0, 480, 800);
        }
        public TestSASimpleSprite()
            : base(SAGraphicManager.GetImage(_resource))
        {
            sourceRectangle = _sourceRectangle;
            position = Vector2.Zero;
            color = Color.White;
        }
        TestSASimpleSprite(string res) : base(res) { }
    }
}
