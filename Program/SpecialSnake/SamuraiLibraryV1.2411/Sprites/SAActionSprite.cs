using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Samurai.Sprites
{
    public class SAActionSprite : SASimpleSprite
    {
        /// <summary>
        /// �Ƽ��������ǣ�
        /// �������еľ�̬�����н���Щ���ݽ��г�ʼ������ȻҪ����������Ӧ�ľ�̬������
        /// �ڹ��췽����ֱ�Ӿ�Ϊ_sourceRectangles & _sizes��ֵ
        /// </summary>
        public Rectangle[] _sourceRectangles;
        public Vector2[] _sizes;
        public int index;
        public int Count { get { return _sourceRectangles.Length; } }
        /// <summary>
        /// ֱ����ʱ���Ҳ��������ԭ��Prop����˵ķ��㣬�������񷽷�һ������д��
        /// </summary>
        public override Vector2 Size { get { return _sizes[index]; } } //��ȡindex֡��Sprite�Ĵ�С
        public override Rectangle rectangle { get { return new Rectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y); } }//��ȡindex֡��Sprite����ײ����
        public override Rectangle sourceRectangle { get { return _sourceRectangles[index]; } } //��ȡ����ͼ��λ��
        /// <summary>
        /// �����鷳����Ĭ�Ϲ��캯��
        /// </summary>
        public SAActionSprite() { }
        /// <summary>
        /// Ĭ�϶�������֡�Ĺ��캯��
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="sourceRectangles"></param>
        /// <param name="sizes"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color)
            : this(texture, sourceRectangles, sizes, position, color, 0) { }
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2 position, Color color, int index)
        {
            this.texture = texture;
            this._sourceRectangles = sourceRectangles;
            this._sizes = new Vector2[this._sourceRectangles.Length];
            for (int i = 0; i < this._sourceRectangles.Length; i++)
            {
                this._sizes[i] = new Vector2(this._sourceRectangles[i].Width, this._sourceRectangles[i].Height);
            }
            this.position = position;
            this.color = color;
            this.index = index;
        }
        /// <summary>
        /// ��ȫ�Ĺ��캯����
        /// </summary>
        /// <param name="texture">����</param>
        /// <param name="sourceRectangles">�����������</param>
        /// <param name="sizes">��С����</param>
        /// <param name="position">����λ��</param>
        /// <param name="color">������ɫ</param>
        /// <param name="index">�����ĵڼ�֡</param>
        public SAActionSprite(Texture2D texture, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color, int index)
        {
            this.texture = texture;
            this._sourceRectangles = sourceRectangles;
            this._sizes = sizes;
            this.position = position;
            this.color = color;
            this.index = index;
        }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2 position, Color color, int index)
        {
            this.texture = SAGraphicManager.GetImage(resource);
            this._sourceRectangles = sourceRectangles;
            this._sizes = new Vector2[this._sourceRectangles.Length];
            for (int i = 0; i < this._sourceRectangles.Length; i++)
            {
                this._sizes[i] = new Vector2(this._sourceRectangles[i].Width, this._sourceRectangles[i].Height);
            }
            this.position = position;
            this.color = color;
            this.index = index;
        }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color, int index)
            : this(SAGraphicManager.GetImage(resource), sourceRectangles, sizes, position, color, index) { }
        public SAActionSprite(string resource, Rectangle[] sourceRectangles, Vector2[] sizes, Vector2 position, Color color)
            : this(SAGraphicManager.GetImage(resource), sourceRectangles, sizes, position, color, 0) { }
        //��Ϊ���Ա�Override��Draw��������������д��
    }
}
