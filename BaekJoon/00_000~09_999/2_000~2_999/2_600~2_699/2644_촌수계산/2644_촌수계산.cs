using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 31
이름 : 배성훈
내용 : 촌수계산
    문제번호 : 2644번

    BFS, DFS 문제다
    촌수 찾아가는 것을 BFS로 찾아갔다
    부모가 유일하므로 A -> C로가는 경로는 유일하다
    그래서 우선순위 큐를 이용한 다익스트라는 무의미한 우선순위 선정이 된다
    반면 플로이드 워셜은 써도된다
*/

namespace BaekJoon.etc
{
    internal class etc_0403
    {

        static void Main403(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();

            int from = ReadInt();
            int to = ReadInt();

            int len = ReadInt();

            List<int>[] line = new List<int>[n + 1];
            int[] ret = new int[n + 1];
            for (int i = 0; i < n; i++)
            {

                line[i + 1] = new();
                ret[i + 1] = -1;
            }

            for (int i = 0; i < len; i++)
            {

                int p = ReadInt();
                int c = ReadInt();

                line[p].Add(c);
                line[c].Add(p);
            }

            Queue<int> q = new Queue<int>(n);
            q.Enqueue(from);
            ret[from] = 0;
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < line[node].Count; i++)
                {

                    int next = line[node][i];
                    if (ret[next] != -1) continue;
                    ret[next] = ret[node] + 1;

                    q.Enqueue(next);
                }
            }

            Console.WriteLine(ret[to]);

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
