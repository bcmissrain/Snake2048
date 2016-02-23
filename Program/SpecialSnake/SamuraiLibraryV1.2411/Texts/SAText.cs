using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samurai//.Texts
{
    public class SAText
    {
        public enum Type
        {
            Horizental,
            Vertical,
            Both,
            None
        }
        //Ĭ�ϱ߿�1px
        protected int BORDER = 1;

        #region ��������
        public Vector2 Position { get; set; }
        public Color TextColor { get; set; }
        public SpriteFont Font { get; set; }
        public string TextString { get; set; }
        public Vector2 TextSize { get; set; }
        public Color BorderColor { get; set; }
        bool IfHaveBorder { get; set; }
        #endregion

        #region ���캯�� ��̬
        public SAText() { }
        public SAText(SpriteFont font, string text, Color textColor, Vector2 pos) : this(font, text, textColor, Color.White, false, Type.None, pos, Rectangle.Empty) { } //�ޱ߿�ֱ�ӷ�
        public SAText(SpriteFont font, string text, Color textColor, Rectangle rec) : this(font, text, textColor, Color.White, true, Type.Both, Vector2.Zero, rec) { }//�ұ߿򣬲ο�λ��
        public SAText(SpriteFont font, string text, Color textColor, Color borderColor, bool ifHaveBorder, Type type, Vector2 pos, Rectangle displayArea)
        {
            this.Font = font;
            this.TextString = text;
            this.Position = pos;
            this.TextColor = textColor;
            this.BorderColor = borderColor;
            this.IfHaveBorder = ifHaveBorder;
            CenterText(type, displayArea);
        }
        #endregion
        //���ֶ���
        private void CenterText(Type type, Rectangle displayArea)
        {
            //ATTENTION���ڴ��������ֵĴ�С��
            TextSize = Font.MeasureString(TextString);
            int x = (int)Position.X;
            int y = (int)Position.Y;
            switch (type)
            {
                case Type.None:
                    break;
                case Type.Both:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    y = (int)((displayArea.Height - TextSize.Y) / 2) + displayArea.Y;
                    break;
                case Type.Horizental:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    break;
                case Type.Vertical:
                    x = (int)((displayArea.Width - TextSize.X) / 2) + displayArea.X;
                    break;
            }
            this.Position = new Vector2(x, y);
        }
        //����
        public void Draw()
        {
            Draw(SAGlobal.spriteBatch);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //���Ʊ߿�
            if (IfHaveBorder)
            {
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(0, BORDER), BorderColor); //��
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(0, BORDER), BorderColor);  //��
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(BORDER, 0), BorderColor);  //��
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(BORDER, 0), BorderColor);   //��
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(BORDER, BORDER), BorderColor); //����
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(BORDER, BORDER), BorderColor); //����
                spriteBatch.DrawString(Font, TextString, Position + new Vector2(-BORDER, BORDER), BorderColor); //����
                spriteBatch.DrawString(Font, TextString, Position - new Vector2(-BORDER, BORDER), BorderColor); //����
            }
            spriteBatch.DrawString(Font, TextString, Position, TextColor);
        }
        //���־���
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)TextSize.X, (int)TextSize.Y);
        }
    }
}
