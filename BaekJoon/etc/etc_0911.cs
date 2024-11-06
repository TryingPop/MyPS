using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 8. 26
이름 : 배성훈
내용 : 보드게임
    문제번호 : 2572번

    dp, 그래프 이론 문제다
    특정 노드에서 간선별로 이동하며 찾았다
    시간 복잡도는 O(N x K)이다
*/

namespace BaekJoon.etc
{
    internal class etc_0911
    {

        static void Main911(string[] args)
        {

            StreamReader sr;
            int n, m, k;
            int[] color;
            List<(int dst, int color)>[] edge;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                SetDp();

                GetRet();
            }

            void GetRet()
            {

                int ret = 0;
                for (int i = 1; i <= m; i++)
                {

                    ret = Math.Max(ret, dp[0][i]);
                }

                Console.Write(ret);
            }

            void SetDp()
            {

                dp = new int[2][];
                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[m + 1];
                    Array.Fill(dp[i], -1);
                }

                dp[0][1] = 0;

                for (int i = 0; i < n; i++)
                {

                    for (int j = 1; j <= m; j++)
                    {

                        if (dp[0][j] == -1) continue;
                        for (int k = 0; k < edge[j].Count; k++)
                        {

                            int nextScore = dp[0][j];
                            if (color[i] == edge[j][k].color)
                                nextScore += 10;

                            dp[1][edge[j][k].dst] = 
                                Math.Max(dp[1][edge[j][k].dst], nextScore);
                        }
                    }

                    for (int j = 1; j <= m; j++)
                    {

                        dp[0][j] = dp[1][j];
                        dp[1][j] = -1;
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                color = new int[n];
                for (int i = 0; i < n; i++)
                {

                    color[i] = ReadColor();
                }

                m = ReadInt();
                k = ReadInt();

                edge = new List<(int dst, int color)>[m + 1];
                for (int i = 1; i <= m; i++)
                {

                    edge[i] = new();
                }

                for (int i = 0; i < k; i++)
                {

                    int f = ReadInt();
                    int b = ReadInt();

                    int c = ReadColor();

                    edge[f].Add((b, c));
                    edge[b].Add((f, c));
                }

                sr.Close();
            }

            int ReadColor()
            {

                int c = sr.Read();
                if (sr.Read() == '\r') sr.Read();

                switch(c)
                {

                    case 'R':
                        return 1;

                    case 'G':
                        return 2;

                    case 'B':
                        return 3;

                    default:
                        return -1;
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
using System;
using System.Collections.Generic;

namespace Onekara
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            string[] ss = s.Split();

            int n = int.Parse(ss[0]);

            s = Console.ReadLine();

            string[] cards = s.Split();

            s = Console.ReadLine();
            ss = s.Split();

            int m = int.Parse(ss[0]);
            int k = int.Parse(ss[1]);

            List<Tuple<int, int, string>> graph = new List<Tuple<int, int, string>>();
            for (int i = 0; i < k; i++)
            {
                s = Console.ReadLine();
                ss = s.Split();

                graph.Add(new Tuple<int, int, string>(int.Parse(ss[0]), int.Parse(ss[1]), ss[2]));
            }

            int[] points = new int[m + 2];
            int[] nextPoints = new int[m + 2];

            for (int i = 2; i <= m; i++)
                points[i] = int.MinValue;
            for(int i=0; i<=m; i++)
                nextPoints[i] = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                string card = cards[i];
                foreach (var v in graph)
                {
                    int left = v.Item1;
                    int right = v.Item2;

                    int nextLeft = points[right] + ((v.Item3 == card) ? 10 : 0);
                    if (nextPoints[left] < nextLeft)
                        nextPoints[left] = nextLeft;

                    int nextRight = points[left] + ((v.Item3 == card) ? 10 : 0);
                    if (nextPoints[right] < nextRight)
                        nextPoints[right] = nextRight;
                }
                points = (int[])nextPoints.Clone();
            }

            int maxVal = 0;
            for (int i = 1; i <= m; i++)
            {
                if (points[i] > maxVal)
                    maxVal = points[i];
            }

            Console.WriteLine(maxVal);
        }
    }
}
#elif other2
// #include<cstdio>
// #include<algorithm>
using namespace std;
struct st {
	int x, y;
	char c;
}e[10000];
int n, m, k, dp1[501], dp2[501];
char c[1000];
int main() {
	scanf("%d", &n);
	for (int i = 0; i < n; i++) scanf(" %c", c + i);
	scanf("%d%d", &m, &k);
	for (int i = 0; i < k; i++) scanf("%d%d %c", &e[i].x, &e[i].y, &e[i].c);
	fill(dp1 + 2, dp1 + 1 + m, -1e6);
	for (int i = 0; i < n; i++) {
		fill(dp2+1, dp2 + 1 + m, -1e6);
		for (int j = 0; j < k; j++) {
			dp2[e[j].y] = max(dp2[e[j].y], dp1[e[j].x] + (c[i] == e[j].c));
			dp2[e[j].x] = max(dp2[e[j].x], dp1[e[j].y] + (c[i] == e[j].c));
		}
		copy(dp2, dp2 + 1 + m, dp1);
	}
	printf("%d", *max_element(dp1, dp1 + 1 + m) * 10);
	return 0;
}
#endif
}
