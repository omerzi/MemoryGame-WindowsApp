using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MemoryGame_Logic;

namespace MemoryGame_UI
{
    public partial class GameSettings : Form
    {
        public enum eBoardSizes
        {
            Four_Four,
            Four_Five,
            Four_Six,
            Five_Six,
            Five_Four,
            Six_Six,
            Six_Five,
            Six_Four,
            Four = 4,
            Six = 6,
            Five = 5
        }

        private int m_BoardHeight = 4;
        private int m_BoardWidth = 4;
        private eBoardSizes m_CurrentSize = eBoardSizes.Four_Four;

        public event Action<string, string, int, int, GameManager.eGameType> StartGameListeners;

        public GameSettings()
        {
            InitializeComponent();
        }

        private void buttonAgainstFriend_ClickPC(object sender, EventArgs e)
        {
            buttonAgainstFriend.Click -= buttonAgainstFriend_ClickPC;
            buttonAgainstFriend.Click += buttonAgainstFriend_ClickFriend;
            textBoxSecondPlayerName.Enabled = true;
            textBoxSecondPlayerName.Text = " ";
            textBoxSecondPlayerName.ReadOnly = false;
            (sender as Button).Text = "Against PC";
        }

        private void buttonAgainstFriend_ClickFriend(object sender, EventArgs e)
        {
            buttonAgainstFriend.Click -= buttonAgainstFriend_ClickFriend;
            buttonAgainstFriend.Click += buttonAgainstFriend_ClickPC;
            textBoxSecondPlayerName.Enabled = false;
            textBoxSecondPlayerName.Text = "- Computer -";
            textBoxSecondPlayerName.ReadOnly = true;
            (sender as Button).Text = "Against a Friend";
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        { 
            switch(m_CurrentSize)
            {
                case eBoardSizes.Four_Four:
                    setBoardSizes(eBoardSizes.Four_Five, (int)eBoardSizes.Four, (int)eBoardSizes.Five);
                    break;
                case eBoardSizes.Four_Five:
                    setBoardSizes(eBoardSizes.Four_Six, (int)eBoardSizes.Four, (int)eBoardSizes.Six);
                    break;
                case eBoardSizes.Four_Six:
                    setBoardSizes(eBoardSizes.Five_Six, (int)eBoardSizes.Five, (int)eBoardSizes.Six);
                    break;
                case eBoardSizes.Five_Six:
                    setBoardSizes(eBoardSizes.Five_Four, (int)eBoardSizes.Five, (int)eBoardSizes.Four);
                    break;
                case eBoardSizes.Five_Four:
                    setBoardSizes(eBoardSizes.Six_Six, (int)eBoardSizes.Six, (int)eBoardSizes.Six);
                    break;
                case eBoardSizes.Six_Six:
                    setBoardSizes(eBoardSizes.Six_Five, (int)eBoardSizes.Six, (int)eBoardSizes.Five);
                    break;
                case eBoardSizes.Six_Five:
                    setBoardSizes(eBoardSizes.Six_Four, (int)eBoardSizes.Six, (int)eBoardSizes.Four);
                    break;
                case eBoardSizes.Six_Four:
                    setBoardSizes(eBoardSizes.Four_Four, (int)eBoardSizes.Four, (int)eBoardSizes.Four);
                    break;
            }
        }

        private void setBoardSizes(eBoardSizes i_Size, int i_Height, int i_Width)
        {
            m_CurrentSize = i_Size;
            m_BoardHeight = i_Height;
            m_BoardWidth = i_Width;
            buttonBoardSize.Text = string.Format("{0} X {1}", i_Height, i_Width);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxFirstPlayerName.Text) && !string.IsNullOrWhiteSpace(textBoxSecondPlayerName.Text))
            {
                if (textBoxSecondPlayerName.Text == "- Computer -")
                {
                    StartGameListeners?.Invoke(
                        textBoxFirstPlayerName.Text,
                        "PC",
                        m_BoardHeight, 
                        m_BoardWidth, 
                        GameManager.eGameType.AgainstPC);
                }
                else
                {
                    StartGameListeners?.Invoke(
                        textBoxFirstPlayerName.Text,
                        textBoxSecondPlayerName.Text,
                        m_BoardHeight,
                        m_BoardWidth,
                        GameManager.eGameType.AgainstPlayer);
                }
            }
        }
    }
}
