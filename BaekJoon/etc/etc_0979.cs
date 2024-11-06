using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9.
이름 : 배성훈
내용 : 토러스
    문제번호 : 13253번

    수학 문제다
    도착할 확률을 찾아야한다
    예를들어 
    2 2 1 1인 경우 n, m은 2이다
    목표 지점은 1, 1 이다

    + 방향으로 이동하는 경우 1일차에 도착한다
    - 방향으로 이동하는 경우 마찬가지로 1일차에 도착한다
    그래서 1.0일이다

    3 3 1 1인 경우 n, m 은 3이다
    + 방향으로 이동하는 경우 1일차에 도착한다
    - 방향으로 이동하는 경우 2, 2로 가고 도착못한다

    2, 2에서 시작한다
    + 방향으로 이동하는 경우 0, 0 으로 다시 돌아온다
    - 방향으로 이동하는 경우 1, 1 로 2일차에 도착한다

    0, 0 에서 다시 시작한다
    이는 1일차와 같다
    이렇게 해가면 확률은 sig (k / ((1 / 2)^k)) 가 된다
    k = 1, .... 이다

    해당 식을 풀면
    f = sig (1 / 2)^k 라 하면
    f + (1 / 2)f + ((1 / 2)^2)f +....
    기하급수이므로 f = 1을 얻을 수 있다
    그러면 1 + 1 / 2 + (1 / 2)^2 + ....
    가 되고 해당 식 역시 기하급수다
    그래서 2임을 알 수 있다

    4 6 1 3역시 해당 방법처럼
    27.0을 얻을 수 있다

    그런데 해당 방법으로 확률을 구하니
    + 방향으로만 x, y로 이동한 값을 p, - 방향으로만 이동한 값을 m
    이라 하면 정답은 p * m 처럼 보였고 
    몇 개의 테스트를 돌린 결과 같음을 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_0979
    {

        static void Main979(string[] args)
        {

            int X, Y, x, y;
            int[][] map;

            Solve();
            void Solve()
            {

                Input();

                FillMap();

                GetRet();
            }

            void GetRet()
            {

                if (map[x][y] == -1)
                {

                    Console.Write(-1);
                    return;
                }

                int r = map[x][y];
                int l = map[X - 1][Y - 1] - r + 1;

                Console.Write($"{l * r:0.0}");
            }

            void FillMap()
            {

                Queue<(int x, int y)> q = new(2);
                q.Enqueue((0, 0));
                map[0][0] = 0;

                while (q.Count > 0)
                {

                    (int x, int y) node = q.Dequeue();

                    int nX = node.x == X - 1 ? 0 : node.x + 1;
                    int nY = node.y == Y - 1 ? 0 : node.y + 1;

                    if (map[nX][nY] != -1) continue;

                    map[nX][nY] = map[node.x][node.y] + 1;
                    q.Enqueue((nX, nY));
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();

                X = int.Parse(temp[0]);
                Y = int.Parse(temp[1]);
                x = int.Parse(temp[2]);
                y = int.Parse(temp[3]);

                map = new int[X][];

                for (int r = 0; r < X; r++)
                {

                    map[r] = new int[Y];
                    Array.Fill(map[r], -1);
                }
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstring>
int main() {
	int N, M, x, y, d[] = { 1,-1 }, cx, cy, i, j, l = 0, r = 0, vi[10][10], C, can = 1;
	scanf("%d%d%d%d", &N, &M, &x, &y);
    for (i = 0; i < 2; i++) {
		cx = cy = 0;
		memset(vi, 0, sizeof vi);
		while (!vi[cx][cy] && (cx != x || cy != y)) {
			vi[cx][cy] = 1;
			cx = (cx + d[i] + N) % N;
			cy = (cy + d[i] + M) % M;
			(i ? r : l)++;
		}
		can &= !vi[cx][cy];
	}
    l--, r--;
	C = l + r + 1;
	printf("%d", can ? (r + 1) * (C - r) : -1);
	return 0;
}
#elif other2
// #include <bits/stdc++.h>
using namespace std;

int n, m, x, y, dst, cnt;
int main() {
	cin >> n >> m >> x >> y;
	for(int i =1; i<n * m; ++i) {
		if (i % n == x && i % m == y) dst = i;
		if (i % n == 0 && i % m == 0) break;
		cnt++;
	}
	if (dst) cout << (cnt - dst + 1) * dst << ".0";
	else cout << -1;
}
#elif other3
using System;
using System.IO;
using System.Linq;

// #nullable disable

public static class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var nmxy = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var m = nmxy[0];
        var n = nmxy[1];
        var edx = nmxy[2];
        var edy = nmxy[3];

        var distribution = new double[n, m];
        distribution[0, 0] = 1;

        var buf = new double[n, m];
        var exp = 0.0;
        var moveCount = 0;

        while (moveCount <= 100000)
        {
            moveCount++;

            Array.Clear(buf);
            for (var y = 0; y < n; y++)
                for (var x = 0; x < m; x++)
                {
                    if (y == edy && x == edx)
                        continue;

                    buf[(y + 1) % n, (x + 1) % m] += 0.5 * distribution[y, x];
                    buf[(y + n - 1) % n, (x + m - 1) % m] += 0.5 * distribution[y, x];
                }

            var tmp = buf;
            buf = distribution;
            distribution = tmp;

            var prob = distribution[edy, edx];
            exp += moveCount * prob;
        }

        if (exp == 0)
            sw.WriteLine(-1);
        else
            sw.WriteLine(exp);
    }
}
#endif
}
