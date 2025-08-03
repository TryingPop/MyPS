using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 2
이름 : 배성훈
내용 : 중2병 호반우
    문제번호 : 20122번

    dp, 비트마스킹 문제다.
    조건을 잘못읽어 12번 이상 틀렸다;
    호반우 우권 침해자가 20명 이하라고 하는데, 이를 호반우 범죄자를 20명으로 해석했다;
    그래서 int 범위에 해결되는줄 알고... 계속해서 틀렸다;

    정방향과 역방향 둘 다 계산했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1803
    {

        static void Main1803(string[] args)
        {

            long NOT_VISIT = -2_000_000_000_000_009;

            int row, col, v1, v2, v3;
            int[][] board;
            long[] min, max, mul;

            Input();

            SetArr();

            GetRet();

            void GetRet()
            {

                int size = row + col;

                for (int state = 0; state < max.Length; state++)
                {

                    for (int r = 0; r < row; r++)
                    {

                        if (((1 << r) & state) != 0) continue;
                        WBeam(r, state);
                    }

                    for (int c = 0; c < col; c++)
                    {

                        if (((1 << (row + c)) & state) != 0) continue;
                        HBeam(c, state);
                    }
                }

                // NOT_VISIT을 엄청나게 작은 수로 잡았다.
                long ret1 = -NOT_VISIT, ret2 = NOT_VISIT;
                for (int i = 0; i < max.Length; i++)
                {

                    ret1 = Math.Min(ret1, min[i]);
                    ret2 = Math.Max(ret2, max[i]);
                }

                Console.Write($"{ret1} {ret2}");

                // ↓ 방향
                void HBeam(int _c, int _state)
                {

                    long addScore = 0;
                    long m = mul[_state];

                    for (int r = 0; r < row; r++)
                    {

                        if (board[r][_c] == 0 || ChkKill(r, _c, _state)) continue;
                        if (board[r][_c] == 1)
                            addScore += v1 * m;
                        else if (board[r][_c] == 2)
                            addScore += v2 * m;
                        else
                            m *= v3;
                    }

                    int nextState = (1 << (row + _c)) | _state;

                    if (max[nextState] == NOT_VISIT)
                    {

                        max[nextState] = max[_state] + addScore;
                        min[nextState] = min[_state] + addScore;
                        mul[nextState] = m;
                    }
                    else
                    {

                        max[nextState] = Math.Max(max[nextState], max[_state] + addScore);
                        min[nextState] = Math.Min(min[nextState], min[_state] + addScore);
                    }
                }

                // → 방향
                void WBeam(int _r, int _state)
                {

                    long addScore = 0;
                    long m = mul[_state];

                    for (int c = 0; c < col; c++)
                    {

                        if (board[_r][c] == 0 || ChkKill(_r, c, _state)) continue;
                        if (board[_r][c] == 1)
                            addScore += v1 * m;
                        else if (board[_r][c] == 2)
                            addScore += v2 * m;
                        else
                            m *= v3;
                    }

                    int nextState = (1 << _r) | _state;

                    if (max[nextState] == NOT_VISIT)
                    {

                        max[nextState] = max[_state] + addScore;
                        min[nextState] = min[_state] + addScore;
                        mul[nextState] = m;
                    }
                    else
                    {

                        max[nextState] = Math.Max(max[nextState], max[_state] + addScore);
                        min[nextState] = Math.Min(min[nextState], min[_state] + addScore);
                    }
                }

                // 호반우가 해당 열 또는 행에 탈모빔을 쐈는지 확인
                bool ChkKill(int _r, int _c, int _state)
                    => (((1 << _r) | (1 << (row + _c))) & _state) != 0;
            }

            void SetArr()
            {

                int size = 1 << (row + col);
                min = new long[size];
                max = new long[size];
                mul = new long[size];

                Array.Fill(max, NOT_VISIT);
                max[0] = 0;
                mul[0] = 1;
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();
                v1 = ReadInt();
                v2 = ReadInt();
                v3 = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
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
    }

#if other
// #include <bits/stdc++.h>
using namespace std;

using ll = long long;

int n, m, a, b, c; 
int s[10][10];
pair<ll, ll> cache[1<<10][1<<10];
bool visited[1<<10][1<<10];

pair<ll, ll> solve(int row, int col) {
  if(visited[row][col]) return cache[row][col];
  visited[row][col] = true;

  pair<ll, ll> r = {0, 0};

  for(int i=0; i<n; i++) {
    if((1<<i)&row) continue;
    ll cur = 0; 
    ll mul = 1;
    for(int j=0; j<m; j++) {
      if(col&(1<<j)) continue;
      else if(s[i][j] == 1) cur += a*mul;
      else if(s[i][j] == 2) cur += b*mul;
      else if(s[i][j] == 3) mul = mul*c;
    }
    r.first = min(r.first, min(cur+solve(row+(1<<i), col).first*mul, cur+solve(row+(1<<i), col).second*mul));
    r.second = max(r.second, max(cur+solve(row+(1<<i), col).first*mul, cur+solve(row+(1<<i), col).second*mul));
  }

  for(int j=0; j<m; j++) {
    if((1<<j)&col) continue;
    ll cur = 0; 
    ll mul = 1;
    for(int i=0; i<n; i++) {
      if(row&(1<<i)) continue;
      else if(s[i][j] == 1) cur += a*mul;
      else if(s[i][j] == 2) cur += b*mul;
      else if(s[i][j] == 3) mul = mul*c;
    }
    r.first = min(r.first, min(cur+solve(row, col+(1<<j)).first*mul, cur+solve(row, col+(1<<j)).second*mul));
    r.second = max(r.second, max(cur+solve(row, col+(1<<j)).first*mul, cur+solve(row, col+(1<<j)).second*mul));
  }
  
  return cache[row][col] = r;
}

int main() {
  cin >> n >> m >> a >> b >> c;
  for(int i=0; i<n; i++) for(int j=0; j<m; j++) cin >> s[i][j];
  
  auto r = solve(0, 0);
  cout << r.first << ' ' << r.second << endl;

  return 0;
}

#endif
}
