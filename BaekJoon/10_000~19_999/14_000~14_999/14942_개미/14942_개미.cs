using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 16
이름 : 배성훈
내용 : 개미
    문제번호 : 14942번

    희소 배열 문제다.
    먼저 희소 배열을 알아봐야 한다.
    
    검색하면 다음과 같은 두 가지 성질이 나온다.
        희소 배열은 배열 원소의 개수가 무조건 배열 길이보다 작은 배열이다.
        희소 배열은 배열의 원소 위치가 연속적이지 않은 배열을 말한다.
    
    chat gpt 에게 물어보니,
    데이터 대부분이 0 또는 Null 로 채워진 배열인 경우
    해당 데이터를 효율적으로 필요한 것만 가져올 때 희소 배열을 사용한다.
    그래서 필요한 것으로만 값을 채운거라 본다.

    위키 백과를 검색하니 성긴 행렬(Sparse matrix, 희소 행렬)도 대부분이 0으로 정의된 행렬이라 한다.
    둘의 희소(Sparse) 알파벳이 같으니 chat gpt 설명에 신뢰할만하다고 본다.
    (평소 좋은 코드 좀 내줬으면 믿겠는데, 오답 코드만 내놓아 gpt 신뢰가 낮다;)

    그래서 희소 배열은 2^i 거리만 기록하는 것을 의미한다.
    이는 트리에서 공통 부모를 찾을 때 쓰는 배열과 비슷하다고 생각한다.

    2^i 거리에 있는 노드들과 거리를 저장하고, 이동을 진행한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1195
    {

        static void Main1195(string[] args)
        {

            bool[] visit;
            int[][] arr, dis;
            List<(int dst, int dis)>[] edge;
            int[] energy;

            int n, m;

            Solve();
            void Solve()
            {

                Input();

                SetArr();

                GetRet();
            }

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 0; i < n; i++)
                {

                    sw.Write($"{DFS(i) + 1}\n");
                }

                int DFS(int _cur)
                {

                    int ret = _cur;
                    // 현재 노드의 에너지
                    int e = energy[_cur];

                    for (int i = m - 1; i >= 0; i--)
                    {

                        // 에너지가 남으면 이동 시도
                        if (arr[i][ret] != -1 && dis[i][ret] <= e)
                        {

                            e -= dis[i][ret];
                            ret = arr[i][ret];
                        }

                        if (ret == 0 || e == 0) break;
                    }

                    return ret;
                }
            }

            void SetArr()
            {

                m = 1 + (int)Math.Log2(n);
                visit = new bool[n];

                arr = new int[m][];
                dis = new int[m][];
                arr[0] = new int[n];
                Array.Fill(arr[0], -1);
                dis[0] = new int[n];
                arr[0][0] = -1;

                DFS();

                // 거리 2^i들 이어준다.
                for (int i = 1; i < m; i++)
                {

                    arr[i] = new int[n];
                    Array.Fill(arr[i], -1);
                    dis[i] = new int[n];
                    for (int j = 1; j < n; j++)
                    {

                        int next = arr[i - 1][j];
                        if (next == -1 || arr[i - 1][next] == -1) continue;
                        arr[i][j] = arr[i - 1][next];
                        dis[i][j] = dis[i - 1][next] + dis[i - 1][j];
                    }
                }

                // 거리 1 찾기
                void DFS(int _cur = 0)
                {

                    if (visit[_cur]) return;
                    visit[_cur] = true;

                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i].dst;
                        if (visit[next]) continue;

                        arr[0][next] = _cur;
                        dis[0][next] = edge[_cur][i].dis;
                        DFS(next);
                    }
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                energy = new int[n];
                edge = new List<(int dst, int dis)>[n];
                for (int i = 0; i < n; i++)
                {

                    energy[i] = ReadInt();
                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;
                    int dis = ReadInt();

                    edge[f].Add((t, dis));
                    edge[t].Add((f, dis));
                }

                sr.Close();
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
}
