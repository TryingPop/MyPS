using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 7
이름 : 배성훈
내용 : 트리
    문제번호 : 1068번

    그래프 이론, 트리 문제다.
    트리에서 해당 노드를 제거했을 때, 리프의 갯수를 찾으면 된다.
    노드가 제거되면 자식 노드들도 모두 제거된다. 또한 그래프는 트리이기에 부모로 가는 간선만 끊으면 된다.
    간선을 끊었다는 의미로 -2로 했다.

    트리에서 부모를 알기에 부모에서 자식으로 가는 간선만 이어주면, 간선의 갯수가 0인게 리프가 된다.
    그래서 DFS로 간선따라 탐색해가며 리프를 세었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1381
    {

        static void Main1381(string[] args)
        {

            int n;
            int[] parent;

            Input();

            GetRet();

            void GetRet()
            {

                List<int>[] edge = new List<int>[n];
                
                for (int i = 0; i < n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < n; i++)
                {

                    if (parent[i] < 0) continue;
                    edge[parent[i]].Add(i);
                }

                int[] cnt = new int[n];

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (parent[i] == -1) ret += DFS(i);
                }

                Console.Write(ret);

                int DFS(int _cur)
                {

                    ref int ret = ref cnt[_cur];
                    if (edge[_cur].Count == 0) ret = 1;
                    if (ret != 0) return ret;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        ret += DFS(next);
                    }

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());
                parent = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                int pop = int.Parse(sr.ReadLine());

                parent[pop] = -2;
            }
        }
    }
}
