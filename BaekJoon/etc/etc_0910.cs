using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. -
이름 : 배성훈
내용 : 유니콘
    문제번호 : 1048번

    dp, 누적합 문제다
    아이디어는 다음과 같다

    
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   X   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0

    X에 있는 유니콘이 있으면 이동할 수 있는 범위를 
    1로 표현하면 다음과 같다

        1   1   1   1   0   0   0   1   1   1   1
        1   1   1   1   0   0   0   1   1   1   1
        1   1   1   1   0   0   0   1   1   1   1
        1   1   1   0   0   0   0   0   1   1   1
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   X   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        1   1   1   0   0   0   0   0   1   1   1
        1   1   1   1   0   0   0   1   1   1   1
        1   1   1   1   0   0   0   1   1   1   1
        1   1   1   1   0   0   0   1   1   1   1

    영역을 보면 다음과 같다
    사각형 영역으로 근사하게 쪼개면 다음과 같고
    각각 X와 가까운 모서리 부분의 값을 빼면 된다

        1   1   1   1|  0   0   0  |1   1   1   1
        1   1   1   1|  0   0   0  |1   1   1   1
        1   1   1   1|  0   0   0  |1   1   1   1
        1   1   1   0|  0   0   0  |0   1   1   1
        -   -   -   -               -   -   -   -
        0   0   0   0   0   0   0   0   0   0   0
        0   0   0   0   0   X   0   0   0   0   0
        0   0   0   0   0   0   0   0   0   0   0
        -   -   -   -               -   -   -   -
        1   1   1   0|  0   0   0  |0   1   1   1
        1   1   1   1|  0   0   0  |1   1   1   1
        1   1   1   1|  0   0   0  |1   1   1   1
        1   1   1   1|  0   0   0  |1   1   1   1

    그래서 dp, sum배열을 정의하기로
    dp[l][r][c]를 r, c에서 1 ~ l번째 문자까지 매칭하는 모든 경우의 수
    sum[r][c]를 0, 0에서 r, c 범위안의 dp의 모든 누적합으로 하면 
    R: 맵의 행, C: 맵의 열, Find: 찾을 문자열의 길이라하면
    시간복잡도는 O(R x C x Find)가되고 로직도 맞아 풀만하다 여겼다

    sum은 재활용해서 썼지만, dp도 2개로 재활용해서 쓰면 좋을거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0910
    {

        static void Main910(string[] args)
        {

            int MOD = 1_000_000_007;
            StreamReader sr;
            int row, col, l;

            int[][] board;
            int[][][] dp;
            int[][] sum;
            int[] match;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        /*
                        
                        첫 번째 문자 일치해야 1
                        아니면 0
                        */
                        dp[1][r][c] = board[r][c] == match[1] ? 1 : 0;
                    }
                }

                // 1번 누적합 계산
                FillSum(1);

                for (int i = 2; i <= l; i++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        for (int c = 1; c <= col; c++)
                        {

                            /*
                            
                            i번째 일치하는 문자 경우의 수 찾기
                            */
                            if (board[r][c] != match[i]) continue;
                            dp[i][r][c] = GetAreaSum(i, r, c);
                        }
                    }

                    // i 번 누적합 계산
                    FillSum(i);
                }

                // 결과 찾기
                int ret = 0;
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        ret = (ret + dp[l][r][c]) % MOD;
                    }
                }

                Console.Write(ret);
            }

            int GetAreaSum(int _l, int _r, int _c)
            {

                /*
                 
                이전 경로에서 해당 경로로 올 수 있는 모든 경우의 수를 찾는다 
                */
                long ret = GetSquareSum(0, 0, _r - 2, _c - 2);
                ret += GetSquareSum(_r + 1, _c + 1, row, col);
                ret += GetSquareSum(_r + 1, 0, row, _c - 2);
                ret += GetSquareSum(0, _c + 1, _r - 2, col);

                ret -= GetSub(_l, _r - 2, _c - 2)
                    + GetSub(_l, _r - 2, _c + 2)
                    + GetSub(_l, _r + 2, _c - 2)
                    + GetSub(_l, _r + 2, _c + 2);

                ret %= MOD;
                if (ret < 0) ret += MOD;

                return (int)ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                /*
                 
                유효 좌표 확인 
                */
                return _r < 0 || _c < 0 || _r > row || _c > col;
            }

            long GetSub(int _l, int _r, int _c)
            {

                /*
                 
                포함되면 안되는 구간 
                */
                if (ChkInvalidPos(_r, _c)) return 0L;
                return dp[_l - 1][_r][_c];
            }

            long GetSquareSum(int _r1, int _c1, int _r2, int _c2)
            {

                /*
                
                사각형 구간의 합을 찾는다
                */

                // 구간이 존재하지 않으면 0
                if (ChkInvalidPos(_r1, _c1) || ChkInvalidPos(_r2, _c2)) return 0L;

                long ret = sum[_r2][_c2];
                ret = ret - sum[_r2][_c1] - sum[_r1][_c2] + sum[_r1][_c1];

                return ret % MOD;
            }

            void FillSum(int _l)
            {

                /*
                 
                사각형 누적합
                sum[r][c] 는 0, 0에서 r, c 사각형 안의 누적합
                */
                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        sum[r][c] = (dp[_l][r][c] + sum[r][c - 1]) % MOD;
                    }
                }

                for (int c = 1; c <= col; c++)
                {

                    for (int r = 1; r <= row; r++)
                    {

                        sum[r][c] = (sum[r][c] + sum[r - 1][c]) % MOD;
                    }
                }
            }

            void SetDp()
            {

                dp = new int[l + 1][][];

                for (int i = 0; i <= l; i++)
                {

                    dp[i] = new int[row + 1][];
                    for (int r = 0; r <= row; r++)
                    {

                        dp[i][r] = new int[col + 1];
                    }
                }

                sum = new int[row + 1][];
                for (int r = 0; r <= row; r++)
                {

                    sum[r] = new int[col + 1];
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                row = int.Parse(temp[0]);
                col = int.Parse(temp[1]);

                string str = sr.ReadLine().Trim();
                l = str.Length;

                match = new int[l + 1];
                for (int i = 1; i <= l; i++)
                {

                    match[i] = str[i - 1];
                }

                board = new int[row + 1][];
                for (int r = 1; r <= row; r++)
                {

                    board[r] = new int[col + 1];

                    for (int c = 1; c <= col; c++)
                    {

                        board[r][c] = sr.Read();
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }
        }
    }

#if other
using ProblemSolving.Templates.Utility;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using mint = ProblemSolving.Templates.ModInt<ProblemSolving.Templates.ModInt.Mod1000000007>;
namespace ProblemSolving.Templates.Utility {}
namespace System {}
namespace System.Data {}
namespace System.Diagnostics.CodeAnalysis {}
namespace System.IO {}
namespace System.Linq {}

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        Solve(sr, sw);
    }

    public static void Solve(StreamReader sr, StreamWriter sw)
    {
        var (n, m, l) = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var s = sr.ReadLine();

        var map = new string[n];
        for (var idx = 0; idx < n; idx++)
            map[idx] = sr.ReadLine();

        var dp = new mint[n, m];
        var backup = new mint[n, m];

        for (var y = 0; y < n; y++)
            for (var x = 0; x < m; x++)
                dp[y, x] = map[y][x] == s[0] ? 1 : 0;

        var ysum = new mint[n];
        var xsum = new mint[m];
        var moveset = new[]
        {
            (2, 2), (2, -2), (-2, 2), (-2, -2)
        };

        foreach (var ch in s.Skip(1))
        {
            Array.Clear(backup);
            Array.Clear(xsum);
            Array.Clear(ysum);

            var gsum = (mint)0;
            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                {
                    gsum += dp[y, x];
                    xsum[x] += dp[y, x];
                    ysum[y] += dp[y, x];
                }

            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                    if (map[y][x] == ch)
                    {
                        var newv = gsum;

                        for (var dy = -1; dy <= 1; dy++)
                            if (0 <= y + dy && y + dy < n)
                                newv -= ysum[y + dy];

                        for (var dx = -1; dx <= 1; dx++)
                            if (0 <= x + dx && x + dx < m)
                                newv -= xsum[x + dx];

                        for (var dy = -1; dy <= 1; dy++)
                            for (var dx = -1; dx <= 1; dx++)
                            {
                                var (ny, nx) = (y + dy, x + dx);
                                if (ny < 0 || nx < 0 || ny >= n || nx >= m)
                                    continue;

                                newv += dp[ny, nx];
                            }

                        foreach (var (dy, dx) in moveset)
                        {
                            var (ny, nx) = (y + dy, x + dx);
                            if (ny < 0 || nx < 0 || ny >= n || nx >= m)
                                continue;

                            newv -= dp[ny, nx];
                        }

                        backup[y, x] = newv;
                    }

            (dp, backup) = (backup, dp);
        }

        var ans = (mint)0;
        for (var y = 0; y < n; y++)
            for (var x = 0; x < m; x++)
                ans += dp[y, x];

        sw.WriteLine(ans);
    }
}

namespace ProblemSolving.Templates.Utility
{
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
}


namespace ProblemSolving.Templates
{
    public struct ModInt
    {
        public struct Mod998244353 : IModIntInterface
        {
            public long GetMod() => 998244353;
        }
        public struct Mod1000000007 : IModIntInterface
        {
            public long GetMod() => 1000000007;
        }
    }
    public struct ModInt<TOp> : IEquatable<ModInt<TOp>>
        where TOp : struct, IModIntInterface
    {
        public long V;
        public long Mod => default(TOp).GetMod();

        public ModInt()
            : this(0)
        {
        }
        public ModInt(long v)
        {
            var mod = default(TOp).GetMod();

            if (v < 0)
                v = (-v + mod - 1) / mod * mod;
            else if (v >= mod)
                v = v % mod;

            V = v;
        }

        public static implicit operator ModInt<TOp>(long val) => new ModInt<TOp>(val);
        public static ModInt<TOp> operator +(ModInt<TOp> l, ModInt<TOp> r)
        {
            var mod = default(TOp).GetMod();
            var newv = l.V + r.V;

            if (newv > mod)
                newv -= mod;

            return new ModInt<TOp>(newv);
        }
        public static ModInt<TOp> operator -(ModInt<TOp> l, ModInt<TOp> r)
        {
            var mod = default(TOp).GetMod();
            var newv = l.V - r.V;

            if (newv < 0)
                newv += mod;

            return new ModInt<TOp>(newv);
        }
        public static ModInt<TOp> operator *(ModInt<TOp> l, ModInt<TOp> r)
        {
            var mod = default(TOp).GetMod();
            var newv = l.V * r.V;

            if (newv > mod)
                newv %= mod;

            return new ModInt<TOp>(newv);
        }
        public static ModInt<TOp> operator /(ModInt<TOp> l, ModInt<TOp> r)
        {
            var rinv = r.ModInv();
            return l * rinv;
        }

        public ModInt<TOp> ModInv()
        {
            var mod = default(TOp).GetMod();
            return new ModInt<TOp>(NumberTheory.FastPow(V, mod - 2, mod));
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ModInt<TOp> other && this.Equals(other);
        }
        public bool Equals(ModInt<TOp> other)
        {
            return this.V == other.V;
        }

        public override int GetHashCode() => V.GetHashCode();
        public override string ToString() => V.ToString();

        public static ModInt<TOp> Pow(ModInt<TOp> p, long m)
        {
            var rv = (ModInt<TOp>)1;
            while (m > 0)
            {
                if ((m & 1) == 1)
                    rv *= p;

                p *= p;
                m >>= 1;
            }

            return rv;
        }
    }
}

namespace ProblemSolving.Templates
{
    public interface IModIntInterface
    {
        long GetMod();
    }
}


namespace ProblemSolving.Templates
{
    public static class NumberTheory
    {
        public static long GCD(long x, long y)
        {
            while (x > 0 && y > 0)
                if (x > y) x %= y;
                else y %= x;

            return Math.Max(x, y);
        }

        public static long FastPow(long b, long p, long mod)
        {
            var rv = 1L;
            while (p != 0)
            {
                if ((p & 1) == 1)
                    rv = rv * b % mod;

                b = b * b % mod;
                p >>= 1;
            }

            return rv;
        }
    }
}

#elif other2
// #include <iostream>
using namespace std;

typedef long long ll;

int n,m,L;
char map[304][304];
char s[51];
int dp[2][304][304];
int acc[304][304];

int main(){

	cin >> n >> m >> L;
	cin >> s;
	int i,j,k;
	ll p=1000000007;
	for(i=2; i<n+2; i++){
		cin >> (map[i]+2);
	}
	for(i=2; i<n+4; i++){
		for(j=2; j<m+4; j++){
			dp[0][i][j] = map[i][j]==s[0];
			acc[i][j] = dp[0][i][j]+acc[i][j-1]+acc[i-1][j]-acc[i-1][j-1];
		}
	}

	for(k=1; s[k]!=0; k++){
		for(i=2; i<n+2; i++){
			for(j=2; j<m+2; j++){
				if(map[i][j]==s[k]){
					ll a=0;
					a  = (ll)acc[n+3][m+3]-acc[i+1][m+3]-acc[n+3][j+1]+acc[i+1][j+1];
					a += (ll)acc[i-2][m+3]-acc[i-2][j+1]+acc[n+3][j-2]-acc[i+1][j-2];
					a += (ll)acc[i-2][j-2];
					a -= (ll)dp[(k+1)%2][i-2][j-2]+dp[(k+1)%2][i-2][j+2]+dp[(k+1)%2][i+2][j-2]+dp[(k+1)%2][i+2][j+2];
					dp[k%2][i][j] = (a+8*p)%p;
				}
				else{
					dp[k%2][i][j] = 0;
				}
			}
		}
		for(i=2; i<n+4; i++){
			for(j=2; j<m+4; j++){
				acc[i][j] = ((ll)dp[k%2][i][j]+acc[i][j-1]+acc[i-1][j]-acc[i-1][j-1]+p)%p;
			}
		}
	}

	cout << acc[n+3][m+3] <<'\n';

	return 0;
}

#endif
}
