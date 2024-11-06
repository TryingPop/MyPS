using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 21
이름 : 배성훈
내용 : 보물섬
    문제번호 : 2589번

    브루트포스, 그래프 탐색, 너비 우선 탐색 문제다
    최대 블록은 2500이고, 하나씩 탐색해도 2500 * 2500 < 1000만이므로 
    브루트포스로 방향을 잡았다

    처음에는 섬에 번호를 부여하고 번호가 다르면 거리 안재는 식으로 제출했다
    다만 처음 방문 처리를 안해서 한 번 틀렸다;

    이후에 다른 사람의 풀이를 보니 섬에 번호를 부여할 필요가 없었고,
    섬의 번호를 빼니 기존 맵에 거리를 기록하는 식으로 사용할 수 있었다
    그래서 거리를 확인하는 배열을 제외하니 전체 메모리는 줄었다

    또한 다른 사람의 풀이를 보니, col이 50이하이므로
    비트마스킹으로 열 방문체크를 했는데 초기화에 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0831
    {

        static void Main831(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;

            Queue<(int r, int c)> q;
            bool[][] visit;
            int[] dirR, dirC;

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

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                visit = new bool[row][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    visit[r] = new bool[col];

                    for (int c = 0; c < col; c++)
                    {

                        int tile = sr.Read();
                        if (tile == 'W') board[r][c] = -1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                q = new(row * col);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                sr.Close();
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            int BFS_Dis(int _r, int _c)
            {

                q.Enqueue((_r, _c));
                board[_r][_c] = 0;
                visit[_r][_c] = true;
                int ret = 0;

                while(q.Count > 0)
                {

                    (int r, int c) node = q.Dequeue();
                    int curDis = board[node.r][node.c];

                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || visit[nextR][nextC]) continue;
                        visit[nextR][nextC] = true;

                        if (board[nextR][nextC] == -1) continue;
                        board[nextR][nextC] = curDis + 1;
                        q.Enqueue((nextR, nextC));
                    }
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        visit[r][c] = false;
                        if (board[r][c] <= 0) continue;
                        else if (ret < board[r][c]) ret = board[r][c];
                        board[r][c] = 0;
                    }
                }

                return ret;
            }

            void GetRet()
            {

                int ret = 0;
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == -1) continue;
                        int chk = BFS_Dis(r, c);
                        ret = ret < chk ? chk : ret;
                    }
                }

                Console.Write(ret);
            }

            void Solve()
            {

                Input();

                GetRet();
            }

            Solve();
        }
    }

#if other
var reader = new Reader();
var (r, c) = (reader.NextInt(), reader.NextInt());
var map = new string[r];
for (int i = 0; i < r; i++)
    map[i] = reader.NextString(c);

var dir = new (int x, int y)[4] { (1, 0), (-1, 0), (0, 1), (0, -1) }; 
var q = new Queue<(int x, int y, int d)>(r * c);
var visited = new long[r];
int max = 0;
for (int i = 0; i < r; i++)
for (int j = 0; j < c; j++)
{
    if (map[i][j] != 'L')
        continue;

    q.Enqueue((i, j, 0));
    visited[i] |= 1L << j;
    while (q.Count > 0)
    {
        var cur = q.Dequeue();
        max = Math.Max(max, cur.d);

        foreach (var d in dir)
        {
            var (dx, dy) = (cur.x + d.x, cur.y + d.y);
            if (dx < 0 || dx >= r || dy < 0 || dy >= c)
                continue;

            if ((visited[dx] & (1L << dy)) > 0 || map[dx][dy] != 'L')
                continue;

            q.Enqueue((dx, dy, cur.d + 1));
            visited[dx] |= 1L << dy;
        }
    }

    Array.Fill(visited, 0);
}

Console.Write(max);

class Reader{StreamReader R;public Reader()=>R=new(new BufferedStream(Console.OpenStandardInput()));
public int NextInt(){var(v,n,r)=(0,false,false);while(true){int c=R.Read();if((r,c)==(false,'-')){(n,r)=(true,true);continue;}if('0'<=c&&c<='9'){(v,r)=(v*10+(c-'0'),true);continue;}if(r==true)break;}return n?-v:v;}
public string NextString(int m){var(v,r,l)=(new char[m+1],false,0);while(true){int c=R.Read();if(r==false&&(c==' '||c=='\n'||c=='\r'))continue;if(r==true&&(l==m||c==' '||c=='\n'||c=='\r'))break;v[l++]=(char)c;r=true;}return new string(v,0,l);}
}
#endif
}
