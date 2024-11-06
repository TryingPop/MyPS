using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 24
이름 : 배성훈
내용 : 마법사 상어와 파이어스톰
    문제번호 : 20058번

    구현, 시뮬레이션 문제다
    문제를 잘못이해해 한참을 고민했다;

    먼저 얼음이 녹는 설명부분 표현이 모호하다
    구현 능력이 아닌 비문학 능력으로 테스트하는 줄 알았다;
    뒤에 잘못구현했을 때 찾아보니 모호하다고 사람들이 글을 올린게 있었다

    예를들어
      1   2   3   4
      5   6   7   8
      9  10  11  12
     13  14  15  16

    을 2로 회전시키면
      9  10   1   2
     13  14   5   6
     11  12   3   4
     15  16   7   8
    인줄 알았는데,

    문제에서 요구하는 것은
     13   9   5   1
     14  10   6   2
     15  11   7   3
     16  12   8   4    
    이었다

    잘못된 방법으로 하면 예제 3에서
        3 5
        1 2 3 4 5 6 7 8
        8 7 6 5 4 3 2 1
        1 2 3 4 5 6 7 8
        8 7 6 5 4 3 2 1
        1 2 3 4 5 6 7 8
        8 7 6 5 4 3 2 1
        1 2 3 4 5 6 7 8
        8 7 6 5 4 3 2 1
        1 2 0 3 2

    다른 결과가 나온다
    잘못된 방법인 경우 시행하고 나면
        5 3 7 1 6 3 6 0
        3 4 6 2 5 4 7 1
        6 3 8 1 6 3 7 0
        5 4 7 2 5 4 7 1
        1 7 4 5 2 7 4 5
        0 7 3 6 1 8 3 6
        1 7 4 5 2 6 4 3
        0 6 3 6 1 7 3 5
    로 두번째 값이 60으로 64와 다르다;

    그래서 질문게시판을 읽어보니,
    단순히 격자별로 그룹짓지 않고 반시계만 회전해주면 되었다;

    이후 구현하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0993
    {

        static void Main993(string[] args)
        {

            StreamReader sr;

            int[][] board;
            bool[][] melt;
            int n, k;
            int[] dirR, dirC;
            int ret1, ret2;
            Queue<(int r, int c)> q;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                for (int i = 0; i < k; i++)
                {

                    int l = ReadInt();

                    Rotate(l);

                    Melt();
                }

                ret1 = 0;
                ret2 = 0;

                CntIce();

                Console.Write($"{ret1}\n{ret2}");
            }

            void CntIce()
            {

                q = new(n * n);
                var visit = melt;
                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        int cur = board[r][c];
                        ret1 += cur;

                        if (visit[r][c]) continue;
                        visit[r][c] = true;

                        if (board[r][c] == 0) continue;
                        q.Enqueue((r, c));
                        ret2 = Math.Max(ret2, BFS());
                    }
                }
            }

            int BFS()
            {

                var visit = melt;
                int ret = 0;
                while (q.Count > 0)
                {

                    var node = q.Dequeue();
                    ret++;

                    for (int i = 0; i < 4; i++)
                    {

                        int nR = node.r + dirR[i];
                        int nC = node.c + dirC[i];

                        if (ChkInvalidPos(nR, nC) || visit[nR][nC]) continue;
                        visit[nR][nC] = true;

                        if (board[nR][nC] > 0) q.Enqueue((nR, nC));
                    }
                }

                return ret;

                bool ChkInvalidPos(int _r, int _c)
                {

                    return _r < 1 || _c < 1 || _r > n || _c > n;
                }
            }

#if WRONG
            void Rotate(int _l)
            {

                if (_l == 0) return;
                int groupSize = 1 << _l;
                int moveSize = groupSize >> 1;

                int gLen = n / groupSize;
                for (int i = 0; i < gLen; i++)
                {

                    for (int j = 0; j < gLen; j++)
                    {

                        int r = groupSize * i + 1;
                        int c = groupSize * j + 1;

                        for (int addR = 0; addR < moveSize; addR++)
                        {

                            for (int addC = 0; addC < moveSize; addC++)
                            {

                                int curR = r + addR;
                                int curC = c + addC;
                                int temp = board[curR][curC];
                                board[curR][curC] = board[curR + moveSize][curC];
                                board[curR + moveSize][curC] = board[curR + moveSize][curC + moveSize];
                                board[curR + moveSize][curC + moveSize] = board[curR][curC + moveSize];
                                board[curR][curC + moveSize] = temp;
                            }
                        }
                    }
                }
            }
#else
            void Rotate(int _l)
            {

                if (_l == 0) return;
                int groupSize = 1 << _l;
                int moveSize = groupSize >> 1;

                int gLen = n / groupSize;
                for (int i = 0; i < gLen; i++)
                {

                    for (int j = 0; j < gLen; j++)
                    {

                        int sR = groupSize * i + 1;
                        int sC = groupSize * j + 1;

                        int eR = groupSize * (i + 1);
                        int eC = groupSize * (j + 1);

                        for (int addR = 0; addR < moveSize; addR++)
                        {

                            for (int addC = 0; addC < moveSize; addC++)
                            {

                                int temp = board[sR + addR][sC + addC];

                                board[sR + addR][sC + addC] = board[eR - addC][sC + addR];
                                board[eR - addC][sC + addR] = board[eR - addR][eC - addC];
                                board[eR - addR][eC - addC] = board[sR + addC][eC - addR];
                                board[sR + addC][eC - addR] = temp;
                            }
                        }
                    }
                }
            }
#endif

            void Melt()
            {

                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        int ice = 0;
                        for (int i = 0; i < 4; i++)
                        {

                            int nR = r + dirR[i];
                            int nC = c + dirC[i];

                            if (board[nR][nC] > 0) ice++;
                        }

                        if (ice < 3 && board[r][c] > 0) melt[r][c] = true;
                    }
                }

                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        if (melt[r][c])
                        {

                            melt[r][c] = false;
                            board[r][c]--;
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = 1 << ReadInt();
                k = ReadInt();

                board = new int[n + 2][];
                melt = new bool[n + 2][];

                for (int i = 0; i < board.Length; i++)
                {

                    board[i] = new int[n + 2];
                    melt[i] = new bool[n + 2];
                }

                for (int r = 1; r <= n; r++)
                {

                    for (int c = 1; c <= n; c++)
                    {

                        board[r][c] = ReadInt();
                    }
                }

                dirR = new int[4] { -1, 0, 1, 0 };
                dirC = new int[4] { 0, -1, 0, 1 };
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp1
{
    public class Program
    {
        public static int N, Q, ans, mapSize;
        public static int[,] map = null;
        public static int[,] tempMap = null;
        public static bool[,] vi = null;

        public static List<int> l = null;

        public static int[] dr = new int[] {-1, 0, 1, -0}; //1
        public static int[] dc = new int[] {0, 1, 0, -1}; //0

        public static StringBuilder sb = new StringBuilder();
        public static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        public static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

        public static void Main()
        {
            string[] tmp = sr.ReadLine().Split(' ');
            N = Convert.ToInt32(tmp[0]);
            Q = Convert.ToInt32(tmp[1]);
            mapSize =  1 << N;
            map = new int[mapSize, mapSize];
            tempMap = new int[mapSize, mapSize];
            vi = new bool[mapSize, mapSize];

            for (int i = 0; i < mapSize; i++)
            {
                tmp = sr.ReadLine().Split(' ');
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = Convert.ToInt32(tmp[j]);
                    vi[i, j] = false;
                }
            }

            tmp = sr.ReadLine().Split(' ');
            l = new List<int>();
            for (int i = 0; i < Q; i++)
            {
                l.Add(Convert.ToInt32(tmp[i]));
            }

            //파이어스톰!!
            for (int i = 0; i < Q; i++)
            {
                if (l[i] != 0)
                    rotate(l[i]);
                
                updateIce();
            }

            // 남아있는 얼음의 합
            int remains = 0;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    remains += map[i, j];
                }
            }

            Console.WriteLine(remains.ToString());


            // 남아있는 얼음 중 가장 큰 덩어리가 차지하는 칸의 개수
            int ans = 0;
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (vi[i, j] == false && map[i, j] > 0)
                        ans = Math.Max(ans, bfs(i, j));
                    
                }
            }

            if (ans < 2) Console.WriteLine('0'); // 한칸 집합 예외처리
            else Console.WriteLine(ans);
        }


        public static void updateIce()
        {
            List<(int, int)> v = new List<(int, int)>();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map[i, j] == 0) continue;
                    int adjIce = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        int ny = i + dr[k];
                        int nx = j + dc[k];
                        if (ny < 0 || nx < 0 || ny >= mapSize || nx >= mapSize || map[ny, nx] == 0) continue;
                        adjIce++;
                    }

                    if (adjIce < 3)
                        v.Add((i, j));
                }
            }

            foreach (var tuple in v)
            {
                map[tuple.Item1, tuple.Item2] -= 1;
            }
        }


// 하나의 회전 단위의 시작점 탐색 후

        public static void rotate(int L)
        {
            int n = 1 << L; //bit\

            for (int mapPosY = 0; mapPosY < mapSize; mapPosY += n)
            {
                for (int mapPosX = 0; mapPosX < mapSize; mapPosX += n)
                {
                    // 시계방향으로 90도 회전
                    {
                        for (int j = mapPosX, ii = 0; j < mapPosX + n; j++, ii++)
                        {
                            for (int i = mapPosY + n - 1, jj = 0; i >= mapPosY; i--, jj++)
                            {
                                tempMap[ii, jj] = map[i, j];
                            }
                        }

                        for (int i = mapPosY, ii = 0; i < mapPosY + n; i++, ii++)
                        {
                            for (int j = mapPosX, jj = 0; j < mapPosX + n; j++, jj++)
                            {
                                map[i, j] = tempMap[ii, jj];
                            }
                        }
                    }
                }
            }
        }

        private static int bfs(int x, int y)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
        
            int ret = 1;
        
            queue.Enqueue((x, y));
            vi[x, y] = true;
        
            while (queue.Count != 0)
            {
                var item = queue.Dequeue();
                int cy = item.Item1;
                int cx = item.Item2;
        
        
                for (int i = 0; i < 4; i++)
                {
                    int ny = cy + dr[i];
                    int nx = cx + dc[i];
                    if (ny < 0 || nx < 0 || ny >= mapSize || nx >= mapSize) continue;
                    if (vi[ny, nx] == false && map[ny, nx] > 0)
                    {
                        queue.Enqueue((ny, nx));
                        vi[ny, nx] = true;
                        ret++;
                    }
                }
            }
        
            return ret;
        }
        
        public static int dfs(int x, int y) {
            int cnt=1;
            vi[x,y]=true;
            if(x>0 && !vi[x-1,y] && map[x-1,y]>0)cnt+=dfs(x-1,y);
            if(y>0 && !vi[x,y-1] && map[x,y-1]>0)cnt+=dfs(x,y-1);
            if(x<mapSize-1 && !vi[x+1,y] && map[x+1,y]>0)cnt+=dfs(x+1,y);
            if(y<mapSize-1 && !vi[x,y+1] && map[x,y+1]>0)cnt+=dfs(x,y+1);
		
            return cnt;
        }
    }
   
}
#endif
}
