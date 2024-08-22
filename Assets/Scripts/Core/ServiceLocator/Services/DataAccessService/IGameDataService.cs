using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.DataServices;
public interface IGameDataService : IGameService
{
    IDataManager GetGameDataManager();
}
