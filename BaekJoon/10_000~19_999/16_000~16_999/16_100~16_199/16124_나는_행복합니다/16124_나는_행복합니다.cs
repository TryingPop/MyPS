using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 13
이름 : 배성훈
내용 : 나는 행복합니다.
    문제번호 : 16124번

    세그먼트 트리, 느리게 갱신되는 세그먼트 트리 문제다.
    https://justicehui.github.io/ps/2020/03/20/BOJ16124/
    해당 글을 참고해 풀었다.

    C#으로는 시간 초과 나온다.
    CPP로 바꾸니 이상없이 통과한다.
    수정 후 값을 가져오는데서 나머지 연산을 안해 2번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1401
    {

        static void Main1401(string[] args)
        {

            long MOD = 998_244_353L;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string input;

            int n;
            long[] pow;
            long[][] tree, lazy;
            long[] calc;
            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                calc = new long[10];

                int S = 0;
                int E = n - 1;

                int q = ReadInt();

                while (q-- > 0)
                {

                    int op = ReadInt();
                    int i = ReadInt() - 1;
                    int j = ReadInt() - 1;

                    if (op == 1)
                    {

                        int f = ReadInt();
                        int t = ReadInt();

                        LazyUpdate(S, E, i, j, f, t);
                    }
                    else
                    {

                        sw.Write(GetVal(S, E, i, j));
                        sw.Write('\n');
                    }
                }
            }

            long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
            {

                ChkLazy(_s, _e, _idx);
                long ret = 0L;
                if (_chkE < _s || _e < _chkS) return ret;
                else if (_chkS <= _s && _e <= _chkE)
                {

                    for (int i = 0; i < 10; i++)
                    {

                        ret = (ret + i * tree[i][_idx]) % MOD;
                    }

                    return ret;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chkE) ret = ((pow[Math.Min(_e, _chkE) - mid] * GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)) % MOD
                                        + GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2)) % MOD;
                else ret = GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1);

                return ret;
            }

            void LazyUpdate(int _s, int _e, int _chkS, int _chkE, int _chk, int _val, int _idx = 0)
            {

                ChkLazy(_s, _e, _idx);

                if (_chkS <= _s && _e <= _chkE)
                {

                    lazy[_chk][_idx] = _val;
                    ChkLazy(_s, _e, _idx);
                    return;
                }
                else if (_e < _chkS || _chkE < _s) return;

                int mid = (_s + _e) >> 1;
                LazyUpdate(_s, mid, _chkS, _chkE, _chk, _val, _idx * 2 + 1);
                LazyUpdate(mid + 1, _e, _chkS, _chkE, _chk, _val, _idx * 2 + 2);

                for (int i = 0; i < 10; i++)
                {

                    tree[i][_idx] = (pow[_e - mid] * tree[i][_idx * 2 + 1] + tree[i][_idx * 2 + 2]) % MOD;
                }
            }

            void ChkLazy(int _s, int _e, int _idx)
            {

                bool flag = true;
                for (int i = 0; i < 10; i++)
                {

                    if (lazy[i][_idx] == i) continue;
                    flag = false;
                    break;
                }

                if (flag) return;

                Array.Fill(calc, 0);
                for (int i = 0; i < 10; i++)
                {

                    long idx = lazy[i][_idx];
                    calc[idx] = (calc[idx] + tree[i][_idx]) % MOD;
                }

                for (int i = 0; i < 10; i++)
                {

                    tree[i][_idx] = calc[i];
                }

                if (_s != _e)
                {

                    SetChild(_idx * 2 + 1);
                    SetChild(_idx * 2 + 2);

                    void SetChild(int _child)
                    {

                        for (int i = 0; i < 10; i++)
                        {

                            calc[i] = lazy[lazy[i][_child]][_idx];
                        }

                        for (int i = 0; i < 10; i++)
                        {

                            lazy[i][_child] = calc[i];
                        }
                    }
                }

                for (int i = 0; i < 10; i++)
                {

                    lazy[i][_idx] = i;
                }
            }

            void Input()
            {

                input = sr.ReadLine();
                n = input.Length;

                pow = new long[n + 1];
                pow[0] = 1;
                for (int i = 0; i < n; i++)
                {

                    pow[i + 1] = (pow[i] * 10) % MOD;
                }

                int log = n == 1 ? 1 : (int)(Math.Log2(n - 1) + 1e-9) + 2;
                tree = new long[10][];
                lazy = new long[10][];
                for (int i = 0; i < 10; i++)
                {

                    tree[i] = new long[1 << log];
                    lazy[i] = new long[1 << log];
                    if (i > 0) Array.Fill(lazy[i], i);
                }

                Update(0, n - 1);

                void Update(int _s, int _e, int _idx = 0)
                {

                    if (_s == _e)
                    {

                        tree[input[_s] - '0'][_idx] = 1;
                        return;
                    }

                    int mid = (_s + _e) >> 1;
                    Update(_s, mid, _idx * 2 + 1);
                    Update(mid + 1, _e, _idx * 2 + 2);

                    for (int i = 0; i < 10; i++)
                    {

                        tree[i][_idx] = (pow[_e - mid] * tree[i][_idx * 2 + 1] 
                            + tree[i][_idx * 2 + 2]) % MOD;
                    }
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
    }

#if other
// #include <iostream>
// #include <string>
// #include <vector>
// #pragma GCC optimize("Ofast")
// #pragma GCC optimize("unroll-loops")

using namespace std;

string str;
const int Q = 998244353;

struct Node
{
    int val[10];
    int lazy[10];
}seg[4040404];
int pow[1010101];
int rpow[1010101];

void prop(int ind)
{
    Node &nd = seg[ind], &x = seg[ind << 1], &y = seg[ind << 1 | 1];
    int X[10]{}, Y[10]{};

    for(int i = 0; i < 10; ++i)
    {
        int tmp = nd.lazy[i];
        X[tmp] += x.val[i];
        if(X[tmp] >= Q) X[tmp] -= Q;
        Y[tmp] += y.val[i];
        if(Y[tmp] >= Q) Y[tmp] -= Q;
    }

    for(int i = 0; i < 10; ++i)
    {
        x.val[i] = X[i];
        y.val[i] = Y[i];
        x.lazy[i] = nd.lazy[x.lazy[i]];
        y.lazy[i] = nd.lazy[y.lazy[i]];
    }

    for(int i = 0; i < 10; ++i) nd.lazy[i] = i;
}

void mrge(int ind, int s, int e)
{
    Node &nd = seg[ind], &x = seg[ind << 1], &y = seg[ind << 1 | 1];
    int mid = s + e >> 1;
    for(int i = 0; i < 10; ++i)
        nd.val[i] = (1ll * pow[e - mid] * x.val[i] + y.val[i]) % Q;
}

void init(int ind, int s, int e)
{
    for(int i = 0; i < 10; ++i) seg[ind].lazy[i] = i;
    if(s + 1 == e)
    {
        for(int i = 0; i < 10; ++i) seg[ind].val[i] = 0;
        seg[ind].val[str[s] - '0'] = 1;
        return;
    }

    int mid = s + e >> 1;
    init(ind << 1, s, mid);
    init(ind << 1 | 1, mid, e);
    mrge(ind, s, e);
}

void upd(int ind, int s, int e, int l, int r, int x, int y)
{
    if(e <= l || r <= s) return;
    if(l <= s && e <= r)
    {
        seg[ind].val[x] += seg[ind].val[y];
        if(seg[ind].val[x] >= Q) seg[ind].val[x] -= Q;
        seg[ind].val[y] = 0;
        for(int i = 0; i < 10; ++i)
            if(seg[ind].lazy[i] == y) seg[ind].lazy[i] = x;
        return;
    }

    int mid = s + e >> 1;
    prop(ind);
    upd(ind << 1, s, mid, l, r, x, y);
    upd(ind << 1 | 1, mid, e, l, r, x, y);
    mrge(ind, s, e);
}

int qr(int ind, int s, int e, int l, int r)
{
    if(e <= l || r <= s) return 0;
    if(l <= s && e <= r)
    {
        long long ret = 0;
        for(int i = 1; i < 10; ++i) ret += 1ll * i * seg[ind].val[i];
        return ret % Q;
    }

    int mid = s + e >> 1;
    prop(ind);
    return (1ll * pow[e - mid] * qr(ind << 1, s, mid, l, r) + qr(ind << 1 | 1, mid, e, l, r)) % Q;
}

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(nullptr);

    cin >> str;
    int N = str.size();
    pow[0] = 1; rpow[0] = 1;
    for(int i = 1; i < N; ++i) pow[i] = 1ll * pow[i - 1] * 10 % Q;
    for(int i = 1; i < N; ++i) rpow[i] = 1ll * rpow[i - 1] * 299473306 % Q;
    init(1, 0, N);

    int T; cin >> T;
    while(T--)
    {
        int c; cin >> c;
        if(c == 1)
        {
            int l, r, x, y; cin >> l >> r >> y >> x; --l;
            if(x != y) upd(1, 0, N, l, r, x, y);
        }
        if(c == 2)
        {
            int l, r; cin >> l >> r; --l;
            cout << 1ll * qr(1, 0, N, l, r) * rpow[N - r] % Q << '\n';
        }
    }
}

#elif CPP
// #include <iostream>
// #include <string>
// #include <vector>
// #include <cmath>

// #define FAST_IO cin.tie(NULL);	\
				cout.tie(NULL);	\
				ios_base::sync_with_stdio(false)

// #define MIN(X, Y) (((X) < (Y)) ? (X) : (Y))

// #define ll long long
// #define endl '\n'

using namespace std;

const ll MOD = 998'244'353LL;

int n;
string input;
ll tpow[1 << 21], tree[10][1 << 21], lazy[10][1 << 21];

long calc[10];

void InitUpdate(int _s, int _e, int _idx = 0)
{

	if (_s == _e)
	{

		tree[input[_s] - '0'][_idx] = 1;
		return;
	}

	int mid = (_s + _e) >> 1;
	InitUpdate(_s, mid, _idx * 2 + 1);
	InitUpdate(mid + 1, _e, _idx * 2 + 2);

	for (int i = 0; i < 10; i++)
	{

		tree[i][_idx] = (tpow[_e - mid] * tree[i][_idx * 2 + 1]
			+ tree[i][_idx * 2 + 2]) % MOD;
	}
}

void Input() 
{

	FAST_IO;
	cin >> input;
	n = input.length();

	tpow[0] = 1;
	for (int i = 0; i < n; i++) 
	{

		tpow[i + 1] = (tpow[i] * 10LL) % MOD;
	}

	for (int i = 0; i < 10; i++)
	{

		int len = 1 << 21;
		for (int j = 0; j < len; j++) 
		{

			lazy[i][j] = i;
		}
	}

	InitUpdate(0, n - 1);
}

void SetChild(int _child, int _idx)
{

	for (int i = 0; i < 10; i++)
	{

		calc[i] = lazy[lazy[i][_child]][_idx];
	}

	for (int i = 0; i < 10; i++)
	{

		lazy[i][_child] = calc[i];
	}
}

void ChkLazy(int _s, int _e, int _idx)
{

	bool flag = true;
	for (int i = 0; i < 10; i++) 
	{

		if (lazy[i][_idx] == i) continue;
		flag = false;
		break;
	}

	if (flag) return;

	for (int i = 0; i < 10; i++)
	{

		calc[i] = 0LL;
	}

	for (int i = 0; i < 10; i++)
	{

		ll idx = lazy[i][_idx];
		calc[idx] = (calc[idx] + tree[i][_idx]) % MOD;
	}

	for (int i = 0; i < 10; i++)
	{

		tree[i][_idx] = calc[i];
	}

	if (_s != _e)
	{

		SetChild(_idx * 2 + 1, _idx);
		SetChild(_idx * 2 + 2, _idx);
	}

	for (int i = 0; i < 10; i++)
	{

		lazy[i][_idx] = i;
	}
}

void LazyUpdate(int _s, int _e, int _chkS, int _chkE, int _chk, int _val, int _idx = 0)
{

	ChkLazy(_s, _e, _idx);

	if (_chkS <= _s && _e <= _chkE)
	{

		lazy[_chk][_idx] = _val;
		ChkLazy(_s, _e, _idx);
		return;
	}
	else if (_e < _chkS || _chkE < _s) return;

	int mid = (_s + _e) >> 1;
	LazyUpdate(_s, mid, _chkS, _chkE, _chk, _val, _idx * 2 + 1);
	LazyUpdate(mid + 1, _e, _chkS, _chkE, _chk, _val, _idx * 2 + 2);

	for (int i = 0; i < 10; i++)
	{

		tree[i][_idx] = (tpow[_e - mid] * tree[i][_idx * 2 + 1]
			+ tree[i][_idx * 2 + 2]) % MOD;
	}
}

ll GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
{

	ChkLazy(_s, _e, _idx);
	long ret = 0LL;

	if (_chkE < _s || _e < _chkS) return ret;
	else if (_chkS <= _s && _e <= _chkE)
	{

		for (ll i = 0; i < 10; i++)
		{

			ret = (ret + i * tree[i][_idx]) % MOD;
		}

		return ret;
	}

	int mid = (_s + _e) >> 1;
	if (mid < _chkE) ret = ((tpow[MIN(_e, _chkE) - mid] * GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1)) % MOD
		+ GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2)) % MOD;
	else ret = GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1);

	return ret;
}

void GetRet()
{

	int S = 0;
	int E = n - 1;

	int q;
	cin >> q;

	while (q--)
	{

		int op, i, j;
		cin >> op >> i >> j;

		i--;
		j--;

		if (op == 1)
		{

			int f, t;
			cin >> f >> t;
			LazyUpdate(S, E, i, j, f, t);
		}
		else
		{

			cout << GetVal(S, E, i, j) << endl;
		}
	}
}

int main()
{

	Input();

	GetRet();
	return 0;
}
#endif
}
