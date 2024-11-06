using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 2
이름 : 배성훈
내용 : 로봇
    문제번호 : 1726번

    BFS 문제다
    방향도 방문 변수로 둬서 BFS 탐색을 하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0935
    {

        static void Main935(string[] args)
        {

            StreamReader sr;

            int row, col;
            int[][] board;
            int[][][] move;
            (int r, int c, int dir) s, e;

            int[] dirR, dirC;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                Queue<(int r, int c, int dir)> q = new(row * col * 4);
                move[s.r][s.c][s.dir] = 0;
                q.Enqueue(s);

                while (q.Count > 0)
                {

                    (int r, int c, int dir) node = q.Dequeue();
                    int cur = move[node.r][node.c][node.dir];

                    (int r, int c, int dir) next = node;

                    // 3칸 이동 시도
                    for (int i = 0; i < 3; i++)
                    {

                        next.r += dirR[next.dir];
                        next.c += dirC[next.dir];

                        // 맵을 벗어나거나 막히면 해당 방향 탐색 종료
                        if (ChkInValidPos(next.r, next.c) || board[next.r][next.c] == 1) break;

                        if (move[next.r][next.c][next.dir] == -1)
                        {

                            move[next.r][next.c][next.dir] = cur + 1;
                            q.Enqueue(next);
                        }
                    }

                    next = node;
                    next.dir = node.dir == 3 ? 0 : node.dir + 1;
                    if (move[next.r][next.c][next.dir] == -1)
                    {

                        move[next.r][next.c][next.dir] = cur + 1;
                        q.Enqueue(next);
                    }

                    next.dir = node.dir == 0 ? 3 : node.dir - 1;
                    if (move[next.r][next.c][next.dir] == -1)
                    {

                        move[next.r][next.c][next.dir] = cur + 1;
                        q.Enqueue(next);
                    }
                }

                Console.Write(move[e.r][e.c][e.dir]);
            }

            bool ChkInValidPos(int _r, int _c)
            {

                return _r < 0 || _c < 0 || _r >= row || _c >= col;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                move = new int[row][][];

                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    move[r] = new int[col][];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = ReadInt();
                        move[r][c] = new int[4];
                        Array.Fill(move[r][c], -1);
                    }
                }

                // 인덱스로 동 0, 서 1, 남 2, 북 3를 넣으면
                // dirR, dirC에 맞는 인덱스로 바꿔준다
                int[] dir = { 3, 1, 2, 0 };
                s = (ReadInt() - 1, ReadInt() - 1, dir[ReadInt() - 1]);
                e = (ReadInt() - 1, ReadInt() - 1, dir[ReadInt() - 1]);

                // 북, 서, 남, 동
                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };

                sr.Close();
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

#if other
namespace ConsoleApp1
{
    internal class Program
    {
        static int answer = 0;
        static int m;
        static int n;
        static int[,] map;
        static bool[,,] visit;
        static (int, int, int) start;
        static (int, int, int) end;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            m = arr[0]; n = arr[1];
            map = new int[m, n];
            visit = new bool[4, m, n];
            for(int i = 0; i < m; i++)
            {
                int[] temp = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < n; j++)
                {
                    map[i, j] = temp[j];
                }
            }
            arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            start = (arr[0] - 1, arr[1] - 1, arr[2] - 1);
            arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            end = (arr[0] - 1, arr[1] - 1, arr[2] - 1);
            bfs();

            output.Write(answer);

            input.Close();
            output.Close();
        }
        static void bfs()
        {
            int[] dy = new int[] { 0, 0, 1, -1 };
            int[] dx = new int[] { 1, -1, 0, 0 };            
            Queue<(int, int, int, int)> q = new();
            q.Enqueue((start.Item1, start.Item2, start.Item3, 0));
            visit[start.Item3, start.Item1, start.Item2] = true;
            
            while (q.Count > 0)
            {
                (int row, int col, int dir, int count) = q.Dequeue();
                if (row == end.Item1 && col == end.Item2 && dir == end.Item3)
                {
                    answer = count;
                    break;
                }
                if(dir == 0 || dir == 1)
                {
                    if (!visit[2, row, col])
                    {
                        q.Enqueue((row, col, 2, count + 1));
                        visit[2, row, col] = true;
                    }
                    if (!visit[3, row, col])
                    {
                        q.Enqueue((row, col, 3, count + 1));
                        visit[3, row, col] = true;
                    }
                }
                else
                {
                    if (!visit[0, row, col])
                    {
                        q.Enqueue((row, col, 0, count + 1));
                        visit[0, row, col] = true;
                    }
                    if (!visit[1, row, col])
                    {
                        q.Enqueue((row, col, 1, count + 1));
                        visit[1, row, col] = true;
                    }
                }
                for (int i = 1; i < 4; i++)
                {
                    int nr = row + dy[dir] * i;
                    int nc = col + dx[dir] * i;
                    if (nr < 0 || nr >= m || nc < 0 || nc >= n || visit[dir, nr, nc]) continue;
                    if (map[nr, nc] == 1) break;
                    q.Enqueue((nr, nc, dir, count + 1));
                    visit[dir, nr, nc] = true;
                }
            }
        }
    }
}
#elif other2
// #include<stdio.h>

typedef struct node {
    int x, y;
    int direct;
    int count;
    node* next;
};

class linkedlist
{
private:
    node* head;
    node* tail;
    int size = 0;

public:
    linkedlist()
    {
        head = new node;

        head->next = NULL;
        tail = head;
    }

    void insert_sort(int x, int y, int direct, int count)
    {
        node* pn = new node;

        pn->x = x;
        pn->y = y;
        pn->direct = direct;
        pn->count = count;
        pn->next = NULL;

        if (!head->next)
        {
            head->next = pn;
            tail = pn;
        }
        else
        {
            node* prev;
            node* c = head;
            while (c->next != NULL)
            {
                prev = c;
                c = c->next;

                if (c->count > count) //입력된 count보다 큰 count를 만나면 그 사이에
                {
                    prev->next = pn;
                    pn->next = c;
                    break;
                }
            }

            if (c->count <= count) //마지막 node 경우 처리
                c->next = pn;
        }
        size++;
    }

    int return_size()
    {
        return size;
    }

    node* delete_head()
    {
        node* c = head->next;

        if (c->next != NULL)
            head->next = c->next;
        else
            head->next = NULL;

        size--;

        return c;
    }
};

int diffdirect(int a, int b)
{
    if (a == b) return 0;
    else if ((a + b == 3 || a + b == 7)) return 2;
    else return 1;
}

int direc[5][2] = { {0,0}, {0,1},{0,-1},{1,0},{-1,0} }; //1234 동서남북
int main(void)
{
    int M, N;
    scanf("%d %d", &M, &N);

    int arr[103][103];
    for (int i = 0; i < M; i++)
        for (int j = 0; j < N; j++)
            scanf("%d", &arr[i][j]);

    int sx, sy, sd;
    int dx, dy, dd;
    scanf("%d %d %d", &sx, &sy, &sd);
    scanf("%d %d %d", &dx, &dy, &dd);

    sx--; sy--; dx--; dy--;
    linkedlist list;
    list.insert_sort(sx, sy, sd, 0);

    bool check[103][103] = { false };
    check[sx][sy] = true;

    while (list.return_size() > 0)
    {
        node* c = list.delete_head();

        if (c->x == dx && c->y == dy)
        {
            if (diffdirect(dd, c->direct) == 0)
            {
                printf("%d\n", c->count + diffdirect(dd, c->direct));
                break;
            }
            else
            {
                list.insert_sort(dx, dy, dd, c->count + diffdirect(dd, c->direct));
                continue;
            }   
        }

        for (int i = 1; i < 5; i++)
        {
            for (int j = 1; j < 4; j++) //2칸 더 갈수있는지 확인
            {
                int rrx = c->x + direc[i][0] * j;
                int rry = c->y + direc[i][1] * j;

                if (rrx < 0 || rry < 0 || rrx >= M || rry >= N || arr[rrx][rry] == 1) break;

                if (!check[rrx][rry])
                {                 
                    if (!(rrx == dx and rry == dy)) check[rrx][rry] = true;
                    list.insert_sort(rrx, rry, i, c->count + 1 + diffdirect(c->direct, i) + (j - 1) / 3);
                }
            }
        }
    }
}
#endif
}
