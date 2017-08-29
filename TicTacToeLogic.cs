﻿using System;

namespace C17_Ex02
{
    class TicTacToeLogic
    {
        public static void computerMove(ref char[,] io_table, PlayerDetails o_compDetails)
        {
            Random randNum = new Random();
            int rowInput;
            int colInput;

            do
            {
                rowInput = randNum.Next(io_table.GetLength(0));
                colInput = randNum.Next(io_table.GetLength(0));
            } while (!TryInsert(o_compDetails.m_PlayerSymbol, ref io_table, rowInput, colInput));
        }

        //The method tries to insert to symbol to the desired place and returns if successful
        public static bool TryInsert(char symbol, ref char[,] io_table, int i_rowInput, int i_colInput)
        {
            bool isChanged = false;

            if (io_table[i_rowInput, i_colInput] == ' ')
            {
                io_table[i_rowInput, i_colInput] = symbol;
                isChanged = true;
            }
            return isChanged;
        }

        //The method checks all the possible options for a streight and returns if successful
        public static bool checkforStraight(char[,] io_table, char i_symbol, int i_numOfSteps)
        {
            int edgeSize = io_table.GetLength(0);
            bool isStraight = false;

            if (i_numOfSteps >= (edgeSize * 2 - 2))
            {
                int matchesCount;

                //Check rows
                for (int rowIndex = 0; rowIndex < edgeSize && !isStraight; rowIndex++)
                {
                    matchesCount = 0;
                    for (int colIndex = 0; colIndex < edgeSize; colIndex++)
                    {
                        if (io_table[rowIndex, colIndex] == i_symbol)
                        {
                            matchesCount++;
                        }
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //check columns
                for (int colIndex = 0; colIndex < edgeSize && !isStraight; colIndex++)
                {
                    matchesCount = 0;
                    for (int rowIndex = 0; rowIndex < edgeSize; rowIndex++)
                    {
                        if (io_table[rowIndex, colIndex] == i_symbol)
                        {
                            matchesCount++;
                        }
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //Check first diagonal
                matchesCount = 0;
                for (int rowAndColIndex = 0; rowAndColIndex < edgeSize && !isStraight; rowAndColIndex++)
                {
                    if (io_table[rowAndColIndex, rowAndColIndex] == i_symbol)
                    {
                        matchesCount++;
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //Check second diagonal
                matchesCount = 0;
                for (int rowIndex = edgeSize - 1; rowIndex >= 0 && !isStraight; rowIndex--)
                {
                    int colIndex = (edgeSize - 1) - rowIndex;
                    if (io_table[rowIndex, colIndex] == i_symbol)
                    {
                        matchesCount++;
                    }
                    isStraight = edgeSize == matchesCount;
                }

            }
            return isStraight;
        }

        //The method declairs the winner
        public static string findWinner(ref PlayerDetails io_player1, ref PlayerDetails io_player2, char i_symbol)
        {
            if (io_player1.m_PlayerSymbol == i_symbol)
            {
                io_player2.m_Points++;
                return io_player2.m_Name;
            }
            io_player1.m_Points++;
            return io_player1.m_Name;            
        }
    }
}