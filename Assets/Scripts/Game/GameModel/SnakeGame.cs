using Game.GameModel;
using Game.Model;
using Game.UI;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Game.GameModel
{
    public class SnakeGame : BaseGameModel
    {
        public SnakeGame()
        {
            name = "SNAKE";
            gameType = GameType.Snake;
        }
        public override bool Goal(List<GridArea> gridAreaList)
        {
            bool result = true;
            List<int> goalList = GetHowToPlay();

            for (int i = 0; i < gridAreaList.Count; i++)
            {
                if (gridAreaList[i].cell != null)
                {
                    if (gridAreaList[i].cell.GetValue() != goalList[i])
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
            for (int i = 0; i < GetSize(); i++)
            {
                int row = i / weight;
                int rowValue;

                if (row % 2 == 0)
                    rowValue = i + 1;
                else
                    rowValue =  (weight * row) +  weight - (i % weight);
                
                result.Add(rowValue % GetSize());
            }
            return result;
        }
    }
}

