using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GameModel;
using Game.UI;

namespace Core.DataServices.Model
{
    [Serializable]
    public class RecodData 
    {
        public GameType gameType;
        public int weight;
        public int height;
        public List<int> tableList = new List<int>();
        public int moveCount;
        public float playTime;
        public string day;
    }

}
