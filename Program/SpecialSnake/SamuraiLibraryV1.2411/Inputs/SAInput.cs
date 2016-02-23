using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samurai.Sprites;

namespace Samurai
{
    public static class SAInput
    {
        static SAInput()
        {
            TouchPanel.EnabledGestures = GestureType.Tap;       //Ĭ�ϳ�ʼ���Դ���Tap������Button
            enable = true;
        }

        public delegate void OnInputDelegate(GestureSample gestureSample);      //�������Ƶ�ί�ж���
        public delegate void OnBackButtonDelegate();        //����back����ί�ж���
        public delegate void OnTouchCollectionDelegate(TouchCollection touchCollection);

        private static OnBackButtonDelegate OnBackButton;        //���Back��ʱInvoke��ί��
        private static OnTouchCollectionDelegate OnTouchCollection;
        private static Dictionary<GestureType, OnInputDelegate> inputDictionary
            = new Dictionary<GestureType, OnInputDelegate>();       //������ί�е��ֵ�
        private static List<SAControl> buttonList = new List<SAControl>();
        private static TouchCollection touchCollection { get; set; }
        private static bool enable { get; set; }

        #region ����Input
        public static void ResetInput()
        {
            CleanGesture();
            CleanBackButton();
            CleanButton();
            CleanTouchCollection();
            TouchPanel.EnabledGestures = GestureType.Tap;       //Ĭ�ϳ�ʼ���Դ���Tap������Button
        }
        #endregion

        #region ����ײ㴥�ؽӿ�
        public static void EnableTouchCollection(OnTouchCollectionDelegate temp)
        {
            OnTouchCollection = temp;
        }

        public static void CleanTouchCollection()
        {
            OnTouchCollection = null;
        }
        #endregion

        #region ��������
        public static void EnableGesture(GestureType gestureType, OnInputDelegate onInput)
        {
            if (!inputDictionary.ContainsKey(gestureType))
            {
                TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | gestureType;
                inputDictionary.Add(gestureType, onInput);
            }
        }

        //ATTENTION ��ȥ�����ƵĹ��̺�ԭ����foreachѭ����������
        public static void DisableGesture(GestureType gestureType)
        {
            if (inputDictionary.ContainsKey(gestureType))
            {
                inputDictionary.Remove(gestureType);
            }
            TouchPanel.EnabledGestures = GestureType.None;
            foreach (GestureType g in inputDictionary.Keys)
            {
                TouchPanel.EnabledGestures = TouchPanel.EnabledGestures | gestureType;
            }
        }

        public static void CleanGesture()
        {
            TouchPanel.EnabledGestures = GestureType.None;
            inputDictionary.Clear();
        }
        #endregion

        #region ����Back����
        public static void EnableBackButton(OnBackButtonDelegate onBackButton)
        {
            OnBackButton = onBackButton;
        }

        public static void DisableBackButton(OnBackButtonDelegate onBackButton)
        {
            OnBackButton -= onBackButton;
        }

        public static void CleanBackButton()
        {
            OnBackButton = null;
        }
        #endregion

        #region ����Button
        public static void AddButton(SAControl button)
        {
            buttonList.Add(button);
        }

        public static void CleanButton()
        {
            buttonList.Clear();
        }

        public static void RemoveButton(SAControl button)
        {
            if (buttonList.Contains(button))
            {
                buttonList.RemoveAt(buttonList.IndexOf(button));
            }
        }
        #endregion

        #region ��Ϣ�ķַ�
        //�����򵥣���Ҳ��Ч����͵�
        public static void UpdateInput()
        {
            UpdateBackButton();
            UpdateButton();
            UpdateTouchCollection();
            UpdateGesture();
        }

        public static void UpdateGesture()
        {
            if (enable)
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gestureSample = TouchPanel.ReadGesture();
                    //�ȴ���Ԥ����Button
                    if (gestureSample.GestureType == GestureType.Tap)
                    {
                        foreach (SAControl b in buttonList)
                        {
                            if (b.IfOnTouch(gestureSample.Position))
                            {
                                b.OnClick();
                                break;//ATTENTION �����ڴ���ɾ��Button
                            }
                        }
                    }

                    //���������¼�
                    foreach (GestureType g in inputDictionary.Keys)
                    {
                        if (gestureSample.GestureType == g)
                        {
                            inputDictionary[g].Invoke(gestureSample);
                            break;  //ATTENTION
                        }
                    }
                }
            }
        }

        public static void UpdateTouchCollection()
        {
            if (enable)
            {
                touchCollection = TouchPanel.GetState();
                //�ȴ���Button
                if (touchCollection.Count == 1)
                {
                    foreach (TouchLocation t in touchCollection)
                    {
                        if (t.State == TouchLocationState.Pressed || t.State == TouchLocationState.Moved)
                        {
                            foreach (SAControl b in buttonList)
                            {
                                b.OnTouch(t.Position);
                            }
                        }
                    }
                }

                if (OnTouchCollection != null)
                {
                    OnTouchCollection.Invoke(touchCollection);
                }
            }
        }

        public static void UpdateBackButton()
        {
            //����Back���¼�
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (OnBackButton != null)
                {
                    OnBackButton.Invoke();
                }
            }
        }

        public static void UpdateButton()
        {
            foreach (SAControl b in buttonList)
            {
                b.Update();
            }
        }
        #endregion

        #region ��ͣ��ָ�(�����BackButton)
        public static void Enable()
        {
            enable = true;
        }
        public static void Disable()
        {
            enable = false;
        }
        #endregion
    }
}
