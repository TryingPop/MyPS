using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 13
이름 : 배성훈
내용 : Miners
    문제번호 : 5475번

    dp 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1764
    {

        static void Main1764(string[] args)
        {

            int n;
            string str;

            Input();

            GetRet();

            void GetRet()
            {

                // idx로 음식 상태를 가져온다.
                // 현재와 이전꺼 이렇게 저장한다.
                // 1, 1인 경우는 1, 0으로  대체되므로 크기 줄인다.
                (int cur, int prev)[] foods = { (0, 0), (1, 0), (2, 0), (3, 0), (2, 1), (3, 1), (1, 2), (3, 2), (1, 3), (2, 3) };

                // 음식 상태를 idx로 변형하는 배열
                int[][] idx = new int[4][];
                for (int i = 0; i < 4; i++)
                {

                    idx[i] = new int[4];
                }

                idx[1][0] = 1;
                idx[2][0] = 2;
                idx[3][0] = 3;
                idx[2][1] = 4;
                idx[3][1] = 5;
                idx[1][2] = 6;
                idx[3][2] = 7;
                idx[1][3] = 8;
                idx[2][3] = 9;

                int[][][] dp = new int[2][][];

                for (int i = 0; i < 2; i++)
                {

                    dp[i] = new int[10][];
                    for (int j = 0; j < 10; j++)
                    {

                        dp[i][j] = new int[10];
                        Array.Fill(dp[i][j], -1);
                    }
                }

                dp[0][0][0] = 0;

                for (int i = 0; i < n; i++)
                {

                    int next = str[i] == 'M' ? 1 : (str[i] == 'B' ? 2 : 3);

                    for (int j = 0; j < 10; j++)
                    {

                        for (int k = 0; k < 10; k++)
                        {

                            // 미방문한 곳이면 탐색 X
                            if (dp[0][j][k] == -1) continue;

                            // j, k로 탄광 1과 탄광 2의 음식 상태를 가져온다.
                            // cur은 최근꺼, prev는 이전 음식 상태이다.
                            (int cur1, int prev1) = foods[j];
                            (int cur2, int prev2) = foods[k];

                            // 현재 음식을 탄광 1에 배달하는 경우
                            // 추가되는 점수다.
                            int add = Coal(next, cur1, prev1);
                            // 탄광 1의 다음 음식 상태가 된다.
                            int nC = next;
                            int nP = cur1 == next ? 0 : cur1;   // 만약 현재와 이전께 같다면 현재에만 적어도 같은 기능을 한다.

                            // 추가하기!
                            dp[1][idx[nC][nP]][k] = Math.Max(dp[1][idx[nC][nP]][k], dp[0][j][k] + add);

                            // 탄광 2에 음식을 배달하는 경우
                            // 추가되는 점수다.
                            add = Coal(next, cur2, prev2);

                            // 탄광 2의 다음 상태다.
                            nC = next;
                            nP = cur2 == next ? 0 : cur2;

                            // 추가하기
                            dp[1][j][idx[nC][nP]] = Math.Max(dp[1][j][idx[nC][nP]], dp[0][j][k] + add);
                        }
                    }

                    // 현재와 다음꺼 바꾸기
                    // 원래라면 다음꺼에 -1을 초기화 해야 하나
                    // 값이 증가하는 형식이므로 갱신 없이 바꾸기만 해도
                    // 우리가 찾는 최댓값은 같다.
                    Swap();
                }

                int ret = 0;
                for (int j = 0; j < 10; j++) 
                {

                    for (int k = 0; k < 10; k++)
                    {

                        ret = Math.Max(ret, dp[0][j][k]);
                    }
                }

                Console.Write(ret);

                void Swap()
                {

                    // 현재와 다음꺼 바꾼다.
                    int[][] temp = dp[0];
                    dp[0] = dp[1];
                    dp[1] = temp;
                }

                int Coal(int _f1, int _f2, int _f3)
                {

                    // 비트마스킹으로 석탄 점수 계산
                    int ret = (1 << _f1) | (1 << _f2) | (1 << _f3);
                    if ((ret & 1) == 1) ret ^= 1;
                    if (ret == 2 || ret == 4 || ret == 8) return 1;
                    else if (ret == 14) return 3;
                    else return 2;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                str = sr.ReadLine();
                n = int.Parse(str);
                str = sr.ReadLine();
            }
        }
    }

#if other
// #include <cstdio>
// #include <cstdlib>
// #include <algorithm>
using namespace std;

const int MAX = 100005;

int n;
char s[MAX + 1];
int dp[2][4][4][4];

void read_input() {
	scanf("%d\n", &n);
	fgets(s, MAX + 1, stdin);
	for (int i = 0; i < n; i++)
		if (s[i] == 'F')
			s[i] = 1;
		else if (s[i] == 'M')
			s[i] = 2;
		else
			s[i] = 3;
}

inline int ore(int a, int b, int c) {
	int sol = 0;
	if (a == 1 || b == 1 || c == 1) sol++;
	if (a == 2 || b == 2 || c == 2) sol++;
	if (a == 3 || b == 3 || c == 3) sol++;
	return sol;
}

int main() {
	read_input();

	int now = 0, prev = 1;
	for (int k = n - 1; k >= 0; --k) {
		for (int a = 0; a <= 3; ++a) {
			for (int b = 0; b <= 3; ++b) {
				for (int c = 0; c <= 3; ++c) {
					int left = ore(s[k], s[k - 1], a) + dp[prev][s[k - 1]][b][c];
					int right = ore(s[k], b, c) + dp[prev][b][s[k - 1]][a];
					dp[now][a][b][c] = max(left, right);
				}
			}
		}

		swap(now, prev);
	}
	printf("%d\n", dp[prev][0][0][0]);
	return 0;
}
#endif
}
