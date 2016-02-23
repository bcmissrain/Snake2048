using Samurai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookLibrary
{
    static class GameData
    {
        const string SINGLE_SCORE_KEY = "SINGLE_SCORE";
        const string DOUBLE_SCORE_KEY = "DOUBLE_SCORE";
        private const string RATE = "RATE";
        private const string NORATE = "NORATE";

        public const int WIN_SCORE = 2048;
        public static int gCurrentScore;
        public static int gBestSingleScore;
        public static int gBestDoubleScore;
        //是否评价
        public static bool gRate;
        public static void SaveRate()
        {
            SAStorage.SaveData(RATE, RATE);
        }
        /// <summary>
        /// 读取是否评价
        /// </summary>
        public static void ReadRate()
        {
            gRate = false;
            string temp = SAStorage.ReadData(RATE);
            if (temp != null)
            {
                if (temp.Equals(RATE))
                {
                    gRate = true;
                }
            }
        }
        public static void ScorePlus(int add)
        {
            gCurrentScore += add;
        }
        public static bool IfSingleNewScore() 
        {
            return gCurrentScore>gBestSingleScore;
        }
        public static bool IfDoubleNewScore()
        {
            return gCurrentScore > gBestDoubleScore;
        }
        public static void SaveSingleRecord()
        {
            if(IfSingleNewScore())
            {
                gBestSingleScore = gCurrentScore;
                SAStorage.SaveData(SINGLE_SCORE_KEY, gBestSingleScore.ToString());
            }
        }
        public static void SaveDoubleRecord()
        {
            if (IfDoubleNewScore())
            {
                gBestDoubleScore = gCurrentScore;
                SAStorage.SaveData(DOUBLE_SCORE_KEY, gBestDoubleScore.ToString());
            }
        }
        public static void ReadSingleScore()
        {
            if (SAStorage.Contains(SINGLE_SCORE_KEY))
            {
                gBestSingleScore = int.Parse(SAStorage.ReadData(SINGLE_SCORE_KEY));
            }
            else
            {
                SAStorage.SaveData(SINGLE_SCORE_KEY, "0");
                gBestSingleScore = int.Parse(SAStorage.ReadData(SINGLE_SCORE_KEY));
            }
        }
        public static void ReadDoubleScore()
        {
            if (SAStorage.Contains(DOUBLE_SCORE_KEY))
            {
                gBestDoubleScore = int.Parse(SAStorage.ReadData(DOUBLE_SCORE_KEY));
            }
            else
            {
                SAStorage.SaveData(DOUBLE_SCORE_KEY, "0");
                gBestDoubleScore = int.Parse(SAStorage.ReadData(DOUBLE_SCORE_KEY));
            }
        }
    }
}
