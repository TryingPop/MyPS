using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 22
이름 : 배성훈
내용 : 인성 문제 있어??
    문제번호 : 19952번

    BFS 문제다
    역방향으로 짰다
    현재 칸에서 해당 칸으로 이동하는데 들어가는 최소비용을 찾았다
    최소비용은 이전 칸에서 현재칸의 높이차와 현재칸까지 이동하는데 들어간 힘 + 1과 비교해서
    큰 값을 계승한다 이렇게 시작지점까지 찾아가고
    시작지점에 힘이 초기에 주어지는 힘보다 작은 경우 가능, 큰 경우면 불가능내리게 했다
    이렇게 제출하니 이상없이 통과했다

    그리고 visual studio에서는 한글이 깨져서 Console.OutputEncoding 구문을 넣었는데,
    백준에도 제출하니 컴파일에러로 한 번 틀렸다
    이외에는 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0320
    {

        static void Main320(string[] args)
        {

            const string YES = "잘했어!!";
            const string NO = "인성 문제있어??";

            // Console.OutputEncoding = Encoding.UTF8;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = ReadInt(sr);
            int[,] board = new int[101, 101];
            int[,] memo = new int[101, 101];
            Queue<(int r, int c)> q = new(100 * 100);

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            while(test-- > 0)
            {

                int row = ReadInt(sr);
                int col = ReadInt(sr);

                int obj = ReadInt(sr);
                int f = ReadInt(sr);

                int sR = ReadInt(sr);
                int sC = ReadInt(sr);

                int eR = ReadInt(sr);
                int eC = ReadInt(sr);

                for (int r = 1; r <= row; r++)
                {

                    for (int c = 1; c <= col; c++)
                    {

                        board[r, c] = 0;
                        memo[r, c] = -1;
                    }
                }

                for (int i = 0; i < obj; i++)
                {

                    int r = ReadInt(sr);
                    int c = ReadInt(sr);

                    int h = ReadInt(sr);

                    board[r, c] = h;
                }

                q.Enqueue((eR, eC));
                memo[eR, eC] = 0;
 
                while(q.Count > 0)
                {

                    var node = q.Dequeue();
                    int curPow = memo[node.r, node.c];
                    for (int i = 0; i < 4; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC, row, col)) continue;

                        int addPow = board[node.r, node.c] - board[nextR, nextC];
                        addPow = addPow <= curPow + 1 ? curPow + 1 : addPow;
                        int chk = memo[nextR, nextC];
                        if (chk != -1 && chk <= addPow) continue;
                        memo[nextR, nextC] = addPow;
                        q.Enqueue((nextR, nextC));
                    }
                }

                if (memo[sR, sC] <= f) sw.WriteLine(YES);
                else sw.WriteLine(NO);
            }

            sr.Close();
            sw.Close();
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r <= 0 || _c <= 0 || _r > _row || _c > _col) return true;
            return false;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }

#if other
using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    struct Pair
    {
        public int r, c;
        public Pair(int r, int c)
        {
            this.r = r; this.c = c;
        }
    }
    readonly static Pair[] next = new Pair[] { new(-1, 0), new(1, 0), new(0, -1), new(0, 1) };
    const int Inf = -1;
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        StringBuilder sb = new();
        for (int i = 0; i < t; i++)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int h = line[0], w = line[1], o = line[2], f = line[3], sr = line[4], sc = line[5], er = line[6], ec = line[7];
            int[,] maze = new int[h + 1, w + 1];
            for (int j = 0; j < o; j++)
            {
                int[] rcl = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int r = rcl[0], c = rcl[1], l = rcl[2];
                maze[r, c] = l;
            }
            int[,] visit = new int[h + 1, w + 1];
            for (int j = 1; j <= h; j++)
            {
                for (int k = 1; k <= w; k++)
                {
                    visit[j, k] = Inf;
                }
            }
            Queue<Pair> queue = new();
            visit[sr, sc] = f;
            queue.Enqueue(new(sr, sc));
            while (queue.Count > 0)
            {
                Pair p = queue.Dequeue();
                int r = p.r, c = p.c;
                for (int j = 0; j < 4; j++)
                {
                    int nr = r + next[j].r, nc = c + next[j].c;
                    if (nr > 0 && nr <= h && nc > 0 && nc <= w && visit[nr, nc] == Inf && visit[r, c] >= Jump(maze[r, c], maze[nr, nc]))
                    {
                        visit[nr, nc] = visit[r, c] - 1;
                        queue.Enqueue(new(nr, nc));
                    }
                }
            }
            sb.Append(visit[er, ec] != Inf ? "잘했어!!" : "인성 문제있어??");
            if (i + 1 < t)
                sb.Append('\n');
        }
        Console.Write(sb.ToString());
    }
    static int Jump(int from, int to)
    {
        return Math.Max(1, to - from);
    }
}
#endif
}
