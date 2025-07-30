using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 15
이름 : 배성훈
내용 : 간단한 동전 문제 (Easy, Hard)
    문제번호 : 33938, 33943번

    BFS 문제다.
    n = 0이 들어올 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1768
    {

        static void Main1768(string[] args)
        {

            int OFFSET = 10_000;
            int MAX = OFFSET << 1;
            Queue<int> q = new(MAX + 1);
            int[] ret = new int[MAX + 1];

            int n, m;
            int[] p;

            Input();

            GetRet();

            void GetRet()
            {

                Array.Fill(ret, -1);

                ret[OFFSET] = 0;
                q.Enqueue(OFFSET);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();

                    for (int i = 0; i < n; i++)
                    {

                        int next = node + p[i];
                        if (next < 0 || MAX < next || ret[next] != -1) continue;
                        ret[next] = ret[node] + 1;
                        q.Enqueue(next);
                    }
                }

                Console.Write(ret[m + OFFSET]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                string[] temp = sr.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);
                if (n > 0) p = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                else p = new int[0];
            }
        }
    }
}
