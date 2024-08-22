using Core.DataServices.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DataServices
{
    public class DataManager : BaseGameData, IDataManager
    {
        public void Initalize() => LoadGameData();
        public SettingsData GetSettings() => settingsData;
        List<RecodData> IDataManager.GetRecodDatas() => recods;

        private void Awake()
        {
            Initalize();
        }
    }
}




