using Game.Manager;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class InGameMenuDetail : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI gameTypeText;
        [SerializeField] TextMeshProUGUI moveCountText;
        [SerializeField] TextMeshProUGUI timerText;

        [SerializeField] GameObject howToPlayPanel;
        public void SetNewGame(string gameType)
        {
            gameTypeText.text = gameType;
            SetCount(0);
            SetTimer(0f);
        }
        public void SetGameText(string gameType)
        {
            gameTypeText.text = gameType;
        }
        public void SetCount(int cnt)
        {
            moveCountText.text ="Moves "+ cnt.ToString();
        }

        public void SetTimer(float timer)
        {
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
           // int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
        }


        public void HowToPlayButton()
        {
            GameStatus.GamePause = true;
            howToPlayPanel.SetActive(true);
            GameManager.ins.HowToPlay();
        }

        public void HowToPlayEnd()
        {
            GameManager.ins.ResumeGame();
            GameStatus.GamePause = false;
            howToPlayPanel.SetActive(false);
        }
    }
}

