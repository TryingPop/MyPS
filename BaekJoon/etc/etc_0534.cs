using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 15
이름 : 배성훈
내용 : 의리 게임
    문제번호 : 28424번

    분리집합, 트리를 사용한 집합과 맵 문제다
    n, m의 범위가 최대 10만개이므로 오른쪽으로 한칸씩 이동하는 N^2의 방법은 좋지 않다고 생각했다
    처음은 세그먼트 트리로 접근해서 활성화된 idx번째 사람으로 자료구조를 만들어 제출하니 틀렸고,
    조금 생각해보니 당연히 틀려야했다 -> i번 보다 큰 수 중에서 안취한 가장 작은 i를 찾아야하기 때문이다!

    이진 트리 형태로 자료구조를 만들어 풀었다
    처음? 만들어보는 자료구조라 시간이 많이 걸렸다

    아이디어는 다음과 같다
    i번째 이후의 인덱스를 갖는 사람 중 안취한 사람을 찾아야 한다
    이진 트리 형태로 자료구조를 설정하고 취한 여부를 저장했다

    해당 사람이 이미 취한 사람이면 부모노드로 가고 
    자신이 해당 조상 노드의 왼쪽에 위치할 때까지 올라간다

    여기서 a가 b의 조상노드라 함은 a에서 부모 이거나
    a의 부모의 부모와 같이 부모를 찾아가면 결국에 b가 나올 때를 의미한다

    그리고, 오른쪽 노드가 활성화 되어져 있는지 확인한다
    활성화 되어있으면 오른쪽 자식 노드로 가고 탐색을 종료한다
    반면 오른쪽 노드가 비활성화 된 경우 부모노드로 가고 
    조상 노드의 왼쪽에 위치할 때까지 다시 반복한다
    오른쪽에 존재성을 보장하기 위해 트리 사이즈를 1 키우고, 가장 끝에 1을 강제로 할당했다

    이제 활성화된 오른쪽을 찾은 경우 가장 작은 인덱스로 가야하는데,
    왼쪽이 활성화 되어 있으면, 왼쪽으로 아니면 오른쪽으로 이동한다
    (부모가 활성화 되어 있으므로, 자식 중 적어도 하나는 활성화 보장!)

    해당 방법으로 찾고 만취하면 비활성화 시키고 진행하니 이상없이 통과했다
    시간 복잡도는 (N + M)logN 이다

    그런데, 다른 사람의 풀이를 보니 유니온 파인드로도 해결가능해 보인다
    해당 사람이 만취 -> 그러면 이전 사람과 유니온 한 뒤 점프 거리를 그룹 거리만큼 변경
    하는 식으로도 풀 수 있어 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0534
    {

        static void Main534(string[] args)
        {

            bool[] tree;
            int len;
            int add;

            StreamReader sr = new(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536);
            StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536);
            int n = ReadInt();
            int m = ReadInt();

            Solve();

            sr.Close();
            sw.Close();

            void Solve()
            {

                SetTree();
                for (int i = 1; i <= n + 1; i++)
                {

                    tree[i + add] = true;
                    Update(i);
                }

                int[] max = new int[n + 1];
                int[] cur = new int[n + 1];

                for (int i = 1; i <= n; i++)
                {

                    max[i] = ReadInt();
                }

                while (m-- > 0)
                {

                    int op = ReadInt();

                    if (op == 2)
                    {

                        int idx = ReadInt();
                        sw.WriteLine(cur[idx]);
                        continue;
                    }
                    else
                    {

                        int start = ReadInt();
                        int d = ReadInt();
                        
                        while(d > 0)
                        {

                            int s = GetIdx(start);
                            if (s == n + 1) break;

                            int drink = Math.Min(d, max[s] - cur[s]);
                            cur[s] += drink;
                            d -= drink;

                            if (max[s] - cur[s] != 0) continue;
                            tree[add + s] = false;
                            Update(s);
                        }
                    }
                }
            }

            void Update(int _n)
            {

                int idx = add + _n;
                while(idx > 0)
                {

                    int p = (idx - 1) / 2;
                    int l = 2 * p + 1;
                    int r = 2 * p + 2;

                    tree[p] = tree[l] || tree[r];
                    idx = p;
                }
            }

            int GetIdx(int _n)
            {

                int idx = add + _n;
                if (tree[idx]) return _n;
                idx = Up(idx);
                idx = Down(idx);

                return idx - add;
            }

            int Down(int _idx)
            {

                while (_idx * 2 + 2 < len)
                {

                    int l = _idx * 2 + 1;
                    int r = _idx * 2 + 2;

                    if (tree[l]) _idx = l;
                    else _idx = r;
                }

                return _idx;
            }

            int Up(int _idx)
            {

                while (IsRight(_idx))
                {

                    _idx = (_idx - 1) / 2;
                }

                return _idx + 1;
            }

            bool IsRight(int _idx)
            {

                int right = 2 * ((_idx - 1) / 2) + 2;
                return right == _idx || !tree[right];
            }

            void SetTree()
            {

                int log = (int)(Math.Ceiling(Math.Log2(1 + n))) + 1;
                len = 1 << log;
                add = (len / 2) - 2;
                tree = new bool[len];
            }

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
// #include <iostream>
using namespace std;

int N, Q;
int R[100002];
int A[100001];
int val[100001];

int findf(int x) {
	if (x == R[x]) return x;
	return R[x] = findf(R[x]);
}

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> N >> Q;
	for (int i = 1; i <= N + 1; i++) R[i] = i;
	for (int i = 1; i <= N; i++) cin >> A[i];

	while (Q--) {
		int q; cin >> q;
		if (q == 1) {
			int i, x; cin >> i >> x;
			while (i <= N && x) {
				int v = min(A[i] - val[i], x);
				val[i] += v, x -= v;
				if (A[i] == val[i]) {
					R[i] = i + 1;
					i = findf(i);
				}
			}
		}
		else {
			int i; cin >> i;
			cout << val[i] << '\n';
		}
	}
}
#elif other2
// #include <bits/stdc++.h>
// #define endl '\n'
// #define INF 987654321
// #define p_q priority_queue
// #define pbk push_back
// #define rep(i, a, b) for (int i=a; i<=b; i++) 
// #define all(v) (v).begin(), (v).end()

using namespace std;
using pii = pair<int, int>;
using ll = long long;
using ull = unsigned long long;
using vi = vector<int>;
using vull = vector<ull>;
using vvi = vector<vi>;
using vpii = vector<pii> ;
using vll = vector<ll>;
using mii = map<int, int>;
using si = set<int>;
using qi = queue<int>;
using qpii = queue<pii>;
using tiii = tuple<int, int, int> ; //get<0>(t);
using tlll = tuple<ll, ll, ll> ; //get<0>(t);
using vtiii = vector<tiii>;
using vtlll = vector<tlll>;
using pll = pair<ll, ll>;
using vpll = vector<pll>;
using spii = set<pii>;
using qtiii = queue<tiii>;
int A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z;
int dy[8] = {1,-1,0,0,1,1,-1,-1};
int dx[8] = {0,0,1,-1,1,-1,1,-1};
//vvi matrix(N, vector<int>(N));
//for finding the intersection of Line(x1,y1,x2,y2) and Line(x3,y3,x4,y4)
//do not solve with tenary search
// Px= (x1*y2 - y1*x2)*(x3-x4) - (x1-x2)*(x3*y4 - y3*x4)
// Py= (x1*y2 - y1*x2)*(y3-y4) - (y1-y2)*(x3*y4 - y3*x4)

//int to string : to_string
//string to int : stoi

//use setw(3) to get nice format for printing out 2-d array
//ex) cout << setw(3) << "a" << endl;

//to make a sorted vector's element unique, you should do v.erase(unique(v.begin(), v.end()), v.end())

// unordered_map<char,int> dx = {{'D',0},{'L',-1},{'R',1},{'U',0}};
// unordered_map<char,int> dy = {{'D',1},{'L',0},{'R',0},{'U',-1}}

//diagonal counting. l[y+x], r[y-x+N]

//supports negative modular operation
ll modular(ll num, ll mod) {
    num%=mod;
    return num < 0 ? num+mod : num;
}
void print(pii a) {
    cout << a.first << " " << a.second << endl;
}
template<typename T>
void print(T *a, int start, int end) {
    rep(i,start,end) cout << a[i] << " ";
    cout << endl;
}

template <typename T>
void print(const T& a) {
    cout << a << endl;
}

void print(const vector<pii>& v) {
    for(auto p : v) {
        print(p);
    }
    cout << endl;
}
template <typename T>
void print(const vector<T>& v) {
    for(auto i : v) cout << i << " ";
    cout << endl;
}
bool inRange(int y, int x) {
    return 1<=y && y<=N && 1<=x && x<=M;
}
bool inRangeN(int y, int x) {
    return 1<=y && y<=N && 1<=x && x<=N;
}
/*
vector<int> combination;
bool visited[1005];
void dfs(int idx, int cnt) { //implement with dfs(1, 0). N and K must be global variable
	if(cnt==K) {
		for(auto i : combination) {
			cout << i << " ";
		}
		cout << endl;
		return;
	}
	
	for(int i=idx;i<=N;i++) { //idx, N을 잘볼것
		if(visited[i]==1) continue;
		
		visited[i]=1;
		combination.push_back(i);
		dfs(i+1, cnt+1); //be careful with i+1, cnt+1
		visited[i]=0;
		combination.pop_back();
	}
}
*/

//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

// This is segment tree
/*
// #define MAX 100001
ll a[MAX], s[4*MAX];

ll merge(ll a, ll b) { //for operation of the segment tree
    return a+b;
}
ll segment(int node, int nodeLeft, int nodeRight) { // use when s, a is available and segment tree is about sum
    if (nodeLeft == nodeRight) {
        return s[node] = a[nodeLeft];
    }
	int mid = (nodeLeft+nodeRight)/2;
    return s[node] = merge(segment(node * 2, nodeLeft, mid), segment(node * 2 + 1, mid + 1, nodeRight));
}
void update(int node, int idx, int nodeLeft, int nodeRight, ll num) {
    if (idx < nodeLeft || nodeRight < idx) return;
    if (nodeLeft == nodeRight) {
        s[node] = num;
        return;
    }
    int mid = (nodeLeft+nodeRight)/2;
    update(node * 2, idx, nodeLeft, mid, num);
    update(node * 2 + 1, idx, mid + 1, nodeRight, num);

    s[node] = merge(s[node * 2], s[node * 2 + 1]);
}
ll query(int node, int l, int r, int nodeLeft, int nodeRight) { //l and r is the range.
    if (nodeRight < l || r < nodeLeft) return 0;
    if (l <= nodeLeft && nodeRight <= r) return s[node];
	int mid = (nodeLeft+nodeRight)/2;
    return merge(query(node * 2, l, r, nodeLeft, mid), query(node * 2 + 1, l, r, mid + 1, nodeRight));
}
void print(int node, int nodeLeft, int nodeRight) {
    cout << nodeLeft << " " << nodeRight << " : " << s[node] << endl;
    if(nodeLeft==nodeRight) return;

    int mid = (nodeLeft + nodeRight) >> 1;
    print(node*2, nodeLeft, mid);
    print(node*2+1, mid+1, nodeRight);
}
*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
/*
//This is lazy propagation. Beginning starts with segment(..) used in above
// #define MAX 100001
ll a[MAX], s[4*MAX];
ll merge(ll a, ll b) { //for operation of the segment tree
    return a+b;
}
ll segment(int node, int nodeLeft, int nodeRight) { // use when s, a is available and segment tree is about sum
    if (nodeLeft == nodeRight) {
        return s[node] = a[nodeLeft];
    }
	int mid = (nodeLeft+nodeRight)/2;
    return s[node] = merge(segment(node * 2, nodeLeft, mid), segment(node * 2 + 1, mid + 1, nodeRight));
}
ll lazy[4*MAX] {};
void propagation(int node, int l, int r) {
    if (lazy[node]) {
        s[node] += (r - l + 1) * lazy[node];
        if (l != r) {
            lazy[node * 2] += lazy[node];
            lazy[node * 2 + 1] += lazy[node];
        }
        lazy[node] = 0;
    }
}
void update(int node, int l, int r, int nodeLeft, int nodeRight, int dif) { //This is for lazy propagation
    propagation(node, nodeLeft, nodeRight);
    if (nodeRight < l || r < nodeLeft) return;
    if (l <= nodeLeft && nodeRight <= r) {
        s[node] += (nodeRight-nodeLeft + 1) * dif;
        if (nodeLeft != nodeRight) {
            lazy[node * 2] += dif;
            lazy[node * 2 + 1] += dif;
        }
        return;
    }
    int mid = (nodeLeft+nodeRight)/2;
    update(node * 2, l, r, nodeLeft, mid, dif);
    update(node * 2 + 1, l, r, mid + 1, nodeRight, dif);
    s[node] = merge(s[node * 2], s[node * 2 + 1]);
}
ll query(int node, int l, int r, int nodeLeft, int nodeRight) { //s should be vll
    propagation(node, nodeLeft, nodeRight);
    if (nodeRight < l || r < nodeLeft) return 0;
    if (l <= nodeLeft && nodeRight <= r) {
        return s[node];
    }
    int mid = (nodeLeft+nodeRight)/2;
    return merge(query(node * 2, l, r, nodeLeft, mid), query(node * 2+1, l, r, mid+1, nodeRight));
}

*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

//This is persistent segment tree (PST)

/*
struct Node {
    Node *l, *r;
    ll v;

    Node() {
        l = r = NULL;
        v = 0;
    }  
};

// #define MAX 1000
//When using MAX, root MAX and arr MAX is DIFFERENT!!!! KEEP IN MIND!!! 
Node* root[MAX];
int arr[MAX];

void build(Node *node, int nodeLeft, int nodeRight) {
    if(nodeLeft == nodeRight) {
        node->v = arr[nodeLeft];
        return;
    }
    int m = nodeLeft + (nodeRight-nodeLeft)/2;

    node->l = new Node();
    node->r = new Node();
   
    build(node->l, nodeLeft, m);
    build(node->r, m+1, nodeRight);

    node->v = node->l->v + node->r->v;
}

//doesn't update origin segment tree but updates new segment tree and connects it into a existing tree
void update(Node* prev, Node* now, int nodeLeft, int nodeRight, int idx, int value) { 
    if(nodeLeft == nodeRight) {
        now->v = value;
        return;
    }

    int middle = nodeLeft + (nodeRight-nodeLeft)/2;

    if(idx <= middle) { //update left node
        now->l = new Node(); now->r = prev->r;
        update(prev->l, now->l, nodeLeft, middle, idx, value);
    }
    else { //update right node
        now->l = prev->l; now->r = new Node();
        update(prev->r, now->r, middle+1, nodeRight, idx, value);
    }
    now->v = now->l->v + now->r->v;
}

ll query(Node *node, int nodeLeft, int nodeRight, int l, int r) { //want to know the addition of l~r
    if(nodeRight < l || r < nodeLeft) return 0;
    if(l <= nodeLeft && nodeRight <= r) return node->v;

    int middle = nodeLeft + (nodeRight-nodeLeft)/2;

    return query(node->l, nodeLeft, middle, l, r) + query(node->r, middle+1, nodeRight, l, r);
}

*/

/* could help if above update isn't appliable
Node* update(Node* now, int nodeLeft, int nodeRight, int idx, int value) {
    if (nodeRight < idx || idx < nodeLeft) return now;
    
    if (nodeLeft == nodeRight) {
        Node* leaf = new Node();
        leaf->v = now->v + value;
        return leaf;
    }

    int middle = nodeLeft + (nodeRight - nodeLeft) / 2;
    Node* leaf = new Node();
    leaf -> l = update(now->l, nodeLeft, middle, idx, value);
    leaf -> r = update(now->r, middle + 1, nodeRight, idx, value);


    leaf->v = leaf->l->v + leaf->r->v;
    return leaf;
}
*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

/*

//THIS IS FENWICK_TREE
//Fenwic_tree starts from index 1. 
// #define MAX 500001
int arr[MAX];
int fenwick[MAX];

//update function reflects the change of arr value, not the absolute value
//if arr value, say arr[3] changes into 3 to 5, then update(3,2) should be used.
//update(idx, c-arr[idx]); arr[idx] = c;
void update(int idx, int Value) { //For Making Fenwick Tree, for(int i=1~N) Update(i, arr[i]);
    while (idx <= N) {
        fenwick[idx] = fenwick[idx] + Value;
        idx = idx + (idx & -idx);
    }
}

int sum(int idx) { //IF 3~5 sum is required it should be sum(5)-sum(2);
    int result = 0; //BE CAREFUL ON RANGE (Long Long could be used)
    while (idx > 0) {
        result += fenwick[idx];
        idx = idx - (idx & -idx);
    }
    return result;
}

*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
/* 

//two dimensional fenwick tree
// #define MAX 1026
ll arr[MAX][MAX];
ll fenwick[MAX][MAX];

void update(int x, int y, ll value) {
    while(x < N+1) {
        int tempy = y;
        while(tempy < N+1) {
            fenwick[x][tempy] += value;
            tempy += (tempy & -tempy);
        }

        x += (x & -x);
    }
}

//sum(x,y) means sum of arr[1][1]~arr[x][y]
ll sum(int x, int y) {
    ll ret=0;
    while(x>0) {
        int tempy = y;
        while(tempy > 0) {  
            ret += fenwick[x][tempy];
            tempy -= (tempy & -tempy);
        }
        x -= (x & -x);
    }
    return ret;
}
*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

// THIS IS TARJAN ALGORITHM FOR SCC

/*
// #define MAX 10001 //when used in 2-sat, you have to double node because not only x1~x100 is required but also Nx1~Nx100
vvi SCC;
int d[MAX];
bool finished[MAX];
vi edge[MAX];
int id, SN; //mark sn[i]
stack<int> s;
int sn[MAX]; //sn[i] is SCC number to which it belongs to. If sn is big, then it is at the start of DAG. If small, it is at the end of DAG. If one wants to start from the beginning of DAG, start from the largest of sn.
int SCCnode[MAX] {}; //if SCCbfs is needed...
int nodeValue[MAX] {}; //if SCCbfs is needed...
int dfs(int x) {
    d[x] = ++id; //노드마다 고유한 아이디 부여
    s.push(x); //스택에 자기 자신을 삽입
    int parent = d[x];
    for (auto i : edge[x]) {
        if (d[i] == 0) { //방문 안 한 이웃
            parent = min(parent, dfs(i));
        }
        else if (finished[i] == 0) { //처리 중인 이웃
            parent = min(parent, d[i]);
        }
    }
    if (parent == d[x]) {
        vector<int> scc;
        while (true) {
            int t = s.top();
            s.pop();
            scc.push_back(t);
            finished[t] = 1;
            sn[t] = SN;
            //d[t] = x; //to make scc recognizable with d
            //SCCnode[SN] += nodeValue[t];
            if (t == x) break;
        }
        SN++;
        SCC.push_back(scc); //SCC의 SN번째 그래프랑 대응된다
    }
    return parent;
}

vi SCCedge[MAX]; //index refers to SN. Could be replaced with set if you don't want to overlap
int inDegree[MAX]; //index refers to SN
void SCCtopology_sort() {
    for(int i=1;i<=N;i++) { //id starts with 1
        for(auto next : edge[i]) {
            if(sn[next]!=sn[i]) {
                SCCedge[sn[i]].pbk(sn[next]); //SN could be overlapped. Could be solved with set but it is often not needed
                inDegree[sn[next]]+=1; //If inDegree is 0, then it is the start of the SCC graph. There could be many
            }
        }
    }
}

//If SCC sum is needed
int SCCdp[MAX] {};
void SCCbfs(int x) { //x is sn. bfs graph is not vertex graph but scc graph. scc graph is DAG so visited array is not needed
    //bfs starts with x. Function flows through SCC graph (topologically)
    qi q;
    q.push(x);
    SCCdp[x] = SCCnode[x];

    while(!q.empty()) {
        int cur = q.front();
        q.pop();
  
        for(auto next : SCCedge[cur]) {
            if(SCCdp[next] < SCCdp[cur] + nodeValue[next]) {
                SCCdp[next] = SCCdp[cur] + nodeValue[next];
                q.push(next);
            }
        }
    }
}

int oppo(int num) { //This is for 2-sat
    return num % 2 ? num + 1 : num - 1;
}

int result[MAX] {}; //For finding value of each clause (x1, x2, x3)
void sat2() { // (x1 or x2) and (Nx1 or x3) //Nx1->x2, Nx2->x1. x1->x3, Nx3->Nx1
    cin >> N >> M; // N is # of node, M is # of conditions
    for (int i = 0; i < M; i++) {
        int A, B; //If num is negative #, then it is (not) positive num
        cin >> A >> B; 
        A = A > 0 ? 2 * A - 1 : -2 * A; //positive num goes 1->1, 2->3, 3->5, 4->7, 5->9... and so on
        B = B > 0 ? 2 * B - 1 : -2 * B; //negative num goes -1->2, -2->4, -3->6, -4->8... and so on
        edge[oppo(A)].push_back(B);
        edge[oppo(B)].push_back(A);
    }
    for (int i = 1; i <= 2 * N; i++) { //node is 1~2*N
       if (d[i] == 0) dfs(i);
    }
    for (int i = 1; i <= N; i++) {
        if (sn[2 * i-1] == sn[2 * i]) {
            cout << "contradiction!" << " " << sn[i] << endl;
        }
    }
   
    //For finding out value of each clause

    memset(result, -1, sizeof(result));
    pii p[MAX];
    for(int i=1;i<=2*N;i++) {
        p[i] = {sn[i], i};
    }
    sort(p+1, p+2*N+1);

    for(int i=2*N;i>0;i--) { //Start from the beginning of DAG 
        int node = p[i].second;
        int src = (node+1)/2;
      
        if(result[src] == -1) result[src] = node%2 ? 0 : 1; 
    }

    for(int i=1;i<=N;i++) {
        cout << result[i] << " ";
    }
}

*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
// This is Dijkstra, Time Complexity O((V+E)logV)
/*
// #define MAX 100001
vpii edge[MAX]; // first is idx, second is weight of edge
int d[MAX];
struct cmp { //pii
    bool operator() (const pii& i, const pii& j) {
        return i.second > j.second;
    }
};
void Dijkstra(int num) {
    p_q<pii, vpii, cmp> pq;
    pq.push({ num, 0 });
    fill(d + 1, d + 1 + N, INF); //INF could be larger, varying from problem to problem
    d[num] = 0;
    while (!pq.empty()) {
        auto [cur, dis] = pq.top();
        pq.pop();
        if(d[cur]<dis) continue;
        for (auto next : edge[cur]) {
            if(d[next.first] > d[cur] + next.second) {
                d[next.first] = d[cur]+next.second;
                pq.push({next.first, d[next.first]});
            }
        }
    }
    for(int i = 1; i <= N; i++) {
        cout << d[i] << " ";
    }
    cout << endl;
}
*/

//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////
//This is SPFA 
/*
// #define MAX 100001
bool inQ[MAX] {};
vpii edge[MAX];
int d[MAX];
int cycle[MAX] {};
void SPFA(int start) {
    fill(d+1, d+1+N, INF);
    qi q;
    d[start] =0;
    q.push(start);
    inQ[start] = 1;
    cycle[start] += 1;
    while(!q.empty()) {
        int cur = q.front();
        q.pop();
        inQ[cur] = 0;
        for(auto next : edge[cur]) {
            if(d[next.first] > d[cur] + next.second) {
                d[next.first] = d[cur] + next.second;
                if(!inQ[next.first]) {
                    cycle[next.first] += 1;
                    if(cycle[next.first]>=N) {
                        cout << "CYCLE!!!!" << endl;
                        return;
                    }
                    q.push(next.first);
                    inQ[next.first] = 1;
                }
            }
        }
    }
}
*/

// This is Floyd-Warshall
/*
// #define MAX 501
int dp[MAX][MAX]; //input should be done in dp table
void floyd_warshall() {
    rep(i, 1, N) {
        rep(j, 1, N) if (dp[i][j] == 0) dp[i][j] = INF;
    }
    rep(k, 1, N) {
        rep(i, 1, N) {
            rep(j, 1, N) {
                if (dp[i][j] > dp[i][k] + dp[k][j]) dp[i][j] = dp[i][k] + dp[k][j];
            }
        }
    }
}
*/
//////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////

// This is Union Find, DSU
// #define MAX 100002
int parent[MAX]; //parent[MAX] should be 1, 2, 3...
int getParent(int num) {
    if (parent[num] == num) return num;
    // int p = getParent(parent[num]);
    // dist[num] += dist[parent[num]];
    return parent[num] = getParent(parent[num]);
}
void merge(int a, int b) {
    a = getParent(a);
    b = getParent(b);

    if(a>b) parent[b] = a;
    else parent[a] = b;
}

bool isSameParent(int a, int b) {
    return getParent(a) == getParent(b);
}

int m[MAX] {};
int drink[MAX] {};
void Solve() {
    cin >> N >> Q;
    rep(i,1,N) cin >> m[i];

    iota(parent+1,parent+N+2, 1);
    while(Q--) {
        int op, i, x; cin >> op;
        if(op==1) {
            cin >> i >> x;
            int p = i;
            while(p<=N && x>0) {
                p = getParent(p);
                if(p>N) break;
                int rest = m[p] - drink[p];
                
                if(x>=rest) {
                    drink[p] = m[p];
                    x -= rest;
                    p++;
                }
                else {
                    drink[p] += x;
                    x=0;
                    break;
                }
            }
            merge(i, p);
        }
        else {
            cin >> i;
            cout << drink[i] << endl;
        }
    }
}
int main() {
	ios::sync_with_stdio(false);
    cin.tie(NULL);
    cout.tie(NULL);
    Solve();
}
#elif other3
/ /#include <bits/stdc++.h>
// #define fi first
// #define se second
using namespace std;
typedef long long ll;
typedef pair<ll,ll> pll;
ll tr[100005],n,ar[100005],br[100005];
set<ll>st;
void update(ll x,ll a)
{
    for(;x<=n;x+=x&-x) tr[x]+=a;
}
ll sum(ll x)
{
    ll a=0;
    for(;x>0;x-=x&-x) a+=tr[x];
    return a;
}
int main()
{
    ios::sync_with_stdio(0); cin.tie(0);
    ll q,t,x,i;
    cin>>n>>q;
    for(i=1;i<=n;i++) cin>>ar[i], update(i,ar[i]), st.insert(i), br[i]=ar[i];
    while(q--)
    {
        cin>>t;
        if(t==1)
        {
            cin>>i>>x;
            ll l=i, r=n+1, m;
            while(l<r)
            {
                m=(l+r)/2;
                if(sum(m)-sum(i-1)>=x)
                    r=m;
                else l=m+1;
            }
            ll a=sum(l-1)-sum(i-1);
            auto it=st.lower_bound(i);
            while(it!=st.end()&&(*it)<l)
            {
                update(*it,-ar[*it]);
                ar[*it]=0;
                it=st.erase(it);
            }
            if(l!=n+1)
            {
                update(l,a-x);
                ar[l]-=x-a;
            }
        }
        else
        {
            cin>>i;
            cout<<br[i]-ar[i]<<'\n';
        }
    }
    return 0;
}

#elif other4
import sys
input = sys.stdin.readline

def find(i):
    if link[i] == i: return i
    link[i] = find(link[i])
    return link[i]

N,Q = map(int,input().split())
link = [0] + [*range(1,N+2)]
pos = [0] + [int(input()) for _ in range(N)] + [0]
ans = [0]*(N+1)

for _ in range(Q):
    n,*o = map(int,input().split())
    
    if n == 1:
        i,x = o 
        
        while True:
            i = find(i)
            
            if i > N: break
            if pos[i] - x <= 0:
                ans[i] += pos[i]
                x -= pos[i]
                link[i] = i+1
                i += 1
                
                if not x: break
            else:
                ans[i] += x
                pos[i] -= x
                break
            
    else: print(ans[o[0]])
#endif
}
