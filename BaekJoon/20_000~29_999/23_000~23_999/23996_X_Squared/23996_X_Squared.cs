using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 22
이름 : 배성훈
내용 : X Squared
    문제번호 : 23996번

    자료 구조, 애드 혹 문제다
    사각형 형태로 존재하는지 확인해야한다!

    초기에는 2로 된 열, 행이 n - 1개, 1로된 열과 행이 1개이면 되는줄 알았으나
        ..X..
        .X..X
        ...XX
        X.X..
        .X.X.
    과 같이 1개짜리가 열과 행이 따로놀면 안된다
    1개짜리가 유일해도 2개짜리가 사각형으로 되는지 확인해야한다
        X...X
        X..X.
        .X..X
        .X.X.
        ..X..

    이후 두 개 확인하니 이상없이 통과한다;
*/

namespace BaekJoon.etc
{
    internal class etc_1069
    {

        static void Main1069(string[] args)
        {

            int MAX = 55;
            string YES = "POSSIBLE\n";
            string NO = "IMPOSSIBLE\n";
            string HEAD = "Case #";
            string MID = ": ";

            StreamReader sr;
            StreamWriter sw;

            int[][] board;
            int n;
            bool[] chk;

            Solve();
            void Solve()
            {

                Init();
                int test = ReadInt();
                for (int t = 1; t <= test; t++)
                {

                    Input();

                    sw.Write($"{HEAD}{t}{MID}");
                    sw.Write(GetRet() ? YES : NO);
                }

                sr.Close();
                sw.Close();
            }

            bool GetRet()
            {

                // 2개 갯수 판별
                int oC = -1;

                for (int r = 0; r < n; r++)
                {

                    int cnt = 0;
                    int chkC = -1;
                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == '.') continue;
                        chkC = c;
                        cnt++;
                    }

                    if (cnt == 2) continue;
                    else if (cnt == 1)
                    {

                        oC = chkC;
                        continue;
                    }

                    return false;
                }

                for (int c = 0; c < n; c++)
                {

                    if (c == oC) continue;
                    int cnt = 0;
                    for (int r = 0; r < n; r++)
                    {

                        if (board[r][c] == '.') continue;
                        cnt++;
                    }

                    if (cnt == 2) continue;
                    return false;
                }

                // 네모 모양 확인
                for (int r = 0; r < n; r++)
                {

                    if (chk[r]) continue;
                    chk[r] = true;
                    int cnt = 0;
                    int curC = -1;
                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] == '.') continue;
                        cnt++;
                        curC = c;
                        break;
                    }

                    if (cnt == 0) return false;

                    int nC = -1;
                    for (int c = curC + 1; c < n; c++)
                    {

                        if (board[r][c] == '.') continue;
                        cnt++;

                        nC = c;
                    }

                    if (cnt > 2) return false;
                    cnt = 1;
                    int nR = -1;
                    for (int curR = r + 1; curR < n; curR++)
                    {

                        if (board[curR][curC] == '.') continue;
                        cnt++;
                        nR = curR;
                    }

                    if (cnt > 2) return false;

                    if (nR == -1 && nC == -1) continue;
                    else if (nR == -1 || nC == -1 || board[nR][nC] == '.') return false;
                    chk[nR] = true;
                    continue;
                }

                return true;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX][];
                for (int i = 0; i < MAX; i++)
                {

                    board[i] = new int[MAX];
                }

                chk = new bool[MAX];
            }

            void Input()
            {

                n = ReadInt();

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = sr.Read();
                    }

                    if (sr.Read() == '\r') sr.Read();
                    chk[r] = false;
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
// #include <iostream>
// #include <algorithm>
using namespace std;
typedef long long ll;
const ll v = 1l << 57;
bool good(ll* bin, int n){
    ll oror = 0l, f = (1l << n) - 1;
    sort(bin,bin+n);
    if (bin[0] / v != 1)
        return 0;
    oror |= bin[0] & f;
    for (int i = 1; i < n; ){
        ll v1 = bin[i++];
        ll v2 = bin[i++];
        if (v1 / v != 2 || v1 != v2)
            return 0;
        v1 &= f;
        oror |= v1;
    }
        
    return oror == f;
}
string solve(){
    ll  rb[60] = {0}, cb[60] = {0};
    int n; cin >> n;

    for (int r = 0; r < n; r++){
        string s; cin >> s;
        for (int c = 0; c < n; c++){
            if (s[c] != '.'){
                rb[r] |= 1l << c;
                rb[r] += v;
                cb[c] |= 1l << r;
                cb[c] += v;
            }
        }
    }

    if (good(rb,n) && good(cb,n))
        return "POSSIBLE";
    else
        return "IMPOSSIBLE";
}

int main(void){
    ios::sync_with_stdio(0);
    cin.tie(0);
    int t; cin >> t;
    for (int i = 1; i <= t; i++)
        cout << "Case #" << i << ": " << solve() << "\n";
}

#elif other2
// #include <bits/stdc++.h>
using namespace std;
using ll = long long;
using pii = pair<int, int>;

void fast_io() {
  cin.tie(nullptr)->sync_with_stdio(false);
}

const int MAX = 60;
char board[MAX + 1][MAX + 1];

int tc;
void solve() {
  cout << "Case #" << ++tc << ": ";

  int N; cin >> N;
  for (int i = 0; i < N; ++i) cin >> board[i];

  int rc[N]{}, cc[N]{};
  for (int i = 0; i < N; ++i) for (int j = 0; j < N; ++j) {
    rc[i] += (board[i][j] == 'X');
    cc[j] += (board[i][j] == 'X');
  }

  map<int, int> rtal, ctal;
  for (int i = 0; i < N; ++i) {
    rtal[rc[i]]++;
    ctal[cc[i]]++;
  }

  if (rtal[1] == 1 && rtal[2] == N - 1 && ctal[1] == 1 && ctal[2] == N - 1) {
    int oi, oj;
    for (int i = 0; i < N; ++i) if (rc[i] == 1) oi = i;
    for (int j = 0; j < N; ++j) if (cc[j] == 1) oj = j;
    if (board[oi][oj] != 'X') return cout << "IMPOSSIBLE\n", void();

    set<pii> x_row;
    for (int i = 0; i < N; ++i) {
      if (i == oi) continue;

      vector<int> x_col;
      for (int j = 0; j < N; ++j) if (board[i][j] == 'X') x_col.push_back(j);
      
      int y1 = x_col[0], y2 = x_col[1];
      if (x_row.count({y1, y2})) x_row.erase({y1, y2});
      else x_row.insert({y1, y2});
    }

    if (x_row.empty()) cout << "POSSIBLE\n";
    else cout << "IMPOSSIBLE\n";
  }
  else cout << "IMPOSSIBLE\n";
}

int main() {
  fast_io();

  int t = 1;
  cin >> t;
  while (t--) solve();
}

#endif
}
