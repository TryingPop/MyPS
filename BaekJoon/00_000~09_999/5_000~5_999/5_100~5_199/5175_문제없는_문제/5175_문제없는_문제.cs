using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 29
이름 : 배성훈
내용 : 문제없는 문제
    문제번호 : 5175번

    브루트포스, 비트마스킹 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1740
    {

        static void Main1740(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n, m;
            int[] algo = new int[20];

            int t = ReadInt();

            for (int i = 1; i <= t; i++)
            {

                sw.Write($"Data Set {i}: ");

                Input();

                GetRet();
            }

            void GetRet()
            {

                int allsolve = 0;
                for (int i = 0; i < n; i++)
                {

                    allsolve |= 1 << i;
                }

                int max = m + 1;
                int ret = 0;

                DFS();

                for (int i = m - 1, j = 'A'; i >= 0; i--, j++)
                {

                    if (((1 << i) & ret) == 0) continue;
                    sw.Write($"{(char)j} ");
                }

                sw.Write('\n');
                sw.Write('\n');

                // _p : 문제 상태
                // _a : 알고리즘 상태
                void DFS(int _p = 0, int _a = 0, int _cnt = 0)
                {

                    // 모든 알고리즘 사용한경우 탈출
                    if (_a == allsolve)
                    {

                        // 푼 문제 갯수 확인
                        if (_cnt < max)
                        {

                            ret = _p;
                            max = _cnt;
                        }
                        // 사전식으로 앞서게!
                        else if (_cnt == max)
                            ret = _p < ret ? ret : _p;

                        return;
                    }

                    for (int i = 0, j = m - 1; i < m; i++, j--)
                    {

                        // 문제 선택
                        int cur = 1 << j;
                        if ((cur & _p) > 0) continue;

                        DFS(_p | cur, _a | algo[i], _cnt + 1);
                    }
                }
            }

            void Input()
            {

                n = ReadInt();
                m = ReadInt();

                for (int i = 0; i < m; i++)
                {

                    string[] temp = sr.ReadLine().Trim().Split();
                    algo[i] = 0;
                    for (int j = 0; j < temp.Length; j++)
                    {

                        int cur = int.Parse(temp[j]);
                        algo[i] |= 1 << (cur - 1);
                    }
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;

void fast_io() {
  cin.tie(0)->sync_with_stdio(0);
}

vector<string> split(const string &str, string delim) {
  vector<string> tokens;

  string::size_type start = 0;
  string::size_type end = 0;

  while ((end = str.find(delim, start)) != string::npos) {
    tokens.push_back(str.substr(start, end - start));
    start = end + delim.size();
  }

  tokens.push_back(str.substr(start));
  return tokens;
}

int tc;
void solve() {
  cout << "Data Set " << ++tc << ":";

  int M, N;
  cin >> M >> N;

  cin.ignore();
  int cov[N] = {0};
  for (int i = 0; i < N; i++) {
    string line;
    getline(cin, line);
    for (auto& token : split(line, " ")) {
      if (token == "") continue;

      int x = stoi(token); x--;
      cov[i] |= 1 << x;
    }
  }

  int mu = N, mans = (1 << N) - 1;
  for (int u = 0; u < (1 << N); u++) {
    int cu = __builtin_popcount(u), acov = 0;
    for (int i = 0; i < N; i++) {
      if (u & (1 << i)) acov |= cov[i];
    }

    if (mu > cu && acov == (1 << M) - 1) {
      mu = cu;
      mans = u;
    }
  }

  for (int i = 0; i < N; i++) {
    if (mans & (1 << i)) cout << ' ' << (char)(i + 'A');
  }
  cout << '\n';
}

int main() {
  fast_io();

  int t = 1;
  cin >> t;
  while (t--) {
    solve();
    if (t) cout << '\n';
  }
}

#endif
}
