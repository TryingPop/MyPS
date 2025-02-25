using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 30
이름 : 배성훈
내용 : Identically Colored Panels Connection
    문제번호 : 4950번

    브루트포스, 시뮬레이션 문제다.
    전류를 흐를 수 있는 모든 경우를 확인해서 풀었다.
    전류가 흐를 때, 같은 색상의 표현은 유니온 파인드 알고리즘을 이용해 연결시켰다.
    그러면 한 번 시뮬레이션 돌릴 때 많아야 맵의 크기 만큼 탐색한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1226
    {

        static void Main1226(string[] args)
        {

            int MAX = 8;
            int COLOR = 7;
            StreamReader sr;
            StreamWriter sw;

            int[][] board, bGroup;
            bool[][] visit;
            int[] cnt, select;
            int[] dirR, dirC;
            int row, col;
            Queue<(int r, int c)>[] conn;

            int[] stk, group;

            Solve();
            void Solve()
            {

                Init();

                while (Input())
                {

                    GetRet();
                }

                sr.Close();
                sw.Close();
            }

            void GetRet()
            {

                Queue<(int r, int c)> q = conn[0];

                int ret = DFS(board[0][0]);
                sw.Write($"{ret}\n");
                int DFS(int _prev, int _depth = 0)
                {

                    if (_depth == 4)
                    {

                        SetBFS();
                        return cnt[0];
                    }

                    int ret = 0;

                    for (int i = 1; i < COLOR; i++)
                    {

                        if (i == _prev) continue;
                        select[_depth] = i;
                        ret = Math.Max(ret, DFS(i, _depth + 1));
                    }

                    return ret;
                }

                void SetBFS()
                {

                    Init();

                    q.Enqueue((0, 0));
                    int curIdx = board[0][0];
                    visit[0][0] = true;

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();
                        Union(0, bGroup[node.r][node.c]);

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || visit[nR][nC]) continue;
                            visit[nR][nC] = true;
                            int idx = board[nR][nC];
                            if (idx == curIdx) q.Enqueue((nR, nC));
                            else conn[idx].Enqueue((nR, nC));
                        }
                    }

                    for (int i = 0; i < 5; i++)
                    {

                        BFS(select[i]);
                    }

                    void BFS(int _idx)
                    {

                        while (conn[_idx].Count > 0)
                        {

                            q.Enqueue(conn[_idx].Dequeue());
                        }

                        while (q.Count > 0)
                        {

                            var node = q.Dequeue();
                            Union(0, bGroup[node.r][node.c]);

                            for (int i = 0; i < 4; i++)
                            {

                                int nR = node.r + dirR[i];
                                int nC = node.c + dirC[i];

                                if (ChkInvalidPos(nR, nC) || visit[nR][nC]) continue;
                                visit[nR][nC] = true;

                                int idx = board[nR][nC];
                                if (idx == _idx) q.Enqueue((nR, nC));
                                else conn[idx].Enqueue((nR, nC));
                            }
                        }
                    }
                }

                void Union(int _g1, int _g2)
                {

                    int g1 = Find(_g1);
                    int g2 = Find(_g2);

                    if (g1 == g2) return;

                    if (g2 < g1)
                    {

                        int temp = g1;
                        g1 = g2;
                        g2 = temp;
                    }

                    group[g2] = g1;
                    cnt[g1] += cnt[g2];
                }

                int Find(int _chk)
                {

                    int len = 0;
                    while (group[_chk] != _chk)
                    {

                        stk[len++] = _chk;
                        _chk = group[_chk];
                    }

                    while (len-- > 0)
                    {

                        group[stk[len]] = _chk;
                    }

                    return _chk;
                }

                bool ChkInvalidPos(int _r, int _c)
                    => _r < 0 || _c < 0 || _r >= row || _c >= col;

                void Init()
                {

                    for (int i = 0; i < COLOR; i++)
                    {

                        conn[i].Clear();
                    }

                    int g = 0;
                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            visit[r][c] = false;
                            cnt[g] = 1;
                            group[g] = g;
                            bGroup[r][c] = g++;
                        }
                    }
                }
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                board = new int[MAX][];
                bGroup = new int[MAX][];
                visit = new bool[MAX][];
                for (int i = 0; i < MAX; i++)
                {

                    board[i] = new int[MAX];
                    bGroup[i] = new int[MAX];
                    visit[i] = new bool[MAX];
                }

                cnt = new int[MAX * MAX];
                stk = new int[MAX * MAX];
                group = new int[MAX * MAX];
                conn = new Queue<(int r, int c)>[COLOR];
                for (int i = 0; i < COLOR; i++)
                {

                    conn[i] = new(MAX * MAX);
                }

                select = new int[5];
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
            }

            bool Input()
            {

                row = ReadInt();
                col = ReadInt();
                select[4] = ReadInt();

                for (int r = 0; r < row; r++) 
                {

                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                return row != 0 || col != 0 || select[4] != 0;
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
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
int m[8][8], h,w,c;
bool v[8][8];
int dx[4] = {-1,0,0,1},dy[4] = {0,1,-1,0};
void foo(int t)
{
    memset(v,0,sizeof(v));
    queue<pair<int,int>> q;
    q.emplace(0,0);
    v[0][0] = true;
    while(!q.empty())
    {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();
        for(int i=0;i<4;i++)
        {
            int xx = x+dx[i], yy = y+dy[i];
            if(xx<0||xx>=w||yy<0||yy>=h) continue;
            if(v[yy][xx] || m[yy][xx] != m[0][0]) continue;
            m[yy][xx] = t;
            v[yy][xx] = true;
            q.emplace(xx,yy);
        }
    }
    m[0][0] = t;
}

int bar()
{
    memset(v,0,sizeof(v));
    queue<pair<int,int>> q;
    q.emplace(0,0);
    int ret = 1;
    v[0][0] = true;
    while(!q.empty())
    {
        int x = q.front().first;
        int y = q.front().second;
        q.pop();
        for(int i=0;i<4;i++)
        {
            int xx = x+dx[i], yy = y+dy[i];
            if(xx<0||xx>=w||yy<0||yy>=h) continue;
            if(v[yy][xx] || m[yy][xx] != m[0][0]) continue;
            v[yy][xx] = true;
            ret++;
            q.emplace(xx,yy);
        }
    }
    return ret;
}
int solve(int t)
{
    if(t == 4)
    {
        foo(c);
        return bar();
    }
    int pm[8][8], ret = 0;
    memcpy(pm,m,sizeof(m));
    for(int i=1;i<=6;i++)
    {
        if(m[0][0] == i) continue;
        foo(i);
        ret = max(ret,solve(t+1));
        memcpy(m,pm,sizeof(m));
    }
    return ret;
}
int main()
{
    while(1)
    {
        scanf("%d%d%d",&h,&w,&c);
        for(int i=0;i<h;i++)
        {
            for(int j=0;j<w;j++)
            {
                scanf("%d",m[i]+j);
            }
        }
        if(!h) break;
        printf("%d\n",solve(0));
    }
}
#elif other2
// # include <bits/stdc++.h>
using namespace std;
// #define sz(x) (int)(x).size()

const int dy[] = { 1, 0, -1, 0 };
const int dx[] = { 0, 1, 0, -1 };

int h, w, c;
vector<vector<int>> a;
vector<set<vector<vector<int>>>> st;

void dfs(int y, int x, vector<vector<bool>>& vis, vector<vector<int>>& now, const vector<vector<int>>& prv, int co) {
    now[y][x] = co;
    vis[y][x] = 1;
    for (int d = 0; d < 4; d++) {
        int ny = y + dy[d], nx = x + dx[d];
        if (ny < 0 || h <= ny || nx < 0 || w <= nx) continue;
        if (vis[ny][nx]) continue;
        if (prv[y][x] != prv[ny][nx]) continue;
        dfs(ny, nx, vis, now, prv, co);
    }
}

int count_co(int y, int x, vector<vector<bool>>& vis, const vector<vector<int>>& now, int co) {
    int ret = 1;
    vis[y][x] = 1;
    for (int d = 0; d < 4; d++) {
        int ny = y + dy[d], nx = x + dx[d];
        if (ny < 0 || h <= ny || nx < 0 || w <= nx) continue;
        if (vis[ny][nx]) continue;
        if (now[y][x] != now[ny][nx]) continue;
        ret += count_co(ny, nx, vis, now, co);
    }
    return ret;
}

int main() {

    cin.tie(NULL); cout.tie(NULL);
    ios_base::sync_with_stdio(false);

    while (1) {
        // initialize
        a.clear();
        st.clear();

        // input
        cin >> h >> w >> c;
        if (h == 0 && w == 0 && c == 0) break;

        a.resize(h, vector<int>(w));
        for (int i = 0; i<h; i++) {
            for (int j = 0; j<w; j++) {
                cin >> a[i][j];
            }
        }

        // base case
        st.resize(6);
st[0].insert(a);

// inductive step
for (int step = 1; step <= 5; step++)
{
    for (auto & prv : st[step - 1])
    {
        for (int co = 1; co <= 6; co++)
        {
            // if (prv[0][0] == co) continue;
            vector<vector<bool>> vis(h, vector<bool>(w));
            vector<vector<int>> now = prv;

            dfs(0, 0, vis, now, prv, co);

            st[step].insert(now);
        }
    }
}

// get answer
int ans = 0;
for (auto & now : st[5])
{
    if (now[0][0] != c) continue;
    vector<vector<bool>> vis(h, vector<bool>(w));
    ans = max(ans, count_co(0, 0, vis, now, c));
}

cout << ans << '\n';
    }
}

#endif
}
