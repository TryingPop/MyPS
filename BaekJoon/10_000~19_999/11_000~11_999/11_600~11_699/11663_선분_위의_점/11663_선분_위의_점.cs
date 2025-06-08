using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 18
이름 : 배성훈
내용 : 선분 위의 점
    문제번호 : 11663번

    정렬, 이분탐색 문제다.
    이분탐색으로 시작지점과 끝지점을 찾는다.
*/

namespace BaekJoon.etc
{
    internal class etc_1345
    {

        static void Main1345(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr);

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    int l = BS1(Math.Min(f, t));
                    int r = BS2(Math.Max(f, t));

                    sw.Write($"{r - l}\n");
                }

                int BS2(int _num)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (arr[mid] <= _num) l = mid + 1;
                        else r = mid - 1;
                    }

                    return l;
                }

                int BS1(int _num)
                {

                    int l = 0;
                    int r = n - 1;

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;
                        if (arr[mid] < _num) l = mid + 1;
                        else r = mid - 1;
                    }

                    return r + 1;
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
