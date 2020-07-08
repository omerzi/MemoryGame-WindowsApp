using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MemoryGame_Logic;

namespace MemoryGame_UI
{
    public class UI
    {
        private const int k_MaxNumOfClicks = 2;
        private Color m_CurrentPlayerColor;
        private readonly GameSettings r_GameSettings = new GameSettings();
        private GameManager m_GameManager;
        private GameForm m_GameForm;
        private readonly List<Point> r_UserClicks = new List<Point>();
        private char[] m_ObjectArray;


        public UI()
        {
            r_GameSettings.StartGameListeners += initGame;
        }

        public void Run()
        {
            DialogResult result = r_GameSettings.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                startGame();
            }
        }

        private void startGame()
        {
            setLabelsData();
            DialogResult gamePlay = m_GameForm.ShowDialog();
            if (gamePlay == DialogResult.OK)
            {
                congratsWinner();
            }
        }

        private void setLabelsData()
        {
            m_GameForm.ChangeLablesProperties(
                m_GameManager.CurrentPlayerName(),
                m_GameManager.FirstPlayerName,
                m_GameManager.FirstPlayerPoints,
                m_GameManager.SecondPlayerName,
                m_GameManager.SecondPlayerPoints);

            if (m_GameManager.CurrentPlayer == GameManager.ePlayerType.FirstPlayer)
            {
                m_GameForm.CurrentPlayer.BackColor = m_GameForm.FirstPlayerPairs.BackColor;
            }
            else
            {
                m_GameForm.CurrentPlayer.BackColor = m_GameForm.SecondPlayerPairs.BackColor;
            }

            m_CurrentPlayerColor = m_GameForm.CurrentPlayer.BackColor;
            m_GameForm.Refresh();
        }

        private void exposeButton(int i_Height, int i_Width, int i_NumOfClicks)
        {
            char charToPrint = m_ObjectArray[m_GameManager.Board[i_Height, i_Width].Index];
            m_GameForm[i_Height, i_Width].BackColor = m_CurrentPlayerColor;
            m_GameForm[i_Height, i_Width].Text = charToPrint.ToString();
            m_GameForm.Refresh();
            if (m_GameManager.CurrentPlayer != GameManager.ePlayerType.PC)
            {
                if (i_NumOfClicks <= k_MaxNumOfClicks)
                {
                    r_UserClicks.Add(new Point(i_Width, i_Height));
                    if (i_NumOfClicks == k_MaxNumOfClicks)
                    {
                        userTurn();
                    }
                }
            }
        }

        private void computerTurn()
        {
            setLabelsData();
            m_GameForm.Refresh();
            m_GameManager.PCTurn(out int firstRowChoise, out int firstColumnChoise);
            exposeButton(firstRowChoise, firstColumnChoise, 0);
            m_GameManager.PCTurn(out int secondRowChoise, out int secondColumnChoise);
            exposeButton(secondRowChoise, secondColumnChoise, 0);
            m_GameManager.CheckChoises(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, out bool toSleep);
            m_GameForm.ChangeButtonsProperties(firstColumnChoise, firstRowChoise, secondColumnChoise, secondRowChoise, toSleep);
            checkIfGameOver();
            m_GameManager.CurrentPlayer = GameManager.ePlayerType.FirstPlayer;
            setLabelsData();
        }

        private void userTurn()
        {
            setLabelsData();
            if (r_UserClicks.Count == k_MaxNumOfClicks)
            {
                Point firstButton = r_UserClicks[0];
                Point secondButton = r_UserClicks[1];
                m_GameManager.CheckChoises(firstButton.X, firstButton.Y, secondButton.X, secondButton.Y, out bool toSleep);
                m_GameForm.ChangeButtonsProperties(firstButton.X, firstButton.Y, secondButton.X, secondButton.Y, toSleep);
                r_UserClicks.Clear();
                checkIfGameOver();
                changeCurrentPlayer();
            }
        }

        private void changeCurrentPlayer()
        {
            if (m_GameManager.GameType == GameManager.eGameType.AgainstPlayer)
            {
                if (m_GameManager.CurrentPlayer == GameManager.ePlayerType.FirstPlayer)
                {
                    m_GameManager.CurrentPlayer = GameManager.ePlayerType.SecondPlayer;
                }
                else
                {
                    m_GameManager.CurrentPlayer = GameManager.ePlayerType.FirstPlayer;
                }

                userTurn();
            }
            else
            {
                m_GameManager.CurrentPlayer = GameManager.ePlayerType.PC;
                computerTurn();
            }
        }

        private void initGame(
            string i_FirstPlayerName,
            string i_SecondPlayerName,
            int i_BoardHeight, int i_BoardWidth,
            GameManager.eGameType i_eGameType)
        {
            m_GameManager = new GameManager(i_BoardWidth, i_BoardHeight, i_FirstPlayerName, i_SecondPlayerName, i_eGameType);
            r_GameSettings.Hide();
            m_GameForm = new GameForm(i_BoardHeight, i_BoardWidth);
            m_GameForm.ExposeButtonListeners += exposeButton;

            m_ObjectArray = new char[(i_BoardHeight * i_BoardWidth) / 2];
            setSigns(m_ObjectArray);
        }

        private void checkIfGameOver()
        {
            if (m_GameManager.IsEnded())
            {
                m_GameForm.Hide();
                congratsWinner();
            }
        }

        private void setSigns(char[] i_Array)
        {
            for (int i = 0; i < i_Array.Length; i++)
            {
                i_Array[i] = (char)('A' + i);
            }
        }

        private void congratsWinner()
        {
            string winnerName = m_GameManager.GetWinnerNameAndPoints(out int numOfPoints, out bool checkTie);
            bool toContinue = m_GameForm.EndGame(winnerName, checkTie);
            if(toContinue)
            {
                initGame(
                    m_GameManager.FirstPlayerName,
                    m_GameManager.SecondPlayerName,
                    m_GameManager.Board.Height,
                    m_GameManager.Board.Width,
                    m_GameManager.GameType);
                startGame();
            }
        }
    }
}