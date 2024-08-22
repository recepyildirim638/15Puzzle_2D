using Game.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameModel
{
    public abstract class BaseGameModel
    {
        public string name = "default";
        public int weight = 3;
        public int height = 3;
        public bool isRecord = false;

        public int GetSize() => weight * height;
        public abstract bool Goal(List<GridArea> gridAreaList);

        public virtual List<int> GetHowToPlay()
        {
            return null;
        }
    }
}

