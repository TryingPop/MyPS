using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 1
이름 : 배성훈
내용 : Calvinball championship, again 2
    문제번호 : 10962번

    브루트포스 알고리즘, 백트래킹 문제다.
    최소 팀의 개수를 찾아야 한다.
    처음에 문제를 최소 부분을 놓쳐 1번 틀렸다.

    이후에 비효율적으로 구현해 여러 번 틀렸다.
    방법은 다음과 같다.
    먼저 남은 인원 중 아직 안고른 사람을 고른다. O(N)
    그리고 임의의 팀에 넣으려고 시도한다.
    넣을 수 있다면 넣고 DFS 탐색을 한다.
    이렇게 진행하니 시간초과가 나왔다.

    반면 남은 사람이 아닌 1, 2, ..., n의 선형으로 해주면 되었다.
    해당 방법으로 수정하니 이상없이 통과한다.
    다만 구현을 잘못해 ... 엄청나게 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1854
    {

#if COST
        static void Main(string[] args)
        {

            int n, m, k;
            HashSet<int>[] edge;
            HashSet<int>[] group;
            HashSet<int>[] team;

            Input();

            GetRet();

            Output();

            void GetRet()
            {

                k = n + 1;

                DFS(1, 0);
                void DFS(int _dep, int _team)
                {

                    if (_dep > n)
                    {

                        if (_team < k)
                        {

                            k = _team;
                            for (int i = 1; i <= k; i++)
                            {

                                team[i].Clear();
                                foreach (int item in group[i])
                                {

                                    team[i].Add(item);
                                }
                            }
                        }
                        return;
                    }
                    else if (k <= _team) return;

                    for (int i = 1; i <= _team; i++)
                    {

                        if (Chk(i)) continue;
                        group[i].Add(_dep);

                        DFS(_dep + 1, _team);
                        group[i].Remove(_dep);
                    }

                    group[_team + 1].Add(_dep);

                    DFS(_dep + 1, _team + 1);
                    group[_team + 1].Remove(_dep);

                    bool Chk(int _t)
                    {

                        foreach (int item in edge[_dep])
                        {

                            if (group[_t].Contains(item)) return true;
                        }

                        return false;
                    }
                }
            }

            void Output()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                sw.Write(k);
                sw.Write('\n');
                for (int i = 1; i <= k; i++)
                {

                    foreach (int item in team[i])
                    {

                        sw.Write(item);
                        sw.Write(' ');
                    }

                    sw.Write('\n');
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                team = new HashSet<int>[n + 1];
                edge = new HashSet<int>[n + 1];
                group = new HashSet<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    team[i] = new();
                    edge[i] = new();
                    group[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
#elif CHAT_GPT

        static int n, m;
        static List<int>[] adj;
        static int[] color;       // 각 플레이어의 팀 번호
        static int bestTeamCount;
        static int[] bestColor;   // 최적 해 저장용

        static void Main()
        {
            var parts = Console.ReadLine().Split();
            n = int.Parse(parts[0]);
            m = int.Parse(parts[1]);

            adj = new List<int>[n + 1];
            for (int i = 1; i <= n; i++) adj[i] = new List<int>();

            for (int i = 0; i < m; i++)
            {
                var edge = Console.ReadLine().Split();
                int a = int.Parse(edge[0]);
                int b = int.Parse(edge[1]);
                adj[a].Add(b);
                adj[b].Add(a);
            }

            color = new int[n + 1];
            bestColor = new int[n + 1];
            bestTeamCount = n; // 최악의 경우 각자 혼자 팀

            Dfs(1, 0);

            // 결과 출력
            Console.WriteLine(bestTeamCount);

            var teams = new List<int>[bestTeamCount + 1];
            for (int i = 1; i <= bestTeamCount; i++) teams[i] = new List<int>();
            for (int i = 1; i <= n; i++) teams[bestColor[i]].Add(i);

            for (int i = 1; i <= bestTeamCount; i++)
                Console.WriteLine(string.Join(" ", teams[i]));
        }

        static void Dfs(int player, int usedTeams)
        {
            if (player > n)
            {
                if (usedTeams < bestTeamCount)
                {
                    bestTeamCount = usedTeams;
                    Array.Copy(color, bestColor, n + 1);
                }
                return;
            }

            // 가지치기
            if (usedTeams >= bestTeamCount) return;

            // 1. 기존 팀 중 가능한 곳에 배치
            for (int t = 1; t <= usedTeams; t++)
            {
                if (CanAssign(player, t))
                {
                    color[player] = t;
                    Dfs(player + 1, usedTeams);
                    color[player] = 0;
                }
            }

            // 2. 새로운 팀 생성
            color[player] = usedTeams + 1;
            Dfs(player + 1, usedTeams + 1);
            color[player] = 0;
        }

        static bool CanAssign(int player, int team)
        {
            foreach (int neighbor in adj[player])
            {
                if (color[neighbor] == team) return false;
            }
            return true;
        }
#else

        static void Main1854(string[] args)
        {

            int n, m;
            List<int>[] edge;

            Input();

            GetRet();

            void GetRet()
            {

                int[] color = new int[n + 1];
                int[] best = new int[n + 1];

                int ret = n + 1;

                DFS(1, 0);

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= ret; i++) edge[i].Clear();
                for (int i = 1; i <= n; i++) edge[best[i]].Add(i);

                sw.Write(ret);
                sw.Write('\n');
                for (int i = 1; i <= ret; i++)
                {

                    for (int j = 0; j < edge[i].Count; j++)
                    {

                        sw.Write(edge[i][j]);
                        sw.Write(' ');
                    }

                    sw.Write('\n');
                }

                void DFS(int _dep, int _team)
                {

                    if (_dep > n)
                    {

                        if (_team < ret)
                        {

                            ret = _team;
                            for (int i = 1; i <= n; i++)
                            {

                                best[i] = color[i];
                            }
                        }
                        return;
                    }
                    else if (_team >= ret) return;

                    for (int t = 1; t <= _team; t++)
                    {

                        if (Chk(_dep, t)) continue;
                        color[_dep] = t;
                        DFS(_dep + 1, _team);
                        color[_dep] = 0;
                    }

                    color[_dep] = _team + 1;
                    DFS(_dep + 1, _team + 1);
                    color[_dep] = 0;

                    bool Chk(int _p, int _t)
                    {

                        for (int i = 0; i < edge[_p].Count; i++)
                        {

                            int chk = edge[_p][i];
                            if (color[chk] == _t) return true;
                        }

                        return false;
                    }
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                m = ReadInt();

                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < m; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
#endif
    }
}
