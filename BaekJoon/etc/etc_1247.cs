using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 4
이름 : 배성훈
내용 : 수족관 3
    문제번호 : 8987번

    그리디, 세그먼트 트리 문제다.

    세그먼트 트리의 더 효율적인 입력 방법을 이용한 다음 사람의 코드를 참고했다.
    https://justicehui.github.io/koi/2019/12/07/BOJ8987/

    설명은 다음 사이트를 참고하면 될거 같다.
    https://azber.tistory.com/4

    그리디로 물을 가장 많이 뺄 수 있는 곳을 빼야한다.
    트리에 위에 있는 물을 부모로 보내며
    각 정점에서 물을 빼면서 빠지는 양을 우선 순위 큐에 담는다.
*/

namespace BaekJoon.etc
{
    internal class etc_1247
    {

        static void Main1247(string[] args)
        {

            int n, k, bias;
            (int x, int y)[] pos;
            (long y, int idx)[] tree;

            Solve();
            void Solve()
            {

                Input();

                SetTree();

                GetRet();
            }

            void GetRet()
            {

                PriorityQueue<long, long> pq = new(n, Comparer<long>.Create((x, y) => y.CompareTo(x)));

                (long y, int idx) INIT = (1_000_000_000_000_000_000L, 1_000_000_000);
                int h = 0;

                long t = Find(0, n - 1);
                pq.Enqueue(t, t);
                long ret = 0;
                for (int i = 0; i < k; i++)
                {

                    if (pq.Count > 0) ret += pq.Dequeue();
                }

                Console.Write(ret);

                int Query(int _l, int _r)
                {

                    _l |= bias;
                    _r |= bias;

                    (long y, int idx) ret = INIT;

                    while (_l <= _r)
                    {

                        if ((_l & 1) == 1)
                        {

                            if (MyComp(ref tree[_l], ref ret)) ret = tree[_l];
                            _l++;
                        }

                        if (((~_r) & 1) == 1)
                        {

                            if (MyComp(ref tree[_r], ref ret)) ret = tree[_r];
                            _r--;
                        }

                        _l >>= 1;
                        _r >>= 1;
                    }

                    return ret.idx;
                }

                long Find(int _s, int _e)
                {

                    if (_s >= _e) return 0L;

                    int idx = Query(_s, _e - 1);
                    int prev = h;
                    h = pos[idx].y;

                    long t1 = Find(_s, idx);
                    long t2 = Find(idx + 1, _e);

                    long min = Math.Min(t1, t2);
                    pq.Enqueue(min, min);

                    // 이전 물
                    h = prev;

                    // 물 계산해서 반환
                    return Math.Max(t1, t2) + 1L * (pos[_e].x - pos[_s].x) * (pos[idx].y - h);
                }
            }

            void SetTree()
            {

                int log = (int)(Math.Ceiling(1e-9 + Math.Log2(n)));
                tree = new (long y, int idx)[1 << log + 1];
                bias = 1 << log;

                for (int i = 0; i < n; i++)
                {

                    tree[i | bias] = (pos[i].y, i);
                }

                for (int i = bias - 1; i > 0; i--) 
                {

                    // 낮은 걸로 부모 세팅하기
                    if (MyComp(ref tree[i << 1], ref tree[i << 1 | 1])) tree[i] = tree[i << 1];
                    else tree[i] = tree[i << 1 | 1];
                }
            }

            bool MyComp(ref (long y, int idx) _f, ref (long y, int idx) _b)
            {

                if (_f.y < _b.y || (_f.y == _b.y && _f.idx < _b.idx)) return true;
                return false;
            }
            
            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                n >>= 1;
                pos = new (int x, int y)[n];
                for (int i = 0; i < n; i++)
                {

                    ReadInt();
                    ReadInt();
                    pos[i] = (ReadInt(), ReadInt());
                }

                k = ReadInt();

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

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                        {

                            if (c == '\r') continue;
                            ret = ret * 10 + c - '0';
                        }

                        return false;
                    }
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
using namespace std;
using ull = unsigned long long;
constexpr int MAXN = 300000 >> 1;

struct BLOCK { ull vol; int h, w; } STK[MAXN];

ull SECTIONS[MAXN];

int main() {
	ios::sync_with_stdio(0); cin.tie(0);
	int N; cin >> N; N >>= 1;

	{int _; cin >> _ >> _; }
	for (int i = 1; i < N; ++i) {
		int _; cin >> _ >> STK[i].h >> STK[i].w >> _;
	}
	{int _; cin >> _ >> _; }

	int ap = 0, sp = 1;
	for (int i = 1; i <= N; ++i) {
		auto [NV, NH, NW] = STK[i]; int TW = STK[sp - 1].w;
		if (sp > 1 && STK[sp - 1].h >= NH) {
			while (sp > 2 && STK[sp - 2].h >= NH) {
				--sp;
				ull TV = STK[sp].vol + (ull)(STK[sp].h - STK[sp - 1].h) * (TW - STK[sp - 1].w);
				if (TV > STK[sp - 1].vol) swap(TV, STK[sp - 1].vol);
				if (TV) SECTIONS[ap++] = TV;
			}
			--sp;
			NV = STK[sp].vol + (ull)(STK[sp].h - NH) * (TW - STK[sp - 1].w);
		}
		STK[sp++] = BLOCK{ NV,NH,NW };
	}
	SECTIONS[ap++] = STK[sp - 1].vol;

	int K; cin >> K; K = min(K, ap);
	nth_element(SECTIONS, SECTIONS + (K - 1), SECTIONS + ap, greater<ull>());

	ull re = 0;
	for (int i = 0; i < K; ++i) re += SECTIONS[i];
	cout << re;

	return 0;
}
#endif
}
