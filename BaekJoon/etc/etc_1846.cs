using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 28
이름 : 배성훈
내용 : Hasty Santa Claus
    문제번호 : 27416번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1846
    {

        static void Main1846(string[] args)
        {


            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            (int s, int e, int idx)[] arr = new (int s, int e, int idx)[n];
            for (int i = 0; i < n; i++)
            {

                arr[i] = (ReadInt(), ReadInt(), i);
            }

            Array.Sort(arr, (x, y) => x.s.CompareTo(y.s));
            PriorityQueue<int, int> pq = new(n);
            int[] ret = new int[n];

            int idx = 0;
            for (int curDay = 1; curDay <= 31; curDay++)
            {

                while (idx < n && arr[idx].s == curDay)
                {

                    pq.Enqueue(idx, arr[idx].e);
                    idx++;
                }

                int r = k;
                while (r > 0 && pq.Count > 0)
                {

                    int go = pq.Dequeue();
                    ret[arr[go].idx] = curDay;
                    r--;
                }
            }

            for (int i = 0; i < n; i++)
            {

                sw.Write(ret[i]);
                sw.Write('\n');
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
