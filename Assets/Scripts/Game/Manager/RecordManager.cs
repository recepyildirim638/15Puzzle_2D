using Core.DataServices.Model;
using Game.GameModel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class RecordManager : MonoBehaviour
    {
        List<RecodData> data;
        private RecodData recodData;

        private void Start()
        {
            IGameDataService gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            data = gameDataService.GetGameDataManager().GetRecodDatas();
        }

        public RecodData GetRecordData() => recodData;

        public void NewGameLoad()
        {
            recodData = null;
        }

        public void GameWin()
        {
            recodData.complated = true;
        }

        public void LoadData(RecodData data)
        {
            recodData = data;
        }

        public void CreateNewRecord(BaseGameModel game)
        {
            recodData = new RecodData();
            recodData.weight = game.weight;
            recodData.height = game.height;
            recodData.gameType = game.gameType;
            recodData.day = DateTime.Now.ToString();
            recodData.id = data.Count;

            data.Add(recodData);
        }

        public void SetCellList(List<int> cellList)
        {
            recodData.tableList = cellList;
        }

        public void SetData(int moveCount, float playTime)
        {
            if (recodData == null)
                return;

            recodData.moveCount = moveCount;
            recodData.playTime = playTime;
        }
    }
}


