using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 아스키 거리
    문제번호 : 2809번

    메모리 초과로 2번 틀렸다
    질문 게시판을 찾아본 결과 여러 번 쪼개서 하면 된다기에
    시도해보니 아호 코라식으로 4.3초에 통과했다
    메모리는 600mb정도 쓴다

    전체를 통과하고 다음 2회전 때 c++ 언어로 lcp방법을 접근해봐야겠다
*/
namespace BaekJoon._56
{
    internal class _56_11
    {

        static void Main11(string[] args)
        {

            int QUERY = 250;
            StreamReader sr;
            string find;
            int[] use;
            Trie root;
            Solve();

            void Solve()
            {

                Input();


                int len = int.Parse(sr.ReadLine());
                for (int i = 1; i <= len; i++)
                {

                    string temp = sr.ReadLine();

                    root.Insert(temp);

                    if (i % QUERY == 0)
                    {

                        root.SetTrie();
                        root.Find(find, use);
                        root.Clear();
                    }
                }
                sr.Close();
                root.SetTrie();
                root.Find(find, use);

                Console.Write(GetRet());
            }

            int GetRet()
            {

                int ret = use[use.Length - 1] > 0 ? 0 : 1;
                for (int i = use.Length - 2; i >= 0; i--)
                {

                    int chk = Math.Max(use[i + 1] - 1, use[i]);
                    if (chk <= 0) 
                    { 
                        
                        chk = 0;
                        ret++;
                    }
                    use[i] = chk;
                }

                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                sr.ReadLine();
                find = sr.ReadLine();
                use = new int[find.Length];

                root = new();
            }
        }

        class Trie
        {

            private Trie[] go;
            private Trie fail;
            private int len;
            private bool isEnd;

            private static Queue<Trie> q = new();

            private Trie this[int _idx]
            {

                set { go[_idx] = value; }
                get { return go[_idx]; }
            }

            public Trie()
            {

                go = new Trie[26];
                fail = null;
                len = 0;
                isEnd = false;
            }

            public void Clear()
            {

                for (int i = 0; i < 26; i++)
                {

                    go[i] = null;
                }

                len = 0;
                isEnd = false;
            }

            public void Insert(string _str)
            {

                Trie next = this;
                for (int i = 0; i < _str.Length; i++)
                {

                    int idx = _str[i] - 'a';
                    if (next[idx] == null) next[idx] = new();
                    next = next[idx];
                }

                next.len = _str.Length;
                next.isEnd = true;
            }

            public void SetTrie()
            {

                this.fail = this;
                q.Enqueue(this);

                while(q.Count > 0)
                {

                    Trie node = q.Dequeue();

                    for (int i = 0; i < 26; i++)
                    {

                        Trie next = node[i];
                        if (next == null) continue;

                        if (node == this) next.fail = this;
                        else
                        {

                            Trie fail = node.fail;
                            while(fail != this && fail[i] == null)
                            {

                                fail = fail.fail;
                            }

                            if (fail[i] != null) fail = fail[i];
                            next.fail = fail;
                        }

                        if (next.fail.isEnd) 
                        { 
                            
                            next.isEnd = true;
                            next.len = Math.Max(next.fail.len, next.len);
                        }
                        q.Enqueue(next);
                    }
                }
            }

            public void Find(string _str, int[] _use)
            {

                Trie current = this;
                for (int i = 0; i < _str.Length; i++)
                {

                    int next = _str[i] - 'a';

                    while(current != this && current[next] == null)
                    {

                        current = current.fail;
                    }

                    if (current[next] != null) current = current[next];

                    if (current.isEnd && _use[i] < current.len) _use[i] = current.len;
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <cstring>
// #include <queue>
// #include <string>
// #include <algorithm>
using namespace std;

// #define N 100001
int turn, tree[N][26], fail[N];
int maximum[N];
long long int s[300002];
string text;
void f(string &str){
	int idx = 0;
	for (auto x : str){
		int k = (x - 'a');
		idx = (tree[idx][k] ? tree[idx][k] : (tree[idx][k] = ++turn));
	}
	maximum[idx] = (int)str.size();
}
void f(){
	queue<int> q;
	for (int k = 0; k < 26; k++) if (tree[0][k]) q.push(tree[0][k]);
	while (!q.empty()){
		int now = q.front(); q.pop();
		for (int k = 0; k < 26; k++)
		if (int next = tree[now][k]){
			int prev = fail[now];
			while (prev && !tree[prev][k]) prev = fail[prev];
			if (tree[prev][k]) prev = tree[prev][k];
			fail[next] = prev;
			q.push(next);
			maximum[next] = max(maximum[next], maximum[prev]);
		}
	}
}
void g(){
	int n = text.size(), idx = 0;
	for (int i = 1; i <= n; i++){
		int k = (text[i - 1] - 'a');
		while (idx && !tree[idx][k]) idx = fail[idx];
		if (tree[idx][k]) idx = tree[idx][k];
		s[i - maximum[idx] + 1]++, s[i + 1]--;
	}
	memset(tree, 0, sizeof(tree)); turn = 0;
	memset(fail, 0, sizeof(fail));
	memset(maximum, 0, sizeof(maximum));
}
int main(void){
	cin.tie(0);
	ios::sync_with_stdio(0);

	int n, m, answer = 0;
	string str; cin >> n >> text >> m;
	for (int i = 0; i < m; i++){
		cin >> str;
		if (turn + (int)str.size() >= N) f(), g();
		f(str);
	}
	f(), g();
	for (int i = 1; i <= n; i++) s[i] += s[i - 1];
	for (int i = 1; i <= n; i++) answer += (s[i] == 0);
	cout << answer;
	return 0;
}
#elif other2

// #include <bits/stdc++.h>

using namespace std;
using vInt = vector<int>;
using matInt = vector<vInt>;
using pii = pair<int, int>;
using vPii = vector<pii>;
using matPii = vector<vPii>;
using LL = long long;
using vLL = vector<LL>;
using matLL = vector<vLL>;
using pLL = pair<LL, LL>;
using vPLL = vector<pLL>;
using vBool = vector<bool>;
using matBool = vector<vBool>;
using vStr = vector<string>;

// #define all(x) (x).begin(), (x).end()
// #define ff first
// #define ss second

struct LCP{
    int N, M;
    vInt ra, nra, sa, isa, lcp, cnt, idx;
    LCP(int N): N(N){
        ra = nra = vInt(2*N);
        sa = isa = lcp = idx = vInt(N);
        M = max(256, N) +1;
        cnt = vInt(M);
    }
    void buildSA(string& S){
        for(int i=0; i<N; i++){
            sa[i] = i;
            ra[i] = S[i];
        }
        for(int d=1; d<N; d<<=1){
            auto cmp = [&](int a, int b)->bool{
                if(ra[a]!=ra[b]) return ra[a] < ra[b];
                return ra[a+d] < ra[b+d];
            };
            fill(all(cnt), 0);
            for(int i=0; i<N; i++) cnt[ra[i+d]]++;
            for(int i=1; i<M; i++) cnt[i] += cnt[i-1];
            for(int i=N-1; i>=0; i--) idx[--cnt[ra[i+d]]] = i;

            fill(all(cnt), 0);
            for(int i=0; i<N; i++) cnt[ra[i]]++;
            for(int i=1; i<M; i++) cnt[i] += cnt[i-1];
            for(int i=N-1; i>=0; i--) sa[--cnt[ra[idx[i]]]] = idx[i];

            nra[sa[0]] = 1;
            for(int i=1; i<N; i++)
                nra[sa[i]] = nra[sa[i-1]] + cmp(sa[i-1], sa[i]);

            swap(ra, nra);
            if(ra[N-1] == N) break;
        }
    }
    void buildLCP(string& S){
        buildSA(S);
        for(int i=0; i<N; i++)
            isa[sa[i]] = i;
        int k = 0;
        for(int i=0; i<N; i++){
            if(isa[i]){
                int j = sa[isa[i]-1];
                while(i+k < N && j+k < N && S[i+k] == S[j+k])
                    k++;
                lcp[i] = k? k-- : 0;
            }
        }
    }
};

struct Node{
    int s, e, v;
};

void solve(){
    int N;
    cin >> N;
    string S;
    cin >> S;
    LCP lcpb(N);
    lcpb.buildLCP(S);
    vInt& sa = lcpb.sa;

    int Q;
    cin >> Q;
    vector<Node> AS(2*Q);
    for(int i=0; i<Q; i++){
        string P;
        cin >> P;

        int l, r;
        
        l = -1; r = sa.size();
        while(l+1 < r){
            int m = l+r >> 1;
            if(P <= S.substr(sa[m], P.size())) r = m;
            else l = m;
        }
        int lo = r;

        l = -1; r = sa.size();
        while(l+1 < r){
            int m = l+r >> 1;
            if(P < S.substr(sa[m], P.size())) r = m;
            else l = m;
        }
        int hi = r;

        AS[2*i] = {lo, hi, (int)P.size()};
        AS[2*i+1] = {hi, lo, (int)P.size()};
    }

    sort(all(AS), [](Node& a, Node& b)->bool{
        return a.s < b.s;
    });

    vInt X(N);
    multiset<int> ST;
    int idx = 0;
    for(int i=0; i<N; i++){
        while(idx < 2*Q && AS[idx].s <= i){
            Node& tar = AS[idx++];
            if(tar.s < tar.e) ST.insert(tar.v);
            else if(tar.e < tar.s) ST.erase(ST.find(tar.v));
        }
        if(ST.size()){
            auto it = ST.end(); it--;
            X[sa[i]] = *it;
        }
    }

    int now = X[0];
    for(int i=1; i<N; i++){
        X[i] = max(X[i], now-1);
        now = X[i];
    }

    int ans = 0;
    for(int i=0; i<N; i++) ans += (X[i] == 0);
    cout << ans << '\n';
}

int main(){
    ios::sync_with_stdio(false);
    cin.tie(nullptr); cout.tie(nullptr);
    
    solve();
    
    return 0;
}

#elif other3
import io, os
input=io.BytesIO(os.read(0,os.fstat(0).st_size)).readline

from collections import deque 
class Node:
  def __init__(self, s=None) :
    self.data = s
    self.go = {}
    self.fail = None
    self.output = 0

class AhoCorasick:
  def __init__(self, length) :
    self.root = Node()
  
  def insert(self, s: str, v) :
    u = self.root
    for c in s :
      if c not in u.go :
        u.go[c] = Node(c)
      u = u.go[c]
    u.output = v
  
  def build(self) :
    Q = deque([self.root])
    while Q :
      u = Q.popleft()
      for k in u.go :
        v = u.go[k]
        if u == self.root :
          v.fail = self.root
        else :
          w = u.fail
          while w != self.root and k not in w.go :
            w = w.fail
          if k in w.go :
            w = w.go[k]
          v.fail = w
        
        if not v.output or v.output < v.fail.output : #가능한 가장 긴 길이의 타일로 업데이트한다
          v.output = v.fail.output
        Q.append(v)
  
  def query(self, s: str, vis: list) :
    u = self.root
    for i, c in enumerate(s) :
      while u != self.root and c not in u.go :
        u = u.fail
      if c in u.go : u = u.go[c]

      if u.output :
        start = i - u.output + 1
        vis[start] = max(vis[start], u.output) #더 긴 타일이 있다면 업데이트한다.

def sol() :
  N = int(input())
  S = input().rstrip()

  M = int(input())
  P = [input().rstrip() for _ in range(M)]
  vis = [0] * N #i번째 위치에서 길이 vis[i]의 타일을 교체할 수 있다.

  cnt = 0
  aho = AhoCorasick(vis)
  for i, s in enumerate(P) :
    if cnt == 100 :
      aho.build()
      aho.query(S, vis)
      aho = AhoCorasick(vis)
      cnt = 0

    cnt += 1
    aho.insert(s, len(s))
  aho.build()
  aho.query(S, vis)

  left = 0
  ans = [False] * N
  for i, v in enumerate(vis) : #순회하면서 교체할 수 있는 타일을 교체한 것으로 표시
    left = max(left, v)
    if left :
      ans[i] = True
    left -= 1
  print(ans.count(False))

sol()
#elif other4
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;

public class Main {
    static StringBuilder sb = new StringBuilder();

    public static void main(String[] args) throws IOException {
        BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
        int N = Integer.parseInt(br.readLine());
        String S = br.readLine();
        char zero = 'a';
        Trie root = new Trie();
        root.root = true;
        root.fail = root;
        int M = Integer.parseInt(br.readLine());
        for (int i = 0; i < M; i++)
            root.insert(0, br.readLine());
        ArrayDeque<Trie> q = new ArrayDeque<>();
        q.addLast(root);
        while (!q.isEmpty()) {
            Trie cur = q.removeFirst();
            for (int i = 0; i < 26; i++) {
                if (cur.go[i] != null) {
                    if (cur.root)
                        cur.go[i].fail = cur;
                    else {
                        Trie j = cur.fail;
                        while (!j.root && j.go[i] == null)
                            j = j.fail;
                        if (j.go[i] != null)
                            j = j.go[i];
                        cur.go[i].fail = j;
                    }
                    if (cur.go[i].fail.leaf) {
                        cur.go[i].leaf = true;
                        cur.go[i].len = Integer.max(cur.go[i].len, cur.go[i].fail.len);
                    }
                    q.addLast(cur.go[i]);
                }
            }
        }
        int[] sum = new int[N + 2];
        Trie cur = root;
        for (int i = 0; i < N; i++) {
            int next = S.charAt(i) - zero;
            while (!cur.root && cur.go[next] == null)
                cur = cur.fail;
            if (cur.go[next] != null)
                cur = cur.go[next];
            if (cur.leaf) {
                sum[i - cur.len + 2] += 1;
                sum[i + 2] += -1;
            }
        }
        int cnt = 0;
        for (int i = 1; i <= S.length(); i++) {
            sum[i] += sum[i - 1];
            if (sum[i] == 0)
                cnt++;
        }
        System.out.println(cnt);
    }
}

class Trie {
    int len = 1;
    char zero = 'a';
    boolean root = false, leaf = false;
    Trie fail;
    Trie[] go = new Trie[26];

    void insert(int idx, String S) {
        if (idx == S.length()) {
            leaf = true;
            len = Integer.max(len, S.length());
            return;
        }
        int next = S.charAt(idx) - zero;
        if (go[next] == null)
            go[next] = new Trie();
        go[next].insert(idx + 1, S);
    }
}
#endif
}
