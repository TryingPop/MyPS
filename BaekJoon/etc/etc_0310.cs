using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 두 로봇
    문제번호 : 15971번

    다익스트라 문제다
    문제 설명을 보면 동굴이 트리의 형태임을 알 수 있다
    
    두 노드가 주어지는데 이어진 통로로 이어진 노드에서 통신이 가능하다고 한다
    이 말은 가장 긴 통로의 길이를 제외하고 두 노드간의 거리를 찾는 문제로 바뀐다

    거리가 양수이므로 다익스트라를 써서 최단 거리를 찾고
    최단 거리에서 가장 긴 거리를 빼서 제출하니 이상없이 통과했다
    노드가 10만개이고, 거리 최대값이 1000이므로 최대 1억의 거리를 갖으므로 int로 거리 변수를 설정했다
    그래서 제출하니 이상없이 136ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0310
    {

        static void Main310(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int s = ReadInt(sr);
            int e = ReadInt(sr);

            List<(int dst, int dis)>[] lines = new List<(int dst, int dis)>[n + 1];

            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 1; i < n; i++)
            {

                int f = ReadInt(sr);
                int b = ReadInt(sr);
                int d = ReadInt(sr);

                lines[f].Add((b, d));
                lines[b].Add((f, d));
            }

            sr.Close();

            PriorityQueue<(int dst, int dis, int mDis), int> q = new(n);
            q.Enqueue((s, 0, 0), 0);

            bool[] visit = new bool[n + 1];
            int ret = 0;
            int mDis = 0;

            // 다익스트라
            while(q.Count > 0)
            {

                var node = q.Dequeue();
                if (visit[node.dst]) continue;
                else if (node.dst == e)
                {

                    ret = node.dis;
                    mDis = node.mDis;
                    break;
                }
                visit[node.dst] = true;

                for (int i = 0; i < lines[node.dst].Count; i++)
                {

                    int next = lines[node.dst][i].dst;
                    if (visit[next]) continue;

                    // 최대거리 확인
                    int maxDis = node.mDis < lines[node.dst][i].dis ? lines[node.dst][i].dis : node.mDis;
                    int nextDis = lines[node.dst][i].dis + node.dis;
                    
                    q.Enqueue((next, nextDis, maxDis), nextDis);
                }
            }

            Console.WriteLine(ret - mDis);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
