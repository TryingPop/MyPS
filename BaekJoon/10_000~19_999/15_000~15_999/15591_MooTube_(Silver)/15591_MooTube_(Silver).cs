using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : MooTube (Silver)
    문제번호 : 15591번

    DFS, BFS 문제다
    깊이가 5_000까지 깊어질 수 있어 BFS로 했다

    매번 확인하기에 시간이 많이 걸린다
    BFS 탐색인줄만 알았는데, 다른사람 풀이를 보니 유니온 파인드 문제같이도 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0275
    {

        static void Main275(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int n = ReadInt();
            int m = ReadInt();

            List<(int dst, int val)>[] lines = new List<(int dst, int val)>[n + 1];

            for (int i = 1; i <= n; i++)
            {

                lines[i] = new();
            }

            for (int i = 1; i < n; i++)
            {

                int f = ReadInt();
                int b = ReadInt();
                int v = ReadInt();

                lines[f].Add((b, v));
                lines[b].Add((f, v));
            }

            bool[] visit = new bool[n + 1];
            Queue<int> q = new Queue<int>(n);

            for (int i = 0; i < m; i++)
            {

                int k = ReadInt();
                int s = ReadInt();

                q.Enqueue(s);
                visit[s] = true;

                int ret = 0;
                while (q.Count > 0)
                {

                    int node = q.Dequeue();
                    for (int j = 0; j < lines[node].Count; j++)
                    {

                        // k이상의 간선만 지나갈 수 있다
                        int next = lines[node][j].dst;
                        int val = lines[node][j].val;
                        if (visit[next] || k > val) continue;
                        visit[next] = true;
                        ret++;

                        q.Enqueue(next);
                    }
                }

                // 방문여부 재처리
                for (int j = 1; j <= n; j++)
                {

                    visit[j] = false;
                }

                sw.WriteLine(ret);
            }

            sr.Close();
            sw.Close();

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
#if other
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleApp3
{
    class MoonTube
    {
        public int index;
        public List<int> connection;
        public Dictionary<int, int> usadoTable;

        public MoonTube(int index)
        {
            this.index = index;
            connection = new List<int>();
            usadoTable = new Dictionary<int, int>();
        }
    }

    class Program
    {
        static StreamReader reader = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter writer = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static int CalculateUSADO(MoonTube[] videos, int start, int limit)
        {
            bool[] visied = new bool[videos.Length];
            visied[start] = true;
            MoonTube target = videos[start];
            Queue<MoonTube> queue = new Queue<MoonTube>();
            queue.Enqueue(target);
            int[] usado = new int[videos.Length];

            int count = 0;
            while(queue.Count != 0)
            {
                target = queue.Dequeue();
                foreach(int path in target.connection)
                {
                    if (visied[path])
                        continue;

                    int u = target.usadoTable[path];
                    if (usado[target.index] != 0)
                        usado[path] = (int)MathF.Min(usado[target.index], u);
                    else
                        usado[path] = u;

                    if (limit > usado[path])
                        continue;

                    visied[path] = true;
                    queue.Enqueue(videos[path]);
                    count++;
                }
            }

            return count;
        }

        static void Main()
        {
            int[] input1 = Array.ConvertAll(reader.ReadLine().Split(' '), int.Parse);
            int N = input1[0];
            int Q = input1[1];

            MoonTube[] videos = new MoonTube[N];
            for (int i = 0; i < N; i++)
                videos[i] = new MoonTube(i);

            for(int i = 0; i < N - 1; i++)
            {
                int[] input2 = Array.ConvertAll(reader.ReadLine().Split(' '), int.Parse);
                videos[input2[0] - 1].connection.Add(input2[1] - 1);
                videos[input2[0] - 1].usadoTable.Add(input2[1] - 1, input2[2]);

                videos[input2[1] - 1].connection.Add(input2[0] - 1);
                videos[input2[1] - 1].usadoTable.Add(input2[0] - 1, input2[2]);
            }

            for(int i = 0; i < Q; i++)
            {
                int[] input3 = Array.ConvertAll(reader.ReadLine().Split(' '), int.Parse);
                int k = input3[0];
                int v = input3[1] - 1;
                // 만약 usado가 k보다 높으면 동영상 추천
                // v는 현재 보고있는 동영상
                sb.AppendLine(CalculateUSADO(videos, v, k).ToString());
            }

            writer.Write(sb);
            writer.Close();
        }
    }
}
#elif other2
// #include <cstdio>
// #include <sys/stat.h>
// #include <sys/mman.h>
// #include <vector>
// #include <numeric>
// #include <algorithm>
using namespace std;

struct UnionFind {
	vector<int16_t> par, cnt;
	UnionFind(int n) : par(n), cnt(n, 1) {
		iota(par.begin(), par.end(), 0);
	}
	int Find(int x) {
		return x == par[x] ? x : par[x] = Find(par[x]);
	}
	void Union(int a, int b) {
		a = Find(a), b = Find(b);
		par[b] = a; cnt[a] += cnt[b];
	}
};

struct Edge {
	int16_t a, b; int cost;
	bool operator < (const Edge& i) const { return cost > i.cost; }
};

struct Query {
	int k; int16_t v, idx;
	bool operator < (const Query& i) const { return k > i.k; }
};

int main() {
    struct stat st; fstat(0, &st);
	char* p = (char*)mmap(0, st.st_size, PROT_READ, MAP_SHARED, 0, 0);
	auto ReadInt = [&]() {
		int ret = 0;
		for (char c = *p++; c & 16; ret = 10 * ret + (c & 15), c = *p++);
		return ret;
	};
    
	int n = ReadInt(), q = ReadInt();
	vector<Edge> v(n - 1); vector<Query> Q(q);
	for (auto& [a, b, cost] : v) a = ReadInt(), b = ReadInt(), cost = ReadInt();
	for (int i = 0; i < q; i++) Q[i] = { ReadInt(), ReadInt(), i };

	sort(v.begin(), v.end());
	sort(Q.begin(), Q.end());

	UnionFind UF(n + 1); vector<int> ans(q);
	for (int i = 0, j = 0; i < q; i++) {
		while (j < n - 1 && v[j].cost >= Q[i].k)
			UF.Union(v[j].a, v[j].b), j++;
		ans[Q[i].idx] = UF.cnt[UF.Find(Q[i].v)] - 1;
	}
	for (int i = 0; i < q; i++) printf("%d\n", ans[i]);
}
#endif
}
