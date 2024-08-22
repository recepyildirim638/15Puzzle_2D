using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DataServices.Model;

namespace Core.DataServices
{
    public interface IDataManager
    {
        void Initalize();
        
        SettingsData GetSettings();

        List<RecodData> GetRecodDatas();



    }
}

