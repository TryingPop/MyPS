using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 4
이름 : 배성훈
내용 : 보드 점프
    문제번호 : 3372번

    dp 문제다.
    종착점 표현에 낚여 1번, 점화식을 잘못 구현해 1번 총 2번 틀렸다.
    문제에서 n - 1, n - 1 좌표의 경우의 수를 찾아야 한다.

    아이디어는 다음과 같다.
    dp[i][j] = val를 해당 i, j 에 올 수 있는 전체 경로 val를 담게 한다.
    그러면 점화식은 열이 증가 시키고 다음으로 행을 증가시킨다.
    그러면 이동가능한 값을 board[i][j] = move라하면
    dp[i + move][j] += dp[i][j], dp[i][j + move] += dp[i][j]를 얻을 수 있다.
    디버깅으로 확인하니 크기 100에 모두 마지막 부분을 제외한 1인 
*/

namespace BaekJoon.etc
{
    internal class etc_1613
    {

        static void Main1613(string[] args)
        {

            int n;
            int[][] board;
            BigInteger[][] dp;

            Input();

            GetRet();

            void GetRet()
            {

                dp = new BigInteger[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new BigInteger[n];
                }

                dp[0][0] = 1;

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        Move(r, c);
                    }
                }

                Console.Write(dp[n - 1][n - 1]);

                void Move(int _r, int _c)
                {

                    int move = board[_r][_c];
                    if (move == 0) return;
                    int nR = _r + move;
                    int nC = _c + move;
                    if (ChkValid(nR))
                        dp[nR][_c] += dp[_r][_c];
                    if (ChkValid(nC))
                        dp[_r][nC] += dp[_r][_c];

                    bool ChkValid(int _val)
                        => _val < n;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                board = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    board[i] = new int[n];
                    for (int j = 0; j < n; j++)
                    {

                        board[i][j] = ReadInt();
                    }
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;

                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }
}
