using Game.Model;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameModel
{
    public class ClassicGame : BaseGameModel
    {
        public ClassicGame()
        {
            name = "CLASSIC";
            gameType = GameType.Classic;
        }

        public override bool Goal(List<GridArea> gridAreaList)
        {
            bool result = true;
            for (int i = 0; i < gridAreaList.Count; i++)
            {
                if (gridAreaList[i].cell != null)
                {
                    if (gridAreaList[i].cell.GetValue() != i + 1)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public override List<int> GetHowToPlay()
        {
            List<int> result = new List<int>();

            for (int i = 0;i < GetSize(); i++)
            {
                if(i == GetSize())
                    result.Add(-1);
                else
                    result.Add(i + 1);
            }
            return result;
        }
    }
}

