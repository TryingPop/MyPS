using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 25
이름 : 배성훈
내용 : 병약한 윤호
    문제번호 : 14677번

    dp, bfs 문제다
    dp로 접근해 풀었다

    dp[i][j]를 문자열이 폐구간 [i, j] 에서 
    약을 먹을 수 있는 최대값이 되게 설정했다

    그러니 많아야 N^2의 범위만 탐색하게 되고,
    메모리도 N = 1500이니 충분히 할만하다 느꼈다
*/

namespace BaekJoon.etc
{
    internal class etc_0997
    {

        static void Main997(string[] args)
        {

            StreamReader sr;
            int n;
            int[] time;
            int[][] dp;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dp = new int[n][];
                for (int i = 0; i < n; i++)
                {

                    dp[i] = new int[n];
                    Array.Fill(dp[i], -1);
                }

                Console.Write($"{DFS(0, n - 1)}");
            }

            int DFS(int _s, int _e, int _t = 1)
            {

                if (_e < _s) return 0;
                else if (dp[_s][_e] != -1) return dp[_s][_e];
                int ret = dp[_s][_e] = 0;

                if (time[_s] == _t) ret = Math.Max(ret, 1 + DFS(_s + 1, _e, Next()));
                if (time[_e] == _t) ret = Math.Max(ret, 1 + DFS(_s, _e - 1, Next()));

                return dp[_s][_e] = ret;

                int Next()
                {
                    
                    return _t == 3 ? 1 : _t + 1;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt() * 3;
                time = new int[n];

                for (int i = 0; i < n; i++)
                {

                    int cur = sr.Read();
                    switch(cur)
                    {

                        case 'B':
                            time[i] = 1;
                            break;

                        case 'L':
                            time[i] = 2;
                            break;

                        case 'D':
                            time[i] = 3;
                            break;
                    }
                }

                sr.Close();
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
// #include <iostream>
// #include <deque>
// #include <algorithm>
// #include <string>
using namespace std;

int N;
string arr;
int visited[1500];
void input() {
	cin >> N;
	cin >> arr;
}

int solve() {
	deque<pair<pair<int, int>, int>> q;
	if (arr[0] == 'B') {
		visited[0] = 1;
		q.push_back(make_pair(make_pair(1, 3 * N - 1), 1));
	}
	if (arr[3 * N - 1] == 'B') {
		visited[3 * N - 1] = 1;
		q.push_back(make_pair(make_pair(0, 3 * N - 2), 1));
	}
	int answer = 0;
	while (!(q.empty())) {
		int left = q.front().first.first;
		int right = q.front().first.second;
		int num = q.front().second;
		q.pop_front();
		if (left == right) return num + 1;
		int temp = 0;
		//전에 먹은것이 저녁약인 경우
		if (num % 3 == 0) {
			if (arr[left] == 'B' && num > visited[left]) {
				visited[left] = num + 1;
				q.push_back(make_pair(make_pair(left + 1, right), num + 1));
				temp = 1;
			}
			if (arr[right] == 'B' && num > visited[right]) {
				visited[right] = num + 1;
				q.push_back(make_pair(make_pair(left, right - 1), num + 1));
				temp = 1;
			}
		}
		//전에 먹은 것이 아침약인 경우
		else if (num % 3 == 1) {
			if (arr[left] == 'L' && num > visited[left]) {
				visited[left] = num + 1;
				q.push_back(make_pair(make_pair(left + 1, right), num + 1));
				temp = 1;
			}
			if (arr[right] == 'L' && num > visited[right]) {
				visited[right] = num + 1;
				q.push_back(make_pair(make_pair(left, right - 1), num + 1));
				temp = 1;
			}
		}
		// 전에 먹은 것이 점심약인 경우
		else {
			if (arr[left] == 'D' && num > visited[left]) {
				visited[left] = num + 1;
				q.push_back(make_pair(make_pair(left + 1, right), num + 1));
				temp = 1;
			}
			if (arr[right] == 'D' && num > visited[right]) {
				visited[right] = num + 1;
				q.push_back(make_pair(make_pair(left, right - 1), num + 1));
				temp = 1;
			}
		}
		if (!temp) answer = max(answer, num);
	}
	return answer;
}

int main() {
	cin.tie(0);
	cin.sync_with_stdio(0);
	input();
	cout << solve();
}
#endif
}
