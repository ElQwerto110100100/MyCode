using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRotS.Entity
{
    class ScoreBoard
    {
        static private Dictionary<string, int> scoreBoard = new Dictionary<string, int>();
        private static ScoreBoard _instance;

        public ScoreBoard()
        {

        }

        public static ScoreBoard Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoreBoard();
                }
                return _instance;
            }
        }

        public void AddScore(string level, int score)
        {
            if (scoreBoard.ContainsKey(level))
            {
                //prevent buffer overflow
                if (scoreBoard[level] < score && scoreBoard[level] != Int32.MaxValue) scoreBoard[level] = score;
            }
            else
            {
                scoreBoard.Add(level, score);
            }
        }

        public int GetScore(string name)
        {
            if (scoreBoard.ContainsKey(name))
            {
                return scoreBoard[name];
            } 
            else
            {
                return 0;
            }
        }
    }
}
