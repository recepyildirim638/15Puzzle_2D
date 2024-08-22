using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DataServices;


public class GameDataService : IGameDataService
{
    private IDataManager dataManager;

    public IDataManager GetGameDataManager()
    {
        if (dataManager == null)
            dataManager = Object.FindObjectOfType<DataManager>();

        return dataManager;
    }
}
