using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 21
이름 : 배성훈
내용 : System Engineer
    문제번호 : 3736번

    이분 매칭 문제다
    호프크로프트 카프 알고리즘을 이용해 이분 매칭을 했다
    출력 문제로 2번 틀렸다;

    시간 복잡도는 정점의 개수를 V, 간선의 수를 E라하자
    그러면 O((root V) * E)가 된다

    처음에 A의 각 정점에 0의 단계를 부여하고 DFS로 최대한 매칭을 해준다
    일반적인 이분 매칭에서 B 노드의 재방문 초기화를 제외하고 최대한 매칭해준다
    
    매칭이 끝나면 다시 처음으로 돌아와서 레벨을 다시 부여해준다
    여기서 매칭되지 않은 점만 0의 단계를 부여하고
    매칭된 점은 매칭 안된 점에서 몇 단계 거쳐야 A로 오는 레벨을 부여한다

    그리고 다시 매칭을 해준다 여기서 만약 다음 단계인 경우 매칭을 시켜주고, 
    아니면 다른 점으로 이을 수 있는지 확인한다

    이렇게 갱신이 안되는 경우까지 매칭해준게 이분매칭이 된다

    해당 사이트를 보고 참고해서 코드를 작성했다
    https://m.blog.naver.com/kks227/220816033373

    알고리즘 이해는 해당 사이트를 보고 이해했다
    https://nhrwv.github.io/ps/%EA%B8%80%EC%93%B0%EA%B8%B0/2022/12/31/Hopcroft-Karp/
*/

namespace BaekJoon.etc
{
    internal class etc_0713
    {

        static void Main713(string[] args)
        {

            int MAX = 10_000;
            int INF = 1_000_000_000;
            StreamReader sr;
            StreamWriter sw;

            int[] A;
            int[] B;
            bool[] visit;

            List<int>[] line;

            Queue<int> q;
            int[] lvl, d;
            
            int n;

            Solve();

            void Solve()
            {

                Init();

                while (Input())
                {

                    int ret = 0;
                    Array.Fill(A, -1, 0, n);
                    Array.Fill(B, -1, 0, n);
                    Array.Fill(visit, false, 0, n);

                    while (true)
                    {

                        Array.Fill(d, 0, 0, n);
                        // 레벨 부여
                        BFS();

                        // 매치 시킨다
                        int match = 0;
                        for (int i = 0; i < n; i++)
                        {

                            if (!visit[i] && DFS(i)) match++;
                        }

                        // 변환 없으면 최대한 매칭 시켰으므로 종료
                        if (match == 0) break;

                        // 변환된 경우 값 누적
                        ret += match;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                line = new List<int>[MAX];
                for (int i = 0; i < MAX; i++)
                {

                    line[i] = new();
                }

                A = new int[MAX];
                B = new int[MAX];
                visit = new bool[MAX];

                lvl = new int[MAX];
                d = new int[MAX];
                q = new(MAX);
            }

            void BFS()
            {

                // A그룹의 각 정점에 단계 부여
                for (int i = 0; i < n; i++)
                {

                    // 아직 매칭 안된 A그룹의 정점
                    if (!visit[i])
                    {

                        lvl[i] = 0;
                        q.Enqueue(i);
                    }
                    // 매칭된 B그룹의 정점
                    else lvl[i] = INF;
                }

                while (q.Count > 0)
                {

                    int a = q.Dequeue();

                    // 
                    for (int i = 0; i < line[a].Count; i++)
                    {

                        int b = line[a][i];
                        // A그룹의 a와 연결되는 B그룹의 정점을 b라하고
                        // 해당 b 정점과 이미 매칭된 A그룹의 정점 B[b]라 하면
                        // B[b]는 a의 레벨 + 1
                        if (B[b] != -1 && lvl[B[b]] == INF)
                        {

                            // 여기에 올 수 있는 간선을
                            // Alternating Path (교차 경로)
                            // 라 명명하는거 같다
                            lvl[B[b]] = lvl[a] + 1;
                            q.Enqueue(B[b]);
                        }
                    }
                }
            }

            bool DFS(int _a)
            {

                for (; d[_a] < line[_a].Count; d[_a]++)
                {

                    int b = line[_a][d[_a]];

                    // 레벨 차이가 1이고 이전 점에서 다른 점으로 매칭될 때
                    // Alternating Path (교차 경로)
                    //
                    // 아직 매칭 안된경우 매칭 성공이라 본다!
                    // Augmenting Path (증가 경로)
                    if (B[b] == -1 || lvl[B[b]] == lvl[_a] + 1 && DFS(B[b]))
                    {

                        visit[_a] = true;
                        A[_a] = b;
                        B[b] = _a;
                        return true;
                    }
                }

                return false;
            }

            bool Input()
            {

                n = ReadInt();
                if (n == -1) return false;
                for (int i = 0; i < n; i++)
                {

                    line[i].Clear();
                }

                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int len = ReadInt();

                    for (int j = 0; j < len; j++)
                    {

                        int b = ReadInt() - n;
                        line[f].Add(b);
                    }
                }

                return true;
            }

            int ReadInt()
            {

                int c = sr.Read();

                if (c == -1) return -1;
                int ret;
                if (c < '0' || c > '9') ret = 0;
                else ret = c - '0';
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c < 48 || c > 57) continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
