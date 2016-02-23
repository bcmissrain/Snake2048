using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;

namespace Samurai
{
    public abstract class SADirector
    {
        //���õ�����
        protected static Game game;
        protected static GraphicsDeviceManager graphics;
        protected static SpriteBatch spriteBatch;
        protected static ContentManager content;
        //ǰһ����Ļ
        protected static ScreenType previousScreenType;
        protected static SAScreen previousScreen;
        //��ǰ��Ļ
        protected static ScreenType currentScreenType;
        protected static SAScreen currentScreen;
        //��һ����Ļ
        protected static ScreenType nextScreenType;
        protected static SAScreen nextScreen;
        public static SAScreen NextScreen { get { return nextScreen; } }
        public static ScreenType NextScreenType { get { return nextScreenType; } }
        //�ڴ滺��
        protected static Dictionary<ScreenType, SAScreen> screenDictionary;
        //��Ļ����ί��
        public delegate SAScreen CreateScreen();
        public SADirector(Game game, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            SADirector.game = game;
            SADirector.content = game.Content;
            SADirector.graphics = graphics;
            SADirector.spriteBatch = spriteBatch;
            screenDictionary = new Dictionary<ScreenType, SAScreen>();
            //ע�� Global
            SAGlobal.Setup(game, spriteBatch, graphics);
            //ע��MusicManager
            SAMusicManager.Setup();
            //ATTENTION ���ص�һ��ҳ��
            LoadFirstScreen();
        }

        #region ������Դ��ʼ��
        /// <summary>
        /// ����ȫ�ֵ�ҳ����Դ
        /// ����ļ����ǵ��̵߳�
        /// </summary>
        public virtual void LoadFirstScreen()
        {
            //TODO
        }
        #endregion

        #region ��Game�Ľӿ�
        public void Update(GameTime gameTime)
        {
            currentScreen.BaseUpdate(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            //game.GraphicsDevice.Clear(Color.CadetBlue);
            //TODEL
            if (previousScreen != null)
            {
                previousScreen.BaseDraw(gameTime);
            }
            if (nextScreen != null)
            {
                //if (nextScreen.IfReady) ��������Ready
                if (CanDrawNextScreen())
                {
                    nextScreen.BaseDraw(gameTime);
                }
            }
            currentScreen.BaseDraw(gameTime);
        }

        public virtual void UnloadContent() { }

        public virtual void OnActivated() { }
        #endregion

        #region �л�ҳ�棨SAScreen��Ҫ֪����
        /// <summary>
        /// ��¶��Screen�ķ�����Screenͨ��ChangeScreenToʵ��״̬ģʽ
        /// </summary>
        /// <param name="screenType"></param>
        public virtual void ChangeScreenTo(ScreenType screenType) { }
        /// <summary>
        /// ���ڻ���������
        /// Ĭ�ϵ�����LoadContent�Լ�Reinit
        /// ע����ǣ�screenType����ΪBetween
        /// </summary>
        /// <param name="screenType">��Ļ����</param>
        /// <param name="createScreen">������Ļ�ķ���</param>
        public void ChangeScreenTo(ScreenType screenType, CreateScreen createScreen)
        {
            if (currentScreen != null)
            {
                previousScreenType = currentScreenType;
                previousScreen = currentScreen;
                //ע�⣬һ��Ҫ���ͷ�Input��Դ������ע�ⲻҪ�ظ��ͷţ�
                previousScreen.UnloadContent();
            }
            if (!screenDictionary.ContainsKey(screenType))
            {
                /*
                #region ������ҳ��
                screenDictionary.Add(screenType, createScreen());
                new Thread(new ThreadStart(screenDictionary[screenType].RegistCotent)).Start();
                nextScreenType = screenType;
                nextScreen = screenDictionary[screenType];
                #endregion

                #region ����Betweenҳ��
                //����BetweenScreen��
                //Ĭ�ϴ���BetweenScreen
                currentScreenType = ScreenType.Between;
                currentScreen = screenDictionary[ScreenType.Between];
                currentScreen.ReInit();
                #endregion
                */
                //Ϊ��ʡ�£������Ϸ���
                screenDictionary.Add(screenType, createScreen());
                currentScreenType = screenType;
                currentScreen = screenDictionary[screenType];
                currentScreen.BaseLoadContent();
            }
            else
            {
                #region ֱ�Ӽ�����ҳ��
                currentScreenType = screenType;
                currentScreen = screenDictionary[screenType];
                //��������
                currentScreen.ReInit();
                #endregion

                #region ������Դ
                ReleaseScreen();
                //����nextScreen
                nextScreen = null;
                #endregion
            }
        }
        /// <summary>
        /// ֻ���ڼ���ҳ�涯����ɺ���ͷ���Դ
        /// </summary>
        public static void ReleaseScreen()
        {
            if (previousScreen != null)
            {
                //������Դ
                //�ر�ע����ǣ��ͷ�Input��ԴӦ���ڼ�����ҳ��֮ǰ��
                SAGlobal.CleanTemporalContent();
                if (previousScreen.Releasable)
                {
                    screenDictionary.Remove(previousScreenType);
                }
                //����ָ��
                previousScreen = null;
            }
        }
        #endregion

        #region ���߷���
        /// <summary>
        /// ҳ���Ƿ�������
        /// </summary>
        /*
        public bool IfScreenReady(ScreenType screenType)
        {
            if (screenDictionary.ContainsKey(screenType))
            {
                return screenDictionary[screenType].IfReady;
            }
            return false;
        }
         */
        /// <summary>
        /// ҳ���Ƿ������ϲ��ҿɻ���
        /// </summary>
        public bool CanDrawNextScreen()
        {
            return ((SABetweenScreen)screenDictionary[ScreenType.Between]).CanDrawNextScreen;
        }
        /// <summary>
        /// ע��ҳ��
        /// </summary>
        public static SAScreen RegistScreen(ScreenType screenType, CreateScreen createScreen)
        {
            screenDictionary.Add(screenType, createScreen());
            screenDictionary[screenType].RegistCotent();
            return screenDictionary[screenType];
        }
        #endregion

        #region ��������(����ģʽ)
        #endregion
    }
}