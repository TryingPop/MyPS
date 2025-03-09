using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 9
이름 : 배성훈
내용 : 트리의 외심
    문제번호 : 17399번

    트리, LCA 문제다.
    트리의 외심은 귀류법으로 접근하면 중심 중 한 곳임을 알 수 있다.
    트리의 성질상 두 정점 사이의 최소 거리는 LCA를 이용하면 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1390
    {

        static void Main1390(string[] args)
        {

            // 150_000 > 2^17 >= 100_000
            int LEN = 18;
            string NO = "-1\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;
            List<int>[] edge;
            int[][] parent;
            int[] depth;

            Input();

            SetLCA();

            GetRet();

            void GetRet()
            {

                int t = ReadInt();

                while (t-- > 0)
                {

                    int a = ReadInt();
                    int b = ReadInt();
                    int c = ReadInt();

                    int ret = GetCenter(a, b, c);
                    if (ret != -1)
                    {

                        sw.Write($"{ret}\n");
                        continue;
                    }

                    ret = GetCenter(b, c, a);
                    if (ret != -1)
                    {

                        sw.Write($"{ret}\n");
                        continue;
                    }

                    ret = GetCenter(c, a, b);
                    if (ret != -1)
                    {

                        sw.Write($"{ret}\n");
                        continue;
                    }

                    sw.Write(NO);
                }
            }

            // 거리가 같은 노드 찾기
            int GetCenter(int _min, int _max, int _chk)
            {

                if (depth[_max] < depth[_min])
                {

                    int temp = _min;
                    _min = _max;
                    _max = temp;
                }

                int lca = GetLCA(_min, _max);

                int dis = GetDis(_min, _max, lca);
                if (dis % 2 != 0) return -1;
                dis /= 2;
                int center = FindParent(_max, dis);
                lca = GetLCA(_chk, center);
                if (dis == GetDis(_chk, center, lca)) return center;
                else return -1;
            }

            // 두 정점 사이의 거리 찾는다
            // LCA를 외부에서 찾아와야 한다.
            // 이능 기능상 LCA만 따로 써서 분리 했다.
            int GetDis(int _f, int _t, int _lca)
            {

                return GetDis(_f, _lca) + GetDis(_t, _lca);

                int GetDis(int _child, int _parent) 
                { 
                    
                    return depth[_child] - depth[_lca]; 
                }
            }

            // LCA 찾기
            int GetLCA(int _f, int _t)
            {

                if (depth[_t] < depth[_f])
                {

                    int temp = _f;
                    _f = _t;
                    _t = temp;
                }

                int f = _f;
                int t = FindParent(_t, depth[_t] - depth[_f]);

                if (f == t) return f;

                for (int i = LEN - 2; i >= 0; i--)
                {

                    if (parent[f][i] == parent[t][i]) continue;
                    f = parent[f][i];
                    t = parent[t][i];
                }

                return parent[f][0];
            }

            // 해당 노드의 dis 위의 부모를 가져온다.
            int FindParent(int _node, int _dis)
            {

                int ret = _node;
                for (int i = (LEN - 1); i >= 0; i--)
                {

                    int up = 1 << i;
                    if (_dis < up) continue;
                    _dis -= up;
                    ret = parent[ret][i];
                }

                return ret;
            }

            void SetLCA()
            {

                depth = new int[n + 1];
                parent = new int[n + 1][];
                for (int i = 0; i <= n; i++)
                {

                    parent[i] = new int[LEN];
                }

                SetDepth();

                SetParent();

                void SetParent()
                {

                    for (int j = 1; j < LEN; j++)
                    {

                        for (int i = 1; i <= n; i++)
                        {

                            int p = parent[i][j - 1];
                            parent[i][j] = parent[p][j - 1];
                        }
                    }
                }

                void SetDepth(int _cur = 1, int _dep = 1)
                {

                    depth[_cur] = _dep;
                    for (int i = 0; i < edge[_cur].Count; i++)
                    {

                        int next = edge[_cur][i];
                        if (depth[next] != 0) continue;
                        parent[next][0] = _cur;
                        SetDepth(next, _dep + 1);
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                edge = new List<int>[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    edge[i] = new();
                }

                for (int i = 1; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    edge[f].Add(t);
                    edge[t].Add(f);
                }
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
                    if (c == ' ' || c == '\n') return true;
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

#if other
// ref : jinhan814
// #include <bits/stdc++.h>
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0);
using namespace std;

const int SZ = 1 << 20;
char read_buf[SZ], write_buf[SZ];

namespace IO { 
    int read_idx, next_idx, write_idx;
	inline bool is_blank(char c) { return c == ' ' || c == '\n'; }
	inline bool is_end(char c) { return c == '\0'; }
	inline char _readChar() {
		if (read_idx == next_idx) {
			next_idx = fread(read_buf, sizeof(char), SZ, stdin);
			if (next_idx == 0) return 0;
			read_idx = 0;
		}
		return read_buf[read_idx++];
	}
	inline char readChar() {
		char ret = _readChar();
		while (is_blank(ret)) ret = _readChar();
		return ret;
	}
	template<typename T>
	inline T _readInt() {
		T ret = 0;
		char cur = _readChar();
		bool flag = 0;
		while (is_blank(cur)) cur = _readChar();
		if (cur == '-') flag = 1, cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret = 10 * ret + cur - '0', cur = _readChar();
		return flag ? -ret : ret;
	}
	inline int readInt() { return _readInt<int>(); }
	inline long long readLL() { return _readInt<long long>(); }
	inline string readString() {
		string ret;
		char cur = _readChar();
		while (is_blank(cur)) cur = _readChar();
		while (!is_blank(cur) && !is_end(cur)) ret.push_back(cur), cur = _readChar();
		return ret;
	}
	inline double readDouble() {
		string ret = readString();
		return stod(ret);
	}
	template<typename T>
	inline int getSize(T n) {
		int ret = 1;
		if (n < 0) n = -n;
		while (n >= 10) ret++, n /= 10;
		return ret;
	}
	inline void bflush() {
		fwrite(write_buf, sizeof(char), write_idx, stdout);
		write_idx = 0;
	}
	inline void writeChar(char c) {
		if (write_idx == SZ) bflush();
		write_buf[write_idx++] = c;
	}
	inline void newLine() { writeChar('\n'); }
	template<typename T>
	inline void _writeInt(T n) {
		int sz = getSize(n);
		if (write_idx + sz >= SZ) bflush();
		if (n < 0) write_buf[write_idx++] = '-', n = -n;
		for (int i = sz - 1; i >= 0; i--) write_buf[write_idx + i] = n % 10 + '0', n /= 10;
		write_idx += sz;
	}
	inline void writeInt(int n) { _writeInt<int>(n); }
	inline void writeLL(long long n) { _writeInt<long long>(n); }
	inline void writeString(string s) { for (auto& c : s) writeChar(c); }
	inline void writeDouble(double d) { writeString(to_string(d)); }
}
using namespace IO;

// #undef fastio
// #define fastio 1
// #define cin in
// #define cout out

struct INPUT{} in;
INPUT& operator>> (INPUT& in, char& i) { i = readChar(); return in; }
INPUT& operator>> (INPUT& in, int& i) { i = readInt(); return in; }
INPUT& operator>> (INPUT& in, long long& i) { i = readLL(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = readString(); return in; }
INPUT& operator>> (INPUT& in, double& i) { i = readDouble(); return in; }

struct OUTPUT{ ~OUTPUT() { bflush(); } } out;
OUTPUT& operator<< (OUTPUT& out, char i) { writeChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, int i) { writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, long long i) { writeLL(i); return out; }
OUTPUT& operator<< (OUTPUT& out, size_t i) { writeInt(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { writeString(i); return out; }
OUTPUT& operator<< (OUTPUT& out, double i) { writeDouble(i); return out; }

template<size_t _sz>
struct HLD {
   vector<int> sz, dep, par, top, in, out, rev;
   vector<vector<int>> inp, adj;
   HLD() : sz(_sz), dep(_sz), par(_sz), top(_sz), in(_sz), out(_sz), inp(_sz), adj(_sz), rev(_sz) {}
   void add_edge(int a, int b) { inp[a].push_back(b), inp[b].push_back(a); }
   void dfs(int cur = 1, int prev = -1) {
      for (auto nxt : inp[cur]) {
         if (nxt == prev) continue;
         adj[cur].push_back(nxt);
         dfs(nxt, cur);
      }
   }
   void dfs1(int cur = 1) {
      sz[cur] = 1;
      for (auto& nxt : adj[cur]) {
         dep[nxt] = dep[cur] + 1; par[nxt] = cur;
         dfs1(nxt); sz[cur] += sz[nxt];
         if (sz[nxt] > sz[adj[cur][0]]) swap(nxt, adj[cur][0]);
      }
   }
   int temp = 0;
   void dfs2(int cur = 1) {
      in[cur] = ++temp;
      rev[temp] = cur;
      for (auto nxt : adj[cur]) {
         top[nxt] = (nxt == adj[cur][0] ? top[cur] : nxt);
         dfs2(nxt);
      }
      out[cur] = temp;
   }
   void construct() { dfs(), dfs1(), dfs2(top[1] = 1); }
   int lca(int a, int b) {
      for (; top[a] ^ top[b]; a = par[top[a]]) {
         if (dep[top[a]] < dep[top[b]]) swap(a, b);
      }
      if (dep[a] > dep[b]) swap(a, b);
      return a;
   }
   int get_dist(int a, int b) {
       return dep[a] + dep[b] - 2 * dep[lca(a, b)];
   }
   int get_mid(int a, int b) {
       int t = lca(a, b);
       int t1 = get_dist(a, t);
       int t2 = get_dist(b, t);
       if (t1 + t2 & 1) return -1;
       if (t1 < t2) swap(a, b);
       int d = t1 + t2 >> 1;
       for (; dep[a] - dep[top[a]] + 1 <= d; a = par[top[a]]) {
           d -= dep[a] - dep[top[a]] + 1;
       }
       return rev[in[a] - d];
   }
};

HLD<1 << 17> hld;

int main() {
   fastio;
   int n; cin >> n;
   for (int i = 0; i < n - 1; i++) {
       int a, b; cin >> a >> b;
       hld.add_edge(a, b);
   }
   hld.construct();
   int q; cin >> q;
   while (q--) {
       int a, b, c; cin >> a >> b >> c;
       int t1 = hld.get_mid(a, b);
       int t2 = hld.get_mid(a, c);
       int t3 = hld.get_mid(b, c);
       if (~t1 && hld.get_dist(a, t1) == hld.get_dist(c, t1)) cout << t1 << '\n';
       else if (~t2 && hld.get_dist(a, t2) == hld.get_dist(b, t2)) cout << t2 << '\n';
       else if (~t3 && hld.get_dist(a, t3) == hld.get_dist(c, t3)) cout << t3 << '\n';
       else cout << -1 << '\n';
   }
}
#endif
}
