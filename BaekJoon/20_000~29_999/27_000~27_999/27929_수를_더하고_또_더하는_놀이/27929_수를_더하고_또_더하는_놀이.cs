using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 2
이름 : 배성훈
내용 : 수를 더하고 또 더하는 놀이
    문제번호 : 27929번

    세그먼트 트리 문제다
    아이디어는 다음과 같다.
    현재 있는 모든 수에 w만큼 더하는 것은
    상대적으로 다음에 들어오는 변수는 
    지금 저장된 값보다 w만큼 작아야 한다.

    추가된 값의 누적을 나타내는 add에 w를 더한다.
    그리고 다음 값을 입력받을 때 -add해주면 된다.

    예를 들어 
    2 4 6 8 10
    이 기존에 입력되어 있고 1을 더한 경우
    3 5 7 9 11이다.

    그리고 다음에 4를 넣는다면
    3 4 5 7 9 11이 된다.

    그런데 1을 더할 때 값을 변경하는게 아닌
    add = 0에 값을 1을 더해주고,
    2 4 6 8 10을 그대로 둔다.
    다음으로 4를 넣는다면 4에 add를 빼준 뒤 넣는다
    2 3 4 6 8 10이고
    3 4 5 7 9 11과 1씩 차이가 난다.
    값을 가져올 때 add = 1를 더해주면 된다.
    이렇게 값을 관리했다.

    그리고 문제에서 add에 누적되는 값은 최대 50만을 넘지 않는다고 한다.
    찾는건 작은거 n개, 큰거 n개를 누적한 값이므로 누적합을 찾는 세그먼트 트리 자료구조를 이용했다.
    레이지 세그먼트 트리로 풀 수 있지만, 
    그냥 세그먼트리를 이용해 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1140
    {

        static void Main1140(string[] args)
        {

            const int S = 0;
            const int E = 1_000_000;
            const int M = 500_000;

            StreamReader sr;
            StreamWriter sw;
            (int cnt, long val)[] seg;
            int n, m;
            int add;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();

                while(test-- > 0)
                {

                    Input();

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                seg = new (int cnt, long val)[1 << 21];
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 0; i < n; i++)
                {

                    int num = ReadInt() + M;
                    SetVal(num);
                }
            }

            void GetRet()
            {

                add = 0;
                for (int i = 0; i < m; i++)
                {

                    int op = ReadInt();
                    int w = ReadInt();

                    if (op == 1)
                    {

                        w = w - add + M;
                        SetVal(w);
                    }
                    else if (op == 2)
                        add += w;
                    else if (op == 3)
                    {

                        long sum = GetRight(w);
                        sum += 1L * add * w;
                        sw.Write($"{sum} ");
                    }
                    else
                    {

                        long sum = GetLeft(w);
                        sum += 1L * add * w;
                        sw.Write($"{sum} ");
                    }
                }

                sw.Write('\n');
                sw.Flush();

                Array.Fill(seg, (0, 0));
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

            void SetVal(int _val, int _s = S, int _e = E, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx].cnt++;
                    seg[_idx].val += _s - M;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _val) SetVal(_val, mid + 1, _e, _idx * 2 + 2);
                else SetVal(_val, _s, mid, _idx * 2 + 1);

                seg[_idx].cnt = seg[_idx * 2 + 1].cnt + seg[_idx * 2 + 2].cnt;
                seg[_idx].val = seg[_idx * 2 + 1].val + seg[_idx * 2 + 2].val;
            }

            long GetLeft(int _cnt, int _s = S, int _e = E, int _idx = 0)
            {

                if (_cnt == 0) return 0L;
                else if (_s == _e) return 1L * _cnt * (_s - M);

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                int mid = (_s + _e) >> 1;

                if (seg[l].cnt <= _cnt)
                    return seg[l].val + GetLeft(_cnt - seg[l].cnt, mid + 1, _e, r);
                return GetLeft(_cnt, _s, mid, l);
            }

            long GetRight(int _cnt, int _s = S, int _e = E, int _idx = 0)
            {

                if (_cnt == 0) return 0L;
                else if (_s == _e) return 1L * _cnt * (_s - M);

                int l = _idx * 2 + 1;
                int r = _idx * 2 + 2;
                int mid = (_s + _e) >> 1;

                if (seg[r].cnt <= _cnt)
                    return seg[r].val + GetRight(_cnt - seg[r].cnt, _s, mid, l);
                return GetRight(_cnt, mid + 1, _e, r);
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
// #define all(x) x.begin(), x.end()
// #define int long long
// #define lb lower_bound
// #define ub upper_bound
// #define MASK(i) (1LL << (i))
void ckmax(int& f, int s)
{
	f = (f > s ? f : s);
}
void ckmin(int& f, int s)
{
	f = (f < s ? f : s);
}
struct fenwick
{
	int n;
	vector<int> bit;
	fenwick() {}
	fenwick(int n)
	{
		this->n = n + 5;
		bit.resize(n + 5);
	}
	void add(int pos, int val)
	{
		while (pos < n)
		{
			bit[pos] += val;
			pos += pos & -pos;
		}
	}
	int get(int pos)
	{
		int ans = 0;
		while (pos)
		{
			ans += bit[pos];
			pos -= pos & -pos;
		}
		return ans;
	}
	int get(int l, int r)
	{
		return get(r) - get(l - 1);
	}
	int find(int k)
	{
		int sum = 0, pos = 0;
		for (int i = __lg(n); i >= 0; i--)
		{
			if (pos + (1 << i) < n && sum + bit[pos + (1 << i)] < k)
			{
				sum += bit[pos + (1 << i)];
				pos += (1 << i);
			}
		}
		return pos + 1;
	}
};
void solve()
{
	int n, nq;
	cin >> n >> nq;
	vector<int> a(n);
	for (int &i : a) cin >> i;
	int id;
	vector<pair<int, int>> cc;
	for (int i = 0; i < n; i++)
	{
		cc.emplace_back(a[i], i);
	}
	id = n;
	int add = 0;
	vector<pair<int, int>> q(nq);
	for (auto &[f, s] : q)
	{
		cin >> f >> s;
		if (f == 1) cc.emplace_back(s - add, id++);
		else if (f == 2) add += s;
	}
	sort(all(cc));
	cc.erase(unique(all(cc)), cc.end());
	auto get_pos = [&](pair<int, int> x)
	{
		return ub(all(cc), x) - cc.begin();
	};
	add = 0;
	fenwick sum(cc.size()), occ(cc.size());
	id = 0;
	for (int i : a)
	{
		int p = get_pos({i, id});
		sum.add(p, i);
		occ.add(p, 1);
		id++;
	}
	for (auto [f, s] : q)
	{
		if (f == 1)
		{
			int p = get_pos({s - add, id});
			sum.add(p, s - add);
			occ.add(p, 1);
			id++;
			n++;
		}
		else if (f == 2)
		{
			add += s;
		}
		else if (f == 3)
		{
			if (n == s) cout << sum.get(cc.size()) + add * n << ' ';
			else 
			{
				int p = occ.find(n - s);
				cout << sum.get(cc.size()) + add * s - sum.get(p) << ' ';
			}
		}
		else
		{
			int p = occ.find(s);
			cout << sum.get(p) + add * s << ' ';
		}
	}
	cout << '\n';
}
int32_t main()
{
	ios_base::sync_with_stdio(false);
	cin.tie(0);
	int t;
	cin >> t;
	while (t--) solve();	
	return 0;
}
#endif
}
