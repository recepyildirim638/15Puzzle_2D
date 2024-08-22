using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.GameModel;
using static MouseHandler;
using System;
using System.Collections;
using Game.Creator;
using TMPro;
using Game.UI;
using Core.DataServices.Model;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager ins;

        [SerializeField] GridCreator creator;
        [SerializeField] RecordManager recordManager;


        [SerializeField] List<GridArea> gridAreaList;

        [SerializeField] InGameMenuDetail gamedetail;

        private BaseGameModel mainGame;

        private int moveCount = 0;
        private float playTime = 0f;

        List<int> recordCellList;


        private void Awake()
        {
            ins = this;
        }
        public List<int> GetRecordList() => recordCellList;

        public void ResumeGame()
        {
            for (int i = 0; i < gridAreaList.Count; i++)
            {
                if (gridAreaList[i].cell != null)
                {
                    Cell cell = gridAreaList[i].cell;
                    cell.transform.position = gridAreaList[i].gridPosition;
                    cell.SetResumeValue();
                }
                   
            }
        }

        public void HowToPlay()
        {
            List<int> list = mainGame.GetHowToPlay();
            List<Cell> cells = new List<Cell>();


            for (int i = 0; i < gridAreaList.Count; i++)
            {
                if (gridAreaList[i].cell != null)
                    cells.Add(gridAreaList[i].cell);
            }
            int showCellIndex = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != 0 && list[i] != list.Count)
                {
                    cells[showCellIndex].transform.position = gridAreaList[i].gridPosition;
                    cells[showCellIndex].SetText(list[i]);
                    showCellIndex++;
                }
            }
        }

        private void FixedUpdate()
        {
            if (GameStatus.GameStart == true)
            {
                if (GameStatus.GamePause == false)
                {
                    playTime += Time.fixedDeltaTime;
                    gamedetail.SetTimer(playTime);
                }
               
            }
        }

        public BaseGameModel GetGame() => mainGame;

        private void ClearGrids()
        {
            if (gridAreaList.Count > 0)
            {
                for (int i = 0; i < gridAreaList.Count; i++)
                {
                    if (gridAreaList[i].cell != null)
                        gridAreaList[i].cell.gameObject.SetActive(false);
                    else
                        continue;
                }
                gridAreaList.Clear();
            }
        }

        public void CreateGame(BaseGameModel game)
        {
            ClearGrids();

            GameStatus.GameStart = false;
            GameStatus.GamePause = false;
            moveCount = 0;
            mainGame = game;

            GameCreator gameCreator = new GameCreator(game, creator, false);
            gridAreaList = gameCreator.GetGridArea();
            gamedetail.SetNewGame(game.name);
            recordManager.NewGameLoad();
            recordCellList = new List<int>();

        }

        public void LoadGame(int id)
        {
            ClearGrids();
            GameStatus.GameStart = false;
            GameStatus.GamePause = false;

            IGameDataService gameDataService = ServiceLocator.Current.Get<IGameDataService>();
            List<RecodData> data = gameDataService.GetGameDataManager().GetRecodDatas();
            RecodData recodData = data[id];
            recordManager.LoadData(recodData);

            switch (recodData.gameType)
            {
                case GameType.Classic:
                    mainGame = new ClassicGame();
                    break;
                case GameType.Snake:
                    mainGame = new SnakeGame();
                    break;
                case GameType.Spiral:
                    mainGame = new SpiralGame();
                    break;
                default:
                    mainGame = new ClassicGame();
                    break;
            }

            mainGame.weight = recodData.weight;
            mainGame.height = recodData.height;
            playTime = recodData.playTime;
            moveCount = recodData.moveCount;

            recordCellList = recodData.tableList;
            GameCreator gameCreator = new GameCreator(mainGame, creator, true);
            gridAreaList = gameCreator.GetGridArea();
            gamedetail.SetNewGame(mainGame.name);


            for (int i = 0; i < recordCellList.Count; i++)
            {
                if (gridAreaList[i].cell == null)
                    continue;
                else
                    gridAreaList[i].cell.SetValue(recordCellList[i]);
            }

        }


        public void Sorgu(Cell cell, MOVE_DIRECTION direction, Action callBack)
        {
            Debug.Log("SORUGU " + direction + " " + cell.GetValue());
            switch (direction)
            {
                case MOVE_DIRECTION.LEFT:
                    MoveLeft(cell, callBack);
                    break;
                case MOVE_DIRECTION.RIGHT:
                    MoveRight(cell, callBack);
                    break;
                case MOVE_DIRECTION.UP:
                    MoveUp(cell, callBack);
                    break;
                case MOVE_DIRECTION.DOWN:
                    MoveDown(cell, callBack);
                    break;
            }


            if (mainGame.Goal(this.gridAreaList))
            {
                Debug.Log("WIN");
            }

            if (GameStatus.GameStart == false)
            {
                GameStatus.GameStart = true;

                RecodData recodData = recordManager.GetRecordData();

                if(recodData == null)
                {
                    recordManager.CreateNewRecord(mainGame);

                    for (int i = 0; i < gridAreaList.Count ; i++)
                        recordCellList.Add(0);

                    recordManager.SetCellList(recordCellList);
                }
            }

            for (int i = 0; i < recordCellList.Count ; i++)
            {
                if (gridAreaList[i].cell == null)
                    recordCellList[i] = -1;
                else
                    recordCellList[i] = gridAreaList[i].cell.GetValue();
            }
                

            recordManager.SetData(moveCount, playTime);
            gamedetail.SetCount(moveCount);
            DelayCallBack(callBack);
        }


        

        bool isDelay = false;
        public void DelayCallBack(Action callBack)
        {
            if (isDelay == false)
                StartCoroutine(DelayCallBackEnumerator(callBack));
        }

        IEnumerator DelayCallBackEnumerator(Action callBack)
        {
            isDelay = true;
            yield return new WaitForSeconds(.1f);
            callBack.Invoke();
            isDelay = false;
        }



        #region RIGTH
        private void MoveRight(Cell rightCell, Action callBack)
        {
            if (IsMoveRigth(rightCell.GetIndex()))
            {
                List<Cell> rightCellList = GetRightCell(rightCell.GetIndex());
                int nullCellIndex = rightCellList[0].GetIndex();
                int currentIndex = nullCellIndex;
                for (int i = 0; i < rightCellList.Count; i++)
                {
                    Cell moveCell = rightCellList[i];
                    moveCell.transform.position = gridAreaList[currentIndex + 1].gridPosition;
                    moveCell.SetIndex(currentIndex + 1);

                    gridAreaList[currentIndex + 1].cell = moveCell;
                    currentIndex++;
                    moveCount++;
                }
                gridAreaList[nullCellIndex].cell = null;
            }
            else
            {
                Debug.Log("SAG GIDEMEZ");
            }
        }

        private bool IsMoveRigth(int index)
        {
            index++;

            if (index % mainGame.weight == 0)
                return false;

            bool result = false;

            while (index % mainGame.weight != 0 )
            {
                if (gridAreaList[index].cell == null)
                {
                    result = true;
                    break;
                }
                index++;
            }
            return result;
        }

        private List<Cell> GetRightCell(int index)
        {
            List<Cell> cells = new List<Cell>();

            while (index + 1 % mainGame.weight != 0)
            {
                if (gridAreaList[index].cell == null)
                {
                    break;
                }
                cells.Add(gridAreaList[index].cell);
                index++;
            }

            return cells;
        }

        #endregion

        #region LEFT
        private void MoveLeft(Cell leftCell, Action callBack)
        {
            if (IsMoveLeft(leftCell.GetIndex()))
            {
                List<Cell> lefCellList = GetLeftCell(leftCell.GetIndex());

                int nullCellIndex = lefCellList[0].GetIndex();
                int currentIndex = nullCellIndex;
                
                for (int i = 0; i < lefCellList.Count; i++)
                {
                    Cell moveCell = lefCellList[i];
                    moveCell.transform.position = gridAreaList[currentIndex - 1].gridPosition;
                    moveCell.SetIndex(currentIndex - 1);

                    gridAreaList[currentIndex - 1].cell = moveCell;
                    currentIndex--;
                    moveCount++;
                }
                gridAreaList[nullCellIndex].cell = null;
            }
            else
            {
                Debug.Log("SOL GIDEMEZ");
            }
        }
        private bool IsMoveLeft(int index)
        {
            if(index <= 0)
                return false;

            if (index % mainGame.weight == 0)
                return false;

            bool result = false;

            while (index % mainGame.weight != 0)
            {
                index--;
                if (gridAreaList[index].cell == null)
                {
                    result = true;
                    break;
                }
                
            }
            return result;
        }

        private List<Cell> GetLeftCell(int index)
        {
            List<Cell> cells = new List<Cell>();

            while (index % mainGame.weight != 0)
            {
                if(index < 0) 
                    break;

                if (gridAreaList[index].cell == null)
                    break;

                cells.Add(gridAreaList[index].cell);
                index--;
            }

            return cells;
        }
        #endregion

        #region DOWN

        private void MoveDown(Cell downCell, Action callBack)
        {
            if (IsDownMove(downCell.GetIndex()))
            {
                List<Cell> downCellList = GetDownCell(downCell.GetIndex());
                int nullIndex = downCellList[0].GetIndex();
              
                Debug.Log("DONW " + downCellList.Count);
              
                for (int i = downCellList.Count - 1; i >= 0; i--)
                {
                    int downIndex = GetDownIndex(downCellList[i].GetIndex());

                    if (downIndex != -1)
                    {
                        Cell moveCell = downCellList[i];
                        moveCell.transform.position = gridAreaList[downIndex].gridPosition;
                        moveCell.SetIndex(downIndex);

                        gridAreaList[downIndex].cell = moveCell;
                        moveCount++;
                    }
                    else
                    {
                        Debug.LogError("DOWN INDEX HATASI");
                    }
                }
                gridAreaList[nullIndex].cell = null;
            }
            else
            {
                Debug.Log("down false");
            }
        }
        private bool IsDownMove(int index)
        {
            if ((index + mainGame.weight) >= (mainGame.weight * mainGame.height))
            {
                return false;
            }

            bool result = false;

            for (int i = 0; i < mainGame.height; i++)
            {
                if (index + (i * mainGame.weight) >= mainGame.weight * mainGame.height)
                {
                    break;
                }

                if (gridAreaList[index + (i * mainGame.weight)].cell == null)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private List<Cell> GetDownCell(int index)
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < mainGame.height; i++)
            {
                if (index + (i * mainGame.weight) >= mainGame.weight * mainGame.height)
                    break;

                Cell cell = gridAreaList[index + (i * mainGame.weight)].cell;

                if (cell != null)
                {
                    cells.Add(cell);
                }
                else
                {
                    break;
                }
            }
            return cells;
        }

        private int GetDownIndex(int index)
        {
            if (index + mainGame.weight >= mainGame.weight * mainGame.height)
                return -1;

            return index + mainGame.weight;
        }

        #endregion

        #region UP

        private void MoveUp(Cell cell, Action callBack)
        {
            if (IsUpMove(cell.GetIndex()))
            {
                List<Cell> upCellList = GetUpCell(cell.GetIndex());
                int nullIndex = upCellList[0].GetIndex();

                for (int i = upCellList.Count - 1; i >= 0; i--)
                {
                    int upIndex = GetUpIndex(upCellList[i].GetIndex());
                    
                    if (upIndex != -1)
                    {
                        Cell moveCell = upCellList[i];
                        moveCell.transform.position = gridAreaList[upIndex].gridPosition;
                        moveCell.SetIndex(upIndex);

                        gridAreaList[upIndex].cell = moveCell;
                        moveCount++;
                    }
                    else
                    {
                        Debug.LogError("UP INDEX HATASI");
                    }
                }
                gridAreaList[nullIndex].cell = null;

            }
            else
            {
                Debug.Log("up false");
            }
        }

        private int GetUpIndex(int index)
        {
            if (index - mainGame.weight < 0)
                return -1;

            return index - mainGame.weight;
        }

        private bool IsUpMove(int index)
        {
            if (index - mainGame.weight < 0)
                return false;

           
            bool result = false;
            for (int i = 0; i < mainGame.height; i++)
            {
                if (index - (i * mainGame.weight) < 0)
                    break;

                if (gridAreaList[index - (i * mainGame.weight)].cell == null)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private List<Cell> GetUpCell(int index)
        {
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < mainGame.height; i++)
            {
                if (index - (i * mainGame.weight) < 0)
                    break;

                Cell cell = gridAreaList[index - (i * mainGame.weight)].cell;

                if(cell != null)
                {
                    cells.Add(cell);
                }
                else
                {
                    break;
                }
            }
          
            return cells;
        }

        #endregion

    }
}

