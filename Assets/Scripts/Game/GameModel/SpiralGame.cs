using Game.Model;
using Game.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameModel
{
    public class SpiralGame : BaseGameModel
    {
        public SpiralGame()
        {
            name = "SPIRAL";
            gameType = GameType.Spiral;
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
            int width = this.weight;
            int height = this.height;
            int[,] matrix = new int[height, width];

            int num = 1;
            int left = 0, right = width - 1, top = 0, bottom = height - 1;

            while (left <= right && top <= bottom)
            {

                for (int i = left; i <= right; i++)
                {
                    matrix[top, i] = num++;
                }
                top++;

                for (int i = top; i <= bottom; i++)
                {
                    matrix[i, right] = num++;
                }
                right--;


                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--)
                    {
                        matrix[bottom, i] = num++;
                    }
                    bottom--;
                }

                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--)
                    {
                        matrix[i, left] = num++;
                    }
                    left++;
                }
            }
            List<int> result = new List<int>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result.Add(matrix[i, j]);

                }
            }
            return result;

        }
    }
}

