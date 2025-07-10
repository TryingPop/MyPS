using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 10
이름 : 배성훈
내용 : Computers
    문제번호 : 3760번

    dp 문제다.
    문제에서 언급했듯이 케이스간 띄어쓰기 줄바꿈이 많다.
    C#에서 제공하는 Trim만으론 해결 안된다.
    직접 입출력을 관리하는게 중요하다.

    dp 풀이는 쉽다.
    dp[i] = val 를 i일에 가장 작은 값이라 한다.
    그러면 배낭형식으로 최솟값을 찾을 수 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1759
    {

        static void Main1759(string[] args)
        {

            int MAX = 1_000;
            int INF = 1_000_000_009;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] dp = new int[MAX + 1];


            int[][] m = new int[MAX + 1][];
            for (int i = 1; i <= MAX; i++)
            {

                m[i] = new int[MAX + 1];
            }

            int c;
            while ((c = ReadInt()) != -1)
            {

                int n = ReadInt();

                for (int i = 1; i <= n; i++)
                {

                    for (int j = i, k = 0; j <= n; j++, k++)
                    {

                        m[i][j] = ReadInt();
                    }
                }
                
                Array.Fill(dp, INF);
                dp[0] = 0;

                for (int i = 1; i <= n; i++)
                {

                    for (int j = i; j <= n; j++)
                    {

                        dp[j] = Math.Min(dp[j], dp[i - 1] + c + m[i][j]);
                    }
                }

                sw.Write(dp[n]);
                sw.Write('\n');
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();

                    while (c != -1 && (c < '0' || c > '9')) 
                    {

                        c = sr.Read();
                    }

                    if (c == -1)
                    {

                        ret = -1;
                        return false;
                    }

                    ret = c - '0';

                    while((c = sr.Read()) <= '9' && '0' <= c)
                    {

                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
import java.awt.*;
import java.awt.event.*;
import java.awt.geom.*;
import java.io.*;
import java.math.*;
import java.text.*;
import java.util.*;

public class Main {
	static BufferedReader br;
	static StringTokenizer st;
	static PrintWriter pw;

	public static void main(String[] args) throws IOException	{
		br = new BufferedReader(new InputStreamReader(System.in));
		pw = new PrintWriter(new BufferedWriter(new OutputStreamWriter(System.out)));
		while(true)	{
			int c = readInt();
			int n = readInt();
			int[][] go = new int[n+1][n+1];
			for(int i = 1; i <= n; i++)	{
				for(int j = i; j <= n; j++)		{
					go[i][j] = readInt();
				}
			}
			int[] dp = new int[n+1];
			Arrays.fill(dp, Integer.MAX_VALUE);
			dp[0] = 0;
			for(int i = 1; i <= n; i++)	{
				for(int j = 1; j <= i; j++)	{
					dp[i] = Math.min(dp[i], dp[j-1] + c + go[j][i]);
				}
			}
			pw.println(dp[n]);
		}
	}



	public static long readLong() throws IOException	{
		return Long.parseLong(nextToken());
	}
	public static double readDouble() throws IOException	{
		return Double.parseDouble(nextToken());
	}
	public static int readInt() throws IOException	{
		return Integer.parseInt(nextToken());
	}
	public static short readShort() throws IOException	{
		return Short.parseShort(nextToken());
	}
	public static String nextToken() throws IOException	{
		while(st == null || !st.hasMoreTokens())	{
			if(!br.ready())	{
				pw.close();
				System.exit(0);
			}
			st = new StringTokenizer(br.readLine().trim());
		}
		return st.nextToken();
	}
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;
using pii = pair<int, int>;

void fast_io() {
  cin.tie(nullptr)->sync_with_stdio(false);
}

const ll INF = 1e18;

int c;
void solve() {
  int n; cin >> n;
  vector<vector<int>> U(n + 1, vector<int>(n + 1));
  for (int i = 1; i <= n; ++i) for (int j = i; j <= n; ++j) cin >> U[i][j];

  vector<ll> dp(n + 1, INF); dp[0] = 0;
  for (int j = 1; j <= n; ++j) for (int i = 1; i <= j; ++i) {
    dp[j] = min(dp[j], dp[i - 1] + U[i][j] + c);
  }
  cout << dp[n] << '\n';
}

int main() {
  fast_io();

  while (cin >> c) solve();
}
#elif other3
// #include<bits/stdc++.h>
using namespace std;

int answer;
int N, C, k;
int d[2010];

int main() {
	ios_base::sync_with_stdio(false);
	cin.tie(NULL);
	while (cin >> C >> N) {
		memset(d, 0x3f, sizeof(d));
		d[0] = 0;
		for (int i = 0; i < N; i++) {
			for (int j = i + 1; j <= N; j++) {
				int x;
				cin >> x;
				d[j] = min(d[j], d[i] + x + C);
			}
		}
		cout << d[N]<<'\n';
	}
}
#endif
}
