using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 23
이름 : 배성훈
내용 : 미친 로봇
    문제번호 : 1405번

    브루트포스, 백트래킹, 확률론 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1455
    {

        static void Main1455(string[] args)
        {

            int n;
            double[] p;

            Input();

            GetRet();

            void GetRet()
            {


                bool[][] board = new bool[31][];
                for (int i = 0; i < board.Length; i++) 
                {

                    board[i] = new bool[31];
                }

                double ret = 0;
                int[] dirR = { -1, 1, 0, 0 }, dirC = { 0, 0, -1, 1 };
                DFS();

                Console.Write(ret);

                void DFS(int _dep = 0, int _curR = 15, int _curC = 15, double _val = 1)
                {

                    if (_dep > n)
                    {

                        ret += _val;
                        return;
                    }

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = _curR + dirR[i];
                        int nC = _curC + dirC[i];

                        if (board[nR][nC]) continue;
                        board[nR][nC] = true;
                        DFS(_dep + 1, nR, nC, _val * p[i]);
                        board[nR][nC] = false;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                p = new double[4];

                for (int i = 1; i <= 4; i++)
                {

                    p[i - 1] = double.Parse(temp[i]) / 100.0;
                }
            }
        }
    }
}
