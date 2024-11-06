using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 21
이름 : 배성훈
내용 : 인물이와 정수
    문제번호 : 20666번

    그리디, 우선순위 큐 문제다
    아이디어는 다음과 같다
    오름차순으로 난이도를 정렬해서
    클리어가 될때까지 난이도를 확인하면 된다

    중간에 난이도가 변경되면 변경된 난이도를 넣어주면 끝이다
    그래서 오는데 난이도까지 최대값이 정답이 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0983
    {

        static void Main983(string[] args)
        {

            StreamReader sr;
            PriorityQueue<int, long> pq;
            int n, m;

            long[] diff;

            List<(int dst, int val)>[] edge;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                pq = new(2 * n);

                for (int i = 0; i < n; i++)
                {

                    pq.Enqueue(i, diff[i]);
                }

                bool[] visit = new bool[n];
                int k = m;
                long ret = 0;
                while (k > 0)
                {

                    var node = pq.Dequeue();

                    if (visit[node]) continue;
                    visit[node] = true;
                    k--;

                    ret = Math.Max(ret, diff[node]);

                    for (int i = 0; i < edge[node].Count; i++)
                    {

                        var next = edge[node][i];
                        diff[next.dst] -= next.val;

                        pq.Enqueue(next.dst, diff[next.dst]);
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                diff = new long[n];
                edge = new List<(int dst, int val)>[n];
                for (int i = 0; i < n; i++)
                {

                    diff[i] = ReadInt();
                    edge[i] = new();
                }

                int k = ReadInt();

                for (int i = 0; i < k; i++)
                {

                    int f = ReadInt() - 1;
                    int t = ReadInt() - 1;
                    int u = ReadInt();

                    edge[f].Add((t, u));
                    diff[t] += u;
                }

                sr.Close();
            }

            bool TryReadInt(out int ret)
            {

                int c = sr.Read();
                ret = 0;
                if (c == -1 || c == ' ' || c == '\n') return true;
                ret = c - '0';

                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return false;
            }

            int ReadInt()
            {

                int ret;
                while (TryReadInt(out ret)) { }

                return ret;
            }
        }
    }

#if other
// #include <iostream>
// #include <vector>
// #include <queue>
using namespace std;
using LL = long long;

// #define MAX 100'000

struct tip { int b, t; };
vector<tip> tips[MAX + 1]; 
bool v[MAX + 1];

struct PQobj {
    int idx;
    LL cost;
    bool operator<(const PQobj& rhs) const {
        return cost > rhs.cost;
    }
};

int main() {
    ios::sync_with_stdio(false);
    cin.tie(nullptr);

    int N, M; cin >> N >> M;
    vector<PQobj> arr(N + 1);
    for (int i = 1; i <= N; i++) {
        int t; cin >> t;
        arr[i].idx = i;
        arr[i].cost = t;
    }
    int P; cin >> P;
    for (int i = 0; i < P; i++) {
        int a, b, t; cin >> a >> b >> t;
        arr[b].cost += t;
        tips[a].push_back({ b, t });
    }
    priority_queue<PQobj> pq;
    for (int i = 1; i <= N; i++) {
        pq.push(arr[i]);
    }

    LL m = 0;
    LL mc = 0;
    while (!pq.empty()) {
        auto cur = pq.top(); pq.pop();
        if (v[cur.idx]) continue;
        v[cur.idx] = true;
        mc = max(mc, cur.cost);
        m++;
        if (m == M) break;

        for (const tip& p : tips[cur.idx]) {
            if (v[p.b]) continue;
            arr[p.b].cost -= p.t;
            pq.push(arr[p.b]);
        }
    }

    cout << mc << endl;

    return 0;
}
#endif
}
