using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 21
이름 : 배성훈
내용 : 상어 중학교
    문제번호 : 21609번

    구현, 시뮬레이션, BFS 문제다
    노드를 방문하면 EMPTY 처리를 하니 시간초과났다
    이후 첫 방문에서 EMPTY 처리를 하니 이상없이 통과한다

    아마도 엄청난 양이 queue에 들어올거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0985
    {

        static void Main985(string[] args)
        {

            int EMPTY = -2;

            StreamReader sr;

            int n, m;
            int[][] board, visit;
            int[] dirR, dirC;
            Queue<(int r, int c)> q;
            int ret;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                q = new(n * n);

                ret = 0;

                while (Find())
                {

                    Gravity();

                    Rotate();

                    Gravity();
                }

                Console.Write(ret);
            }

            // 반시계 90도 회전
            void Rotate()
            {

                int halfR = (n + 1) >> 1;
                int halfC = n >> 1;
                for (int r = 0; r < halfR; r++)
                {

                    for (int c = 0; c < halfC; c++)
                    {

                        int temp = board[r][c];
                        board[r][c] = board[c][n - 1 - r];
                        board[c][n - 1 - r] = board[n - 1 - r][n - 1 - c];
                        board[n - 1 - r][n - 1 - c] = board[n - 1 - c][r];
                        board[n - 1 - c][r] = temp;
                    }
                }
            }

            // 중력
            void Gravity()
            {

                for (int c = 0; c < n; c++)
                {

                    int bot = n - 1;
                    for (int r = n - 1; r >= 0; r--)
                    {

                        if (board[r][c] < 0) 
                        {

                            if (board[r][c] == -1) bot = r - 1;
                            continue; 
                        }

                        if (bot == r)
                        {

                            bot--;
                            continue;
                        }

                        int temp = board[bot][c];
                        board[bot][c] = board[r][c];
                        board[r][c] = temp;
                        bot--;
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= n || _c >= n;
            }

            bool Find()
            {

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        visit[r][c] = 0;
                    }
                }

                int max = 0;
                int mR = -1, mC = -1;
                int rainbow = 0;

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        if (board[r][c] <= 0 
                            || visit[r][c] != 0) continue;

                        q.Enqueue((r, c));
                        visit[r][c] = board[r][c];
                        int chkRain;
                        int chk = BFS_FIND(board[r][c], out chkRain);

                        if (max < chk)
                        {

                            max = chk;
                            rainbow = chkRain;
                            mR = r;
                            mC = c;
                        }
                        else if (chk < max) continue;
                        else if (rainbow < chkRain)
                        {

                            rainbow = chkRain;
                            mR = r;
                            mC = c;
                        }
                        else if (rainbow == chkRain)
                        {

                            mR = r;
                            mC = c;
                        }
                    }
                }

                if (max < 2) return false;

                int num = board[mR][mC];
                board[mR][mC] = EMPTY;
                q.Enqueue((mR, mC));
                ret += max * max;

                BFS_EMPTY(num);

                return true;
            }

            // 빈 그룹 만들기
            void BFS_EMPTY(int _num)
            {

                while (q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (Chk(nR, nC, _num)) continue;
                        board[nR][nC] = EMPTY;

                        q.Enqueue((nR, nC));
                    }
                }

                bool Chk(int _r, int _c, int _num)
                {

                    if (ChkInvalidPos(_r, _c) || board[_r][_c] < 0) return true;
                    else if (board[_r][_c] > 0 && board[_r][_c] != _num) return true;
                    return false;
                }
            }

            // 그룹 찾기
            int BFS_FIND(int _num, out int _rainbow)
            {

                int ret = 0;
                _rainbow = 0;

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    if (board[node.r][node.c] == 0) _rainbow++;
                    ret++;

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (Chk(nR, nC, _num)) continue;

                        visit[nR][nC] = _num;
                        q.Enqueue((nR, nC));
                    }
                }

                return ret;


                bool Chk(int _r, int _c, int _num)
                {

                    if (ChkInvalidPos(_r, _c) || board[_r][_c] < 0) return true;
                    else if (board[_r][_c] > 0 && board[_r][_c] != _num) return true;
                    else if (visit[_r][_c] == _num) return true;
                    return false;
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();

                board = new int[n][];
                visit = new int[n][];
                for (int r = 0; r < n; r++)
                {

                    board[r] = new int[n];
                    visit[r] = new int[n];
                    for (int c = 0; c < n; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }



                sr.Close();
            }

            int ReadInt()
            {

                int c = sr.Read();
                bool positive = c != '-';
                int ret = positive ? c - '0' : 0;

                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return positive ? ret : -ret;
            }
        }
    }

#if other
var sr = new StreamReader(Console.OpenStandardInput());
var sw = new StreamWriter(Console.OpenStandardOutput());

int N;
int M;
int[,] board;
long result = 0;
int[] dx = { 0, 0, 1, -1 };
int[] dy = { 1, -1, 0, 0 };

var inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
N = inputs[0]; // 격자 한 변의 크기
M = inputs[1]; // 색상의 개수

board = new int[N, N];

for (var y = 0; y < N; y++)
{
    var line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
    for (var x = 0; x < N; x++)
    {
        board[y, x] = line[x];
    }
}

while (true)
{
    var group = find();
    if (group.Count < 2) break;
    result += group.Count * group.Count;
    foreach (var (y, x) in group)
    {
        board[y, x] = -100;
    }

    Gravity();
    Rotation();
    Gravity();
}

sw.WriteLine(result);
sw.Flush();
sw.Close();
sr.Close();

List<(int, int)> find()
{
    var visit = new bool[N, N];
    List<(int, int)> list = new();
    var max = 1;
    var rain = 0;
    var row = 0;
    var col = 0;
    for (int i = 0; i < N; i++)
    {
        for (int j = 0; j < N; j++)
        {
            if (visit[i, j] || board[i, j] < 0 || board[i, j] == 0) continue;
            List<(int, int)> temp = new();
            int color = board[i, j];
            int count = 0;
            int rc = 0;
            visit[i, j] = true;
            Queue<(int, int)> q = new();
            q.Enqueue((i, j));
            while (q.Count > 0)
            {
                (int r, int c) = q.Dequeue();
                temp.Add((r, c));
                if (board[r, c] == 0) rc++;
                count++;

                for (int k = 0; k < 4; k++)
                {
                    int nr = r + dy[k];
                    int nc = c + dx[k];
                    if (nr < 0 || nr == N || nc < 0 || nc == N || visit[nr, nc] || board[nr, nc] == -1 || (board[nr, nc] != 0 && board[nr, nc] != color)) continue;
                    q.Enqueue((nr, nc));
                    visit[nr, nc] = true;
                }
            }

            if (count > max || (count == max && (rc > rain || (rc == rain && (i > row || (i == row && j > col))))))
            {
                max = count;
                rain = rc;
                row = i;
                col = j;
                list = temp;
            }

            foreach ((int r, int c) in temp)
            {
                if (board[r, c] == 0)
                    visit[r, c] = false;
            }
        }
    }

    return list;
}
void Gravity()
{
    for (var i = N - 2; i >= 0; i--) // 맨 아래 행 바로 위에서부터 시작해서 위로 올라가며 검사
    {
        for (var j = 0; j < N; j++) // 각 열을 순차적으로 검사
        {
            if (board[i, j] > -1) // 현재 셀이 -1이 아니면, 즉 타일이 존재하면
            {
                var r = i;
                while (true) // 타일을 아래로 이동 가능한 만큼 이동
                {
                    if (r + 1 < N && board[r + 1, j] == -100) // 다음 행이 격자 범위 내이고, -2(빈 공간)라면
                    {
                        board[r + 1, j] = board[r, j]; // 타일을 아래 행으로 이동
                        board[r, j] = -100; // 현재 행은 빈 공간으로 설정
                        r += 1; // 계속 아래로 내려가면서 검사
                    }
                    else
                    {
                        break; // 더 이상 내려갈 수 없으면 반복 중단
                    }
                }
            }
        }
    }
}


void Rotation()
{
    var temp = new int[N, N];
    for (var y = 0; y < N; y++)
    {
        for (var x = 0; x < N; x++)
        {
            temp[y, x] = board[y, x];
        }
    }

    for (var y = 0; y < N; y++)
    {
        for (var x = 0; x < N; x++)
        {
            board[y, x] = temp[x, N - 1 - y];
        }
    }
}
#endif
}
