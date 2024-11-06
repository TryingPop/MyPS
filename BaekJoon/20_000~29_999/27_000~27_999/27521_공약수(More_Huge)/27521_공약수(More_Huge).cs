using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 20
이름 : 배성훈
내용 : 공약수(More Huge)
    문제번호 : 27521번
    https://www.acmicpc.net/problem/27521 

    //////////////////////////////////////////////////////////////////////////
    많아야 17개 소수
    2 * 3 * 5 * 7 * 11 * 13 * 17 * 19 * 23 * 29 * 31 = 614,889,782,588,491,410
    일단 쓰는 메모를 보고 알고리즘을 확인했다;

    세 수의 합부터 풀고 이번 달? 안에 풀 수 있게 노력해보자!

    일단 도전! -> 시간 초과다
    -> 실상 Meet In Middle 아이디어도 안썼다;

    4%에서 시간초과다

    해당 아이디어를 참고해서 해봐야겠다
    https://ji-gwang.tistory.com/369

    힌트가 있었다.
    https://www.acmicpc.net/category/detail/3519
    보고 조만간 다시 해봐야겠다!

    힌트보고 드디어 해결했다! - > 2024. 4. 20
    중간에서 만나기 아이디어가 잘못된줄 알았으나, 예제를 보니 정렬이 안돼서 틀렸었다;
    ///////////////////////////////////////////////////////////////////////////

    수학, 정수론, 두 포인터, 중간에서 만나기 문제다

    아이디어는 다음과 같다
    먼저 소인수들을 서로소가 되게 소수 제곱으로 분류해야한다
    예를들어 소인수로 2, 2, 2, 3, 5를 입력 받은 경우
        8 (=2^3), 3, 5로 묶는다
    이를 계산하기 위해 딕셔너리 자료구조를 이용했다
    앞에서 10^18승에서는 많아야 17개의 서로다른 소수가 가능하다
    그래서 미리 크기를 20으로 잡았다 -> 17로 줄여도되고, 다른 방법이 있다면, 다른방법을 써도 된다

    다음으로 gcd와 lcm의 정의로 lcm / gcd로 나눠서 봤다
    이제 나눈 소인수에대해 나올 수 있는 곱의 경우들을 모두 찾는다
    그러면, 2^16까지 간다

    이는 50000개 까지 쿼리가 들어오기에 50_000 * 2^16 >= 30억 으로 시간초과가 난다
    실제로 4%에서 컷당한다

    이를 해결하기 위해 중간에서 만나기 아이디어를 쓴다
    그러면 (2^8 + 2^8) * 50_000 < 2000만으로 확 줄일 수 있다
    이제 중간에서 만나기를 어떻게 적용하는가가 중요하다

    중간에서 만나기는 정렬된 소인수 제곱들을 반으로 나누면 된다
    그리고 해당 소인수들로 곱해서 만들 수 있는 수들을 모두 찾고 정렬한다
    -> 많아야 2^8 시간이 소모된다

    이후에, 반으로 나눈 그룹을 투 포인터로 진행하면서
    작은 그룹은 작은부터 올라오는 식으로, 큰 쪽은 큰값에서 내려오는 식으로 두 포인터를 적용한다
    이제 두 포인터를 옮기는 법은 곱해서 나온 수, 남은 소인수들을 곱해서 나온 수
    크기를 비교해서 곱해서 나온 수가 큰 경우 왼쪽 값을 올리던, 오른쪽 값을 내리던 하나를 선택하면 된다
    여기서는 오른쪽 값을 줄여 곱해서 나온수가 작도록? 유지할려고 했다
    이렇게 두 포인터를 적용하면서 매번 크기를 비교해 합이 최소가 될 때를 찾아주면 된다
    해당 결과를 제출하니 580ms에 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0100
    {

        static void Main100(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()), bufferSize: 65536 * 16);

            int test = ReadInt();
            StringBuilder sb = new StringBuilder(20 * test);
            Dictionary<long, long> lcmDic = new Dictionary<long, long>(20);
            Dictionary<long, long> gcdDic = new Dictionary<long, long>(20);
            long[] calc = new long[20];
            long[] ret = new long[2];
            long mul;
            List<long> left = new(1 << 10);
            List<long> right = new(1 << 10);

            Solve();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }

            void Solve()
            {

                while (test-- > 0)
                {

                    int len = ReadInt();
                    long gcd = 1;
                    gcdDic[1] = 1;
                    for (int i = 0; i < len; i++)
                    {

                        long cur = ReadLong();
                        gcd *= cur;

                        if (gcdDic.ContainsKey(cur)) gcdDic[cur] *= cur;
                        else gcdDic[cur] = cur;
                    }

                    len = ReadInt();
                    long lcm = 1;
                    lcmDic[1] = 1;
                    for (int i = 0; i < len; i++)
                    {

                        long cur = ReadLong();
                        lcm *= cur;

                        if (lcmDic.ContainsKey(cur)) lcmDic[cur] *= cur;
                        else lcmDic[cur] = cur;
                    }

                    foreach (var key in gcdDic.Keys)
                    {

                        lcmDic[key] /= gcdDic[key];
                    }

                    int idx = 0;
                    foreach (var key in lcmDic.Keys)
                    {

                        calc[idx++] = lcmDic[key];
                    }

                    lcmDic.Clear();
                    gcdDic.Clear();

                    mul = lcm / gcd;

                    Array.Sort(calc, 0, idx);
                    // MITM 알고리즘!
                    left.Add(1);
                    right.Add(1);

                    int mid = idx / 2 + 1;
                    SetList(left, 1, mid);
                    SetList(right, mid, idx);

                    left.Sort();
                    right.Sort();

                    ret[0] = 1;
                    ret[1] = mul;

                    FindRet();

                    if (ret[0] > ret[1]) sb.Append($"{gcd * ret[1]} {gcd * ret[0]}\n");
                    else sb.Append($"{gcd * ret[0]} {gcd * ret[1]}\n");
                    left.Clear();
                    right.Clear();
                }
            }
            void SetList(List<long> _fill, int _from, int _to)
            {

                if (_from == _to) return;
                
                int cnt = _fill.Count;
                for (int i = 0; i < cnt; i++)
                {

                    _fill.Add(_fill[i] * calc[_from]);
                }

                SetList(_fill, _from + 1, _to);
            }
            void FindRet()
            {

                int l = 0;
                int r = right.Count - 1;
                while (l < left.Count && r >= 0)
                {

                    long ret1 = left[l] * right[r];
                    long ret2 = mul / ret1;

                    if (ret1 < ret2) l++;
                    else r--;

                    long calc1 = ret[0] + ret[1];
                    long calc2 = ret1 + ret2;
                    if (calc1 < calc2) continue;
                    ret[0] = ret1;
                    ret[1] = ret2;
                }

                /*
                while (l < left.Count)
                {

                    long ret1 = left[l] * right[0];
                    long ret2 = mul / ret1;

                    l++;

                    long calc1 = ret[0] + ret[1];
                    long calc2 = ret1 + ret2;
                    if (calc1 < calc2) continue;
                    ret[0] = ret1;
                    ret[1] = ret2;
                }

                l--;
                while(r >= 0)
                {

                    long ret1 = left[l] * right[r];
                    long ret2 = mul / ret1;

                    r--;

                    long calc1 = ret[0] + ret[1];
                    long calc2 = ret1 + ret2;
                    if (calc1 < calc2) continue;
                    ret[0] = ret1;
                    ret[1] = ret2;
                }
                */
            }

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;

                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
            long ReadLong()
            {

                int c;
                long ret = 0;

                while ((c = sr.Read()) != -1 && c != '\n' && c != ' ')
                {

                    if (c == '\r') continue;

                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
// #include<bits/stdc++.h>
using namespace std;
using ll=long long;

constexpr int SZ = 1 << 20;

class INPUT {
private:
    char read_buf[SZ];
    int read_idx, next_idx;
    bool __END_FLAG__, __GETLINE_FLAG__;
public:
    explicit operator bool() { return !__END_FLAG__; }
    bool IsBlank(char c) { return c == ' ' || c == '\n'; }
    bool IsEnd(char c) { return c == '\0'; }
    char _ReadChar() {
        if (read_idx == next_idx) {
            next_idx = fread(read_buf, sizeof(char), SZ, stdin);
            if (next_idx == 0) return 0;
            read_idx = 0;
        }
        return read_buf[read_idx++];
    }
    char ReadChar() {
        char ret = _ReadChar();
        for (; IsBlank(ret); ret = _ReadChar());
        return ret;
    }
    template<typename T> T ReadInt() {
        T ret = 0; char cur = _ReadChar(); bool flag = 0;
        for (; IsBlank(cur); cur = _ReadChar());
        if (cur == '-') flag = 1, cur = _ReadChar();
        for (; !IsBlank(cur) && !IsEnd(cur); cur = _ReadChar()) ret = 10 * ret + (cur & 15);
        if (IsEnd(cur)) __END_FLAG__ = 1;
        return flag ? -ret : ret;
    }
    string ReadString() {
        string ret; char cur = _ReadChar();
        for (; IsBlank(cur); cur = _ReadChar());
        for (; !IsBlank(cur) && !IsEnd(cur); cur = _ReadChar()) ret.push_back(cur);
        if (IsEnd(cur)) __END_FLAG__ = 1;
        return ret;
    }
    double ReadDouble() {
        string ret = ReadString();
        return stod(ret);
    }
    string getline() {
        string ret; char cur = _ReadChar();
        for (; cur != '\n' && !IsEnd(cur); cur = _ReadChar()) ret.push_back(cur);
        if (__GETLINE_FLAG__) __END_FLAG__ = 1;
        if (IsEnd(cur)) __GETLINE_FLAG__ = 1;
        return ret;
    }
    friend INPUT& getline(INPUT& in, string& s) { s = in.getline(); return in; }
} _in;

class OUTPUT {
private:
    char write_buf[SZ];
    int write_idx;
public:
    ~OUTPUT() { Flush(); }
    explicit operator bool() { return 1; }
    void Flush() {
        fwrite(write_buf, sizeof(char), write_idx, stdout);
        write_idx = 0;
    }
    void WriteChar(char c) {
        if (write_idx == SZ) Flush();
        write_buf[write_idx++] = c;
    }
    template<typename T> int GetSize(T n) {
        int ret = 1;
        for (n = n >= 0 ? n : -n; n >= 10; n /= 10) ret++;
        return ret;
    }
    template<typename T> void WriteInt(T n) {
        int sz = GetSize(n);
        if (write_idx + sz >= SZ) Flush();
        if (n < 0) write_buf[write_idx++] = '-', n = -n;
        for (int i = sz; i-- > 0; n /= 10) write_buf[write_idx + i] = n % 10 | 48;
        write_idx += sz;
    }
    void WriteString(string s) { for (auto& c : s) WriteChar(c); }
    void WriteDouble(double d) { WriteString(to_string(d)); }
} _out;

/* operators */
INPUT& operator>> (INPUT& in, char& i) { i = in.ReadChar(); return in; }
INPUT& operator>> (INPUT& in, string& i) { i = in.ReadString(); return in; }
template<typename T, typename std::enable_if_t<is_arithmetic_v<T>>* = nullptr>
INPUT& operator>> (INPUT& in, T& i) {
    if constexpr (is_floating_point_v<T>) i = in.ReadDouble();
    else if constexpr (is_integral_v<T>) i = in.ReadInt<T>(); return in; }

OUTPUT& operator<< (OUTPUT& out, char i) { out.WriteChar(i); return out; }
OUTPUT& operator<< (OUTPUT& out, string i) { out.WriteString(i); return out; }
template<typename T, typename std::enable_if_t<is_arithmetic_v<T>>* = nullptr>
OUTPUT& operator<< (OUTPUT& out, T i) {
    if constexpr (is_floating_point_v<T>) out.WriteDouble(i);
    else if constexpr (is_integral_v<T>) out.WriteInt<T>(i); return out; }

/* macros */
// #define fastio 1
// #define cin _in
// #define cout _out
// #define istream INPUT
// #define ostream OUTPUT

void subsetprod_s(vector<ll>&a,vector<ll>&res)
{
    for(ll&v:a)
    {
        int ts=size(res);
        for(int i=0;i<ts;i++)res.push_back(res[i]*v);
        inplace_merge(begin(res),begin(res)+ts,end(res));
    }
}
// usage: subsetprod(arr,0,1,res);
 
void solve(vector<ll>&G,vector<ll>&L)
{
    sort(begin(G),end(G));
    sort(begin(L),end(L));
    
    if(G==L)
    {
        ll g=1;
        for(ll&i:G)g*=i;
        cout<<g<<" "<<g<<"\n";
        return;
    }
    vector<ll>fac;
    set_difference(begin(L),end(L),begin(G),end(G),back_inserter(fac));

    ll vf=1;
    for(ll&i:fac)vf*=i;
    
    vector<ll>v;
    v.push_back(fac[0]);
    for(int i=1;i<size(fac);i++)
    {
        if(v.back()%fac[i]==0)v.back()*=fac[i];
        else v.push_back(fac[i]);
    }
    ll n=size(v);
    vector<ll>H1(begin(v),begin(v)+n/2);
    vector<ll>H2(begin(v)+n/2,end(v));
    vector<ll>S1{1},S2{1};
    subsetprod_s(H1,S1);subsetprod_s(H2,S2);
    reverse(begin(S2),end(S2));
    ll rt=sqrtl(vf)+5;while(rt*rt>vf)rt--;
    int i1=0,i2=0;
    ll mx=1e18,ma,mb;
    while(i1<size(S1)&&i2<size(S2))
    {
        ll curp=S1[i1]*S2[i2];
        if(curp>rt)i2++;
        else
        {
            if(curp+vf/curp<mx)
            {
                mx=curp+vf/curp;
                ma=curp;mb=vf/curp;
            }
            i1++;
        }
    }

    ll g=1;
    for(ll&i:G)g*=i;
    
    ma*=g;mb*=g;
    cout<<ma<<" "<<mb<<"\n";
}
 
int main()
{
    int q;cin>>q;
    while(q--)
    {
        ll a;cin>>a;
        vector<ll>G(a);
        for(ll&i:G)cin>>i;
        ll b;cin>>b;
        vector<ll>L(b);
        for(ll&i:L)cin>>i;
        solve(G,L);
    }
}
#elif other2
def ext(g,l):
    g.pop(0);l.pop(0);g.sort();l.sort();t,L=1,[]
    for i in range(len(l)):
        if not len(g):L+=[l[i]];continue
        if g[0]==l[i]:g.pop(0);continue
        L+=[l[i]]
    if not(len(L)):return []
    A,c=[],L[0]
    for i in range(len(L)-1):
        if L[i]==L[i+1]:c*=L[i]
        else: A+=[c];c=L[i+1]
    return A+[c]
def div(A):
    D=[1]
    for i in A:D+=[i*j for j in D];D=list(set(D))
    D.sort()
    return D
def sol(g,l):
    G=1
    if len(g)>1:
        for i in g:G*=i
        G//=g[0]
    F=ext(g,l)
    F1,F2=div(F[0:len(F)//2]),div(F[len(F)//2::])
    M,c=F1[-1]*F2[-1],-1;x=1
    for i in F1:
        if F2[c]==1 and i*i>M: break
        while (i*F2[c])**2>M and F2[c]!=1:c-=1
        if (i*F2[c])**2<=M:x=max(x,i*F2[c])
    return [G*x,G*M//x]
import sys
for i in range(int(input())):
    a=list(map(int,sys.stdin.readline().split()))
    b=list(map(int,sys.stdin.readline().split()))
    print(*sol(a,b))

#elif other3
#endif
}
