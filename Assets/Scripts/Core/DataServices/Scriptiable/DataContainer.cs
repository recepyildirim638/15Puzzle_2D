using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DataServices.Model;

namespace Core.DataServices.Scriptiable
{
    [CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/Game Data", order = 1)]
    public class DataContainer : ScriptableObject
    {
        [Header("Settings Data")]
        public SettingsData settingsData = new SettingsData();

        [Header("Recordin Data")]
        public List<RecodData> recodDatas = new List<RecodData>();

        [ContextMenu("Reset")]
        public void ResetData()
        {
            settingsData.cellColor = 0;
            settingsData.backgroundColor = 0;

            recodDatas.Clear();

            PlayerPrefs.DeleteAll();
            SaveManager.SaveGameData(this);
        }

        [ContextMenu("Save")]
        public void Save()
        {
            SaveManager.SaveGameData(this);
        }
    }
}


