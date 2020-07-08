using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame_UI
{
    public partial class GameForm : Form
    {
        private Button[,] m_ButtonMatrix;
        private readonly Dictionary<Button, Point> r_ButtonsLocations = new Dictionary<Button, Point>();
        private int m_ClickCounter = 0;
        private Button m_LastSender = null;

        public event Action <int, int, int> ExposeButtonListeners;

        public GameForm(int i_BoardHeight, int i_BoardWidth)
        {
            InitializeComponent();
            this.ClientSize = new Size(i_BoardWidth * 70 + 40, i_BoardHeight * 70 + 100);
            initBoard(i_BoardHeight, i_BoardWidth);
        }

        private void initBoard(int i_BoardHeight, int i_BoardWidth)
        {
            m_ButtonMatrix = new Button[i_BoardHeight, i_BoardWidth];
            for (int i = 0; i < i_BoardHeight; i++)
            {
                for (int j = 0; j < i_BoardWidth; j++)
                {
                    m_ButtonMatrix[i, j] = new Button();
                    m_ButtonMatrix[i, j].Size = new Size(60, 60);
                    m_ButtonMatrix[i, j].Location = new Point(20 + 70 * j, 20 + 70 * i);
                    m_ButtonMatrix[i, j].MouseClick += removeButtonFocus_MouseClick;
                    m_ButtonMatrix[i, j].Click += ExposeButton_Click;
                    m_ButtonMatrix[i, j].BackColor = Color.LightGray;
                    m_ButtonMatrix[i, j].TabStop = false;

                    r_ButtonsLocations.Add(m_ButtonMatrix[i, j], new Point(j, i));
                    this.Controls.Add(m_ButtonMatrix[i, j]);
                }
            }
        }

        private void removeButtonFocus_MouseClick(object sender, MouseEventArgs e)
        {
            labelFirstPlayerPairs.Focus();
        }

        internal void ChangeLablesProperties(
            string i_CurrentPlayerName,
            string i_FirstPlayerName,
            int i_FirstPlayerPoints,
            string i_SecondPlayerName,
            int i_SecondPlayerPoints)
        {
            CurrentPlayer.Text = string.Format("Current Player: {0}", i_CurrentPlayerName);
            FirstPlayerPairs.Text = string.Format(
                "{0} : {1} Pairs",
                i_FirstPlayerName,
                i_FirstPlayerPoints);
            SecondPlayerPairs.Text = string.Format(
               "{0} : {1} Pairs",
               i_SecondPlayerName,
               i_SecondPlayerPoints);
        }

        private void ExposeButton_Click(object sender, EventArgs e)
        {
            if ((sender as Button) != m_LastSender)
            {
                m_LastSender = (sender as Button);
                m_ClickCounter++;
                Point matrixLocation;
                bool isPointExist = r_ButtonsLocations.TryGetValue(sender as Button, out matrixLocation);
                if (isPointExist)
                {
                    ExposeButtonListeners?.Invoke(matrixLocation.Y, matrixLocation.X, m_ClickCounter);
                    if (m_ClickCounter == 2)
                    {
                        m_LastSender = null;
                        m_ClickCounter = 0;
                    }
                }
            }
        }

        public Label CurrentPlayer
        {
            get { return labelCurrentPlayer; }
            set { labelCurrentPlayer = value; }
        }

        public Label FirstPlayerPairs
        {
            get { return labelFirstPlayerPairs; }
        }

        public Label SecondPlayerPairs
        {
            get { return labelSecondPlayerPairs; }
        }

        public Button this[int row, int column]
        {
            get
            {
                return m_ButtonMatrix[row, column];
            }
            set
            {
                m_ButtonMatrix[row, column] = value;
            }
        }

        internal void ChangeButtonsProperties(
            int i_FirstColumnChoise,
            int i_FirstRowChoise,
            int i_SecondColumnChoise,
            int i_SecondRowChoise, 
            bool i_ToSleep)
        {
            if (i_ToSleep)
            {
                System.Threading.Thread.Sleep(1000);
                m_ButtonMatrix[i_FirstRowChoise, i_FirstColumnChoise].Text = " ";
                m_ButtonMatrix[i_SecondRowChoise, i_SecondColumnChoise].Text = " ";
                m_ButtonMatrix[i_FirstRowChoise, i_FirstColumnChoise].BackColor = Color.LightGray;
                m_ButtonMatrix[i_SecondRowChoise, i_SecondColumnChoise].BackColor = Color.LightGray;
                this.Refresh();
            }
            else
            {
                m_ButtonMatrix[i_FirstRowChoise, i_FirstColumnChoise].Enabled = false;
                m_ButtonMatrix[i_SecondRowChoise, i_SecondColumnChoise].Enabled = false;
            }
        }

        internal bool EndGame(string i_WinnerName, bool i_CheckTie)
        {
            string result;
            if (!i_CheckTie)
            {
                result = string.Format(
@"Congratulations {0}, you won the Game!
Would you like another round?", i_WinnerName);
            }
            else
            {
                result = string.Format(
@"Its a Tie!
Would you like another round?");
            }

            DialogResult toContinue = MessageBox.Show(result, "Game Ended", MessageBoxButtons.YesNo);
            return toContinue == DialogResult.Yes;
        }
    }
}
