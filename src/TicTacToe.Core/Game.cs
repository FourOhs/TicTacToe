using System;
using System.Collections.Generic;

namespace TicTacToe;

public class Game
{
    private readonly char[] _board = new char[9];

    public const char Human = 'X';
    public const char Computer = 'O';
    public const char Empty = ' ';

    public Game()
    {
        Reset();
    }

    public void Reset()
    {
        for (int i = 0; i < _board.Length; i++)
        {
            _board[i] = Empty;
        }
    }

    public bool MakeMove(int position, char player)
    {
        if (position < 0 || position >= _board.Length || _board[position] != Empty)
            return false;

        if (player != Human && player != Computer)
            return false;

        _board[position] = player;
        return true;
    }

    public bool IsWin(char player)
    {
        int[,] lines =
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < lines.GetLength(0); i++)
        {
            if (_board[lines[i,0]] == player &&
                _board[lines[i,1]] == player &&
                _board[lines[i,2]] == player)
                return true;
        }

        return false;
    }

    public bool IsDraw() => !IsWin(Human) && !IsWin(Computer) && GetAvailableMoves().Count == 0;

    public bool IsGameOver() => IsWin(Human) || IsWin(Computer) || IsDraw();

    public char GetWinner()
    {
        if (IsWin(Human))
            return Human;

        if (IsWin(Computer))
            return Computer;

        return Empty;
    }

    public List<int> GetAvailableMoves()
    {
        var moves = new List<int>();
        for (int i = 0; i < _board.Length; i++)
        {
            if (_board[i] == Empty)
                moves.Add(i);
        }

        return moves;
    }

    public int ChooseComputerMove()
    {
        var moves = GetAvailableMoves();
        if (moves.Count == 0)
            return -1;

        var bestMove = -1;
        var bestScore = int.MinValue;

        foreach (var move in moves)
        {
            var nextBoard = (char[])_board.Clone();
            nextBoard[move] = Computer;
            var score = Minimax(nextBoard, 0, false);

            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private static bool IsWin(char[] board, char player)
    {
        int[,] lines =
        {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < lines.GetLength(0); i++)
        {
            if (board[lines[i, 0]] == player &&
                board[lines[i, 1]] == player &&
                board[lines[i, 2]] == player)
                return true;
        }

        return false;
    }

    private static List<int> GetAvailableMoves(char[] board)
    {
        var moves = new List<int>();
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == Empty)
                moves.Add(i);
        }

        return moves;
    }

    private static int Minimax(char[] board, int depth, bool maximizingPlayer)
    {
        if (IsWin(board, Computer))
            return 10 - depth;

        if (IsWin(board, Human))
            return depth - 10;

        var moves = GetAvailableMoves(board);
        if (moves.Count == 0)
            return 0;

        if (maximizingPlayer)
        {
            var bestScore = int.MinValue;
            foreach (var move in moves)
            {
                board[move] = Computer;
                bestScore = Math.Max(bestScore, Minimax(board, depth + 1, false));
                board[move] = Empty;
            }

            return bestScore;
        }

        var worstScore = int.MaxValue;
        foreach (var move in moves)
        {
            board[move] = Human;
            worstScore = Math.Min(worstScore, Minimax(board, depth + 1, true));
            board[move] = Empty;
        }

        return worstScore;
    }

    public string RenderBoard()
    {
        return $"  {_board[0]} | {_board[1]} | {_board[2]}\n" +
               " ---+---+---\n" +
               $"  {_board[3]} | {_board[4]} | {_board[5]}\n" +
               " ---+---+---\n" +
               $"  {_board[6]} | {_board[7]} | {_board[8]}\n";
    }
}
