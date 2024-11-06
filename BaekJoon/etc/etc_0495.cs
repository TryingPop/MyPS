using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 10
이름 : 배성훈
내용 : 내려가기
    문제번호 : 2096번

    dp, 슬라이딩 윈도우 문제다
    슬라이딩 윈도우로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_0495
    {

        static void Main495(string[] args)
        {

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 4);
            int n = ReadInt();

            int[] bMax = new int[3];
            int[] cMax = new int[3];

            int[] bMin = new int[3];
            int[] cMin = new int[3];
            for (int i = 0; i < n; i++)
            {

                int[] temp = bMin;
                bMin = cMin;
                cMin = temp;

                temp = bMax;
                bMax = cMax;
                cMax = temp;

                int a = ReadInt();
                int b = ReadInt();
                int c = ReadInt();

                cMax[0] = Math.Max(bMax[0], bMax[1]) + a;
                cMax[1] = Math.Max(Math.Max(bMax[0], bMax[1]), bMax[2]) + b;
                cMax[2] = Math.Max(bMax[1], bMax[2]) + c;

                cMin[0] = Math.Min(bMin[0], bMin[1]) + a;
                cMin[1] = Math.Min(Math.Min(bMin[0], bMin[1]), bMin[2]) + b;
                cMin[2] = Math.Min(bMin[1], bMin[2]) + c;
            }

            sr.Close();

            int ret1 = cMax[0];
            int ret2 = cMin[0];
            for (int i = 1; i < 3; i++)
            {

                ret1 = ret1 < cMax[i] ? cMax[i] : ret1;
                ret2 = ret2 < cMin[i] ? ret2 : cMin[i];
            }

            Console.Write($"{ret1} {ret2}");

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.IO;

var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

int c, n = 0;
while ((c = sr.Read()) != '\n')
{
    if (c == '\r')
    {
        sr.Read();
        break;
    }
    n = 10 * n + c - '0';
}
var preMaxDp = new int[3];
var maxDp = new int[3];
var preMinDp = new int[3];
var minDp = new int[3];
var cur = new byte[3];
for (int i = 0; i < n; i++)
{
    for (int j = 0; j < 3; j++)
    {
        cur[j] = (byte)(sr.Read() - '0');
        if (sr.Read() == '\r')
            sr.Read();
    }

    maxDp[0] = Max(preMaxDp[0], preMaxDp[1]) + cur[0];
    maxDp[1] = Max3(preMaxDp[0], preMaxDp[1], preMaxDp[2]) + cur[1];
    maxDp[2] = Max(preMaxDp[1], preMaxDp[2]) + cur[2];

    minDp[0] = Min(preMinDp[0], preMinDp[1]) + cur[0];
    minDp[1] = Min3(preMinDp[0], preMinDp[1], preMinDp[2]) + cur[1];
    minDp[2] = Min(preMinDp[1], preMinDp[2]) + cur[2];

    (preMaxDp, maxDp) = (maxDp, preMaxDp);
    (preMinDp, minDp) = (minDp, preMinDp);
}

var max = Max3(preMaxDp[0], preMaxDp[1], preMaxDp[2]);
var min = Min3(preMinDp[0], preMinDp[1], preMinDp[2]);
Console.Write($"{max} {min}");

int Max(int a, int b) => a > b ? a : b;
int Max3(int a, int b, int c) => a > b ? Max(a, c) : Max(b, c);
int Min(int a, int b) => a < b ? a : b;
int Min3(int a, int b, int c) => a < b ? Min(a, c) : Min(b, c);
#endif
}
