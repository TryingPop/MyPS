using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 16
이름 : 배성훈
내용 : 용액
    문제번호 : 2467번

    이분 탐색, 두 포인터 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0701
    {

        static void Main701(string[] args)
        {

            StreamReader sr;
            int[] dp;
            int[] arr;
            int n;

            Solve();

            void Solve()
            {

                Input();
                Array.Sort(arr);

                int idx1 = 0;
                int idx2 = n - 1;
                int diff = 2_000_000_001;

                int ret1 = 0;
                int ret2 = n - 1;
                while(idx1 < idx2)
                {

                    int sum = arr[idx1] + arr[idx2];
                    int abs = Math.Abs(sum);
                    if (abs < diff)
                    {

                        diff = abs;
                        ret1 = idx1;
                        ret2 = idx2;
                    }

                    if (sum < 0) idx1++;
                    else idx2--;
                }

                Console.WriteLine($"{arr[ret1]} {arr[ret2]}");
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                if (c == -1) return 0;
                bool plus = c != '-';

                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : - ret;
            }
        }
    }

#if other
using System;
using System.IO;

using var sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

var n = ScanInt();
var liquids = new int[n];
for (int i = 0; i < n; i++)
    liquids[i] = ScanInt();
int l = 0, r = n - 1;


if (liquids[l] >= 0 && liquids[r] >= 0)
{
    Console.Write($"{liquids[l]} {liquids[l + 1]}");
    return;
}
if (liquids[l] <= 0 && liquids[r] <= 0)
{
    Console.Write($"{liquids[r - 1]} {liquids[r]}");
    return;
}

int retL = l, retR = r, minTerm = int.MaxValue;
while (l < r)
{
    var sum = liquids[l] + liquids[r];
    var term = Math.Abs(sum);
    if (minTerm > term)
    {
        retL = liquids[l];
        retR = liquids[r];
        if (term == 0)
            break;
        minTerm = term;
    }
    if (sum > 0)
        r--;
    else
        l++;
}
Console.Write($"{retL} {retR}");

int ScanInt()
{
    int c = sr.Read(), n = 0;
    if (c == '-')
    {
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n - (c - '0');
        }
    }
    else
    {
        n = c - '0';
        while (!((c = sr.Read()) is ' ' or '\n'))
        {
            if (c == '\r')
            {
                sr.Read();
                break;
            }
            n = 10 * n + c - '0';
        }
    }
    return n;
}
#endif
}
