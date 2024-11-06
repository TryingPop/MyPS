using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 10
이름 : 배성훈
내용 : 수열과 쿼리 5
    문제번호 : 13547번

    오프라인 쿼리, mo's 문제다

    mo's 알고리즘은 업데이트가 필요없는 쿼리문제에서 쓰이는 알고리즘이다
    스위핑?이 쓰인 알고리즘 같다

    쿼리들을 먼저 시작지점을 최우선으로, 길이를 후순위로 정렬한 뒤
    단순히 시작지점으로만하면, 0 ~ 10만 구간, 1 ~ 2구간, 2 ~ 10만구간으로
    아주 비효율적인 탐색이 될 수 있다

    그래서 시작지점을 그냥 넣는게 아닌 전체의 루트로 나눠서 정렬을 진행한다
    투 포인트 알고리즘을 진행한다

    왼쪽 끝을 나타내는 l과 오른쪽 끝을 나타내는 r이 있다
    그리고 해당 길이에 맞게 투포인터로 카운팅한다

    이렇게 정답을 인덱스에 맞게 찾아간다
*/

namespace BaekJoon._53
{
    internal class _53_08
    {

        static void Main8(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int sqrt;
            int n, m;
            int[] arr;
            int[] ret;
            (int s, int e, int idx)[] queries;

            Solve();

            void Solve()
            {

                Input();

                Query();

                Output();
            }

            void Query()
            {

                Array.Sort(queries, (x, y) => QueryComp(ref x, ref y));

                ret = new int[m];
                int[] cnt = new int[1_000_001];
                int l = 1;
                int r = 0;
                int temp = 0;

                for (int i = 0; i < m; i++)
                {

                    // 새로 추가되는거 확인
                    while (queries[i].s < l) temp += ++cnt[arr[--l]] == 1 ? 1 : 0;
                    // 빠지는거 확인
                    while (queries[i].s > l) temp -= --cnt[arr[l++]] == 0 ? 1 : 0;
                    // 새로 빠지는거 확인
                    while (queries[i].e < r) temp -= --cnt[arr[r--]] == 0 ? 1 : 0;
                    // 새로 추가되는거 확인
                    while (queries[i].e > r) temp += ++cnt[arr[++r]] == 1 ? 1 : 0;

                    ret[queries[i].idx] = temp;
                }
            }

            void Output()
            {

                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int i = 0; i < m; i++)
                {

                    sw.Write($"{ret[i]}\n");
                }

                sw.Close();
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                m = ReadInt();
                queries = new (int s, int e, int idx)[m];
                for (int i = 0; i < m; i++)
                {

                    queries[i] = (ReadInt() - 1, ReadInt() - 1, i);
                }

                sqrt = (int)Math.Sqrt(n);
                sr.Close();
            }

            int QueryComp(ref (int s, int e, int idx) _query1, ref (int s, int e, int idx) _query2)
            {

                // 시작점이 앞서는거
                if (_query1.s / sqrt != _query2.s / sqrt) return _query1.s.CompareTo(_query2.s);
                // 길이가 짧은거
                return _query1.e.CompareTo(_query2.e);
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
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

int rt;
int arr[100001], ans[100001];

int t[400001], lazy[400001];
int last[1000001];

void up_lazy(int i, int lo, int hi)
{
	if (lazy[i] != 0)
	{
		t[i] += lazy[i];
		if (lo != hi)
		{
			lazy[i * 2] += lazy[i];
			lazy[i * 2 + 1] += lazy[i];
		}
		lazy[i] = 0;
	}
}

void up(int i, int lo, int hi, int l, int r)
{
	if (hi < l || lo > r)
		return;
	up_lazy(i, lo, hi);
	if (l <= lo && hi <= r)
	{
		t[i]++;
		if (lo != hi)
		{
			lazy[i * 2]++;
			lazy[i * 2 + 1]++;
		}
		return;
	}
	int mid = (lo + hi) / 2;
	up(i * 2, lo, mid, l, r);
	up(i * 2 + 1, mid + 1, hi, l, r);
}

int query(int i, int lo, int hi, int x)
{
	up_lazy(i, lo, hi);
	if (lo == hi)
		return t[i];
	int mid = (lo + hi) / 2;
	if (x <= mid)
		return query(i * 2, lo, mid, x);
	else
		return query(i * 2 + 1, mid + 1, hi, x);
}

struct A {
	int l, r, n;
	bool operator<(const A& o) const {
		return r < o.r;
	}
} q[100001];

char buf[2200100];
inline int ri()
{
	static int i = 0;
	int r = buf[i++] - '0';
	while (buf[i] >= '0')
		r = (r * 10) + buf[i++] - '0';
	return ++i, r;
}

char buf2[700100];
int idx;
inline void wi(int x)
{
	char s[10];
	int i = 0;
	do {
		s[i++] = x % 10 + '0';
		x /= 10;
	} while (x);
	for (i--; i >= 0; i--)
		buf2[idx++] = s[i];
	buf2[idx++] = '\n';
}

int main()
{
	fread(buf, 1, sizeof buf, stdin);

	int n, m, i, j;
	n = ri();
	rt = (int)sqrt(n);
	for (i = 1; i <= n; i++)
		arr[i] = ri();
	m = ri();
	for (i = 0; i < m; i++)
	{
		q[i].l = ri();
		q[i].r = ri();
		q[i].n = i;
	}
	sort(q, q + m);

	j = 0;
	for (i = 0; i < m; i++)
	{
		while (j <= q[i].r)
		{
			up(1, 1, n, last[arr[j]], j);
			last[arr[j]] = j + 1;
			j++;
		}
		ans[q[i].n] = query(1, 1, n, q[i].l);
	}

	for (i = 0; i < m; i++)
		wi(ans[i]);
	fwrite(buf2, 1, strlen(buf2), stdout);
}
#elif other2
using System;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var n = Int32.Parse(sr.ReadLine());
        var sqrtn = Math.Max(1, (int)Math.Sqrt(n));

        var nums = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var numCount = new int[1_000_001];
        var distinctCount = 0;

        var q = Int32.Parse(sr.ReadLine());
        var queries = new (int idx, int l, int r)[q];
        var results = new int[q];

        for (var idx = 0; idx < q; idx++)
        {
            var l = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            queries[idx] = (idx, l[0] - 1, l[1] - 1);
        }

        var currl = 0;
        var currr = 0;
        numCount[nums[0]]++;
        distinctCount++;

        foreach (var (idx, l, r) in queries.OrderBy(r => r.l / sqrtn).ThenBy(v => v.r))
        {
            while (currr > r)
            {
                numCount[nums[currr]]--;
                if (numCount[nums[currr]] == 0)
                    distinctCount--;

                currr--;
            }
            while (currr < r)
            {
                currr++;
                if (numCount[nums[currr]] == 0)
                    distinctCount++;

                numCount[nums[currr]]++;
            }

            while (currl > l)
            {
                currl--;
                if (numCount[nums[currl]] == 0)
                    distinctCount++;

                numCount[nums[currl]]++;
            }
            while (currl < l)
            {
                numCount[nums[currl]]--;
                if (numCount[nums[currl]] == 0)
                    distinctCount--;

                currl++;
            }

            results[idx] = distinctCount;
        }

        foreach (var v in results)
            sw.WriteLine(v);
    }
}

#endif
}
