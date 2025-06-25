using System;
using System.IO;
using System.Collections.Generic;

/*
날짜 : 2024. 2. -
이름 : 배성훈
내용 : 방탈출
    문제번호 : 23352번

    해당 방법을 푸는 알고리즘을 몰라서 힌트를 봤고, 
    브루트포스가 있어 완전 탐색으로 풀었다

    아이디어는 다음과 같다
    각 점에서 가장 긴 길이에 큰값을 찾는다!
    그리고 길이가 길면 무조건 값을 갱신하고, 길이가 같을 때는 값 비교를 해서 큰 값으로 갱신한다
    탐색 방법은 길이 갱신을 확인하기 쉬운 BFS 탐색을 이용했다

    해당 방법으로 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0074
    {

        static void Main74(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = ReadInt(sr);
                }
            }
            sr.Close();

            int[,] dp = new int[row, col];

            int[] dirX = { -1, 1, 0, 0 };
            int[] dirY = { 0, 0, -1, 1 };

            Queue<(int r, int c)> q = new(row * col);
            int maxLen = 0;
            int max = 0;
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    if (board[r, c] == 0) continue;

                    q.Enqueue((r, c));

                    dp[r, c] = 1;
                    int chk = 1;
                    int find = 0;

                    while(q.Count > 0)
                    {

                        var node = q.Dequeue();
                        int curLength = dp[node.r, node.c];
                        int curVal = board[node.r, node.c];
                        if (curLength > chk)
                        {

                            // 길이 갱신 -> 큰값도 갱신!
                            chk = curLength;
                            find = curVal;
                        }
                        else if (find < curVal) find = curVal;

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirY[i];
                            int nextC = node.c + dirX[i];
                            if (ChkInvalidPos(nextR, nextC, row, col)) continue;

                            if (board[nextR, nextC] == 0 || dp[nextR, nextC] != 0) continue;

                            dp[nextR, nextC] = curLength + 1;
                            q.Enqueue((nextR, nextC));
                        }
                    }

                    // 1칸 이상 탐색했다!
                    if (chk > 1) find += board[r, c];

                    if (maxLen < chk) 
                    { 
                        
                        // 최대 길이가 늘어나는경우다
                        maxLen = chk;
                        max = find;
                    }
                    // 
                    else if (chk == maxLen && max < find) max = find;

                    for (int i = 0; i < row; i++)
                    {

                        for (int j = 0; j < col; j++)
                        {

                            dp[i, j] = 0;
                        }
                    }
                }
            }

            Console.WriteLine(max);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _r >= _row) return true;
            if (_c < 0 || _c >= _col) return true;

            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
