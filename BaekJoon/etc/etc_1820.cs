using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 13
이름 : 배성훈
내용 : 회색 영역
    문제번호 : 3885번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1820
    {

        static void Main1820(string[] args)
        {

            int n, w;
            int maxIdx;
            int[] hist = new int[101];
            double[] area = new double[101];

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                int cnt = 0;
                for (int i = 0; i <= 100; i++)
                {

                    if (hist[i] != 0) cnt++;
                    area[i] = hist[i] / (double)hist[maxIdx];
                }

                int mo = cnt - 1;
                int ja = cnt - 1;

                double ret = 0.01;
                for (int i = 0; i <= 100; i++)
                {

                    if (area[i] == 0) continue;
                    ret += (area[i] * ja--) / mo;
                }

                sw.Write($"{ret}\n");
            }

            bool Input()
            {

                n = ReadInt();
                w = ReadInt();
                maxIdx = 0;

                Array.Fill(hist, 0);
                Array.Fill(area, 0);
                for (int i = 0; i < n; i++)
                {

                    int idx = ReadInt() / w;
                    hist[idx]++;

                    if (hist[maxIdx] < hist[idx]) maxIdx = idx;
                }

                return n != 0;
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
