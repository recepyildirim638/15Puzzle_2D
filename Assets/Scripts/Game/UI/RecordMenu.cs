using Core.DataServices.Model;
using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.UI
{
    public class RecordMenu : MonoBehaviour
    {
        [SerializeField] GameObject recordButtonPrefabs;

        [SerializeField] GameObject panel;

        [SerializeField] List<GameObject> recordButtons;
        [SerializeField] GameObject content;
        public void OpenPanel()
        {
            GameStatus.GamePause = true;
            panel.SetActive(true);
            SetRecords();

        }

        public void ClosePanel()
        {
            panel.SetActive(false);
            GameStatus.GamePause = true;

        }

        public void SetRecords()
        {
            IGameDataService gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            List<RecodData> data = gameDataService.GetGameDataManager().GetRecodDatas();

            if(data.Count > recordButtons.Count)
            {
                CreateRecordButton(data.Count);
            }
            else
            {
                HideRecordButton(data.Count);
            }

            for (int i = 0; i < data.Count; i++)
            {
                recordButtons[i].GetComponent<RecordButton>().SetData(data[i]);
            }

        }

        private void HideRecordButton(int target)
        {
            for (int i = 0; i < recordButtons.Count; i++)
            {
                if (i < target)
                    recordButtons[i].gameObject.SetActive(true);
                else
                    recordButtons[i].gameObject.SetActive(false);
            }
        }

        private void CreateRecordButton(int target)
        {
            int needButtnoCount = target - recordButtons.Count ;

            for (int i = 0; i < needButtnoCount; i++)
            {
                GameObject button = Instantiate(recordButtonPrefabs, content.transform);
                recordButtons.Add(button);
            }
        }

        private void OnEnable()
        {
            ActionManager.LoadGame += ClosePanel;
        }

        private void OnDisable()
        {
            ActionManager.LoadGame -= ClosePanel;
        }
    }

}