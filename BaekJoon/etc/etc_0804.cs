using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : Sort 마스터 배지훈의 후계자
    문제번호 : 20551번

    정렬, 이분탐색 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0804
    {

        static void Main804(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] arr;
            int n, m;

            Solve();
            void Solve()
            {

                Input();

                for (int i = 0; i < m; i++)
                {

                    int find = ReadInt();
                    sw.Write($"{BinarySearch(find)}\n");
                }

                sr.Close();
                sw.Close();
            }

            int BinarySearch(int _find)
            {

                int ret = -1;

                int l = 0;
                int r = n - 1;
                while (l <= r)
                {

                    int mid = (l + r) >> 1;

                    if (_find <= arr[mid]) r = mid - 1;
                    else l = mid + 1;
                }

                if (r < n - 1 && arr[r + 1] == _find) ret = r + 1;

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                Array.Sort(arr);
            }

            int ReadInt()
            {

                int c = sr.Read();

                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
