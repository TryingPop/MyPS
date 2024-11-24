using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 24
이름 : 배성훈
내용 : Battle for Silver
    문제번호 : 9644번

    평면 그래프, 그래프 이론 문제다.
    문제에서 요구하는 Chain은 완전 그래프이다.
    그런데 만들어지는 그래프는 평면그래프이므로
    완전그래프의 최대 크기는 4가 된다.

    삼각형과 삼각형 내부에 점을 하나찍은 총 4개의 점에 대해,
    모두 이어주면 완전그래프가 된다.
    5개는 23039번의 앞의 토끼굴? 문제처럼 겹치지 않는 경우는 존재할 수 없다.
    그래서 1, 2, 3, 4개 모두를 조사했다.

    점의 개수를 n, 직선의 개수를 m이라 하면
    3개는 점과 직선을 잡아 비교하면 되니 n x m 의 시간이 걸린다.
    4개는 직선 2개를 잡아 비교하면 되므로 m x m의 시간이 걸린다.

    여기서 n, m은 1000을 넘지 않으므로 n x m, m^2 으로 접근해도 넉넉하다.
    그리고 직선 존재 여부는 n의 크기가 500 미만이므로 배열로 관리했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1130
    {

        static void Main1130(string[] args)
        {

            int N = 450;
            int M = 900;
            StreamReader sr;
            StreamWriter sw;

            int[] silver;
            bool[][] chkEdge;
            (int v1, int v2)[] edge;
            int n, m;
            int ret;
            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                for (int v3 = 0; v3 < n; v3++)
                {

                    for (int j = 0; j < m; j++)
                    {

                        (int v1, int v2) = edge[j];
                        if (v1 == v3 || v2 == v3) continue;
                            
                        if (chkEdge[v1][v3] && chkEdge[v2][v3])
                            ret = Math.Max(ret, silver[v1] + silver[v2] + silver[v3]);
                    }
                }

                for (int i = 0; i < m; i++)
                {

                    (int v1, int v2) = edge[i];
                    int f = silver[v1] + silver[v2];
                    for (int j = i + 1; j < m; j++)
                    {

                        (int v3, int v4) = edge[j];

                        if (v1 == v3 || v1 == v4 || v2 == v3 || v2 == v4) continue;
                        int t = silver[v3] + silver[v4];

                        if (chkEdge[v1][v3] && chkEdge[v1][v4] && chkEdge[v2][v3] && chkEdge[v2][v4])
                            ret = Math.Max(ret, f + t);
                    }
                }

                sw.Write($"{ret}\n");
            }

            bool Input()
            {

                n = ReadInt();
                m = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    int s = ReadInt();
                    silver[i] = s;
                    ret = Math.Max(ret, s);

                    Array.Fill(chkEdge[i], false, 0, n);
                }

                ret = 0;
                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;

                    edge[i] = (f, t);
                    chkEdge[f][t] = true;
                    chkEdge[t][f] = true;

                    ret = Math.Max(ret, silver[f] + silver[t]);
                }

                return n != 0;
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
                    else if (c == -1) return false;

                    ret = c - '0';
                    while((c = sr.Read()) != ' ' && c != '\n' && c != -1)
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                edge = new (int v1, int v2)[M];
                chkEdge = new bool[N][];
                silver = new int[N];
                ret = 0;

                for (int i = 0; i < N; i++)
                {

                    chkEdge[i] = new bool[N];
                }
            }
        }
    }

#if other
// #include <stdio.h>

int ed[512][512], sil[512];

int main(void) {
    int v, e, x, y, res;

    while (scanf("%d %d", &v, &e) != EOF) {
        for (int i = 1; i <= v; i++) {
            scanf("%d", &sil[i]);
        }
        for (int i = 0; i < e; i++) {
            scanf("%d %d", &x, &y);
            ed[x][y] = 1;
            ed[y][x] = 1;
        }

        res = 0;
        for (int i = 1; i <= v; i++) {
            if (sil[i] > res) res = sil[i];
            for (int j = i + 1; j <= v; j++) {
                if (!ed[i][j]) continue;
                if (sil[i] + sil[j] > res) res = sil[i] + sil[j];
                for (int k = j + 1; k <= v; k++) {
                    if (!ed[i][k] || !ed[j][k]) continue;
                    if (sil[i] + sil[j] + sil[k] > res) res = sil[i] + sil[j] + sil[k];
                    for (int l = k + 1; l <= v; l++) {
                        if (!ed[i][l] || !ed[j][l] || !ed[k][l]) continue;
                        if (sil[i] + sil[j] + sil[k] + sil[l] > res) res = sil[i] + sil[j] + sil[k] + sil[l];
                    }
                }
            }
        }
        printf("%d\n", res);

        for (int i = 1; i <= v; i++) {
            for (int j = 1; j <= v; j++) ed[i][j] = 0;
        }
    }
    return 0;
}
#endif
}
