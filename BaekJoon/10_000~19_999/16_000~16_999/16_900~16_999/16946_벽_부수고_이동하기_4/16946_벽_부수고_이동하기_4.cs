using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 6
이름 : 배성훈
내용 : 벽 부수고 이동하기 4
    문제번호 : 16946번

    BFS, DFS 문제다.
    이동할 수 있는 장소를 기준으로 집합을 나눈다.
    그리고 영역에 포함된 노드의 수를 구한다.
    그리고 벽에서 인접한 영역의 수를 찾으며 정답을 구했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1162
    {

        static void Main1162(string[] args)
        {

            int row, col;
            int[][] board, group;
            int[] cnt, dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                SetGroup();

                GetRet();
            }

            void GetRet()
            {

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    int[] use = new int[4];
                    for (int r = 0; r < row; r++)
                    {

                        for (int c = 0; c < col; c++)
                        {

                            if (board[r][c] == 0)
                                sw.Write(0);
                            else
                            {

                                int ret = 0;
                                int add = 0;
                                for (int i = 0; i < 4; i++)
                                {

                                    int nR = r + dirR[i];
                                    int nC = c + dirC[i];

                                    if (ChkInvalidPos(nR, nC)) continue;
                                    int g = group[nR][nC];

                                    bool flag = false;
                                    for (int j = 0; j < add; j++)
                                    {

                                        if (use[j] != g) continue;
                                        flag = true;
                                        break;
                                    }

                                    if (flag) continue;
                                    use[add++] = g;
                                    ret += cnt[g];
                                }

                                ret++;
                                ret %= 10;
                                sw.Write(ret);
                            }
                        }

                        sw.Write('\n');
                    }
                }

            }

            void SetGroup()
            {

                Queue<(int r, int c)> q = new(row * col);
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
                int g = 0;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (group[r][c] != 0 || board[r][c] == 1) continue;
                        group[r][c] = ++g;
                        q.Enqueue((r, c));
                        BFS();
                    }
                }

                cnt = new int[g + 1];

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r][c] == 1) continue;
                        cnt[group[r][c]]++;
                    }
                }

                void BFS()
                {

                    while (q.Count > 0)
                    {

                        var node = q.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nR = node.r + dirR[i];
                            int nC = node.c + dirC[i];

                            if (ChkInvalidPos(nR, nC) || board[nR][nC] == 1 || group[nR][nC] == g) continue;
                            group[nR][nC] = g;
                            q.Enqueue((nR, nC));
                        }
                    }
                }
            }

            bool ChkInvalidPos(int _r, int _c) => _r < 0 || _c < 0 || _r >= row || _c >= col;

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                group = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    group[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                int ReadInt()
                {

                    int c, ret = 0;
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return ret;
                }
            }
        }
    }

#if other
StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

int[] inputs = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
int N = inputs[0];
int M = inputs[1];
string[] map = new string[N];

for (int i = 0; i < N; i++)
    map[i] = sr.ReadLine();

int[,] field = new int[N + 2, M + 2];
for(int i=0; i<=N+1; i++)
{
    for(int j=0; j<=M+1; j++)
    {
        if (i == 0 || j == 0 || i == N + 1 || j == M + 1)
            field[i, j] = -1;
        else
            field[i, j] = map[i-1][j-1] - '0';
    }
}

(int r, int c)[] finder = { (-1, 0), (1, 0), (0, -1), (0, 1) };
int[,] check = new int[N+2, M+2];
List<int> counter = new();
counter.Add(-1);
int flag = 0;


for (int i = 1; i <= N; i++)
{
    for (int j = 1; j <= M; j++)
    {
        if (check[i, j] > 0 || field[i, j] == 1)
            continue;

        Queue<(int r, int c)> bfs = new();
        flag++;
        int fieldCount = 0;
        check[i, j] = flag;
        bfs.Enqueue((i, j));

        while(bfs.Count > 0)
        {
            var cur = bfs.Dequeue();
            fieldCount++;
            for(int k=0; k<4; k++)
            {
                (int r, int c) next = (cur.r + finder[k].r, cur.c + finder[k].c);
                if (field[next.r, next.c] != 0 || check[next.r, next.c] > 0)
                    continue;
                check[next.r, next.c] = flag;
                bfs.Enqueue(next);
            }
        }
        counter.Add(fieldCount);
    }
}


for (int i = 1; i <= N; i++)
{
    for (int j = 1; j <= M; j++)
    {
        if(field[i,j] > 0)
        {
            int sum = 1;
            List<int> added = new();
            {
                for (int k = 0; k < 4; k++)
                    if (field[i + finder[k].r, j + finder[k].c] == 0 && !added.Contains(check[i + finder[k].r, j + finder[k].c]))
                    {
                        sum += counter[check[i + finder[k].r, j + finder[k].c]];
                        added.Add(check[i + finder[k].r, j + finder[k].c]);
                    }
            }
            field[i, j] = sum;
        }
    }
}
for(int i=1; i<=N; i++)
{
    for (int j = 1; j <= M; j++)
        sw.Write(field[i, j] % 10);
    sw.WriteLine();
}    

sr.Close();
sw.Close();
#endif
}
