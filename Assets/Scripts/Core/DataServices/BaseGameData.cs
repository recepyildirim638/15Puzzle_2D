using Core.DataServices.Scriptiable;
using UnityEngine;
using Core.DataServices.Model;
using System.Collections.Generic;

namespace Core.DataServices
{
    public class BaseGameData : MonoBehaviour
    {
        [Header("Game Data")]
        [SerializeField] protected DataContainer data;

        [Header("Behaviour")]
        protected SettingsData settingsData;
        protected List<RecodData> recods;
        protected void LoadGameData()
        {
            SaveManager.LoadGameData(data);
            settingsData = data.settingsData;
            recods = data.recodDatas;
        }
        public void SaveGameData()
        {
            data.settingsData = settingsData;
            data.recodDatas = recods;
            SaveManager.SaveGameData(data);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveGameData();
            }
        }

        private void OnApplicationQuit()
        {
            SaveGameData();
        }
    }
}

