using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 26
이름 : 배성훈
내용 : 초코칩 케이크
    문제번호 : 23823번

    수학, 애드혹 문제다
    아이디어는 다음과 같다
    초코 칩이 많이 쌓인 조각들의 개수를 찾는 문제다
    초코칩은 가로 or 세로만 쌓으므로 가로, 세로 누적된 경우만 기록했다

    그리고 가로 최대값을 갖는 구간의 개수와, 세로 최대값을 갖는 구간의 개수를
    곱해서 초코칩이 가장 많이 쌓인 조각들을 세었다

    이렇게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0629
    {

        static void Main629(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            int[] w;
            int[] h;

            Solve();

            void Solve()
            {

                Init();
                int len = ReadInt();
                int wMax = 0;
                int hMax = 0;
                int wLen = n;
                int hLen = n;

                for (int i = 0; i < len; i++)
                {

                    int op = ReadInt();
                    int line = ReadInt();

                    if (op == 1)
                    {

                        w[line]++;
                        int cur = w[line];
                        if (cur == wMax) wLen++;
                        else if(wMax < cur)
                        {

                            wMax = cur;
                            wLen = 1;
                        }
                    }
                    else
                    {

                        h[line]++;
                        int cur = h[line];
                        if (cur == hMax) hLen++;
                        else if (hMax < cur)
                        {

                            hMax = cur;
                            hLen = 1;
                        }
                    }

                    long ret = wLen * (long)hLen;
                    sw.Write($"{ret}\n");
                }

                sr.Close();
                sw.Close();
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 8);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536 * 4);

                n = ReadInt();

                w = new int[n + 1];
                h = new int[n + 1];
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nq = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nq[0];
        var q = nq[1];

        var argmaxX = Enumerable.Range(0, n).ToList();
        var argmaxY = Enumerable.Range(0, n).ToList();
        var x = new int[n];
        var y = new int[n];

        while (q-- > 0)
        {
            var query = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
            var idx = query[1] - 1;

            if (query[0] == 1)
            {
                var maxx = x[argmaxX[0]];
                x[idx]++;

                if (maxx <= x[idx])
                {
                    if (maxx < x[idx])
                        argmaxX.Clear();

                    argmaxX.Add(idx);
                }
            }
            else
            {
                var maxy = y[argmaxY[0]];
                y[idx]++;

                if (maxy <= y[idx])
                {
                    if (maxy < y[idx])
                        argmaxY.Clear();

                    argmaxY.Add(idx);
                }
            }

            sw.WriteLine(argmaxX.Count * argmaxY.Count);
        }
    }
}

#elif other2
// #include <iostream>
// #include <vector>

using namespace std;



int main() {
	ios::sync_with_stdio(false);
	cin.tie(NULL);
	int colmax = 0,rowmax=0;
	int colmaxnum = 0,rowmaxnum=0;
	int n, q,t,a;
	cin >> n >> q;
	int col[30001]={0};
	int row[30001]={0};
	while (q--) {
		cin >> t >> a;
		a = a - 1;
		if (t == 1) {
			col[a]++;
			if (col[a] > colmax) {
				colmax = col[a];
				colmaxnum = 1;
			}
			else if (col[a] == colmax) {
				colmaxnum++;
			}

		}
		else{
			row[a]++;
			if (row[a] > rowmax) {
				rowmax = row[a];
				rowmaxnum = 1;
			}
			else if (row[a] == rowmax) {
				rowmaxnum++;
			}
		}
		if (colmaxnum == 0) {
			cout<<rowmaxnum* n<<'\n';
		}
		else if (rowmaxnum == 0) {
			cout << colmaxnum* n << '\n';
		}
		else cout<< colmaxnum*rowmaxnum<<'\n';
	}
	//cout << max << '\n' << maxnum;
	return 0;
}
#endif
}
