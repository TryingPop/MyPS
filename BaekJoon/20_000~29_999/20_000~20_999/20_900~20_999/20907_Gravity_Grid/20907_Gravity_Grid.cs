using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 11
이름 : 배성훈
내용 : Gravity Grid
    문제번호 : 20907번

    구현, 시뮬레이션, 분리집합 문제다
    돌을 놓는 것을 위에서부터 누적해도 결과에는 이상없기에 위에서부터 누적했다
    밑에 있는 돌을 확인하는게 아닌 이전 놓은 위치를 기록해
    누적하는 형식으로 메모리를 써서 O(1)에 놓을 수 있게 했다
    여기서 열의 길이만큼 할당해야하는데, 행의 길이로 해서 index에러를 일으켰다
    이후에 수정하니 이상없이 통과했다

    아이디어는 다음과 같다
    돌을 놓으면 해당 위치 가로, 세로, 대각선으로 
    같은 색상으로 길이가 k인 직선이 존재하는지 확인한다

    w x h <= 250_000 이고 1 <= w, h 이고 1 <= k <= max(w, h)
    이므로 모든 길이를 탐색해버리면 시간이 많이 소요될거 같아 분리집합을 이용했다

    그래서 분리집합으로 각 선분에 대해 인접하면 연결하는 식으로
    개수를 찾아 8방향 탐색으로 O(1)에 연결된 길이를 확인했다
*/

namespace BaekJoon.etc
{
    internal class etc_1045
    {

        static void Main1045(string[] args)
        {

            StreamReader sr;
            int row, col, k;
            int[][] board;
            int[] next;

            int[] gRow, gCol, gRDia, gLDia;
            int[] cRow, cCol, cRDia, cLDia;

            int[] stack;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                int len = row * col;

                stack = new int[len];
                gRow = new int[len];
                gCol = new int[len];
                gRDia = new int[len];
                gLDia = new int[len];

                cRow = new int[len];
                cCol = new int[len];
                cRDia = new int[len];
                cLDia= new int[len];

                for (int i = 0; i < len; i++)
                {

                    gRow[i] = i;
                    gCol[i] = i;
                    gRDia[i] = i;
                    gLDia[i] = i;

                    cRow[i] = 1;
                    cCol[i] = 1;
                    cRDia[i] = 1;
                    cLDia[i] = 1;
                }

                bool turnAlice = true;
                for (int i = 1; i <= len; i++)
                {

                    int c = ReadInt() - 1;
                    int r = next[c]++;

                    int cur = turnAlice ? 1 : 2;
                    board[r][c] = cur;

                    if (GroupRow(r, c) || GroupCol(r, c) 
                        || GroupR(r, c) || GroupL(r, c))
                    {

                        char w = turnAlice ? 'A' : 'B';
                        Console.Write($"{w} {i}");
                        return;
                    }

                    turnAlice = !turnAlice;
                }

                Console.Write('D');

                bool ChkBoard(int[] _group, int[] _cnt, int _r1, int _c1, int _r2, int _c2)
                {

                    if (ChkInValidPos(_r2, _c2) || board[_r1][_c1] != board[_r2][_c2]) return 1 >= k;
                    int idx1 = GetIdx(_r1, _c1);
                    int idx2 = GetIdx(_r2, _c2);

                    int sum = Union(_group, _cnt, idx1, idx2);

                    return sum >= k;
                }

                bool GroupL(int _r, int _c)
                    => ChkBoard(gLDia, cLDia, _r, _c, _r - 1, _c + 1)
                        || ChkBoard(gLDia, cLDia, _r, _c, _r + 1, _c - 1);

                bool GroupR(int _r, int _c)
                    => ChkBoard(gRDia, cRDia, _r, _c, _r - 1, _c - 1)
                        || ChkBoard(gRDia, cRDia, _r, _c, _r + 1, _c + 1);

                bool GroupCol(int _r, int _c)
                    => ChkBoard(gCol, cCol, _r, _c, _r, _c - 1)
                        || ChkBoard(gCol, cCol, _r, _c, _r, _c + 1);


                bool GroupRow(int _r, int _c)
                    => ChkBoard(gRow, cRow, _r, _c, _r - 1, _c)
                        || ChkBoard(gRow, cRow, _r, _c, _r + 1, _c);

                int GetIdx(int _r, int _c) 
                    => _r * col + _c;
            }

            bool ChkInValidPos(int _r, int _c) 
                => _r < 0 || _c < 0 || _r >= row || _c >= col;

            int Union(int[] _group, int[] _cnt, int _idx1, int _idx2)
            {

                int gIdx1 = Find(_group, _idx1);
                int gIdx2 = Find(_group, _idx2);

                if (gIdx1 == gIdx2) return _cnt[gIdx1];
                _group[gIdx1] = gIdx2;
                _cnt[gIdx2] += _cnt[gIdx1];

                return _cnt[gIdx2];
            }

            int Find(int[] _group, int _chk)
            {

                int len = 0;
                while (_chk != _group[_chk])
                {

                    stack[len++] = _chk;
                    _chk = _group[_chk];
                }

                while (len > 0)
                {

                    _group[stack[--len]] = _chk;
                }

                return _chk;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();
                k = ReadInt();

                board = new int[row][];
                next = new int[col];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
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
                    if (c < '0' || '9' < c) return true;

                    ret = c - '0';

                    while ((c = sr.Read()) <= '9' && '0' <= c)
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
// #include<bits/stdc++.h>
using namespace std;
typedef long long ll;
typedef pair<int, int> pii;
typedef pair<ll, ll> pll;
// #define xx first
// #define yy second
// #define all(v) (v).begin(), (v).end()

struct DequeMax{
    deque<pii> q;
    int i=0, j=0, sz=0;
    void clear(){
        q.clear();
        i = j = sz = 0;
    }
    int size(){return sz;}
    void push(int x){
        while(!q.empty() && q.back().xx <= x) q.pop_back();
        q.push_back({x, j});
        ++j; ++sz;
    }
    void pop(){
        if(q.front().yy == i) q.pop_front();
        ++i; --sz;
    }
    int get(){
        return q.front().xx;
    }
}maxq;

int n, m, k;
vector<int> bottom;
vector<vector<pii>> board;
pii ans = {INT_MAX, 0};
void go(int x, int y, int dx, int dy){
    int pre = board[x][y].xx;
    maxq.clear();
    while(0 <= x && x < n && 0 <= y && y < m){
        if(pre != board[x][y].xx){
            pre = board[x][y].xx;
            maxq.clear();
        }
        maxq.push(board[x][y].yy);
        if(maxq.size() > k) maxq.pop();
        if(maxq.size() >= k) ans = min(ans, {maxq.get(), board[x][y].xx});
        x += dx, y += dy;
    }
}

int main(){
    ios_base::sync_with_stdio(0);
    cin.tie(0);

    cin >> n >> m >> k;
    bottom.resize(m);
    board.resize(n, vector<pii>(m, pii(-1,-1)));

    for(int t=1; t<=n*m; ++t){
        int y; cin >> y; --y;
        board[bottom[y]][y] = {t&1, t};
        ++bottom[y];
    }

    for(int x=0; x<n; ++x) {
        go(x, 0, 0, 1); //오른쪽
        go(x, 0, 1, 1); //오른쪽 아래
        go(x, m-1, 1, -1); //왼쪽 아래
    }
    for(int y=0; y<m; ++y) {
        go(0, y, 1, 0); //아래
        go(0, y, 1, 1); //오른쪽 아래
        go(0, y, 1, -1); //왼쪽 아래
    }

    if(ans.xx == INT_MAX) cout << 'D';
    else cout << char(!ans.yy + 'A') << ' ' << ans.xx;
}
#endif
}
