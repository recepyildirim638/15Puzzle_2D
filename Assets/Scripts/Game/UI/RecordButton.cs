using Core.DataServices.Model;
using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RecordButton : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI timeText;
        [SerializeField] TextMeshProUGUI gameTypeText;
        [SerializeField] TextMeshProUGUI playTimeText;

        Button button;
        int id;

        private void Start()
        {
            button = GetComponent<Button>(); 
            button.onClick.AddListener(() => SetlectData());
        }

        public void SetlectData()
        {
            GameManager.ins.LoadGame(id);
            ActionManager.LoadGame?.Invoke();
        }

        public void SetData(RecodData data)
        {
            id = data.id;
            timeText.text = data.day.ToString();
            gameTypeText.text = data.gameType.ToString() +" " + data.weight + "x"+ data.height ;
            playTimeText.text = "Time:" + data.playTime.ToString("n0") + " Moves " + data.moveCount;
        }
    }
}

