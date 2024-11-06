using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 8
이름 : 배성훈
내용 : 하늘에서 떨어지는 1, 2, ..., R-L+1개의 별
    문제번호 : 17353번

    느리게 갱신되는 세그먼트 트리 문제다
    느리게 갱신되는 세그먼트 트리에 누적된 값 시작과 간격을 저장해 풀었다

    8개의 점에 현재 떨어진 별의 개수가 모두 0이라 하자
    그러면 1번점부터 8번점까지 나열해보면
        0 0 0 0 0 0 0 0
    와 같다

    그리고 2 ~ 7번까지 별이 떨어지면
        0 1 2 3 4 5 6 0 
    과 같다

    이후 3 ~ 6번에 별이 떨어진다고 보면
        0 1 3 5 7 9 6 0
    과 같다

    범위로 업데이트가 이뤄지기에 느리게 갱신되는 세그먼트 트리 자료구조가 적당해 보인다
    1, 2, 3, 4, ..., k로 1씩 증가하면서 내리는 양이 결정된다
    만약 4개짜리 구간에 1 2 3 4를 더해야하고, 3 4 5 6을 추가로 더해야하는 경우라면
    결과적으로 4 6 8 10 으로 더해줘야한다

    이를 보면 시작값과 간격을 저장하는게 좋아보인다
    이렇게 제출하니 시작 부분 계산을 잘못해 3번 틀리고 이후에는 이상없이 통과했다
*/

namespace BaekJoon._57
{
    internal class _57_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            (long val, long lazy, int n)[] seg;

            Solve();
            void Solve()
            {

                Input();

                GetRet();

                sr.Close();
                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                n = ReadInt();

                int log = 1 + (int)Math.Ceiling(Math.Log2(n));
                seg = new (long val, long lazy, int n)[1 << log];

                for (int i = 1; i <= n; i++)
                {

                    int val = ReadInt();
                    Update(1, n, i, i, val);
                }
            }

            void GetRet()
            {

                int len = ReadInt();

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int f = ReadInt();

                    if (op == 1)
                    {

                        int b = ReadInt();
                        Update(1, n, f, b, 1);
                    }
                    else
                    {

                        long ret = GetVal(1, n, f);
                        sw.Write($"{ret}\n");
                    }
                }
            }

            long GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_s == _e) return seg[_idx].val;

                int mid = (_s + _e) >> 1;
                long ret;
                if (mid < _chk) ret = GetVal(mid + 1, _e, _chk, _idx * 2 + 2);
                else ret = GetVal(_s, mid, _chk, _idx * 2 + 1);

                return ret;
            }

            void Update(int _s, int _e, int _chkS, int _chkE, long _val, int _idx = 0)
            {

                LazyUpdate(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE)
                {

                    // 시작값 확인해서 넣는다
                    long add = _s - _chkS;
                    seg[_idx].lazy += _val + add;
                    seg[_idx].n++;
                    return;
                }
                else if (_chkE < _s || _e < _chkS) return;

                int mid = (_s + _e) >> 1;

                Update(_s, mid, _chkS, _chkE, _val, _idx * 2 + 1);
                Update(mid + 1, _e, _chkS, _chkE, _val, _idx * 2 + 2);
            }

            void LazyUpdate(int _s, int _e, int _idx)
            {

                long lazy = seg[_idx].lazy;
                if (lazy == 0) return;
                seg[_idx].lazy = 0;
                int cnt = seg[_idx].n;
                seg[_idx].n = 0;

                if (_s == _e)
                {

                    seg[_idx].val += lazy;
                    return;
                }

                seg[_idx * 2 + 1].lazy += lazy;
                seg[_idx * 2 + 1].n += cnt;

                // 증가 값만큼 시작값 변경해 추가
                int mid = (_s + _e) >> 1;
                long add = (mid - _s + 1);
                add *= cnt;

                seg[_idx * 2 + 2].lazy += lazy + add;
                seg[_idx * 2 + 2].n += cnt;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
//17353
StreamReader sr = new StreamReader(Console.OpenStandardInput());
StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
long len = long.Parse(sr.ReadLine()!) - 1, questCount;
long[] order;
long[] nums = Array.ConvertAll(sr.ReadLine()!.Split(), long.Parse), tree = new long[nums.Length * 4];
(long first, long add)[] lazy = new (long, long)[tree.Length];
Init(0, len);
questCount = long.Parse(sr.ReadLine()!);
for (long i = questCount; i > 0; i--)
{
    order = Array.ConvertAll(sr.ReadLine()!.Split(), long.Parse);
    switch (order[0])
    {
        default:
            ChangeValue(0, len, order[1] - 1, order[2] - 1);
            break;
        case 2:
            sw.WriteLine(FindValue(0, len, order[1] - 1, order[1] - 1));
            break;
    }
    //PrlongTree();
}
//Console.WriteLine(string.Join(",", tree));
//Console.WriteLine(string.Join(",", lazy));
sr.Close();
sw.Close();
void Propagate(long left, long right, long node)
{
    if (lazy[node].first != 0)
    {
        tree[node] += Sigma(lazy[node].first, right - left + 1, lazy[node].add);
        if (left != right)
        {
            lazy[node * 2].first += lazy[node].first;
            lazy[node * 2].add += lazy[node].add;
            lazy[node * 2 + 1].first += lazy[node].first + ((left + right) / 2 - left + 1) + (lazy[node].add - 1) * ((left + right) / 2 - left + 1);
            lazy[node * 2 + 1].add += lazy[node].add;
        }
    }
    lazy[node].first = 0;
    lazy[node].add = 0;
}
void ChangeValue(long left, long right, long rangeLeft, long rangeRight, long node = 1)
{
    if (node >= lazy.Length || right < rangeLeft || rangeRight < left)//포함 x
        return;
    if (left >= rangeLeft && right <= rangeRight)//현재 범위가 전부 포함
    { lazy[node].first += left - rangeLeft + 1; ++lazy[node].add; }
    else//일부 포함
    {
        long mid = left + (right - left) / 2;
        if (left <= rangeLeft)
        {
            if (right >= rangeRight)//더하는 범위가 현재 범위 안에 있음
                tree[node] += Sigma(1, rangeRight - rangeLeft + 1);
            else//더하는 범위의 왼쪽만 현재 범위 안에 있음
                tree[node] += Sigma(1, right - rangeLeft + 1);
        }
        else//더하는 범위의 오른쪽만 현재 범위 안에 있음
            tree[node] += Sigma(left - rangeLeft + 1, rangeRight - left + 1);

        ChangeValue(left, mid, rangeLeft, rangeRight, node * 2);
        ChangeValue(mid + 1, right, rangeLeft, rangeRight, node * 2 + 1);
    }
}
long Sigma(long startNum, long len, long add = 1) => startNum * len + add * (len - 1) * len / 2;
long FindValue(long left, long right, long rangeLeft, long rangeRight, long node = 1)
{
    if (right < rangeLeft || left > rangeRight) return 0;
    Propagate(left, right, node);
    if (left >= rangeLeft && right <= rangeRight) return tree[node];
    long mid = left + (right - left) / 2;
    return FindValue(left, mid, rangeLeft, rangeRight, node * 2)
        + FindValue(mid + 1, right, rangeLeft, rangeRight, node * 2 + 1);
}
long Init(long left, long right, long node = 1)
{
    if (left == right) tree[node] = nums[left];
    else tree[node] = Init(left, left + (right - left) / 2, node * 2) + Init(left + (right - left) / 2 + 1, right, node * 2 + 1);
    return tree[node];
}
#elif other2
// #include <cstdio>
// #include <unistd.h>
constexpr int MAX=1e5+1, SZ=1<<16;
int main() {
	char I[SZ], *c=I; syscall(0,0,I,SZ);
	auto f=[&]{if(c>=I+SZ-16){char*p=I;do*p++=*c++;while(c!=I+SZ);syscall(0,0,p,I+SZ-p);c=I;}int x=0;do{x=x*10+*c-'0';}while(*++c>='0');++c;return x;};
	int A[MAX], Y[MAX]={}; long X[MAX]={};
	int N=f();
	for(int i=1; i<=N; i++) A[i]=f();
	int Q=f();
	while(Q--) {
		int x=f();
		if(x==1) {
			int l=f(), r=f();
			for(int i=l; i<=N; i+=i&-i) X[i]-=l-1, Y[i]++;
			for(int i=r+1; i<=N; i+=i&-i) X[i]+=l-1, Y[i]--;
		} else {
			x=f();
			long a=A[x], b=0;
			for(int i=x; i>0; i-=i&-i) a+=X[i], b+=Y[i];
			printf("%ld\n", a+b*x);
		}
	}
}
#elif other3
// #include <bits/stdc++.h>
using namespace std;

using ll = long long;

const int LM = 1e5 + 4;

int N,Q;
int arr[LM];

ll tree1[LM], tree2[LM];
void update(ll tree[], int i, ll v){
    while(i<=N){
        tree[i] += v;
        i+=i&-i;
    }
}
ll get(int i){
    ll a = 0, b = 0;
    for(int idx=i;idx>0;idx-=idx&-idx){
        a += tree1[idx];
        b += tree2[idx];
    }
    return a*i+b;
}

int main(){
    ios_base::sync_with_stdio(false); cin.tie(NULL);
    int T=1;while(T--){
    	cin>>N;
    	for(int i=1;i<=N;i++) cin>>arr[i];
    	cin>>Q;
    	while(Q--){
            int a,b,c;
            cin>>a;
            if(a == 1){
                cin>>b>>c;
                update(tree1, b, +1);
                update(tree2, b, 1-b);
                if(c^N){
                    update(tree1, c+1, -1);
                    update(tree2, c+1, b-1);
                }
            }
            else if(a == 2){
                cin>>b;
                cout << arr[b] + get(b) << '\n';
            }
    	}
	}
    return 0;
}
#endif
}
