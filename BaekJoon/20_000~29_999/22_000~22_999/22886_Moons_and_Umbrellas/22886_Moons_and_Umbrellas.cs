using System;
using System.IO;

/*
날짜 : 2024. 10. 18
이름 : 배성훈
내용 : Moons and Umbrellas
    문제번호 : 22886번

    dp, 그리디 문제다
    바텀 - 업 dp 로 해결했다
*/

namespace BaekJoon.etc
{
    internal class etc_1066
    {

        static void Main1066(string[] args)
        {

            string CASE = "Case #";
            string AND = ": ";

            int INF = 1_000_000;
            StreamReader sr;
            StreamWriter sw;

            int x, y, len;
            int ret;
            int[] str;
            int[][] dp;

            Solve();
            void Solve()
            {

                Init();

                int test = ReadInt();
                for (int t = 1; t <= test; t++)
                {

                    Input();

                    GetRet();

                    sw.Write($"{CASE}{t}{AND}{ret}\n");
                }

                sr.Close();
                sw.Close();
            }
            
            void GetRet()
            {

                for (int i = 0; i < len; i++)
                {

                    dp[0][i] = INF;
                    dp[1][i] = INF;
                }

                ret = Math.Min(DFS(0, 0), DFS(1, 0));
            }
            
            int DFS(int _idx1, int _idx2)
            {

                // ? 가 아니고 J로 채울려고 시도하면 INF를 반환해 탐색 경로를 끊는다
                if (str[_idx2] != '?' && (str[_idx2] == 'C') != (_idx1 == 0)) return INF;

                // ? 이거나 옳은 탐색 경로
                int ret = dp[_idx1][_idx2];

                if (ret != INF) return ret;
                ret = 0;

                if (_idx2 + 1 < len)
                {

                    // 아직 길이가 남아있다면 해당 경로로 탐색 시도
                    int r1 = DFS(_idx1, _idx2 + 1);
                    // 변환한 경우로 탐색 시도
                    int r2 = DFS(1 - _idx1, _idx2 + 1) + (_idx1 == 1 ? y : x);

                    // 둘 중 하나는 적어도 탐색된다
                    ret = Math.Min(r1, r2);
                }

                return dp[_idx1][_idx2] = ret;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                dp = new int[2][];
                dp[0] = new int[1_000];
                dp[1] = new int[1_000];

                str = new int[1_000];
            }

            void Input()
            {

                x = ReadInt();
                y = ReadInt();
                len = 0;

                ReadStr();

                void ReadStr()
                {

                    int c;
                    
                    while ((c = sr.Read()) == 'C' || c == 'J' || c == '?')
                    {

                        str[len++] = c;
                    }

                    while (c != '\n') c = sr.Read();
                }
            }

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

#if other
// #include <stdio.h>
// #include <string.h>
// #define MAX 999999999
int min(int a, int b)
{
	if (a < b) return a;
	else return b;
}
int main()
{
	int testcase, t, x, y, len, i, j;
	char c[1002];
	int cost[1002][2];
	scanf("%d", &testcase);
	for (t = 1; t <= testcase; t++)
	{
		scanf("%d %d", &x, &y);
		scanf("%s", c);
		len = strlen(c);
		
		cost[0][0] = 0;
		cost[0][1] = 0;
		if (c[0] == 'C') cost[0][1] = MAX;
		else if (c[0] == 'J') cost[0][0] = MAX;

		for (i = 1; i < len; i++)
		{
			cost[i][0] = min(cost[i - 1][0], cost[i - 1][1] + y);
			cost[i][1] = min(cost[i - 1][0] + x, cost[i - 1][1]);
			if (c[i] == 'C')
			{
				cost[i][1] = MAX;
			}
			else if (c[i] == 'J')
			{
				cost[i][0] = MAX;	
			}
		}
		
		printf("Case #%d: %d\n", t, min(cost[len - 1][0], cost[len - 1][1]));
	}
	return 0;
}
#endif
}
