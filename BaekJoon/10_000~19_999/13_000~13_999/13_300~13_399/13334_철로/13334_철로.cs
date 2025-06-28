using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 9
이름 : 배성훈
내용 : 철로
    문제번호 : 13334번

    정렬, 스위핑, 우선순위 큐 문제다.
    해당 간선을 가장 많이포함하는 구간을 찾아야 한다.

    아이디어는 다음과 같다.
    우선 간선들을 끝지점으로 정렬한다.
    e에 따라 그리고 길이가 d이하인 지점들을 모두 pq에 담는다.
    pq는 시작지점에 따라 오름차순 정렬되게 한다.
    그리고 e - d인 지점들은 빼게 pq를 세팅하면
    끝지점이 e일 때 포함된 간선의 개수가 된다.

    그러면 각 끝지점을 1칸씩 이동하며 
    가장 많이 포함된 지점을 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1169
    {

        static void Main1169(string[] args)
        {

            int n, d;
            (int s, int e)[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Array.Sort(arr, (x, y) => x.e.CompareTo(y.e));
                PriorityQueue<int, int> pq = new(n);

                int e = -1_000_000_000;
                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (arr[i].e != e)
                    {

                        ret = Math.Max(pq.Count, ret);
                        e = arr[i].e;

                        while (pq.Count > 0 && pq.Peek() < e - d) { pq.Dequeue(); }
                    }

                    if (arr[i].e - arr[i].s <= d) pq.Enqueue(arr[i].s, arr[i].s);

                }

                ret = Math.Max(ret, pq.Count);

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new (int s, int e)[n];
                for (int i = 0; i < n; i++)
                {

                    int f = ReadInt();
                    int t = ReadInt();
                    if (t < f)
                    {

                        int temp = f;
                        f = t;
                        t = temp;
                    }

                    arr[i] = (f, t);
                }

                d = ReadInt();

                sr.Close();

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

                        while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
    }

#if other
// # include <unistd.h>
// # include <algorithm>
// # include <queue>
using namespace std;

using pii = pair<int, int>;
constexpr int bias = 100'000'000;
constexpr int SZ = 1 << 18;

void radix_sort(register pii v[], int n) {
	const unsigned int SZ = 6;
    const unsigned int mask = (1 << SZ) - 1;
	static queue<pii> Q[1 << SZ];
    for (int M = 0; M < 5; M++) {
        for (int i = 0; i < n; i++) Q[(unsigned)v[i].second >> M * SZ & mask].push(v[i]);
        for (int i = 0, j = 0; i < 1 << SZ; i++) while (Q[i].size()) v[j++] = Q[i].front(), Q[i].pop();
    }
}

int main() {
	char r[SZ], w[10], *p = r; read(0, r, SZ);
// #define ReadChar(c) { \
        if (p == r + SZ) read(0, p = r, SZ); \
        c = *p++; }
// #define ReadInt(n) { \
        char c, flag = 0; ReadChar(c); \
        if (c == '-') { flag = 1; ReadChar(c); } \
        for (; c & 16; ) { n = (n << 1) + (n << 3) + (c & 15); ReadChar(c); } \
        if (flag) n = -n; }
    
	int n = 0, d = 0, ans = 0; ReadInt(n); register pii v[n];
	for (int i = 0; i < n; i++) {
        auto& [a, b] = v[i]; ReadInt(a); ReadInt(b);
        if ((a += bias) > (b += bias)) swap(a, b);
	} ReadInt(d); radix_sort(v, n);
    
    register int PQ[100001];
	for (int i = 0, sz = 0; i < n; i++) {
        int cur = ++sz;
        while (cur > 1 && PQ[cur >> 1] > v[i].first) {
            PQ[cur] = PQ[cur >> 1]; cur >>= 1;
        } PQ[cur] = v[i].first;
        for (int t = v[i].second - d; sz && PQ[1] < t;) {
            int par = 1, child = 2;
            while (child <= sz) {
                if (child < sz && PQ[child] > PQ[child + 1]) child++;
                if (PQ[sz] <= PQ[child]) break;
                PQ[par] = PQ[child]; par = child; child <<= 1;
            } PQ[par] = PQ[sz--];
        }
        if (ans < sz) ans = sz;
	}
    
    int sz = 1, t = ans;
    for (; t >= 10; t /= 10) sz++;
    for (int i = sz; i --> 0; ans /= 10) w[i] = ans % 10 | 48;
    write(1, w, sz);
}
#endif
}
