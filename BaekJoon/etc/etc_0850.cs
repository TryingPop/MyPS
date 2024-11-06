using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 30
이름 : 배성훈
내용 : Desity Map
    문제번호 : 8029번

    누적합, 브루트포스 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0850
    {

        static void Main850(string[] args)
        {

            StreamReader sr;

            int[][] sum;
            int[][] ret;

            int n, r;
            Solve();
            void Solve()
            {

                Input();

                GetRet();

                Output();
            }

            void Output()
            {

                StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        sw.Write($"{ret[i][j]} ");
                    }

                    sw.Write('\n');
                }

                sw.Close();
            }

            void GetRet()
            {

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        int minR = Math.Max(0, i - r - 1);
                        int maxR = Math.Min(n, i + r);

                        int minC = Math.Max(0, j - r - 1);
                        int maxC = Math.Min(n, j + r);

                        ret[i][j] = sum[maxR][maxC] - sum[maxR][minC]
                            - sum[minR][maxC] + sum[minR][minC];
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                r = ReadInt();

                sum = new int[n + 1][];
                sum[0] = new int[n + 1];
                ret = new int[n + 1][];

                for (int i = 1; i <= n; i++)
                {

                    sum[i] = new int[n + 1];
                    ret[i] = new int[n + 1];

                    for (int j = 1; j <= n; j++)
                    {

                        sum[i][j] = sum[i][j - 1] + ReadInt();
                    }
                }

                for (int i = 1; i <= n; i++)
                {

                    for (int j = 1; j <= n; j++)
                    {

                        sum[i][j] += sum[i - 1][j];
                    }
                }
                sr.Close();
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
// #include <iostream>
using namespace std;

int N, K;
int A[252][252];

int main() {
	ios::sync_with_stdio(0);
	cin.tie(0);

	cin >> N >> K;
	for (int r = 1; r <= N; r++) {
		for (int c = 1; c <= N; c++) {
			cin >> A[r][c];
			A[r][c] += A[r - 1][c] + A[r][c - 1] - A[r - 1][c - 1];
		}
	}

	for (int r = 1; r <= N; r++) {
		for (int c = 1; c <= N; c++) {
			int r1 = max(r - K, 1), c1 = max(c - K, 1);
			int r2 = min(r + K, N), c2 = min(c + K, N);
			cout << A[r2][c2] - A[r1 - 1][c2] - A[r2][c1 - 1] + A[r1 - 1][c1 - 1] << ' ';
		}
		cout << '\n';
	}
}
#endif
}
