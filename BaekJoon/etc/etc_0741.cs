using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 5. 30
이름 : 배성훈
내용 : 구슬 탈출, 구슬 탈출 2
    문제번호 : 13459번, 13460번

    구현, 시뮬레이션, BFS 문제다
    앞에서 2048 Hard문제의 경험으로 DFS로 가지치기 하면서 해결했다

    구슬이 이동 안한 경우, 파란 공이 들어가는 경우 더 확인하지 않는다!
    구슬탈출 3, 4도 있으니 이는 힌트에 있는 BFS로 해야겠다
*/

namespace BaekJoon.etc
{
    internal class etc_0741
    {

        static void Main741(string[] args)
        {

            StreamReader sr;

            int[][] board;
            int row, col;

            (int r, int c) red = (0, 0);
            (int r, int c) blue = (0, 0);

            Solve();

            void Solve()
            {

                Input();

                int ret = DFS(1, red, blue);
                Console.Write(ret == 100 ? -1 : ret);
            }

#if one
            // 구슬탈출 1 문제
            int DFS(int _depth, (int r, int c) _red, (int r, int c) _blue)
            {

                if (_depth > 10) return 0;

                for (int i = 0; i < 4; i++)
                {

                    red = _red;
                    blue = _blue;

                    int chk = Op(i);
                    if (chk == 0) chk = DFS(_depth + 1, red, blue);
                    if (chk == 1) return 1;
                }

                return 0;
            }
#endif
            int DFS(int _depth, (int r, int c) _red, (int r, int c) _blue)
            {

                if (_depth > 10) return 100;
                int ret = 100;
                for (int i = 0; i < 4; i++)
                {

                    red = _red;
                    blue = _blue;

                    int chk = Op(i);
                    if (chk == 0) ret = Math.Min(ret, DFS(_depth + 1, red, blue));
                    if (chk == 1) return _depth;
                }

                return ret;
            }

            int Op(int _op)
            {

                switch (_op)
                {

                    case 0:
                        return U();

                    case 1:
                        return D();

                    case 2:
                        return L();

                    default:
                        return R();
                }
            }

            int U()
            {

                bool chk1;
                bool chk2;

                (int r, int c) bR = red;
                (int r, int c) bB = blue;

                chk1 = MoveR(-1, 0);
                chk2 = MoveB(-1, 0);

                if (blue == red)
                {

                    if (bB.r < bR.r) red.r += 1;
                    else blue.r += 1;
                }

                if (chk2 || (bB == blue && bR == red)) return -1;
                if (chk1) return 1;
                return 0;
            }

            int D()
            {

                bool chk1;
                bool chk2;

                (int r, int c) bR = red;
                (int r, int c) bB = blue;

                chk1 = MoveR(1, 0);
                chk2 = MoveB(1, 0);


                if (blue == red)
                {

                    if (bB.r < bR.r) blue.r -= 1;
                    else red.r -= 1;
                }

                if (chk2 ||(bB == blue && bR == red)) return -1;
                if (chk1) return 1;
                return 0;
            }

            int L()
            {

                bool chk1;
                bool chk2;

                (int r, int c) bR = red;
                (int r, int c) bB = blue;

                chk1 = MoveR(0, -1);
                chk2 = MoveB(0, -1);


                if (blue == red)
                {

                    if (bB.c < bR.c) red.c += 1;
                    else blue.c += 1;
                }

                if (chk2 || (bB == blue && bR == red)) return -1;
                if (chk1) return 1;
                return 0;
            }

            int R()
            {

                bool chk1;
                bool chk2;

                (int r, int c) bR = red;
                (int r, int c) bB = blue;

                chk1 = MoveR(0, 1);
                chk2 = MoveB(0, 1);


                if (blue == red)
                {

                    if (bB.c < bR.c) blue.c -= 1;
                    else red.c -= 1;
                }

                if (chk2 || (bB == blue && bR == red))  return -1;
                if (chk1) return 1;
                return 0;
            }

            bool MoveR(int _dr, int _dc)
            {

                int r = red.r;
                int c = red.c;

                bool ret = false;
                while(true)
                {

                    int nextR = r + _dr;
                    int nextC = c + _dc;

                    int val = board[nextR][nextC];
                    if (ChkInvalidPos(nextR, nextC) || val < 0) break;
                    if (val == 1) ret = true;

                    r = nextR;
                    c = nextC;
                }

                red = (r, c);
                return ret;
            }

            bool MoveB(int _dr, int _dc)
            {

                int r = blue.r;
                int c = blue.c;

                bool ret = false;
                while (true)
                {

                    int nextR = r + _dr;
                    int nextC = c + _dc;

                    int val = board[nextR][nextC];
                    if (ChkInvalidPos(nextR, nextC) || val < 0) break;
                    if (val == 1) ret = true;

                    r = nextR;
                    c = nextC;
                }

                blue = (r, c);
                return ret;
            }

            bool ChkInvalidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        if (cur == '.') continue;
                        if (cur == '#') board[r][c] = -1;
                        else if (cur == 'R') red = (r, c);
                        else if (cur == 'B') blue = (r, c);
                        else board[r][c] = 1;
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

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
        static int[] dy = new int[] { 1, 0, -1, 0 };
        static int[] dx = new int[] { 0, 1, 0, -1 };
        static int[,] map;
        static bool[,,,] visit;
        static int n;
        static int m;
        static bool answer;
        public static void Main(string[] args)
        {
            StreamReader input = new StreamReader(
                new BufferedStream(Console.OpenStandardInput()));
            StreamWriter output = new StreamWriter(
                new BufferedStream(Console.OpenStandardOutput()));
            int[] arr = Array.ConvertAll(input.ReadLine().Split(' '), int.Parse);
            n = arr[0]; m = arr[1];
            map = new int[n, m];
            visit = new bool[n, m, n, m];
            int rr = 0;
            int rc = 0;
            int br = 0;
            int bc = 0;
            for (int i = 0; i < n; i++)
            {
                string s = input.ReadLine();
                for (int j = 0; j < m; j++)
                {
                    if (s[j] == '#') map[i, j] = 1;
                    else if (s[j] == 'R')
                    {
                        rr = i;
                        rc = j;
                    }
                    else if (s[j] == 'B')
                    {
                        br = i;
                        bc = j;
                    }
                    else if (s[j] == 'O')
                    {
                        map[i, j] = 2;
                    }
                }
            }
            dfs(rr,rc,br,bc,1);

            output.Write(answer ? 1 : 0);
            input.Close();
            output.Close();
        }
        static void dfs(int rr, int rc, int br,int bc,int count)
        {
            if (answer || count > 10) return;

            for (int i = 0; i < 4; i++)
            {
                (int nbr, int nbc, int rm) = move(br, bc, i, rr, rc);
                if (map[nbr, nbc] == 2) continue;
                nbr -= (1 + rm) * dy[i];
                nbc -= (1 + rm) * dx[i];
                (int nrr, int nrc, int bm) = move(rr, rc, i, br, bc);
                if (map[nrr,nrc] == 2)
                {
                    answer = true;
                    return;
                }
                nrr -= (1 + bm) * dy[i];
                nrc -= (1 + bm) * dx[i];
                if (visit[nrr, nrc, nbr, nbc]) continue;
                visit[nrr, nrc, nbr, nbc] = true;
                dfs(nrr, nrc, nbr, nbc, count + 1);
                visit[nrr, nrc, nbr, nbc] = false;
            }
        }
        static (int,int,int) move(int row, int col,int dir,int dr,int dc)
        {
            int check = 0;
            while (map[row,col] != 1 && map[row,col] != 2)
            {
                row += dy[dir];
                col += dx[dir];
                if (row == dr && col == dc) check = 1;
            }
            return (row, col, check);
        }
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.IO;

// CapitalLetters for class names and methods, camelCase for variable names.
// Write your code clearly enough so that it doesn't need to be commented, or at least, so that it rarely needs to be commented.

namespace Testpad
{
    public class BaekJoon13459
    {
        static char[,] map; // 보드의 정보

        static int[] moveX = new int[4] { 1, -1, 0, 0 }; // 구슬의 x축 이동
        static int[] moveY = new int[4] { 0, 0, 1, -1 }; // 구슬의 y축 이동

        // 두 구슬의 좌표
        struct Pos
        {
            public int redX;
            public int redY;
            public int blueX;
            public int blueY;

            public Pos(int redX, int redY, int blueX, int blueY)
            {
                this.redX = redX;
                this.redY = redY;
                this.blueX = blueX;
                this.blueY = blueY;
            }
        }

        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            string[] inputs = sr.ReadLine().Split(" ");
            int y = int.Parse(inputs[0]); // 보드의 사이즈는 x * y (y가 세로축)
            int x = int.Parse(inputs[1]);

            map = new char[x + 1, y + 1];

            // 처음 주어진 구슬의 좌표
            int redFirstX = -1;
            int redFirstY = -1;
            int blueFirstX = -1;
            int blueFirstY = -1;

            // 보드의 정보를 입력받고, 구슬 2개의 위치를 저장함
            for (int i = 1; i <= y; i++)
            {
                char[] tempInputs = sr.ReadLine().ToCharArray();

                for (int j = 1; j <= x; j++)
                {
                    map[j, i] = tempInputs[j - 1];

                    // 구슬 2개의 위치를 저장함
                    switch (map[j, i])
                    {
                        case 'R':
                            redFirstX = j;
                            redFirstY = i;
                            break;

                        case 'B':
                            blueFirstX = j;
                            blueFirstY = i;
                            break;

                        default:
                            break;
                    }
                }
            }

            Console.WriteLine(BFS(redFirstX, redFirstY, blueFirstX, blueFirstY));
        }

        static int BFS(int redFirstX, int redFirstY, int blueFirstX, int blueFirstY)
        {
            Queue<Pos> queue = new Queue<Pos>(); // BFS에서 쓸 큐

            // 두 구슬의 좌표를 큐에 넣음
            queue.Enqueue(new Pos(redFirstX, redFirstY, blueFirstX, blueFirstY));

            int count = 0; // 보드를 기울인 횟수

            while (count < 10 && queue.Count > 0)
            {
                // 빨간 구슬을 넣는 것이 목적이니 빨간 구슬 중심으로 계산함
                int thisTime = queue.Count; // 이번에 연산할 횟수
                count++;

                for (int i = 0; i < thisTime; i++)
                {                 
                    // 4방향 이동, 두 구슬의 충돌 문제가 좀 골치아픈데..
                    // 생각해보니 그냥 끝까지 기울이면 알아서 조정된다.
                    for (int j = 0; j < 4; j++)
                    {
                        // 두 구슬의 위치
                        int redX = queue.Peek().redX;
                        int redY = queue.Peek().redY;
                        int blueX = queue.Peek().blueX;
                        int blueY = queue.Peek().blueY;

                        // 두 구슬이 구멍에 넣어졌는가?
                        bool redOut = false;
                        bool blueOut = false;

                        // Console.WriteLine("red = ({0}, {1}), blue = ({2}, {3}), count = {4}", redX, redY, blueX, blueY, count); // 디버그용

                        // 두 구슬 전부 벽이나 다른 구슬로 인해 이동 못할때까지 계속 이동시킴
                        // 이때, 구슬 하나가 다른 구슬에 닿아서 이동 못하고 다른 구슬 하나만 이동될 수 있으니
                        // 빨간색 구슬의 for문을 한번 더돌림
                        bool extraMove = false; // 빨간 구슬의 for문을 한번 더 돌릴지의 여부 

                        // 빨간 구슬의 이동
                        map[redX, redY] = '.'; // 구슬이 없어진 공간은 빈칸으로 만듬

                        for (int k = 1; k <= 9; k++)
                        {
                            int redMoveX = redX + moveX[j] * k;
                            int redMoveY = redY + moveY[j] * k;

                            // 구슬이 구멍에 빠졌다면, 구슬을 맵에서 제거하고, 구슬이 빠졌음을 저장
                            if (map[redMoveX, redMoveY] == 'O')
                            {
                                redOut = true;
                                // 빨간 구슬 위치 = 파란 구슬 위치로 만들어서 제거함
                                // 이때 구슬 위치는 매 계산마다 리셋되니 이래도 됨  
                                redX = blueX; 
                                redY = blueY;
                                break;
                            }

                            // 구슬이 벽이나 다른 구슬에 닿을때까지 굴림
                            // 함부로 이동경로 저장하자니 간 곳을 또 가야 하는 경우 (소코반을 생각하면..)가 있어서 안됨
                            // 만약 파란 구슬로 인해 경로가 막혔다면, 파란 구슬을 굴린 다음 한번 더 굴림
                            if (map[redMoveX, redMoveY] == '#')
                            {
                                redX = redMoveX - moveX[j];
                                redY = redMoveY - moveY[j];
                                break;
                            }
                            else if (redMoveX == blueX && redMoveY == blueY)
                            {
                                extraMove = true;
                                break;
                            }
                        }

                        // 파란 구슬의 이동
                        map[blueX, blueY] = '.';

                        for (int k = 1; k <= 9; k++)
                        {
                            int blueMoveX = blueX + moveX[j] * k;
                            int blueMoveY = blueY + moveY[j] * k;

                            if (map[blueMoveX, blueMoveY] == 'O')
                            {
                                blueOut = true;
                                break;
                            }

                            if (map[blueMoveX, blueMoveY] == '#' || (blueMoveX == redX && blueMoveY == redY))
                            {
                                blueX = blueMoveX - moveX[j];
                                blueY = blueMoveY - moveY[j];
                                break;
                            }
                        }

                        // 빨간 구슬의 재이동 (파란 구슬로 인해 진로가 막힌 경우)                       
                        if (extraMove == true && blueOut == false)
                        {
                            for (int k = 1; k <= 9; k++)
                            {
                                int redMoveX = redX + moveX[j] * k;
                                int redMoveY = redY + moveY[j] * k;

                                // 벽에 닿는 경우라면, 애초에 재이동할 필요가 없었음
                                if (redMoveX == blueX && redMoveY == blueY)
                                {
                                    redX = redMoveX - moveX[j];
                                    redY = redMoveY - moveY[j];
                                    break;
                                }
                            }
                        }

                        // 빨간 구슬만 빠졌다면, 1을 반환함
                        if (redOut == true && blueOut == false)
                        {
                            return 1;
                        }

                        // 디버그용
                        /*Console.WriteLine("==red : {0} , blue : {1}=======================", redOut, blueOut);
                        for (int n = 1; n <= y; n++)
                        {
                            for (int m = 1; m <= x; m++)
                            {
                                Console.Write(map[m, n]);
                            }

                            Console.WriteLine();
                        }*/

                        // 구슬이 구멍에 빠지지 않았다면, 구슬의 위치를 저장
                        if (redOut == false && blueOut == false)
                        {
                            queue.Enqueue(new Pos(redX, redY, blueX, blueY));
                        }
                    }

                    queue.Dequeue();
                }
            }

            // 구슬을 10번 이하로 빼낼 수 없는 경우, 0을 반환
            return 0;
        }
    }
}
#elif other_2
using System;

namespace Algorithm
{
    public class MainApp
    {
        private class Position
        {
            public int X;
            public int Y;

            public Position(int x, int y)
            {
                X = x;
                Y = y;
            }
        };
        private class State
        {
            public Position PosRed;
            public Position PosBlue;
            public int Cnt;

            public State(Position posRed, Position posBlue, int cnt)
            {
                PosRed = posRed;
                PosBlue = posBlue;
                Cnt = cnt;
            }
        };

        private static string[] m_Map = new string[10];
        private static bool[,,,] m_bCache = new bool[10, 10, 10, 10];
        private static int[] m_DX = { 1, -1, 0, 0 };
        private static int[] m_DY = { 0, 0, 1, -1 };

        static void Main(string[] args)
        {
            string[] inputLine = Console.ReadLine().Split(' ');
            int N = int.Parse(inputLine[0]);
            int M = int.Parse(inputLine[1]);

            Position? posR = null;
            Position? posB = null;
            for (int i = 0; i < N; ++i)
            {
                m_Map[i] = Console.ReadLine();
                for (int j = 0; j < M; ++j)
                {
                    if (m_Map[i][j] == 'R')
                    {
                        posR = new Position(j, i);

                    }
                    else if (m_Map[i][j] == 'B')
                    {
                        posB = new Position(j, i);
                    }
                }
            }

            Console.Write(Bfs(posR, posB));
        }

        private static int Bfs(Position posR, Position posB)
        {
            int result = int.MaxValue;

            Queue<State> que = new Queue<State>();
            que.Enqueue(new State(posR, posB, 0));

            m_bCache[posR.Y, posR.X, posB.Y, posB.X] = true;

            while (que.Count > 0)
            {
                State curState = que.Dequeue();

                if (curState.Cnt >= 10)
                {
                    continue;
                }

                for (int direction = 0; direction < 4; ++direction)
                {
                    Position redPos = curState.PosRed;
                    Position bluePos = curState.PosBlue;

                    bool bRedBreak = false;
                    bool bBlueBreak = false;
                    bool bBlueFound = false;
                    bool bRedFound = false;
                    while (!(bRedBreak && bBlueBreak))
                    {
                        bRedBreak = bBlueBreak = false;

                        int nextRedPosX = redPos.X + m_DX[direction];
                        int nextRedPosY = redPos.Y + m_DY[direction];
                        int nextBluePosX = bluePos.X + m_DX[direction];
                        int nextBluePosY = bluePos.Y + m_DY[direction];

                        if ((redPos.X == -1 && redPos.Y == -1) ||
                            m_Map[nextRedPosY][nextRedPosX] == '#' ||
                            (nextRedPosX == bluePos.X && nextRedPosY == bluePos.Y))
                        {
                            bRedBreak = true;
                        }
                        else if (m_Map[nextRedPosY][nextRedPosX] == 'O')
                        {
                            bRedBreak = true;
                            bRedFound = true;
                            redPos = new Position(-1, -1);
                        }
                        else
                        {
                            redPos = new Position(nextRedPosX, nextRedPosY);
                        }

                        if ((bluePos.X == -1 && bluePos.Y == -1) ||
                            m_Map[nextBluePosY][nextBluePosX] == '#' ||
                            (nextBluePosX == redPos.X && nextBluePosY == redPos.Y))
                        {
                            bBlueBreak = true;
                        }
                        else if (m_Map[nextBluePosY][nextBluePosX] == 'O')
                        {
                            bBlueBreak = true;
                            bBlueFound = true;
                            bluePos = new Position(-1, -1);
                        }
                        else
                        {
                            bluePos = new Position(nextBluePosX, nextBluePosY);
                        }
                    }

                    if (bBlueFound)
                    {
                        continue;
                    }
                    if (bRedFound)
                    {
                        result = Math.Min(result, curState.Cnt + 1);
                        continue;
                    }

                    if (m_bCache[redPos.Y, redPos.X, bluePos.Y, bluePos.X])
                    {
                        continue;
                    }
                    m_bCache[redPos.Y, redPos.X, bluePos.Y, bluePos.X] = true;
                    que.Enqueue(new State(redPos, bluePos, curState.Cnt + 1));
                }
            }

            result = (result == int.MaxValue ? -1 : result);
            return result;
        }
    }
}
#elif other2_2
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
class main {
    static StreamReader rd = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    static StreamWriter wr = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
    static StringBuilder std = new StringBuilder();
    static int y; static int x;
    static char[,] arr;
    static int[] b;
    static int[] r;
  static void Main(){
      int[] inp = Array.ConvertAll(rd.ReadLine().Split(), int.Parse);
      y = inp[0]; x = inp[1];
      arr = new char[y,x];
      b = new int[2];
      r = new int[2];
      for(int i = 0; i < y; i++)
      {
          string s = rd.ReadLine();
          for(int j = 0; j < x; j++)
          {
              arr[i,j] = s[j];
              if(s[j] == 'R')
              {
                  r[0] = i;
                  r[1] = j;
              }
              else if(s[j] == 'B')
              {
                  b[0] = i;
                  b[1] = j;
              }
          }
      }
      std.Append(bfs());
      
      wr.Write(std);
      wr.Close();
  }
  static int bfs()
  {
      Queue<pos> qu = new Queue<pos>();
      qu.Enqueue(new pos(b[0], b[1], r[0], r[1], 1));
      bool[,,,] visit = new bool[y,x,y,x];
      visit[b[0], b[1], r[0], r[1]] = true;
      
      int[,] move = new int[,] {{-1,0}, {0,1}, {1,0}, {0,-1}};
      
      while(qu.Count > 0)
      {
          pos data = qu.Dequeue();
          
          if(data.level > 10)
          {
              return -1;
          }
          
          for(int i = 0; i < 4; i++)
          {
              int curby = data.by;
              int curbx = data.bx;
              int curry = data.ry;
              int currx = data.rx;
              int level = data.level;
              
              bool isRedFinsh = false;
              bool isBlueFinsh = false;
              
              while(arr[curry + move[i,0], currx + move[i,1]] != '#')
              {
                  curry += move[i,0];
                  currx += move[i,1];
                  if(arr[curry, currx] == 'O')
                  {
                      isRedFinsh = true;
                      break;
                  }
              }
              while(arr[curby + move[i,0], curbx + move[i,1]] != '#')
              {
                  curby += move[i,0];
                  curbx += move[i,1];
                  if(arr[curby, curbx] == 'O')
                  {
                      isBlueFinsh = true;
                      break;
                  }
              }
              
              if(isBlueFinsh)
              {
                  continue;
              }
              else if(isRedFinsh)
              {
                  return level;
              }
              
              if(curry == curby && curbx == currx)
              {
                  switch(i)
                  {
                      case 0:
                      {
                          if(data.ry > data.by)
                          {
                              curry -= move[i,0];
                          }
                          else
                          {
                              curby -= move[i,0];
                          }
                          break;
                      }
                      case 1:
                      {
                          if(data.rx > data.bx)
                          {
                              curbx -= move[i,1];
                          }
                          else
                          {
                              currx -= move[i,1];
                          }
                          break;
                      }
                      case 2:
                      {
                          if(data.ry > data.by)
                          {
                              curby -= move[i,0];
                          }
                          else
                          {
                              curry -= move[i,0];
                          }
                          break;
                      }
                      case 3:
                      {
                          if(data.rx > data.bx)
                          {
                              currx -= move[i,1];
                          }
                          else
                          {
                              curbx -= move[i,1];
                          }
                          break;
                      }
                  }
              }
              
              if(!visit[curby, curbx, curry, currx])
              {
                  visit[curby, curbx, curry, currx] = true;
                  qu.Enqueue(new pos(curby, curbx, curry, currx, level + 1));
              }
          }
      }
      return -1;
  }
}
class pos
{
    public int by;
    public int bx;
    public int ry;
    public int rx;
    public int level;
    public pos(int a, int b, int c, int d, int e)
    {
        by = a;
        bx = b;
        ry = c;
        rx = d;
        level = e;
    }
}
#endif
}
