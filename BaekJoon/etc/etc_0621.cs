using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 킬로미터를 마일로
    문제번호 : 6504번

    수학, 구현 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0621
    {

        static void Main621(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int[] fibo;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                int test = ReadInt();

                PreCalc();

                while(test-- > 0)
                {

                    int n = ReadInt();
                    int ret = 0;
                    for (int i = 20; i >= 1; i--)
                    {

                        if (n < fibo[i]) continue;
                        n -= fibo[i];
                        ret += fibo[i - 1];
                        if (n == 0) break;
                    }

                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void PreCalc()
            {

                fibo = new int[21];
                fibo[0] = 1;
                fibo[1] = 2;

                for (int i = 2; i < 21; i++)
                {

                    fibo[i] = fibo[i - 1] + fibo[i - 2];
                }
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
// #define fastio ios::sync_with_stdio(0), cin.tie(0), cout.tie(0)
using namespace std;

typedef long long ll;
ll f[26]{ 1, 2 };

int main() {
	fastio;
	for (int i = 2; i <= 25; i++) f[i] = f[i - 1] + f[i - 2];
	int N; cin >> N;
	while (N--) {
		int n, t = 0; cin >> n;
		for (int i = 25; i > 0; i--) {
			if (f[i] <= n) {
				n -= f[i];
				t += f[i - 1];
			}
		}
		cout << t << '\n';
	}
}
#endif
}
