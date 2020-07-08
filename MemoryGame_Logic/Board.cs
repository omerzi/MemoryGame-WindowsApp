using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame_Logic
{
    public class Board
    {
        private int m_Width;
        private int m_Height;
        private Card[,] m_GameBoard;
        private Random m_ChooseIndexForCard = new Random();

        public Board(int i_Height, int i_Width)
        {
            m_Width = i_Width;
            m_Height = i_Height;
            m_GameBoard = new Card[i_Height, i_Width];
            initBoard();
        }

        public int Height
        {
            get
            {
                return m_Height;
            }

            set
            {
                m_Height = value;
            }
        }

        public int Width
        {
            get
            {
                return m_Width;
            }

            set
            {
                m_Width = value;
            }
        }

        public Card[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }

            set
            {
                m_GameBoard = value;
            }
        }

        public Card this[int row, int column]
        {
            get
            {
                return m_GameBoard[row, column];
            }

            set 
            {
                m_GameBoard[row, column] = value; 
            }
        }

        private void initBoard()
        {
            putIndexersInBoard();
            shuffleIndexers();
        }

        private void putIndexersInBoard()
        {
            int value = 0;
            int counter = 1;
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    m_GameBoard[i, j] = new Card(value);
                    if(counter == 2)
                    {
                        counter = 0;
                        value++;
                    }

                    counter++;
                }
            }
        }

        private void shuffleIndexers()
        {
            int columnRandom, lineRandom, tempIndex;
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    columnRandom = m_ChooseIndexForCard.Next(0, Width);
                    lineRandom = m_ChooseIndexForCard.Next(0, Height);
                    tempIndex = m_GameBoard[i, j].Index;
                    m_GameBoard[i, j].Index = m_GameBoard[lineRandom, columnRandom].Index;
                    m_GameBoard[lineRandom, columnRandom].Index = tempIndex;
                }
            }
        }
    }
}
