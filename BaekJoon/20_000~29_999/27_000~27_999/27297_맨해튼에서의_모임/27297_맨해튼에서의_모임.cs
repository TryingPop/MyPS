using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 22
이름 : 배성훈
내용 : 맨해튼에서의 모임
    문제번호 : 27297번

    수학, 정렬 문제다.
    ∑|X - A_i|의 최솟값이 되는 X는 A_i의 중앙값이 됨이 잘 알려져 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1566
    {

        static void Main1566(string[] args)
        {

            int n, m;
            long[][] pos;

            Input();

            GetRet();

            void GetRet()
            {

                long dis = 0;
                long[] ret = new long[n];

                for (int i = 0; i < n; i++)
                {

                    Array.Sort(pos[i]);

                    ret[i] = pos[i][m >> 1];
                    for (int j = 0; j < m; j++)
                    {

                        dis += Math.Abs(ret[i] - pos[i][j]);
                    }
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                sw.Write($"{dis}\n");
                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{ret[i]} ");
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                pos = new long[n][];

                for (int i = 0; i < n; i++)
                {

                    pos[i] = new long[m];
                }

                for (int i = 0; i < m; i++)
                {

                    for (int j = 0; j < n; j++)
                    {

                        pos[j][i] = ReadLong();
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

                        while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        bool positive = c != '-';

                        ret = positive ? c - '0' : 0;
                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        ret = positive ? ret : -ret;

                        return false;
                    }
                }
            }
        }
    }
}
