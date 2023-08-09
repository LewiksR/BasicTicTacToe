using System;

namespace TicTacToe
{
    public class Game
    {
        string[,] board = new string[3, 3];

        private void Setup()
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    board[x, y] = " ";
                }
            }
        }

        public void Run()
        {
            Setup();

            bool isXTurn = true;

            for (int i = 0; i < 9; i++)
            {
                bool hasValidInput = false;

                while (!hasValidInput)
                {
                    DisplayBoard();

                    int inputX, inputY = -1;
                    Console.WriteLine("Enter the coordinates to play: \"x y\"");
                    string input = Console.ReadLine() ?? string.Empty;
                    string[] inputArray = input.Split(" ");
                    inputX = int.Parse(inputArray[0]);
                    inputY = int.Parse(inputArray[1]);

                    if (string.IsNullOrWhiteSpace(board[inputX, inputY]))
                    {
                        hasValidInput = true;
                        board[inputX, inputY] = GetCurrentTurnCharacter(isXTurn);
                    }
                }

                if (IsVictoryState())
                {
                    DisplayBoard();
                    string victor = GetCurrentTurnCharacter(isXTurn);
                    Console.WriteLine($"Victory by {victor}!");
                    break;
                }

                isXTurn = !isXTurn;
            }
        }

        private void DisplayBoard()
        {
            Console.WriteLine();
            Console.WriteLine($"2 | {board[0, 2]} | {board[1, 2]} | {board[2, 2]} |");
            Console.WriteLine($"1 | {board[0, 1]} | {board[1, 1]} | {board[2, 1]} |");
            Console.WriteLine($"0 | {board[0, 0]} | {board[1, 0]} | {board[2, 0]} |");
            Console.WriteLine("  | 0 | 1 | 2 |");
        }

        private string GetCurrentTurnCharacter(bool isXTurn)
        {
            return isXTurn ? "X" : "O";
        }

        private bool IsVictoryState()
        {
            return CheckLine(1, 0, 0, 0) ||
                   CheckLine(1, 0, 0, 1) ||
                   CheckLine(1, 0, 0, 2) ||
                   CheckLine(0, 1, 0, 0) ||
                   CheckLine(0, 1, 1, 0) ||
                   CheckLine(0, 1, 2, 0) ||
                   CheckLine(1, 1, 0, 0) ||
                   CheckLine(1, -1, 0, 2);
        }

        private bool CheckLine(int xIncrement, int yIncrement, int xOffset = 0, int yOffset = 0)
        {
            int x = xOffset;
            int y = yOffset;
            string previousCharacter = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                string currentCharacter = board[x, y];
                x += xIncrement;
                y += yIncrement;

                if (i == 0)
                {
                    previousCharacter = currentCharacter;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(currentCharacter))
                    return false;

                if (currentCharacter != previousCharacter)
                    return false;

                previousCharacter = currentCharacter;
            }

            return true;
        }
    }
}