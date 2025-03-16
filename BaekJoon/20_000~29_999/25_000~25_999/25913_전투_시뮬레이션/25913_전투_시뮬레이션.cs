using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 16
이름 : 배성훈
내용 : 전투 시뮬레이션
    문제번호 : 25913번

    누적합, 머지 소트 트리 문제다.
    min(|Sum(l, k) - Sum(k + 1, r)|) = val의 값을 찾아야 한다.
    여기서 Sum(a, b)는 구간 a ~ b까지의 arr의 누적합이고, |a|는 절댓값 기호다.

    sum 배열을 sum[i] = Sum(0, i)라 하자.
    그러면, min(Sum(l, k) - Sum(k + 1, r)) = val는
    val = min(|sum[r] + sum[l - 1] - 2 * sum[k]|)가 된다.
    해당 k를 만족하는 k를 찾아야 한다.

    min(|sum[r] + sum[l - 1] - 2 * sum[k]|)를 찾을 때
    l, r범위를 브루트 포스로 하면 r - l + 1의 탐색을 한다.

    |sum[r] + sum[l - 1] - 2 * sum[k]|의 최솟값은
    sum[r] + sum[l - 1]과 가까운 2 * sum[k]인 sum[k]를 찾는 것이다.
    
    (sum[r] + sum[l - 1]) / 2은 상수로 볼 수 있다.
    C <= sum[k]인 가장작은 sum[k]와 C >= sum[k]인 가장 큰 sum[k]를 조사하면 된다.
    그래서 머지 소트 트리를 이용해 구간의 누적합을 정렬하고 이분 탐색으로 찾았다.
    여기서 그리디로 C <= sum[k]인 가장 작은 sum[k]를 찾았다.
    그러면 정렬되었으므로 k - 1이 sum[k] <= C인 가장 큰 sum[k]가 된다.
    해당 두개를 비교해 |C - sum[k]| 의 가장 작은 값을 찾았다.
*/

namespace BaekJoon.etc
{
    internal class etc_1411
    {

        static void Main1411(string[] args)
        {

            int S, E;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n;
            long[] sum;
            List<long>[] merSeg;

            Input();

            SetSeg();

            GetRet();

            void GetRet()
            {

                long INF = 5_000_000_000_000_000;
                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int m = ReadInt();

                long _chk;

                while (m-- > 0)
                {

                    int f = ReadInt();
                    int t = ReadInt();

                    int len = (t - f + 1) / 3;
                    _chk = sum[t] + sum[f - 1];
                    long ret = GetVal(S, E, f + len - 1, t - len);

                    sw.Write($"{ret}\n");
                }

                long GetVal(int _s, int _e, int _chkS, int _chkE, int _idx = 0)
                {

                    long ret = INF;
                    if (_chkS <= _s && _e <= _chkE)
                    {

                        int idx = BinarySearch();
                        if (idx > 0) ret = Math.Min(ret, Math.Abs(_chk - 2 * merSeg[_idx][idx - 1]));
                        if (idx < merSeg[_idx].Count) ret = Math.Min(ret, Math.Abs(_chk - 2 * merSeg[_idx][idx])); 

                        return ret;
                    }
                    else if (_e < _chkS || _chkE < _s) return ret;

                    int mid = (_s + _e) >> 1;

                    ret = Math.Min(GetVal(_s, mid, _chkS, _chkE, _idx * 2 + 1), GetVal(mid + 1, _e, _chkS, _chkE, _idx * 2 + 2));
                    return ret;

                    int BinarySearch()
                    {

                        List<long> list = merSeg[_idx];
                        int l = 0;
                        int r = list.Count - 1;

                        while (l <= r)
                        {

                            int mid = (l + r) >> 1;
                            if (list[mid] * 2 < _chk) l = mid + 1;
                            else r = mid - 1;
                        }

                        return l;
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                sum = new long[n + 1];
                for (int i =1; i <= n; i++)
                {

                    sum[i] = ReadInt() + sum[i - 1];
                }
            }

            void SetSeg()
            {

                S = 1;
                E = n;

                int log = (int)(Math.Log2(n - 1) + 1e-9) + 2;
                merSeg = new List<long>[1 << log];

                for (int i = 1; i <= n; i++)
                {

                    Init(S, E, i);
                }

                for (int i = 0; i < merSeg.Length; i++)
                {

                    if (merSeg[i] == null) continue;
                    merSeg[i].Sort();
                }

                void Init(int _s, int _e, int _chk, int _idx = 0)
                {

                    merSeg[_idx] ??= new(_e - _s + 1);
                    merSeg[_idx].Add(sum[_chk]);

                    if (_s == _e) return;

                    int mid = (_s + _e) >> 1;

                    if (_chk <= mid) Init(_s, mid, _chk, _idx * 2 + 1);
                    else Init(mid + 1, _e, _chk, _idx * 2 + 2);
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

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>

using namespace std;

using ll = long long;
using li = pair<ll, int>;
using query = tuple<ll, int, int, int>;

const ll INF = 4e18;

template <typename T = ll>
class Seg {
public:
	Seg(int sz) : n(sz), st(2 * sz, INF) { }
	void update(int p, T val) {
		for (st[p += n] = val; p > 1; p /= 2) {
			st[p / 2] = min(st[p], st[p ^ 1]);
		}
	}
	T rmq(int l, int r) {
		T ret = INF;
		for (l += n, r += n; l < r; l /= 2, r /= 2) {
			if (l & 1) {
				ret = min(ret, st[l++]);
			}
			if (r & 1) {
				ret = min(st[--r], ret);
			}
		}
		return ret;
	}
private:
	int n;
	vector<T> st;
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	int N{};
	cin >> N;
	vector<ll> A(N + 1);
	for (int i = 1; i <= N; ++i) {
		cin >> A[i];
	}
	vector<ll> psum(N + 1);
	partial_sum(A.begin(), A.end(), psum.begin());
	vector<li> B(N);
	for (int i = 0; i < N; ++i) {
		B[i] = {2 * psum[i + 1], i + 1};
	}
	sort(B.begin(), B.end());
	int Q{};
	cin >> Q;
	vector<query> C(Q);
	for (int i = 0; i < Q; ++i) {
		auto &[sum, l, r, idx] = C[i];
		cin >> l >> r;
		sum = psum[l - 1] + psum[r];
		idx = i;
	}
	sort(C.begin(), C.end());
	vector<ll> ans(Q, INF);
	for (int k = 0; k < 2; ++k) {
		Seg seg(N + 1);
		for (int i = 0, j = 0; i < Q; ++i) {
			auto &[sum, l, r, i1] = C[i];
			while (j < B.size()) {
				auto &[val, i2] = B[j];
				if ((val <= sum) ^ k) {
					seg.update(i2, val * (k ? 1 : -1));
					++j;
				} else {
					break;
				}
			}
			int len = (r - l + 1) / 3;
			ans[i1] = min(ans[i1], seg.rmq(l + len - 1, r - len + 1) + sum * (k ? -1 : 1));
		}
		reverse(B.begin(), B.end());
		reverse(C.begin(), C.end());
	}
	for (ll x : ans) {
		cout << x << "\n";
	}
	return 0;
}

#endif
}
