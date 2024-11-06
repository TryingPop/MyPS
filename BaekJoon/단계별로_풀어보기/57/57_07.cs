using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 12
이름 : 배성훈
내용 : 벽
    문제번호 : 10070번

    느리게 갱신되는 세그먼트 트리 문제다
    UpdateDown에서 분할할 때, UpdateDown 2개로 분할해야 하는데
    UpdateDown, UpdateUp이렇게 해서 쓸모없이 LazyUpdate부분만 많이 수정했다

    처음에는 더하기와 빼기 순서를 고려가 필요한 줄 알았다
    그래서 type 변수를 뒀는데, 다시 고민해보니 필요 없어보였고 없이 제출해도 통과했다;

    아이디어는 다음과 같다
    올리는 연산과 빼는 연산 각각에 lazy를 적용한다
    둘 다 -1으로 연산이 없다면 갱신하고

    올리는 연산의 경우 이전에 저장된 값보다 높은 경우 갱신한다
    5층 올리기에서 7층 올리는건 결국 7층 올리라는 말과 같다

    그리고 내리는 연산의 경우는 이전 값보다 작은 경우 갱신한다
    6층 내리기에서 3층으로 내리라는건 결국 3층으로 내리라는 것과 같다
    
    그리고 순차적으로 진행하기에
    3층까지 내린 뒤에 5층까지 올리는 연산을 보면,
    3층 이하인 경우 5층으로 간다
    3층 이상인 경우 3층으로 갔다가 5층으로 간다

    즉 모든 층에서 5층으로 맞춰야한다
    그래서 내리는 연산 뒤에 올리는 연산이 오고,
    내리는 값이 올리는 값보다 작은 경우
    내리는 값을 올리는 값으로 맞춰준다

    또한 5층까지 올리고 3층으로 내려간다고 보자
    5층 이상인 경우 3층으로 내려간다
    5층 이하인 경우 5층까지 왔다가 3층까지 내려간다

    그래서 올리기 연산 뒤에 내리기 연산이 오고,
    올리는 값이 내리는 값보다 큰 경우
    올리는 값을 내리는 값으로 맞춘다

    한 층에 있던 상황을 똑같이 구간에 적용하면 된다

    그리고 이렇게 조절할 경우
    항상 올리는 연산 값은 내리는 연산 값보다 작거나 같기 때문에
    올리는 연산을 먼저하던, 내리는 연산을 먼저하던 상관없다
*/

namespace BaekJoon._57
{
    internal class _57_07
    {

        static void Main7(string[] args)
        {

#if !second
            int START;
            int END;
            int NONE = -1;

            StreamReader sr;
            StreamWriter sw;

            int n, k;
            (int val, int u, int d)[] seg;

            Solve();

            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int i = 0;

                while (i < n)
                {

                    for (int j = 0; j < 1_000 && i < n; j++, i++)
                    {

                        sw.Write($"{GetVal(START, END, i)}\n");
                    }

                    sw.Flush();
                }

                sw.Close();
            }

            void GetRet()
            {

                k = ReadInt();
                START = 0;
                END = n - 1;
                
                while (k-- > 0)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();
                    int val = ReadInt();
                    if (op == 1) UpdateUp(START, END, f, t, val);
                    else UpdateDown(START, END, f, t, val);
                }

                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int val, int u, int d)[1 << log];

                Array.Fill(seg, (0, NONE, NONE));
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int u = seg[_idx].u;
                int d = seg[_idx].d;

                if (u == NONE && d == NONE) return;

                seg[_idx].u = NONE;
                seg[_idx].d = NONE;

                if (_s == _e)
                {

                    if (d != NONE) seg[_idx].val = Math.Min(seg[_idx].val, d);
                    if (u != NONE) seg[_idx].val = Math.Max(seg[_idx].val, u);
                    return;
                }

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                if (d != NONE)
                {

                    if (d < seg[l].u) seg[l].u = d;
                    if (d < seg[r].u) seg[r].u = d;

                    if (seg[l].d == NONE || d < seg[l].d)
                    {
                            
                        seg[l].d = d;
                    }

                    if (seg[r].d == NONE || d < seg[r].d)
                    {

                        seg[r].d = d;
                    }
                }

                if (u != NONE)
                {

                    if (seg[l].d != NONE && seg[l].d < u) seg[l].d = u;
                    if (seg[r].d != NONE && seg[r].d < u) seg[r].d = u;

                    if (seg[l].u < u)
                    {

                        seg[l].u = u;
                    }

                    if (seg[r].u < u)
                    {

                        seg[r].u = u;
                    }
                }
            }

            void UpdateUp(int _s, int _e, int _chkS, int _chkE, int _up, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                if (_chkS <= _s && _e <= _chkE)
                {

                    if (seg[_idx].d != NONE && seg[_idx].d < _up) seg[_idx].d = _up;
                    if (seg[_idx].u < _up) seg[_idx].u = _up;

                    return;
                }

                int mid = (_s + _e) >> 1;

                UpdateUp(_s, mid, _chkS, _chkE, _up, l);
                UpdateUp(mid + 1, _e, _chkS, _chkE, _up, r);
            }

            void UpdateDown(int _s, int _e, int _chkS, int _chkE, int _down, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                if (_chkS <= _s && _e <= _chkE)
                {

                    if (_down < seg[_idx].u) seg[_idx].u = _down;
                    if (seg[_idx].d == NONE || _down < seg[_idx].d) seg[_idx].d = _down;

                    return;
                }

                int mid = (_s + _e) >> 1;

                UpdateDown(_s, mid, _chkS, _chkE, _down, l);
                UpdateDown(mid + 1, _e, _chkS, _chkE, _down, r);
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_s == _e) return seg[_idx].val;

                int mid = (_s + _e) >> 1;

                if (mid < _chk) return GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
                return GetVal(_s, mid, _chk, _idx * 2 + 1);
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
#else
            int START;
            int END;
            int NONE = -1;

            StreamReader sr;
            StreamWriter sw;

            int n, k;
            (int val, int u, int d, int type)[] seg;

            Solve();

            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int i = 0;

                while (i < n)
                {

                    for (int j = 0; j < 1_000 && i < n; j++, i++)
                    {

                        sw.Write($"{GetVal(START, END, i)}\n");
                    }

                    sw.Flush();
                }

                sw.Close();
            }

            void GetRet()
            {

                k = ReadInt();
                START = 0;
                END = n - 1;
                
                while (k-- > 0)
                {

                    int op = ReadInt();
                    int f = ReadInt();
                    int t = ReadInt();
                    int val = ReadInt();
                    if (op == 1) UpdateUp(START, END, f, t, val);
                    else UpdateDown(START, END, f, t, val);
                }

                sr.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                int log = (int)Math.Ceiling(Math.Log2(n)) + 1;
                seg = new (int val, int u, int d, int type)[1 << log];

                Array.Fill(seg, (0, NONE, NONE, 0));
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                int type = seg[_idx].type;
                if (type == 0) return;

                int u = seg[_idx].u;
                int d = seg[_idx].d;

                seg[_idx].type = 0;
                seg[_idx].u = NONE;
                seg[_idx].d = NONE;

                if (_s == _e)
                {

                    if (type == 1)
                    {

                        if (d != NONE) seg[_idx].val = Math.Min(seg[_idx].val, d);
                        seg[_idx].val = Math.Max(seg[_idx].val, u);
                    }
                    else
                    {

                        if (u != NONE) seg[_idx].val = Math.Max(seg[_idx].val, u);
                        seg[_idx].val = Math.Min(seg[_idx].val, d);
                    }
                    return;
                }

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                if (type == 1)
                {

                    if (d != NONE)
                    {

                        if (d < seg[l].u) seg[l].u = d;
                        if (d < seg[r].u) seg[r].u = d;

                        if (seg[l].d == NONE || d < seg[l].d)
                        {
                            
                            seg[l].d = d;
                            seg[l].type = 2;
                        }

                        if (seg[r].d == NONE || d < seg[r].d)
                        {

                            seg[r].d = d;
                            seg[r].type = 2;
                        }
                    }

                    if (seg[l].d != NONE && seg[l].d < u) seg[l].d = u;
                    if (seg[r].d != NONE && seg[r].d < u) seg[r].d = u;

                    if (seg[l].u < u)
                    { 
                        
                        seg[l].u = u; 
                        seg[l].type = 1;
                    }

                    if (seg[r].u < u)
                    {

                        seg[r].u = u;
                        seg[r].type = 1;
                    }
                }
                else
                {

                    if (u != NONE)
                    {

                        if (seg[l].d != NONE && seg[l].d < u) seg[l].d = u;
                        if (seg[r].d != NONE && seg[r].d < u) seg[r].d = u;

                        if (seg[l].u < u)
                        {

                            seg[l].u = u;
                            seg[l].type = 1;
                        }

                        if (seg[r].u < u)
                        {

                            seg[r].u = u;
                            seg[r].type = 1;
                        }
                    }

                    if (d < seg[l].u) seg[l].u = d;
                    if (d < seg[r].u) seg[r].u = d;

                    if (seg[l].d == NONE || d < seg[l].d) 
                    { 
                        
                        seg[l].d = d; 
                        seg[l].type = 2; 
                    }

                    if (seg[r].d == NONE || d < seg[r].d)
                    {

                        seg[r].d = d;
                        seg[r].type = 2;
                    }
                }
            }

            void UpdateUp(int _s, int _e, int _chkS, int _chkE, int _up, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                if (_chkS <= _s && _e <= _chkE)
                {

                    if (seg[_idx].d != NONE && seg[_idx].d < _up)
                    {
                     
                        seg[_idx].d = _up;
                    }

                    if (seg[_idx].u < _up)
                    {

                        seg[_idx].u = _up;
                        seg[_idx].type = 1;
                    }
                    return;
                }

                int mid = (_s + _e) >> 1;

                UpdateUp(_s, mid, _chkS, _chkE, _up, l);
                UpdateUp(mid + 1, _e, _chkS, _chkE, _up, r);
            }

            void UpdateDown(int _s, int _e, int _chkS, int _chkE, int _down, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_e < _chkS || _chkE < _s) return;

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;

                if (_chkS <= _s && _e <= _chkE)
                {

                    if (_down < seg[_idx].u)
                    {

                        seg[_idx].u = _down;
                    }
                    
                    if (seg[_idx].d == NONE || _down < seg[_idx].d)
                    {

                        seg[_idx].d = _down;
                        seg[_idx].type = 2;
                    }

                    return;
                }

                int mid = (_s + _e) >> 1;

                UpdateDown(_s, mid, _chkS, _chkE, _down, l);
                UpdateDown(mid + 1, _e, _chkS, _chkE, _down, r);
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_s == _e) return seg[_idx].val;

                int mid = (_s + _e) >> 1;

                if (mid < _chk) return GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
                return GetVal(_s, mid, _chk, _idx * 2 + 1);
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
#endif
        }
    }

#if other
// #pragma GCC optimize("Ofast")
// #include<bits/stdc++.h>
using namespace std;
using ll = long long;
// #define dbg(x) cerr<<#x<<"= "<<x<<'\n';
namespace IN{
	const int SIZ=1<<20;
	char buf[SIZ+1],*p=buf+SIZ;
	inline bool isblank(char c){return c=='\n'||c==' '||c=='\t'||c=='\r'||c==0;}
	inline char read(){
		if(p==buf+SIZ) buf[fread(buf,1,SIZ,stdin)]=0, p=buf;
		return *p++;
	}
	inline void scan(char&c){do c=read(); while(isblank(c));}
	template<typename T=int>
	inline T geti(){
		char c; scan(c);
		T res=0; bool f=1;
		if(c=='-') f=0, c=read();
		while(c>='0'&&c<='9') res=res*10+(c&15), c=read();
		return f?res:-res;
	}
	inline void scan(string&s){
		s.clear(); char c; scan(c);
		while(!isblank(c)) s.push_back(c), c=read();
	}
	template<typename T> inline void scan(T&n){ n = geti<T>(); }
	template<typename T, typename... Args> inline void scan(T&n, Args&...args){
		scan(n); scan(args...);
	}
}
using namespace IN;
namespace OUT{
	const int SIZ=1<<20;
	char buf[SIZ+1],*p=buf,tmp[21];
	inline void flush(){fwrite(buf,1,p-buf,stdout); p=buf;}
	inline void mark(char c){
		if(p==buf+SIZ) flush();
		*p++=c;
	}
	inline void mark(const string&s,char sep = '\n'){
		for(char c:s) mark(c);
		if(sep) mark(sep);
	}
	template<typename T> inline void mark(T ans,char sep = '\n'){
		if(ans<0) mark('-'),ans*=-1;
		int cnt=0;
		do tmp[cnt++]=(ans%10)|48, ans/=10; while(ans>0);
		for(;cnt--;) mark(tmp[cnt]);
		if(sep) mark(sep);
	}
	template<typename T> inline void mark(const vector<T>&v){
		for(auto k:v) mark(k,' ');
		mark('\n');
	}
	struct ff{ ~ff(){flush();}}flu;
}
using namespace OUT;

const int inf = 1e9;

struct Node{
    int Min, Max;
    Node(int a=0,int b=inf):Min(a),Max(b){}
    void apply(const Node&x){
        int t;
        if(Min < x.Min) Min = x.Min, t=0;
        if(Max > x.Max) Max = x.Max, t=1;
        if(Min > Max){
            if(t==0) Max = Min;
            else Min = Max;
        }
    }
    inline bool empty(){return Min==0&&Max==inf;}
};
struct Seg{
    int st=1; vector<Node> tree;
    Seg(int k=1){
        while(st<k) st<<=1;
        tree=vector<Node>(st*2);
    }
    inline void apply(int nd,const Node&v){
        tree[nd].apply(v);
    }
    inline void push(int nd){
        apply(nd<<1,tree[nd]); apply(nd<<1|1,tree[nd]);
        tree[nd] = Node();
    }
    //md=0 : range update, md=1: query
    Node qry(int nd,int s,int e,int l,int r,int md,Node v=Node()){
        Node res;
        if(l <= s && e <= r){
            if(md==0) apply(nd,v);
            else res = tree[nd];
        }else if(s <= r && l <= e){
            if(!tree[nd].empty()) push(nd);
            Node L = qry(nd<<1,s,s+e>>1,l,r,md,v), R = qry(nd<<1|1,s+e+2>>1,e,l,r,md,v);
            if(md==1) res.apply(L), res.apply(R);
        }
        return md==0?tree[nd]:res;
    }
    void dfs(int nd,int s,int e){
        if(s==e)
            mark(tree[nd].Min);
        else{
            push(nd);
            dfs(nd<<1,s,s+e>>1); dfs(nd<<1|1,s+e+2>>1,e);
        }
    }
};

int main(){
    int n,m; scan(n,m);
    Seg tree(n); Node x;
    while(m--){
        int t,a,b,h; scan(t,a,b,h);
        if(t==1) x = Node(h,inf);
        else x = Node(0,h);
        tree.qry(1,0,n-1,a,b,0,x);
    }
    tree.dfs(1,0,n-1);
	return 0;
}
#elif other2
import java.io.*;
import java.util.*;

public class Main {
    static Reader r = new Reader();
    static StringBuilder sb = new StringBuilder();

    public static void main(String args[]) throws Exception {
        int n = r.readInt(), k = r.readInt();
        SegTree seg = new SegTree(n);

        while(k-->0){
            int q = r.readInt(), a = r.readInt(), b = r.readInt(), h = r.readInt();
            if(q==1) seg.add(a,b,0,n-1,1,h);
            else seg.sub(a,b,0,n-1,1,h);
        }
        seg.update_all(0,n-1,1);
        for(int x:seg.a)
            sb.append(x).append("\n");
        System.out.println(sb);
    }
}

class SegTree{
    private final int M = 100001;
    public int[] a, UB, LB;
    public SegTree(int n){
        a = new int[n];
        UB = new int[4*n];
        LB = new int[4*n];
        Arrays.fill(UB,M);
    }

    public void add(int s, int e, int l, int r, int node, int h){
        if(l<r) update_lazy(node);
        if(l>e || r<s) return;
        if(s<=l && r<=e){
            if(LB[node]<h) LB[node] = h;
            if(UB[node]<h) UB[node] = h;
            return;
        }
        int mid = (l+r)>>1;
        add(s,e,l,mid,node*2,h);
        add(s,e,mid+1,r,node*2+1,h);
    }
    public void sub(int s, int e, int l, int r, int node, int h){
        if(l<r) update_lazy(node);
        if(l>e || r<s) return;
        if(s<=l && r<=e){
            if(UB[node]>h) UB[node] = h;
            if(LB[node]>h) LB[node] = h;
            return;
        }
        int mid = (l+r)>>1;
        sub(s,e,l,mid,node*2,h);
        sub(s,e,mid+1,r,node*2+1,h);
    }

    public void update_all(int l, int r, int node){
        if(l<r) update_lazy(node);
        if(l==r){
            a[l] = Math.min(Math.max(a[l],LB[node]),UB[node]);
            return;
        }
        int mid = (l+r)>>1;
        node<<=1;
        update_all(l,mid,node);
        update_all(mid+1,r,node+1);
    }
    public void update_lazy(int node){
        LB[node*2] = Math.min(Math.max(LB[node],LB[node*2]),UB[node]);
        UB[node*2] = Math.max(Math.min(UB[node],UB[node*2]),LB[node]);
        LB[node*2+1] = Math.min(Math.max(LB[node],LB[node*2+1]),UB[node]);
        UB[node*2+1] = Math.max(Math.min(UB[node],UB[node*2+1]),LB[node]);
        LB[node] = 0; UB[node] = M;
    }
}

class Reader {
    final private int BUFFER_SIZE = 1 << 16;
    private DataInputStream din;
    private byte[] buffer;
    private int bufferPointer, bytesRead;

    public Reader() {
        din = new DataInputStream(System.in);
        buffer = new byte[BUFFER_SIZE];
        bufferPointer = bytesRead = 0;
    }
    public int readInt() throws IOException {
        int ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    public String readLine() throws IOException {
        byte[] buf = new byte[10000005]; // line length
        int cnt = 0, c;
        while((c=read())!=-1){
            if(c=='\n'){
                if(cnt!=0) break;
                else continue;
            }
            buf[cnt++] = (byte)c;
        }
        return new String(buf, 0, cnt);
    }

    public long readLong() throws IOException {
        long ret = 0;
        byte c = read();
        while(c <= ' '){ c = read();}
        boolean neg = (c == '-');
        if(neg) c = read();
        do{
            ret = (ret<<3) + (ret<<1) + c - '0';
        } while ((c = read()) >= '0' && c <= '9');
        return neg ? -ret : ret;
    }

    private void fillBuffer() throws IOException {
        bytesRead = din.read(buffer, bufferPointer = 0, BUFFER_SIZE);
        if(bytesRead == -1) buffer[0] = -1;
    }

    private byte read() throws IOException {
        if(bufferPointer == bytesRead) fillBuffer();
        return buffer[bufferPointer++];
    }

    public void close() throws IOException {
        if(din==null) return;
        din.close();
    }
}
#elif other3
import sys
input=sys.stdin.readline
F=lambda:[*map(int,input().split())]
INF=1<<60

N,Q=F()
MN=[INF]*(2*N)
MX=[-INF]*(2*N)

def prop(p):
  par=p>>1
  mn=MN[par]; mx=MX[par]
  MN[par]=INF; MX[par]=-INF
  MN[p]=max(min(MN[p],mn),mx)
  MX[p]=max(min(MX[p],mn),mx)
  MN[p^1]=max(min(MN[p^1],mn),mx)
  MX[p^1]=max(min(MX[p^1],mn),mx)
def push(p:int):
  n=p.bit_length()
  for i in range(n-1,-1,-1):
    prop(p>>i)
def upd_mn(l,r,v):
  l+=N; r+=N
  push(l); push(r)
  while l<=r:
    if l&1==1:
      MN[l]=min(MN[l],v)
      MX[l]=min(MX[l],v)
    if r&1==0:
      MN[r]=min(MN[r],v)
      MX[r]=min(MX[r],v)
    l=l+1>>1; r=r-1>>1
def upd_mx(l,r,v):
  l+=N; r+=N
  push(l); push(r)
  while l<=r:
    if l&1==1:
      MN[l]=max(MN[l],v)
      MX[l]=max(MX[l],v)
    if r&1==0:
      MN[r]=max(MN[r],v)
      MX[r]=max(MX[r],v)
    l=l+1>>1; r=r-1>>1

for qry in range(Q):
  q,l,r,v=F()
  if q==1:upd_mx(l,r,v)
  else:upd_mn(l,r,v)

for i in range(1,2*N):prop(i)
for i in range(N):print(max(MX[N+i],min(MN[N+i],0)))
#endif
}
