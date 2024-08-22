using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bootstraper 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        ServiceLocator.Initalize();

        ServiceLocator.Current.Register<IGameDataService>(new GameDataService());
    }
}
