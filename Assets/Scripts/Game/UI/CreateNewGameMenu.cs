using Game.GameModel;
using Game.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class CreateNewGameMenu : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] NewGameMenuUI gameMenuUI;

        private GameType gameType;
        private int weight = 3;
        private int height = 3;
        private bool record = false;

        private void Start()
        {
            GameManager.ins.CreateGame(GetDefaultGame());
        }

        private void CreatNewGame()
        {
            GameManager.ins.CreateGame(GetGame());
            gameMenuUI.SetAllText(GameManager.ins.GetGame());
        }

        public void ExitPanel()
        {
            panel.SetActive(false);
            GameStatus.GamePause = false;
        }

        public void OpenPanel()
        {
            panel.SetActive(true);
            GameStatus.GamePause = true;
            gameMenuUI.SetAllText(GameManager.ins.GetGame());
        }

        public void ChangeGameType()
        {
            gameType++;
            int val = (int) gameType % 3;
            gameType = (GameType)val;
            CreatNewGame();
        }

        public void AddHeight()
        {
            height++;

            if (height > 10)
                height = 3;

            CreatNewGame();
        }

        public void AddWeight()
        {
            weight++;

            if (weight > 10)
                weight = 3;

            CreatNewGame();
        }

        private BaseGameModel GetGame()
        {
            BaseGameModel game = null;

            switch (gameType)
            {
                case GameType.Classic:
                    game = new ClassicGame();
                    break;
                case GameType.Snake:
                    game = new SnakeGame();
                    break;
                case GameType.Spiral:
                    game = new SpiralGame();
                    break;
                default:
                    game = new ClassicGame();
                    break;

            }

            game.height = height;
            game.weight = weight;
            game.isRecord = record;
            return game;
        }

        private BaseGameModel GetDefaultGame()
        {
            ClassicGame game = new ClassicGame();
            game.weight = 3;
            game.height = 3;
            game.isRecord = false;
            return game;
        }
    }

    public enum GameType
    {
        Classic = 0,
        Snake = 1,
        Spiral = 2
    }
}

