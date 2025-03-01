using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 1
이름 : 배성훈
내용 : 서울의 지하철
    문제번호 : 16166번

    구현, BFS, 해시 문제다.
    먼저 지하철 번호가 0 ~ 2^32 - 1이므로 낮은 숫자로 변형했다.
    그리고 같은 정거장이 있는 경우 지하철간 이동할 수 있다는 의미의 간선을 1로 했다.

    이후에는 시작 지점에서 도착지점으로 가는 최단 거리나, 
    도착지점에서 시작지점으로 가는 최단 거리는 같으므로
    도착지점에서 시작지점으로 가는 최단 거리를 BFS로 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1365
    {

        static void Main1365(string[] args)
        {

            // 16166번
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();

            int len = n * 10;
            Dictionary<int, int> nTi = new(len);
            HashSet<int>[] train = new HashSet<int>[n];
            int[][] edge;
            Queue<int> q;
            int newIdx = 0;
            SetIdx(0);
            SetTrain();
            SetEdge();
            BFS();

            void BFS()
            {

                q = new(n);
                int[] dis = new int[n];
                Array.Fill(dis, n + 1);

                int dst = GetIdx(ReadInt());
                
                for (int i = 0; i < n; i++)
                {

                    if (!train[i].Contains(dst)) continue;
                    q.Enqueue(i);
                    dis[i] = 0;
                }

                while (q.Count > 0)
                {

                    int cur = q.Dequeue();

                    for (int j = 0; j < n; j++)
                    {

                        if (edge[cur][j] == 0) continue;
                        if (dis[j] != n + 1) continue;
                        q.Enqueue(j);
                        dis[j] = dis[cur] + 1;
                    }
                }

                int ret = n + 1;

                for (int i = 0; i < n; i++)
                {

                    if (!train[i].Contains(0)) continue;
                    ret = Math.Min(ret, dis[i]);
                }

                if (ret == n + 1) ret = -1;
                Console.Write(ret);
            }

            void SetEdge()
            {

                for (int i = 0; i < n; i++)
                {

                    foreach (int num in train[i])
                    {

                        for (int j = 0; j < n; j++)
                        {

                            if (i == j) continue;
                            if (train[j].Contains(num)) edge[i][j] = 1;
                        }
                    }
                }
            }

            void SetTrain()
            {

                edge = new int[n][];

                for (int i = 0; i < n; i++)
                {

                    int l = ReadInt();
                    train[i] = new(l);
                    for (int j = 0; j < l; j++)
                    {

                        int add = GetIdx(ReadInt());

                        train[i].Add(add);
                    }

                    edge[i] = new int[n];
                }
            }

            void SetIdx(int _num)
            {

                nTi[_num] = newIdx++;
            }

            int GetIdx(int _num)
            {

                if (!nTi.ContainsKey(_num)) SetIdx(_num);
                return nTi[_num];
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
                    if (c == '\n' || c == ' ') return true;

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

#if other
// #include <stdio.h>
struct x{ 
    unsigned int station;
    int line;
} ;
int main(void){
    x a[105];
    int an = 0;
    int n;
    scanf("%d", &n);
    for (int i = 1; i <= n; i++){
        int k;
        scanf("%d", &k);
        while (k--){
            unsigned int sn;
            scanf("%u", &sn);
            a[an++] = {sn,i};
        }
    }
    unsigned int dest;
    scanf("%u", &dest);

    int conn[15][15] = {0};
    for (int i = 0; i < an; i++){
        auto [s0,l0] = a[i];
        if (s0 == 0){
            conn[l0][11] = conn[11][l0] = 1;
        }
        if (s0 == dest){
            conn[l0][12] = conn[12][l0] = 1;
        }
        for (int j = 0; j < an; j++){
            auto [s1,l1] = a[(i+j)%an];
            if (s0 == s1){
                conn[l0][l1] = conn[l1][l0] = 1;
            }
        }
    }
    for (int k = 1; k <= 12; k++){
        for (int i = 1; i <= 12; i++){
            for (int j = 1; j <= 12; j++){
                if (!conn[i][k] || !conn[k][j])
                    continue;
                int v = conn[i][k] + conn[k][j];
                if (!conn[i][j])
                    conn[i][j] = v;
                else if (v < conn[i][j])
                    conn[i][j] = v;
            }
        }
    }
    if (conn[11][12] == 0)
        printf("-1\n");
    else
        printf("%d\n",conn[11][12] - 2);
}

#endif
}
