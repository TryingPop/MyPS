using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 10
이름 : 배성훈
내용 : 금광
    문제번호 : 10167번

    세그먼트 트리, 스위핑, 좌표 압축 문제다
    아이디어는 다음과 같다

    전체 최대값은 바로 찾기 힘드니,
    아랫 변을 고정시켜 놓고
    윗변은 아랫 변에서 한칸씩 위로 올라간다
    점이 있다면 윗변의 값을 아주 작은 값으로 옮려 점보다 위에 있게한다 (실수의 완비성으로 올릴 수 있다)
    그러면 윗변과 아랫변 사이에 영역이 생기는데,
    x구간에서의 w의 합이 최대가 되는 fixedW를 찾는다

    윗변이 가능한 높이 만큼 다 올라가서 더 이상 탐색할 수 없을 때
    아랫변을 한칸 올린다

    그리고 앞과 같이 반복한다
    이렇게 찾은 fixedW들의 최대값이 전체의 최대값이 된다

    그림으로 보면

            o           o

                                o

        o       o       o

    이렇게 점이 있을 때



    맨 밑에 밑 변을 선택한다

            o           o

                                o

        o       o       o

        --------------------------  밑변

    그리고 밑 변에서 한칸씩 위로 올라간다
            o           o

                                o

        o       o       o
        --------------------------  윗변
        --------------------------  밑변
        구간에 포함되는 점 없다


            o           o

                                o
        --------------------------  윗변
        o       o       o
        
        --------------------------  밑변      
    구간에 포함되는 점이 있다
    해당 점들의 x구간 중 w가 최대가 되게 찾는다
    
            o           o

                                o
        --------------------------  윗변
        o      (o       o)
        
        --------------------------  밑변          
    찾은 구간이 소괄호라하자
    그리고 최대인 fixedW값을 저장
    
    이후 다시 윗 변을 한 칸 올린다
            o           o
        --------------------------  윗변
                                o
        
        o       o       o 
        
        --------------------------  밑변          
    여기서 다시 최대 값을 찾는다

    
            o           o
        --------------------------  윗변
                       (        o)
                       (         )  
        o       o      (o        )
        
        --------------------------  밑변  
    찾은 구간이 다음과 같이 변형될 수 있다
    그리고 여기서 찾은 fixedW값을 이전 fixedW보다 큰지 확인한다
    큰 경우 해당 값을 기록한다

    다음으로 윗 변을 다시 올린다
        --------------------------  윗변
            o           o

                                o
                                   
        o       o       o        
        
        --------------------------  밑변  
    그리고 연속합의 최대값을 찾는다
    여기서 

        --------------------------  윗변
            o           o
                        |   
                        |       o
                        |          
        o       o       o        
        
        --------------------------  밑변  
    해당 x값이 같은 경우 더해줘 최대값을 찾는다


        --------------------------  윗변
           (o)          o
           ( )
           ( )                  o
           ( )                     
        o  ( )  o       o        
        
        --------------------------  밑변     
    해당 구간이 될 수 있다
    여기서 fixedW의 최대값이 갱신되었는지 확인한다

    윗변이 모든 점들 보다 위에 있으니 이제 밑 변을 올린다

        
            o           o

                                o

        --------------------------  밑변                                     
        o       o       o        
        
    그리고 밑변에서 한칸 위에 윗변을 다시 둬서 앞과 같은 탐색을 한다
            o           o

                                o
        --------------------------  윗변
        --------------------------  밑변                                     
        o       o       o        


            o           o
        --------------------------  윗변
                                o

        --------------------------  밑변                                     
        o       o       o        
    ... 이렇게 진행해서

        --------------------------  밑변                                     
            o           o

                                o

        o       o       o        
    밑 변위에 점이 없을 때까지 진행하고
    여기까지 온 fixedW의 최대값이 정답이 된다

    이 밑변 윗변 아이디어를 못떠올려 검색을 했다
    해당 방법으로 접근하면 됨을 아니 앞의 연속합 테크닉?으로 풀었다
    시간은 1.3초 걸렸다 최적화 잘만하면 1초내에 풀릴거 같다
*/

namespace BaekJoon._57
{
    internal class _57_05
    {

        static void Main5(string[] args)
        {

            long INF = -1_000_000_001L;
            (long lMax, long rMax, long tMax, long sum) ZERO = (INF, INF, INF, 0);
            StreamReader sr;
            int n;
            (int x, int y, int w)[] gold;
            (long lMax, long rMax, long tMax, long sum)[] seg;

            Solve();

            void Solve()
            {

                Input();

                CompactAndSort();

                SetSeg();

                Console.Write(GetRet());
            }

            void SetSeg()
            {

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (long lMax, long rMax, long tMax, long sum)[1 << log];

                Array.Fill(seg, ZERO);
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {
                    if (seg[_idx] == ZERO) seg[_idx] = (_val, _val, _val, _val);
                    else
                    {

                        seg[_idx].lMax += _val;
                        seg[_idx].rMax += _val;
                        seg[_idx].tMax += _val;
                        seg[_idx].sum += _val;
                    }
                    return;
                }

                int mid = (_s + _e) >> 1;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                if (mid < _chk) Update(mid + 1, _e, _chk, _val, r);
                else Update(_s, mid, _chk, _val, l);

                seg[_idx].sum = seg[l].sum + seg[r].sum;
                seg[_idx].lMax = Math.Max(seg[l].lMax, seg[l].sum + seg[r].lMax);
                seg[_idx].rMax = Math.Max(seg[r].rMax, seg[r].sum + seg[l].rMax);

                seg[_idx].tMax = Math.Max(seg[l].tMax, seg[r].tMax);
                seg[_idx].tMax = Math.Max(seg[_idx].tMax, seg[l].rMax + seg[r].lMax);
            }

            long GetRet()
            {

                int START = 0;
                int END = n - 1;

                int bBot = -1;
                long ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (bBot == gold[i].y) continue;

                    int before = gold[i].y;
                    for (int j = i; j < n; j++)
                    {

                        if (before != gold[j].y) 
                        { 

                            ret = Math.Max(ret, seg[0].tMax);
                            before = gold[j].y;
                        }

                        Update(START, END, gold[j].x, gold[j].w);
                    }

                    ret = Math.Max(ret, seg[0].tMax);
                    bBot = gold[i].y;

                    Array.Fill(seg, ZERO);
                }

                return ret;
            }

            void CompactAndSort()
            {

                Array.Sort(gold, (x, y) => x.x.CompareTo(y.x));

                int idx = 0;
                int before = gold[0].x;
                for (int i = 0; i < n; i++)
                {

                    if (gold[i].x == before) gold[i].x = idx;
                    else
                    {

                        before = gold[i].x;
                        gold[i].x = ++idx;
                    }
                }

                Array.Sort(gold, (x, y) => x.y.CompareTo(y.y));

                idx = 0;
                before = gold[0].y;
                for (int i = 0; i < n; i++)
                {

                    if (gold[i].y == before) gold[i].y = idx;
                    else
                    {

                        before = gold[i].y;
                        gold[i].y = ++idx;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                gold = new (int x, int y, int w)[n];

                for (int i = 0; i < n; i++)
                {

                    gold[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();

                bool plus = c != '-';
                int ret = plus ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

#nullable disable

public record struct GoldMineSegNode(long LeftStartMax, long RightStartMax, long Max, long Sum);
public sealed class GoldMineSeg : GenericSeg<GoldMineSegNode>
{
    public GoldMineSeg(int n)
        : base(n)
    {
    }

    protected override GoldMineSegNode Merge(GoldMineSegNode lhs, GoldMineSegNode rhs)
    {
        return new GoldMineSegNode(
            Math.Max(lhs.LeftStartMax, lhs.Sum + rhs.LeftStartMax),
            Math.Max(rhs.RightStartMax, rhs.Sum + lhs.RightStartMax),
            Math.Max(Math.Max(lhs.Max, rhs.Max), lhs.RightStartMax + rhs.LeftStartMax),
            lhs.Sum + rhs.Sum);
    }
}

public abstract class GenericSeg<T>
    where T : struct
{
    private int _leafMask;
    private T?[] _tree;

    public GenericSeg(int n)
    {
        _leafMask = (int)BitOperations.RoundUpToPowerOf2((uint)n);
        _tree = new T?[2 * _leafMask];
    }

    public T Top()
    {
        return _tree[1].Value;
    }

    public void Clear()
    {
        Array.Clear(_tree);
    }
    public void Set(int idx, T val)
    {
        var pos = _leafMask | idx;
        _tree[pos] = val;
        pos /= 2;

        while (pos != 0)
        {
            _tree[pos] = SafeMerge(_tree[2 * pos], _tree[2 * pos + 1]);
            pos /= 2;
        }
    }
    
    public T Range(int stIncl, int edExcl)
    {
        var q = new Queue<(int pos, int stIncl, int edExcl)>();
        q.Enqueue((1, 0, _leafMask));

        var targets = new List<(int stIncl, int pos)>();
        while (q.TryDequeue(out var s))
        {
            if (s.edExcl <= stIncl || edExcl <= s.stIncl)
                continue;

            if (stIncl <= s.stIncl && s.edExcl <= edExcl)
            {
                targets.Add((s.stIncl, s.pos));
                continue;
            }

            var mid = (s.stIncl + s.edExcl) / 2;
            q.Enqueue((2 * s.pos, s.stIncl, mid));
            q.Enqueue((2 * s.pos + 1, mid, s.edExcl));
        }

        return targets
            .OrderBy(p => p.stIncl)
            .Select(p => _tree[p.pos])
            .Aggregate(SafeMerge)
            .Value;
    }

    private T? SafeMerge(T? lhs, T? rhs)
    {
        if (!lhs.HasValue)
            return rhs;

        if (!rhs.HasValue)
            return lhs;

        return Merge(lhs.Value, rhs.Value);
    }

    protected abstract T Merge(T lhs, T rhs);
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var points = new (int x, int y, int w)[n];

        for (var idx = 0; idx < n; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            points[idx] = (l[0], l[1], l[2]);
        }

        var xcomp = points.Select(v => v.x).Distinct().OrderBy(v => v).Select((v, idx) => (v, idx)).ToDictionary(p => p.v, p => p.idx);
        var ycomp = points.Select(v => v.y).Distinct().OrderBy(v => v).Select((v, idx) => (v, idx)).ToDictionary(p => p.v, p => p.idx);

        for (var idx = 0; idx < n; idx++)
        {
            var p = points[idx];
            points[idx] = (xcomp[p.x], ycomp[p.y], p.w);
        }

        var arr = new GoldMineSegNode[xcomp.Count];
        var seg = new GoldMineSeg(arr.Length);

        var groupByYCoord = points.GroupBy(v => v.y).ToDictionary(g => g.Key, g => g.ToArray());
        var max = 0L;

        for (var styIncl = 0; styIncl < ycomp.Count; styIncl++)
        {
            Array.Clear(arr);
            seg.Clear();

            for (var edyIncl = styIncl; edyIncl < ycomp.Count; edyIncl++)
            {
                foreach (var p in groupByYCoord[edyIncl])
                {
                    var prev = arr[p.x];
                    arr[p.x] = new GoldMineSegNode(prev.LeftStartMax + p.w, prev.RightStartMax + p.w, prev.Max + p.w, prev.Sum + p.w);
                    seg.Set(p.x, arr[p.x]);
                }

                max = Math.Max(max, seg.Top().Max);
            }
        }

        sw.WriteLine(max);
    }
}

#elif other2
// #include <bits/stdc++.h>
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0)
using namespace std;

const int SZ = 1 << 20;
char read_buf[SZ];

namespace IO {
    int read_idx, next_idx, write_idx;
	inline bool is_blank(char c) { return c == ' ' || c == '\n' || c == '\t' || c == '\v' || c == '\f' || c == '\r'; }
	inline bool is_end(char c) { return c == '\0'; }
	inline char readChar() {
		if (read_idx == next_idx) {
			next_idx = fread(read_buf, sizeof(char), SZ, stdin);
			if (next_idx == 0) return 0;
			read_idx = 0;
		}
		return read_buf[read_idx++];
	}
	inline long long readLL() {
		long long ret = 0;
		char cur = readChar();
		bool flag = 0;
		while (is_blank(cur)) cur = readChar();
		if (cur == '-') {
			flag = 1;
			cur = readChar();
		}
		while (!is_blank(cur) && !is_end(cur)) {
			ret = 10 * ret + cur - '0';
			cur = readChar();
		}
		return flag ? -ret : ret;
	}
}
using namespace IO;

typedef long long ll;
const ll INF = ll(1e18);

struct SegTree {
	struct Node {
		ll sum, l, r, mx;
	};
	Node f(Node a, Node b) {
		Node ret;
		ret.sum = a.sum + b.sum;
		ret.l = max(a.l, a.sum + b.l);
		ret.r = max(a.r + b.sum, b.r);
		ret.mx = max({ a.mx, b.mx, a.r + b.l });
		return ret;
	}
	Node tree[1 << 13];
	int sz = 1 << 12;

	void init() { memset(tree, 0, sizeof(tree)); }
	void update(int i, int val) {
		i |= sz; tree[i].sum = tree[i].l = tree[i].r = tree[i].mx += val;
		while (i >>= 1) {
			tree[i] = f(tree[i << 1], tree[i << 1 | 1]);
		}
	}
}ST;

struct p {
	ll x, y, w;
	bool operator <(const p& i) const {
		if (y != i.y) return y < i.y;
		return x < i.x;
	}
};

ll n, ans;
vector<p> v;
void precalc() {
	vector<ll> xx, yy;
	for (int i = 0; i < n; i++) {
        v[i].x = readLL(), v[i].y = readLL(), v[i].w = readLL();
		xx.push_back(v[i].x);
		yy.push_back(v[i].y);
	}
	sort(xx.begin(), xx.end()); sort(yy.begin(), yy.end());
	xx.erase(unique(xx.begin(), xx.end()), xx.end());
	yy.erase(unique(yy.begin(), yy.end()), yy.end());
	for (auto& [x, y, w] : v) {
		x = lower_bound(xx.begin(), xx.end(), x) - xx.begin();
		y = lower_bound(yy.begin(), yy.end(), y) - yy.begin();
	}
	sort(v.begin(), v.end());
}

int main() {
	fastio;
	n = readLL(); v.resize(n);
    precalc();
	for (int i = 0; i < n; i++) {
		if (i && v[i - 1].y == v[i].y) continue;
		ST.init();
		for (int j = i; j < n; j++) {
			ST.update(v[j].x, v[j].w);
			if (j == n - 1 || v[j].y != v[j + 1].y) ans = max(ans, ST.tree[1].mx);
		}
	}
	cout << ans << '\n';
}
#elif other3
// #include <cstdio>
// #include <cstring>
// #include <algorithm>
// #include <vector>
using namespace std;

struct maxsum{
    int lim;
    long long tmax[8200], left[8200], right[8200], tree[8200];
    void init(int n){
        memset(tree,0,sizeof(tree));
        memset(tmax,0,sizeof(tmax));
        memset(left,0,sizeof(left));
        memset(right,0,sizeof(right));
        for(lim = 1; lim <= n; lim <<= 1);
    }
    void add(int x, long long v){
        x += lim;
        tree[x] += v;
        tmax[x] = right[x] = left[x] = max(tree[x],0ll);
        while(x > 1){
            x >>= 1;
            tmax[x] = max(tmax[2*x],tmax[2*x+1]);
            tmax[x] = max(right[2*x] + left[2*x+1],tmax[x]);
            left[x] = max(left[2*x],tree[2*x] + left[2*x+1]);
            right[x] = max(right[2*x+1],tree[2*x+1] + right[2*x]);
            tree[x] = tree[2*x] + tree[2*x+1];
        }
    }
    long long q(){return tmax[1];}
}seg;

int n;
struct pt{int x,y,c;}a[3005];
bool cmp(pt a, pt b){return a.x < b.x;}

vector<int> vx,vy;

int main(){
    scanf("%d",&n);
    for (int i=0; i<n; i++) {
        scanf("%d %d %d",&a[i].x,&a[i].y,&a[i].c);
        vx.push_back(a[i].x);
        vy.push_back(a[i].y);
    }
    vx.push_back(-1);
    vy.push_back(-1);
    sort(vx.begin(),vx.end());
    sort(vy.begin(),vy.end());
    vx.resize(unique(vx.begin(),vx.end()) - vx.begin());
    vy.resize(unique(vy.begin(),vy.end()) - vy.begin());
    for (int i=0; i<n; i++) {
        a[i].x = (int)(lower_bound(vx.begin(),vx.end(),a[i].x) - vx.begin());
        a[i].y = (int)(lower_bound(vy.begin(),vy.end(),a[i].y) - vy.begin());
    }
    sort(a,a+n,cmp);
    long long ret = 0;
    for (int i=0; i<n; i++) {
        seg.init((int)vy.size());
        int e = i;
        while(a[e].x == a[i].x) e++;
        for (int j=i; j<n; j++) {
            int e = j;
            while(a[e].x == a[j].x) e++;
            for (int k=j; k<e; k++) {
                seg.add(a[k].y,a[k].c);
            }
            ret = max(ret,seg.q());
            j = e-1;
        }
        i = e-1;
    }
    printf("%lld",ret);
}
#endif
}
