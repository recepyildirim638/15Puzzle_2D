using Core.DataServices.Model;
using Game.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] Image cellColor;
        [SerializeField] Image backGroundColor;

        [SerializeField] GameObject panel;

        [SerializeField] Color[] cellColorList;
        [SerializeField] Color[] backGroundColorList;
        SettingsData data;

        public static SettingsMenu ins;

        private void Awake()
        {
            ins = this;
        }

        private void Start()
        {
            IGameDataService gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            data = gameDataService.GetGameDataManager().GetSettings();

            Camera.main.backgroundColor = GetCurrentBackGroundColor();
        }

        public Color GetCurrentCellColor()
        {
            return cellColorList[data.cellColor];
        }

        public Color GetCurrentBackGroundColor()
        {
            return backGroundColorList[data.backgroundColor];
        }

        public void OpenPanel()
        {
            GameStatus.GamePause = true;
            panel.SetActive(true);
            SetCellColorButton();
            SetBackGroundColorButton();
        }

        public void ClosePanel()
        {
            panel.SetActive(false);
            GameStatus.GamePause = true;

        }

        public void SetCellColorButton()
        {
            cellColor.color = cellColorList[data.cellColor];
            ActionManager.ChangeCellColor?.Invoke(cellColorList[data.cellColor]);
        }
        public void SetBackGroundColorButton()
        {
            backGroundColor.color = backGroundColorList[data.backgroundColor];
            Camera.main.backgroundColor = GetCurrentBackGroundColor();
        }

        public void CellColorButton()
        {
            data.cellColor++;
            if(data.cellColor >= cellColorList.Length)
                data.cellColor = 0;

            SetCellColorButton();
        }

        public void BackGroundColorButton()
        {
            data.backgroundColor++;
            if (data.backgroundColor >= backGroundColorList.Length)
                data.backgroundColor = 0;

            SetBackGroundColorButton();
        }
    }

    public enum CellColor
    {

    }
}



