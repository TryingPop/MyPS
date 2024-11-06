using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 1
이름 : 배성훈
내용 : 어른 상어
    문제번호 : 19237번

    구현, 시뮬레이션 문제다
    문제 자체는 재미있는데, 구현에 시간이 많이 걸린다
    BFS 탐색으로 구현했다

    아이디어? 구현 방법?은 다음과 같다
    맨 먼저 상어의 이동 위치를 찾는다
    이동 위치는 빈공간 확인 -> 있으면 우선 순위 높은 '빈공간'으로, 
    빈공간이 없으면 우선 자신의 이전 이동 경로로 간다

    빈 공간으로 이동하는 경우 바로 이동시키는게 상어와 이동 좌표를 보관해둔다
    빈 공간이 아닌 경우는 바로 이동 시킨다

    그리고 모든 상어의 이동 좌표가 설정되었을 때
    빈 공간에 이동하는 상어들은 다 함께 이동시킨다
    여기서 상어들을 내쫓는다
    이동은 냄세를 k + 1 이라 놓았다
    
    그리고 채취가 있으면 1감소시키고,
    채취가 k인 경우 갓 이동한 장소이므로 여기 상어 좌표를 큐에 넣는다

    이렇게 상어가 2마리 이상이면 1001초동안 진행했고 상어가 1마리면 탈출시켰다
    그리고 해당 시간을 정답에 맞게 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0422
    {

        static void Main422(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();
            int k = ReadInt();

            // 상어와 채취? 턴
            (int shark, int k)[,] board = new (int shark, int k)[n, n];
            // 상어 위치 큐
            Queue<(int r, int c, int n)> shark = new(n * n);
            for (int r = 0; r < n; r++)
            {

                for (int c = 0; c < n; c++)
                {

                    // 좌표 입력 받기
                    int cur = ReadInt();
                    if (cur == 0) continue;
                    board[r, c] = (cur, k);
                    shark.Enqueue((r, c, cur));
                }
            }

            // 현재 상어들 방향 입력 받기
            int[] curDirs = new int[m + 1];
            for (int i = 1; i <= m; i++)
            {

                curDirs[i] = ReadInt() - 1;
            }

            // 상어의 방향별 이동 우선순위를 입력 받는다
            // 상어의 번호 1 ~ m
            // 방향은 0 : 상, 1 : 하, 2 : 좌, 3 : 우
            int[][][] pDirs = new int[m + 1][][];
            for (int a = 1; a <= m; a++)
            {

                pDirs[a] = new int[4][];

                for (int b = 0; b < 4; b++)
                {

                    pDirs[a][b] = new int[4];
                    
                    for (int c = 0; c < 4; c++)
                    {

                        pDirs[a][b][c] = ReadInt() - 1;
                    }
                }
            }

            // 방향
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };

            // 상어 임시보관용 큐
            Queue<(int r, int c, int n)> calc = new(n * n);

            // 타이머 == 결과
            int ret = 0;
            while (ret <= 1000 && shark.Count > 1)
            {

                // 타이머 증가
                ret++;
                while(shark.Count > 0)
                {

                    // 상어 이동할 곳 확인
                    var node = shark.Dequeue();
                    int[] myDir = pDirs[node.n][curDirs[node.n]];

                    bool exsistEmpty = false;
                    for (int i = 0; i < 4; i++)
                    {

                        // 빈공간부터 파악
                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC].shark > 0) continue;

                        exsistEmpty = true;
                        break;
                    }

                    if (exsistEmpty)
                    {

                        for (int i = 0; i < 4; i++)
                        {

                            // 빈공간이 있는경우 빈공간으로 먼저 이동!
                            int nextR = node.r + dirR[myDir[i]];
                            int nextC = node.c + dirC[myDir[i]];

                            if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC].shark > 0) continue;

                            // 이동 방향으로 상어의 방향 전환!
                            curDirs[node.n] = myDir[i];
                            // 상어 보관
                            calc.Enqueue((nextR, nextC, node.n));
                            break;
                        }

                        continue;
                    }

                    for (int i = 0; i < 4; i++)
                    {

                        // 빈 공간이 없는 경우 자신의 채취로 이동
                        int nextR = node.r + dirR[myDir[i]];
                        int nextC = node.c + dirC[myDir[i]];

                        if (ChkInvalidPos(nextR, nextC) || board[nextR, nextC].shark != node.n) continue;

                        // 바로 이동
                        curDirs[node.n] = myDir[i];
                        board[nextR, nextC].k = k + 1;
                        break;
                    }
                }

                while(calc.Count > 0)
                {

                    // 빈공간으로 이동한 상어들 쫓아내야하는지 확인
                    var node = calc.Dequeue();

                    int cur = board[node.r, node.c].shark;
                    if (cur == 0 || node.n < cur) 
                    { 
                        
                        // 아직 상어가 안온 경우나 자기보다 우선순위가 높은 상어만 들어 올 수 있다!
                        // 아니면 쫓겨난다
                        board[node.r, node.c].shark = node.n;
                        board[node.r, node.c].k = k + 1;
                    }
                }

                for (int r = 0; r < n; r++)
                {

                    for (int c = 0; c < n; c++)
                    {

                        // 채취 제거 및, 상어 위치 파악
                        if (board[r, c].k == 0) continue;
                        board[r, c].k--;
                        if (board[r, c].k == 0) board[r, c].shark = 0;
                        else if (board[r, c].k == k)
                        {

                            shark.Enqueue((r, c, board[r, c].shark));
                        }
                    }
                }
            }

            // 1_000초 넘어간 즉, 초과인 경우
            if (ret == 1_001) ret = -1;
            // 결과 출력
            Console.WriteLine(ret);

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= n || _c >= n) return true;
                return false;
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
public static class PS
{
    public class Shark
    {
        public (int r, int c) pos;
        public int num;
        public int dir;
        public bool isKicked;
    }

    public struct Scent
    {
        public int num;
        public int left;
    }

    public static (int r, int c)[] dir = { (-1, 0), (1, 0), (0, -1), (0, 1) }; //0:상 1:하 2:좌 3:우
    public static int[,,] priority;

    public static int n, m, k;
    public static int sharkCount;
    public static Shark[] sharkList;
    public static Shark[,] sharkMap;
    public static Scent[,] scentMap;

    static PS()
    {
        string[] input;

        //n, m, k
        input = Console.ReadLine().Split();
        n = int.Parse(input[0]);
        m = int.Parse(input[1]);
        k = int.Parse(input[2]);
        sharkCount = m;

        //격자
        sharkList = new Shark[m];
        sharkMap = new Shark[n, n];
        scentMap = new Scent[n, n];

        for (int i = 0; i < n; i++)
        {
            input = Console.ReadLine().Split();

            for (int j = 0; j < n; j++)
            {
                if (input[j][0] != '0')
                {
                    sharkMap[i, j] = new Shark { pos = (i, j), num = int.Parse(input[j]) - 1 };
                    sharkList[sharkMap[i, j].num] = sharkMap[i, j];
                }
            }
        }

        //상어 방향
        input = Console.ReadLine().Split();

        for (int i = 0; i < m; i++)
        {
            sharkList[i].dir = int.Parse(input[i]) - 1;
        }

        //우선순위
        priority = new int[m, 4, 4];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                input = Console.ReadLine().Split();

                for (int k = 0; k < 4; k++)
                {
                    priority[i, j, k] = int.Parse(input[k]) - 1;
                }
            }
        }
    }

    public static void Main()
    {
        int time = 0;

        while (time++ < 1000)
        {
            SpreadScent();
            MoveShark();

            if (sharkCount == 1)
            {
                Console.Write(time);
                return;
            }    

            FadeScent();
        }

        Console.Write(-1);
    }

    public static void SpreadScent()
    {
        for (int i = 0; i < m; i++)
        {
            if (sharkList[i].isKicked)
                continue;

            scentMap[sharkList[i].pos.r, sharkList[i].pos.c] = new Scent { num = i, left = k };
        }
    }

    public static void MoveShark()
    {
        Shark[,] nextSharkMap = new Shark[n, n];
        (int r, int c) nextSharkPos;
        bool flag;

        for (int i = 0; i < m; i++)
        {
            if (sharkList[i].isKicked)
                continue;

            flag = false;

            for (int j = 0; j < 4; j++)
            {
                nextSharkPos = 
                    (sharkList[i].pos.r + dir[priority[i, sharkList[i].dir, j]].r, 
                    sharkList[i].pos.c + dir[priority[i, sharkList[i].dir, j]].c);

                if (nextSharkPos.r < 0 || nextSharkPos.r >= n || nextSharkPos.c < 0 || nextSharkPos.c >= n)
                    continue;

                if (scentMap[nextSharkPos.r, nextSharkPos.c].left == 0)
                {
                    flag = true;

                    if (nextSharkMap[nextSharkPos.r, nextSharkPos.c] == null)
                    {
                        sharkList[i].pos = nextSharkPos;
                        sharkList[i].dir = priority[i, sharkList[i].dir, j];
                        nextSharkMap[nextSharkPos.r, nextSharkPos.c] = sharkList[i];

                    }
                    else
                    {
                        sharkList[i].isKicked = true;
                        sharkCount--;
                    }

                    break;
                }
            }

            if (!flag)
            {
                for (int j = 0; j < 4; j++)
                {
                    nextSharkPos =
                        (sharkList[i].pos.r + dir[priority[i, sharkList[i].dir, j]].r,
                        sharkList[i].pos.c + dir[priority[i, sharkList[i].dir, j]].c);

                    if (nextSharkPos.r < 0 || nextSharkPos.r >= n || nextSharkPos.c < 0 || nextSharkPos.c >= n)
                        continue;

                    if (scentMap[nextSharkPos.r, nextSharkPos.c].num == i)
                    {
                        if (nextSharkMap[nextSharkPos.r, nextSharkPos.c] == null)
                        {
                            sharkList[i].pos = nextSharkPos;
                            sharkList[i].dir = priority[i, sharkList[i].dir, j];
                            nextSharkMap[nextSharkPos.r, nextSharkPos.c] = sharkList[i];
                        }
                        else
                        {
                            sharkList[i].isKicked = true;
                            sharkCount--;
                        }

                        break;
                    }
                }
            }
        }

        sharkMap = nextSharkMap;
    }

    public static void FadeScent()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (scentMap[i, j].left > 0)
                    scentMap[i, j].left--;
            }
        }
    }
}
#elif other2
#pragma warning disable CS8602
namespace BOJ;
static class P19237
{
    static StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 102400);
    static StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 102400);
    static bool Step(int x, int y, int r, int c) => x < 0 || x >= r || y < 0 || y >= c;
    static string[] ReadSplit() => sr.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    static IEnumerable<(int, int)> Range(int r, int c) { for (int i = 0; i < r; ++i) for (int j = 0; j < c; ++j) yield return (i, j); }

    static int n, m, k; // gridsize, sharksize, smelllength
    class P
    {
        public int shark;
        public int smellWho;
        public int smellEnd;
        public override string ToString()
        {
            return $"{shark}-{smellWho}-{smellEnd}";
        }
    }
    static P[,] a;
    static P[,] na;
    static int[] sdir;
    static int[,,] sndir;
    static int scnt;
    static int ans;
    static int[] dx = { 0, -1, 1, 0, 0 };
    static int[] dy = { 0, 0, 0, -1, 1 };

    static void Move(int t)
    {
        if (scnt <= 1)
        {
            ans = t - 1;
            return;
        }
        if (t > 1000)
        {
            ans = -1;
            return;
        }

        foreach (var (x, y) in Range(n, n))
        {
            na[x, y].shark = 0;
            na[x, y].smellWho = a[x, y].smellWho;
            na[x, y].smellEnd = a[x, y].smellEnd;
        }

        // a to na
        foreach (var (x, y) in Range(n, n))
        {
            int si = a[x, y].shark;
            if (si == 0)
                continue;
            int cdir = sdir[si];

            // find next dir
            int nx, ny, ndir = -1;
            for (int i = 0; i < 4; ++i)
            {
                int d = sndir[si, cdir, i];
                nx = x + dx[d];
                ny = y + dy[d];
                if (Step(nx, ny, n, n))
                    continue;

                // first = empty smell
                // second = my smell
                if (a[nx, ny].smellEnd < t)
                {
                    ndir = d;
                    break;
                }
                if (ndir == -1 && a[nx, ny].smellWho == si)
                {
                    ndir = d;
                }
            }

            // move shark
            nx = x + dx[ndir];
            ny = y + dy[ndir];
            sdir[si] = ndir;
            if (na[nx, ny].shark != 0 && na[nx, ny].shark < si)
            {
                scnt -= 1;
                continue;
            }
            else if (na[nx, ny].shark > si)
            {
                scnt -= 1;
            }
            na[nx, ny].shark = si;
            na[nx, ny].smellWho = si;
            na[nx, ny].smellEnd = t + k;
        }

        (a, na) = (na, a);

        //sw.WriteLine("T:" + t);
        //for (int i = 0; i < n; ++i)
        //{
        //    for (int j = 0; j < n; ++j)
        //    {
        //        sw.Write(a[i, j]);
        //        sw.Write(" ");
        //    }
        //    sw.WriteLine();
        //}

        Move(t + 1);
    }

    static void Main()
    {
        int[] s = ReadSplit().Select(int.Parse).ToArray();
        n = s[0];
        m = s[1];
        k = s[2];
        a = new P[n, n];
        na = new P[n, n];
        for (int i = 0; i < n; ++i)
        {
            int j = 0;
            foreach (var si in ReadSplit().Select(int.Parse))
            {
                a[i, j] = new P();
                na[i, j] = new P();
                a[i, j].shark = si;
                a[i, j].smellWho = si;
                a[i, j].smellEnd = si == 0 ? 0 : k;
                j += 1;
            }
        }
        sdir = ReadSplit().Select(int.Parse).Prepend(0).ToArray();
        sndir = new int[m + 1, 5, 4]; // si
        for (int i = 1; i <= m; ++i)
        {
            for (int j = 1; j < 5; ++j)
            {
                int k = 0;
                foreach(var d in ReadSplit().Select(int.Parse))
                {
                    sndir[i, j, k] = d;
                    k += 1;
                }
            }
        }

        scnt = m;
        Move(1);
        sw.WriteLine(ans);
        sw.Flush();
    }
}
#endif
}
