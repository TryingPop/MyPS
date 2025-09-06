using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 27
이름 : 배성훈
내용 : Smartphone 
    문제번호 : 28023번

    그리디, 우선순위 큐 문제다.
    한참을 고민해도 방법이 안떠올라 GPT를 이용해 풀었다.

    그리디로 i날에 가장 많이 사용한 날짜로 찾아간다.
    그러면 k일이 되었을 때 가장 많이 사용한 날을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1824
    {

        static void Main1824(string[] args)
        {

            // 28023번 - 최적화를 해야 한다
#if CHAT_GPT
            var parts = Console.ReadLine().Split();
            int N = int.Parse(parts[0]);
            long K = long.Parse(parts[1]);

            long[] A = new long[N];
            long[] B = new long[N];
            long[] C = new long[N];

            var starts = new Dictionary<long, List<(long b, long c)>>();

            for (int i = 0; i < N; i++)
            {
                var line = Console.ReadLine().Split();
                A[i] = long.Parse(line[0]);
                B[i] = long.Parse(line[1]);
                C[i] = long.Parse(line[2]);

                if (!starts.ContainsKey(A[i]))
                    starts[A[i]] = new List<(long, long)>();
                starts[A[i]].Add((B[i], C[i]));
            }

            // 이벤트 시점: 1, K+1, 모든 Ai, 모든 Bi+1
            var events = new SortedSet<long>();
            events.Add(1);
            events.Add(K + 1);
            for (int i = 0; i < N; i++)
            {
                events.Add(A[i]);
                events.Add(B[i] + 1);
            }

            var evList = events.ToList();

            // 최소 힙 (Bi 기준 오름차순)
            var pq = new PriorityQueue<(long b, long c), long>();

            long answer = 0;

            for (int idx = 0; idx < evList.Count - 1; idx++)
            {
                long t = evList[idx];
                long next = evList[idx + 1];
                long L = next - t;

                // (1) 새로 시작하는 스마트폰 추가
                if (starts.ContainsKey(t))
                {
                    foreach (var (b, c) in starts[t])
                    {
                        pq.Enqueue((b, c), b);
                    }
                }

                // (2) 만료된 스마트폰 제거
                while (pq.Count > 0 && pq.Peek().b < t)
                    pq.Dequeue();

                // (3) 구간 [t, next-1] 배치
                long remain = L;
                while (remain > 0 && pq.Count > 0)
                {
                    var (b, c) = pq.Dequeue();
                    if (b < t) continue;

                    long use = Math.Min(c, remain);
                    answer += use;
                    remain -= use;
                    c -= use;

                    if (c > 0)
                        pq.Enqueue((b, c), b);
                }
            }

            Console.WriteLine(answer);
#else

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = ReadInt();
            long k = ReadLong();

            (long a, long b, long c)[] arr = new (long a, long b, long c)[n];
            long[] idx = new long[n * 2];

            for (int i = 0, j = 0; i < n; i++)
            {

                arr[i] = (ReadLong(), ReadLong(), ReadLong());
                idx[j++] = arr[i].a;
                idx[j++] = arr[i].b + 1;
            }

            Array.Sort(arr, (x, y) => x.a.CompareTo(y.a));
            Array.Sort(idx);

            PriorityQueue<int, long> pq = new(n);

            long ret = 0;
            
            for (int i = 0, j = 0; i < idx.Length - 1; i++)
            {

                while (j < n && idx[i] == arr[j].a)
                {

                    pq.Enqueue(j, arr[j].b);
                    j++;
                }

                while (pq.Count > 0 && arr[pq.Peek()].b < idx[i])
                {

                    // 날짜가 지났다.
                    pq.Dequeue();
                }

                long r = idx[i + 1] - idx[i];

                while (pq.Count > 0 && r > 0) 
                {

                    int cur = pq.Dequeue();

                    long use = Math.Min(arr[cur].c, r);
                    ret += use;
                    arr[cur].c -= use;
                    r -= use;

                    if (arr[cur].c > 0) pq.Enqueue(cur, arr[cur].b);
                }
            }

            Console.Write(ret);

            long ReadLong()
            {

                long ret = 0;
                while (TryReadLong()) ;
                return ret;

                bool TryReadLong()
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

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) ;
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

#endif
        }
    }

#if other
// #include <bits/stdc++.h>
 
using namespace std;
typedef long long ll;
typedef __int128 lll;
typedef long double ld;
typedef pair<ll, ll> pll;
typedef pair<ld, ld> pld;
// #define MAX 9223372036854775807LL
// #define MIN -9223372036854775807LL
// #define INF 0x3f3f3f3f3f3f3f3f
// #define fi first
// #define se second
// #define fastio ios_base::sync_with_stdio(false); cin.tie(NULL); cout.tie(NULL); cout << fixed; cout.precision(10);
// #define sp << " "
// #define en << "\n"
// #define compress(v) sort(v.begin(), v.end()), v.erase(unique(v.begin(), v.end()), v.end())

struct gujo
{
	ll S, E, C;
	
	bool operator < (const gujo &xx) const
	{
		return S < xx.S;
	}
};

ll n, K;
gujo a[300010];
priority_queue<pll, vector<pll>, greater<pll> > pq;
ll now = 1;
ll ans;

int main(void)
{
	fastio
	
	cin >> n >> K;
	
	for(ll i = 1 ; i <= n ; i++)
		cin >> a[i].S >> a[i].E >> a[i].C;
	
	sort(a + 1, a + 1 + n);
	
	pq.push({a[1].E, 1});
	now = a[1].S;
	
	a[n + 1].S = K + 1;
	
	for(ll i = 2 ; i <= n + 1 ; i++)
	{
		if(a[i].S == a[i - 1].S)
		{
			pq.push({a[i].E, i});
			continue;
		}
		
		while(!pq.empty())
		{
			pll qq = pq.top();
			pq.pop();
			
			ll num = qq.se;
			
			if(qq.fi < a[i].S)
			{
				if(now + a[num].C - 1 <= qq.fi)
				{
					ans += a[num].C;
					now += a[num].C;
				}
				
				else
				{
					ans += qq.fi - now + 1;
					a[num].C -= (qq.fi - now + 1);
					now = qq.fi + 1;
				}
			}
			
			else
			{
				if(now + a[num].C - 1 < a[i].S)
				{
					ans += a[num].C;
					now += a[num].C;
				}
				
				else
				{
					ans += a[i].S - now;
					a[num].C -= (a[i].S - now);
					now = a[i].S;
					pq.push(qq);
					break;
				}
			}
		}
		
		now = a[i].S;
		pq.push({a[i].E, i});
	}
	
	cout << ans;
	return 0;
}
#endif
}
