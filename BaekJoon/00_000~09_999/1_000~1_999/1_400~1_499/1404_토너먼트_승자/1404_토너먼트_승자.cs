using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 토너먼트 승자
    문제번호 : 1404번

    확률론 문제다.
    독립시행에 확률을 찾아 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1443
    {

        static void Main1443(string[] args)
        {

            long SUM = 100_000_000_000_000;

            long[][] winRate;
            Input();

            GetRet();

            void GetRet()
            {

                // win[i][j] = val
                // j번의 i연승할 확률
                long[][] win = new long[4][];
                for (int i = 0; i < 4; i++)
                {

                    win[i] = new long[8];
                }

                Array.Fill(win[0], 1);
                for (int i = 1; i < 4; i++)
                {

                    SetWin(i);
                }

                for (int i = 0; i < 8; i++)
                {

                    if (win[3][i] == SUM) Console.Write(1.0);
                    else Console.Write($"0.{win[3][i]:00000000000000}");
                    Console.Write(' ');
                }

                // win[2]는 1만단위, win[3]은 1억?
                void SetWin(int _bit)
                {

                    for (int i = 0; i < 8; i++)
                    {

                        int s = 1 << (_bit - 1);
                        int e = 1 << _bit;
                        for (int j = s; j < e; j++)
                        {

                            int other = i ^ j;
                            win[_bit][i] += win[_bit - 1][i] * win[_bit - 1][other] * winRate[i][other];
                        }
                    }
                }
            }

            void Input()
            {

                int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                winRate = new long[8][];
                for (int i = 0; i < 8; i++)
                {

                    winRate[i] = new long[8];
                }

                int idx = 0;
                
                for (int i = 0; i < 8; i++)
                {

                    for (int j = i + 1; j < 8; j++)
                    {

                        winRate[i][j] = arr[idx++];
                        winRate[j][i] = 100 - winRate[i][j];
                    }
                }
            }
        }
    }
}
