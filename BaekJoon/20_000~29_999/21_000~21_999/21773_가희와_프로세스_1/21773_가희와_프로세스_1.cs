using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 5
이름 : 배성훈
내용 : 가희와 프로세스 1
    문제번호 : 21773번

    우선순위 큐 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_1030
    {

        static void Main1030(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            PriorityQueue<int, int> pq;
            int t, n;
            (int id, int t, int p)[] proc;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                var comp = Comparer<int>.Create((x, y) =>
                {

                    int ret = proc[y].p.CompareTo(proc[x].p);
                    if (ret == 0) return proc[x].id.CompareTo(proc[y].id);
                    return ret;
                });

                pq = new(n, comp);
                for (int i = 0; i < n; i++)
                {

                    pq.Enqueue(i, i);
                }

                for (int i = 0; i < t; i++)
                {

                    int idx = pq.Dequeue();
                    sw.Write($"{proc[idx].id}\n");

                    proc[idx].p--;
                    proc[idx].t--;
                    if (proc[idx].t > 0) pq.Enqueue(idx, idx);
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                t = ReadInt();
                n = ReadInt();

                proc = new (int id, int t, int p)[n];

                for (int i = 0; i < n; i++)
                {

                    proc[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
