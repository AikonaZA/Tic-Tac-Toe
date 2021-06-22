using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {

        string currentPlayer = "Human";
        int winner = 0;//Current draw, 1 = Human, 2 = AI
        int score = 0;
        List<int> XMoves = new List<int>(); 
        List<int> YMoves = new List<int>();
        List<int> scores = new List<int>();

        int bestXMove = 0, bestYMove = 0;
        bool testing = false;
        int[,] gameBoard = new int[,] { {0,0,0}, 
                                        {0,0,0}, 
                                        {0,0,0} };

        public Form1()
        {
            InitializeComponent();

            //winningXMoves.Add(1);
            //winningYMoves.Add(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
        }

        void PressButton(Button press)
        {
            if (currentPlayer == "Human")
            {
                press.Text = "O";
            }
            //else
            //{
            //    press.Text = "X";
            //}
            AIMove();

            //List doesn't populate? can't progress
            MessageBox.Show("X value = " + XMoves.First().ToString() + "\nY value = " + YMoves.First().ToString());
            
            press.Enabled = false;
        }

        void CheckVictory()
        {
            int movesSpent = 0;
            //Across
            if (gameBoard[0, 0] != 0 && gameBoard[0, 0] == gameBoard[0, 1] && gameBoard[0, 1] == gameBoard[0, 2])
            {
                if (gameBoard[0, 0] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            else if (gameBoard[1, 0] != 0 && gameBoard[1,0] == gameBoard[1,1] && gameBoard[1,1] == gameBoard[1,2])
            {
                if (gameBoard[1, 0] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            else if (gameBoard[2, 0] != 0 && gameBoard[2, 0] == gameBoard[2,1] && gameBoard[2,1] == gameBoard[2, 2])
            {
                if (gameBoard[2, 0] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }//Down
            else if (gameBoard[0, 0] != 0 && gameBoard[0, 0] == gameBoard[1,0] && gameBoard[1,0] == gameBoard[2,0])
            {
                if (gameBoard[0, 0] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            else if (gameBoard[0, 1] != 0 && gameBoard[0,1] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2,1])
            {
                if (gameBoard[0,1] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            else if (gameBoard[0,2] != 0 && gameBoard[0,2] == gameBoard[1,2] && gameBoard[1,2] == gameBoard[2, 2])
            {
                if (gameBoard[0,2] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }//Diagonal
            else if (gameBoard[0, 0] != 0 && gameBoard[0, 0] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 2])
            {
                if (gameBoard[0, 0] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            else if (gameBoard[0, 2] != 0 && gameBoard[0, 2] == gameBoard[1, 1] && gameBoard[1, 1] == gameBoard[2, 0])
            {
                if (gameBoard[0, 2] == 1)
                    Victory("Human");
                else
                    Victory("AI");
            }
            for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    if (gameBoard[x,y] != 0){//Check end of moves for draw
                        movesSpent++;
                    }
                }
            }

            if(movesSpent == 9)
            {
                Victory("Draw");
            }
        }

        int AIMove()
        {
            testing = true;
            CheckVictory();

            if(winner == 0)
            {
                return 0;
            }else if(winner == 1)
            {
                return -10;
            }else if (winner == 2)
            {
                return 10;
            }
            
            for (int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    if (gameBoard[x, y] == 0)
                    {
                        SetBoardValue(x, y);
                        score = AIMove();

                        XMoves.Add(x);
                        YMoves.Add(y);
                        scores.Add(score);
                        gameBoard[x, y] = 0;
                    }
                }
            }
            int place = 0;
            foreach(int points in scores)
            {
                if(points == 10)
                {
                    bestXMove = XMoves.ElementAt(place);
                    bestYMove = YMoves.ElementAt(place);
                }
                place++;
            }


            //if (score == 10)
            //{
            //    winningXMoves.Add(moveX);
            //    winningYMoves.Add(moveY);
            //}
            return 10;
        }

        void Victory(string victor)
        {
            if(testing == false)
            {
                if(victor == "Draw")
                {
                    MessageBox.Show("It was a Draw");
                }
                else
                {
                    MessageBox.Show(victor + " has won");
                }
            }
            else
            {
                if (victor == "Human")
                {
                    winner = 1;
                    score = -10;
                }
                else if (victor == "AI")
                {
                    winner = 2;
                    score = 10;
                }
                else
                {
                    winner = 0;
                    score = 0;
                }
            }
        }

        void SetBoardValue(int x, int y)
        {
            if(currentPlayer == "Human")
            {
                gameBoard[x, y] = 1;

                currentPlayer = "AI";
            }
            else
            {
                gameBoard[x, y] = 2;
                currentPlayer = "Human";
            }
            CheckVictory();
        }


        #region ButtonClicks

        private void button1_Click(object sender, EventArgs e)
        {
            PressButton(button1);

            SetBoardValue(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PressButton(button2);

            SetBoardValue(0, 1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PressButton(button3);

            SetBoardValue(0, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PressButton(button4);

            SetBoardValue(1, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PressButton(button5);

            SetBoardValue(1, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            PressButton(button6);

            SetBoardValue(1, 2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PressButton(button7);

            SetBoardValue(2, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            PressButton(button8);

            SetBoardValue(2, 1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PressButton(button9);

            SetBoardValue(2, 2);
        }
        #endregion
    }
}
