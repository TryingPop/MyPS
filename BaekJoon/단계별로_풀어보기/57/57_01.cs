using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 6
이름 : 배성훈
내용 : 증가 수열의 개수
    문제번호 : 17409번

    세그먼트 트리, dp 문제다
    아이디어는 다음과 같다
    lcs 알고리즘을 세그먼트 트리에 적용하면 된다

    인덱스 i에서 길이가 n인 모든 오름차순 부분수열의 개수를 F(i, n)이라 하면
    sig F(j, n - 1) 이된다 j < i고, sig는 시그마 섬을 의미한다
*/

namespace BaekJoon._57
{
    internal class _57_01
    {

        static void Main1(string[] args)
        {

#if first
            int MOD = 1_000_000_007;
            StreamReader sr;
            int n, k;
            int[][] seg;
            int[] arr;

            Solve();

            void Solve()
            {

                Input();

                SetDp();

                Console.Write(seg[k - 1][0]);
            }

            
            void SetDp()
            {

                for (int i = 0; i < n; i++)
                {

                    Update(0, n, arr[i], 0, 1);

                    for (int j = 1; j < k; j++)
                    {

                        int val = GetVal(0, n, arr[i] - 1, j - 1);
                        Update(0, n, arr[i], j, val);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                arr = new int[n];
                seg = new int[k][];
                int log = (int)(Math.Ceiling(Math.Log2(n + 1))) + 1;
                for (int i = 0; i < k; i++)
                {

                    seg[i] = new int[1 << log];
                }

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int GetVal(int _s, int _e, int _chk, int _k, int _idx = 0)
            {

                if (_chk < _s) return 0;
                if (_e <= _chk) return seg[_k][_idx];

                int mid = (_s + _e) >> 1;
                int ret = (GetVal(_s, mid, _chk, _k, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chk, _k, _idx * 2 + 2));

                return ret % MOD;
            }

            void Update(int _s, int _e, int _chk, int _k, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_k][_idx] = (seg[_k][_idx] + _val) % MOD;
                    return;
                }

                int mid = (_s + _e) >> 1;

                if (mid < _chk) Update(mid + 1, _e, _chk, _k, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _k, _val, _idx * 2 + 1);

                seg[_k][_idx] = (seg[_k][_idx * 2 + 1] + seg[_k][_idx * 2 + 2]) % MOD;
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

            int MOD = 1_000_000_007;
            StreamReader sr;
            int n, k;
            int[] seg;
            int[] arr;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                Console.Write(seg[0]);
            }

            void SetDp()
            {

                for (int i = 1; i <= k; i++)
                {

                    Array.Fill(seg, 0);

                    for (int j = 0; j < n; j++)
                    {

                        Update(0, n, arr[j], dp[i - 1][j]);
                        dp[i][j] = GetVal(0, n, arr[j] - 1);
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

                int log = (int)(Math.Ceiling(Math.Log2(n + 1))) + 1;
                seg = new int[1 << log];

                dp = new int[k + 1][];
                for (int i = 0; i <= k; i++)
                {

                    dp[i] = new int[n];
                }

                Array.Fill(dp[0], 1);

                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
            }

            int GetVal(int _s, int _e, int _chk, int _idx = 0)
            {

                if (_chk < _s) return 0;
                else if (_e <= _chk) return seg[_idx];

                int mid = (_s + _e) >> 1;
                int ret = GetVal(_s, mid, _chk, _idx * 2 + 1)
                    + GetVal(mid + 1, _e, _chk, _idx * 2 + 2);

                return ret % MOD;
            }

            void Update(int _s, int _e, int _chk, int _val, int _idx = 0)
            {

                if (_s == _e)
                {

                    seg[_idx] = (seg[_idx] + _val) % MOD;
                    return;
                }

                int mid = (_s + _e) >> 1;
                if (mid < _chk) Update(mid + 1, _e, _chk, _val, _idx * 2 + 2);
                else Update(_s, mid, _chk, _val, _idx * 2 + 1);

                seg[_idx] = (seg[_idx * 2 + 2] + seg[_idx * 2 + 1]) % MOD;
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
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

#nullable disable

public static class DeconstructHelper
{
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2) => (v1, v2) = (arr[0], arr[1]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3) => (v1, v2, v3) = (arr[0], arr[1], arr[2]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4) => (v1, v2, v3, v4) = (arr[0], arr[1], arr[2], arr[3]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5) => (v1, v2, v3, v4, v5) = (arr[0], arr[1], arr[2], arr[3], arr[4]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6) => (v1, v2, v3, v4, v5, v6) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7) => (v1, v2, v3, v4, v5, v6, v7) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
    public static void Deconstruct<T>(this T[] arr, out T v1, out T v2, out T v3, out T v4, out T v5, out T v6, out T v7, out T v8) => (v1, v2, v3, v4, v5, v6, v7, v8) = (arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
}


public sealed class SumSeg
{
    private long[] _tree;
    private int _leafMask;

    public SumSeg(int size)
    {
        var initSizeLog = 1 + BitOperations.Log2((uint)size - 1);

        _leafMask = 1 << initSizeLog;
        var treeSize = 2 * _leafMask;

        _tree = new long[treeSize];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Increase(int index, long diff)
    {
        var mod = 1_000_000_007;
        var curr = _leafMask | index;

        while (curr != 0)
        {
            _tree[curr] = (_tree[curr] + diff + mod) % mod;
            curr >>= 1;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public long Range(int stIncl, int edExcl)
    {
        var mod = 1_000_000_007;
        var leftNode = _leafMask | stIncl;
        var rightNode = _leafMask | (edExcl - 1);

        var aggregated = 0L;
        while (leftNode <= rightNode)
        {
            if ((leftNode & 1) == 1)
                aggregated = (aggregated + _tree[leftNode++]) % mod;
            if ((rightNode & 1) == 0)
                aggregated = (aggregated + _tree[rightNode--]) % mod;

            leftNode >>= 1;
            rightNode >>= 1;
        }

        return aggregated;
    }
}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var (n, k) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var a = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();

        var segs = new SumSeg[k];
        var maxa = 1 + a.Max();

        for (var idx = 0; idx < segs.Length; idx++)
            segs[idx] = new SumSeg(1 + maxa);

        foreach (var v in a.Reverse())
        {
            for (var idx = 0; idx < segs.Length - 1; idx++)
            {
                var count = segs[idx + 1].Range(v + 1, maxa);
                segs[idx].Increase(v, count);
            }

            segs[^1].Increase(v, 1);
        }

        sw.WriteLine(segs[0].Range(0, maxa));
    }
}
#elif other2
// #include<stdio.h>
// #include<iostream>
// #include<vector>
// #include<string>
// #include<queue>
// #include<deque>
// #include<set>
// #include<map>
// #include<algorithm>
// #include<math.h>
// #include<string.h>
using namespace std;

// #define INF 1234567890
// #define ll long long
// #define MOD 1'000'000'007

int N, K;
int v[100101];
ll dp[11][100101], fen[100101];

ll Sum(int idx)
{
	ll ret = 0;
	while (idx)
	{
		ret += fen[idx];
		idx &= (idx - 1);
	}
	ret %= MOD;
	return ret;
}

void Add(int idx, ll val)
{
	while (idx <= N)
	{
		fen[idx] += val;
		idx += idx & -idx;
	}
}

int main()
{
	ios::sync_with_stdio(0);
	cin.tie(0);
	cin >> N >> K;
	for (int i = 0; i < N; i++)
	{
		cin >> v[i];
		dp[1][i] = 1;
	}

	for (int k = 2; k <= K; k++)
	{
		memset(fen, 0, sizeof(fen));
		for (int i = 0; i < N; i++)
		{
			dp[k][i] = Sum(v[i] - 1);
			Add(v[i], dp[k - 1][i]);
		}
	}

	ll res = 0;
	for (int i = 0; i < N; i++)
		res += dp[K][i];
	res %= MOD;
	cout << res;
	return 0;
}
#endif
}
