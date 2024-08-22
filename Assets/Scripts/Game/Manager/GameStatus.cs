using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public static class GameStatus
    {
        private static bool _gameStart;

        public static bool GameStart
        {
            get { return _gameStart; }
            set { _gameStart = value; }
        }

        private static bool _gamePause;

        public static bool GamePause
        {
            get { return _gamePause; }
            set { _gamePause = value; }
        }
    }
}
