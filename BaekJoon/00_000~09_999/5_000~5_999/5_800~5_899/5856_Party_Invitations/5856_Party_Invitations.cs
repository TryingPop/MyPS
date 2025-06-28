using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 22
이름 : 배성훈
내용 : Party Invitations
    문제번호 : 5856번

    구현 문제다.
    처음에는 위상정렬로 접근해야할거 같았고,
    N의 범위가 100만으로 메모리초과날거 같아 해당 방법을 보류했다.

    힌트를 보니 구현이 있었고 다음과 같이 구현했다.
    1번을 초대하고 그룹을 조사한다.
    여기서 k - 1명이 포함된 그룹이 있으면 남은 인원을 초대하는 식으로 구현했다.
    이렇게 더 이상 초대할 수 없는 경우 종료한다.

    그룹의 갯수를 g라하면 1 ~ 2, 2 ~ 3, ..., (g-1) ~ g이렇게 그룹이 있으면 시간초과 날거 같다.
    O(g^2)의 방법이므로 시간초과 날 수 있다.
    구현이 있으니 구현을 믿고 위 방법을 제출해봤다.
    그러니 통과했다...

    이후 위상정렬로 접근하니 메모리는 3배 안되게 더 쓰는데 25%정도 빨리 풀린다.
*/

namespace BaekJoon.etc
{
    internal class etc_1640
    {

        static void Main1640(string[] args)
        {

#if first
            int n, g;
            HashSet<int>[] mem;

            Input();

            GetRet();

            void GetRet()
            {

                // 포함된 숫자
                bool[] use = new bool[n + 1];

                // 찾을 인원
                Queue<int> cur = new(n);
                // 인원이 최소 2명 이상인 그룹들 번호 보관하는 자료구조
                Queue<int> remain = new(g);
                // 연산용 스택
                int[] temp = new int[g];

                // 무조건 포함해야하는 1번
                use[1] = true;
                for (int i = 0; i < g; i++)
                {

                    // 그룹에 남은 멤버가 1명인 경우
                    // 문제 조건에 따라 파티에 초대
                    if (mem[i].Count == 1) 
                    {

                        int add = mem[i].First();
                        // 이미 초대된 경우면 재초대 X
                        if (!use[add]) 
                        {

                            cur.Enqueue(add); 
                            use[add] = true;
                        }
                    }
                    // 2명 이상인 그룹이다.
                    else remain.Enqueue(i);
                }

                while (cur.Count > 0)
                {

                    Invite(cur.Dequeue());
                }

                void Invite(int _n)
                {

                    // _n 멤버를 초대했다.
                    int len = 0;

                    // 2명 이상인 그룹 조사
                    while (remain.Count > 0)
                    {

                        int r = remain.Dequeue();

                        // 찾는 _n을 포함한 그룹
                        if (mem[r].Contains(_n))
                        {

                            // _n이 초대되었으므로 그룹에서 해당 인원 제외
                            mem[r].Remove(_n);

                            if (mem[r].Count == 1)
                            {

                                // _n을 초대한 뒤 그룹에 남은 인원이 1명인 경우 
                                // 문제 조건에 의해 남은 인원 초대
                                int add = mem[r].First();
                                // 이미 초대된 경우면 재초대 X
                                if (!use[add]) 
                                {

                                    cur.Enqueue(add); 
                                    use[add] = true;
                                }
                                
                                // 남은 인원이 1명 이하면 해당 그룹 제거!
                                continue;
                            }
                        }

                        // 남은 인원이 2명 이상이면 다시 넣어줘야 한다.
                        temp[len++] = r;
                    }

                    // 다시 넣기
                    while (len-- > 0)
                    {

                        remain.Enqueue(temp[len]);
                    }
                }

                // 초대된 인원 세기
                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (use[i]) ret++;
                }

                Console.Write(ret);
            }

                        void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                g = ReadInt();

                mem = new HashSet<int>[g];
                for (int i = 0; i < g; i++)
                {

                    int size = ReadInt();
                    mem[i] = new(size);
                    for (int j = 0; j < size; j++)
                    {

                        int cur = ReadInt();
                        if (cur == 1) continue;
                        mem[i].Add(cur);
                    }
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
#elif TOPO

            int n, g;
            // mem[i] : i번 사람이 포함된 그룹들
            // group[j] : j번째 그룹이 포함하는 인원들
            HashSet<int>[] mem, group;

            Input();

            GetRet();

            void GetRet()
            {

                Queue<int> q = new(n);
                bool[] visit = new bool[n + 1];

                q.Enqueue(1);

                // 위상 정렬
                while (q.Count > 0)
                {

                    int cur = q.Dequeue();

                    if (visit[cur]) continue;
                    visit[cur] = true;

                    foreach(int gIdx in mem[cur])
                    {

                        group[gIdx].Remove(cur);
                        if (group[gIdx].Count == 1) q.Enqueue(group[gIdx].First());
                    }

                    mem[cur].Clear();
                }

                int ret = 0;
                for (int i = 1; i <= n; i++)
                {

                    if (visit[i]) ret++;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                g = ReadInt();

                mem = new HashSet<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    mem[i] = new();
                }

                group = new HashSet<int>[g];

                for (int i = 0; i < g; i++)
                {

                    int size = ReadInt();
                    group[i] = new(size);
                    for (int j = 0; j < size; j++)
                    {

                        int add = ReadInt();
                        group[i].Add(add);
                        mem[add].Add(i);
                    }
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
#endif


        }
    }

#if other
// #include <bits/stdc++.h>
// #define int ll
// #define all(x) begin(x), end(x)
using namespace std;
using ll = long long;
using pii = pair<int, int>;
using pll = pair<ll, ll>;
using vi = vector<int>;
using vvi = vector<vi>;
using vll = vector<ll>;
using vpii = vector<pii>;
using vpll = vector<pll>;
const ll INF = 0x3f3f3f3f3f3f3f3f;
namespace rng = ranges;
namespace vw = views;
int dy[]{0, 0, 1, -1};
int dx[]{1, -1, 0, 0};

signed main() {
    // ifstream cin("z.txt");
    cin.tie(nullptr)->sync_with_stdio(false);
    int N, G;
    cin >> N >> G;
    vvi group(G);
    vi cnt(G);
    vi vst(N);
    vvi adj(N);
    for (int i = 0; i < G; ++i) {
        cin >> cnt[i];
        group[i].resize(cnt[i]);
        for (auto& e : group[i]) {
            cin >> e;
            --e;
            adj[e].emplace_back(i);
        }
    }
    function<void(int)> f = [&](int u) {
        if (vst[u]) return;
        vst[u] = true;
        for (auto& v : adj[u]) {
            if (--cnt[v] == 1) {
                for (auto& e : group[v]) {
                    if (!vst[e]) {
                        f(e);
                    }
                }
            }
        }
    };
    f(0);
    cout << count(all(vst), 1);
    return 0;
}
#endif
}
