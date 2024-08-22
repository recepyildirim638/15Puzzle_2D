using Game.GameModel;
using Game.Manager;
using Game.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Creator
{
    public class GameCreator 
    {
        [SerializeField] List<GridArea> gridAreaList = new List<GridArea>();

        public List<GridArea> GetGridArea() => gridAreaList;

       
        public GameCreator(BaseGameModel game, GridCreator creator, bool isLoad)
        {
            int weight = game.weight;
            int height = game.height;

            List<Vector3> gridPos = creator.CreateGrid(weight, height);
            float gridScale = creator.GetGridScale();

            int gridCount = weight * height;

            gridAreaList.Clear();
            List<int> gameMap;

            if (!isLoad)
                gameMap = CreateRandomMap(gridCount);
            else
                gameMap = GameManager.ins.GetRecordList();


            for (int i = 0; i < gameMap.Count; i++)
            {
                Cell cell = null;

                if (gameMap[i] != -1)
                {
                    GameObject gridObject = PoolManager.ins.GetPoolItem(POOL_TYPE.CELL);
                    gridObject.transform.position = gridPos[i];
                    gridObject.name = gameMap[i].ToString();
                    gridObject.transform.localScale = new Vector3(gridScale, gridScale, 1f);
                    cell = gridObject.GetComponent<Cell>();
                    cell.SetValue(gameMap[i]);
                    cell.SetText(gameMap[i]);
                    cell.SetIndex(i);
                }

                GridArea area = new GridArea();
                area.gridPosition = gridPos[i];
                area.cell = cell;
                gridAreaList.Add(area);
            }
        }

        private List<int> CreateRandomMap(int count)
        {
            List<int> map = new List<int>();

            for (int i = 0; i < count; i++)
            {
                if (i == count - 1)
                    map.Add(-1);
                else
                    map.Add(i + 1);
            }

            for (int i = 0; i < 100; i++)
            {
                int rnd = UnityEngine.Random.Range(0, count);
                int temp = map[rnd];
                map[rnd] = map[0];
                map[0] = temp;
            }


            //int[] test = new int[] { 1, 2, 3, 4, 5, 6, -1, 7, 8 };
            //for (int i = 0; i < map.Count; i++)
            //{
            //    map[i] = test[i];
            //}

            return map;
        }
    }
}

