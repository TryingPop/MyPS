using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 26
이름 : 배성훈
내용 : 칙령
    문제번호 : 12875번

    플로이드-워셜, 최단 경로 문제다.
    bfs를 이용해 해결했다.
    찾아야할 것을 보면 떨어진 노드가 존재하면 무한히 늘어날 수 있다.
    그래서 모든 노드는 연결되어야 한다.

    그리고 최댓값은 가장 큰 거리와 같다.

    풀고나서 힌트를 보니 플로이드-워셜이 맞는거 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1583
    {

        static void Main1583(string[] args)
        {

            int n, d;
            List<int>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                int[] dis = new int[n];
                Queue<int> q = new(n);

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int chk = BFS(i);
                    if (chk == -1)
                    {

                        ret = -1;
                        break;
                    }

                    ret = Math.Max(ret, chk);
                }

                if (ret == -1) Console.Write(ret);
                else Console.Write(ret * d);

                int BFS(int _s)
                {

                    Array.Fill(dis, -1);
                    dis[_s] = 0;

                    q.Enqueue(_s);
                    while (q.Count > 0)
                    {

                        int cur = q.Dequeue();

                        for (int i = 0; i < edge[cur].Count; i++)
                        {

                            int next = edge[cur][i];
                            if (dis[next] != -1) continue;
                            q.Enqueue(next);
                            dis[next] = dis[cur] + 1;
                        }
                    }

                    int ret = 0;
                    for (int i = 0; i < n; i++)
                    {

                        if (dis[i] == -1) return -1;
                        ret = Math.Max(ret, dis[i]);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                d = int.Parse(sr.ReadLine());

                edge = new List<int>[n];
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new(n);
                    string input = sr.ReadLine();
                    for (int j = 0; j < n; j++)
                    {

                        if (input[j] == 'N') continue;
                        edge[i].Add(j);
                    }
                }
            }
        }
    }

#if other
// #include <stdio.h>
// #include <algorithm>
using namespace std;

int main()
{
    int N, D; scanf("%d %d", &N, &D);

    char c; int d[50][50];
    for(int i = 0; i < N; i++)
        for(int j = 0; j < N; j++){
            scanf(" %c", &c);
            d[i][j] = (c == 'Y' ? 1 : -1);
        }

    for(int k = 0; k < N; k++)
        for(int i = 0; i < N; i++) {
            if(d[i][k] < 0) continue;
            for (int j = 0; j < N; j++) {
                if(d[k][j] < 0) continue;
                if (d[i][j] < 0) d[i][j] = d[i][k] + d[k][j];
                else d[i][j] = min(d[i][j], d[i][k] + d[k][j]);
            }
        }

    int ans = 0;
    for(int i = 0; i < N; i++) {
        int h = 0;
        for (int j = 0; j < N; j++) {
            if(i == j) continue;
            if(d[i][j] < 0){ ans = -1; break; }
            h = max(h, d[i][j]);
        }
        if(ans < 0) break;
        ans = max(ans, h);
    }

    printf("%d\n", ans < 0 ? ans : (ans * D));
    return 0;
}

#endif
}
