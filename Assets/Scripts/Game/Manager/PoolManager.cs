using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager ins;
        public List<PoolCell> poolCellList = new List<PoolCell>();
        private void Awake()
        {
            ins = this;
        }

        public GameObject CratePoolItem(POOL_TYPE items)
        {
            PoolCell poolCell = GetPoolCell(items);
            return poolCell.prefabs;
        }

        public GameObject GetPoolItem(POOL_TYPE items)
        {
            PoolCell poolCell = GetPoolCell(items);
            GameObject resultObject = null;
            bool result = false;

            for (int i = 0; i < poolCell.itemList.Count; i++)
            {
                if (poolCell.itemList[i].gameObject.activeSelf == false)
                {
                    resultObject = poolCell.itemList[i].gameObject;

                    resultObject.SetActive(true);
                    result = true;
                    break;
                }
            }
            if (result == false)
            {
                resultObject = Instantiate(poolCell.prefabs, transform);
                resultObject.SetActive(true);
                poolCell.itemList.Add(resultObject);
            }
            return resultObject;
        }

        PoolCell GetPoolCell(POOL_TYPE items)
        {
            if ((int)items > poolCellList.Count)
                return poolCellList[0];
            else
                return poolCellList[(int)items];
        }
    }

    [System.Serializable]
    public class PoolCell
    {
        public POOL_TYPE type;
        public GameObject prefabs;
        public List<GameObject> itemList = new List<GameObject>();
    }
    public enum POOL_TYPE
    {
        CELL = 0
    }
}



