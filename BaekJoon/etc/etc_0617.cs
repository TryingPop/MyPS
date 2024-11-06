using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 25
이름 : 배성훈
내용 : 제곱수 찾기
    문제번호 : 1025번

    브루트포스 문제다
    9자리까지 가능하다 -> 10억 미만의 수이다
    그래서 제곱수는 33_000개 이고 매번 확인해야한다
    확인 시간을 줄이기 위해 HashSet에 먼저 저장하고 포함되어져 있는지 확인했다

    깔끔하게 구현하고 싶었으나, 깔끔하게 구현이 안된거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0617
    {

        static void Main617(string[] args)
        {

            StreamReader sr;
            int row, col;
            int[][] board;

            HashSet<int> sq;

            Solve();

            void Solve()
            {

                Input();

                Init();

                int ret = -1;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        // 해당 자리를 중심으로 4방향 이동 확인
                        int start = board[r][c];
                        if (sq.Contains(start) && ret < start) ret = start;
                        // 이동
                        for (int dr = 0; dr < row; dr++)
                        {

                            for (int dc = 0; dc < col; dc++)
                            {

                                // 1자리는 처음에 체크했으므로 넘긴다
                                if (dr == 0 && dc == 0) continue;

                                // 먼저 ++ 방향
                                int chk = start;
                                int nextR = r;
                                int nextC = c;
                                while (true)
                                {

                                    nextR += dr;
                                    nextC += dc;
                                    if (nextR >= row || nextC >= col) break;
                                    chk = chk * 10 + board[nextR][nextC];
                                    if (sq.Contains(chk) && ret < chk) ret = chk;
                                }

                                // -+ 방향
                                chk = start;
                                nextR = r;
                                nextC = c;
                                while (true)
                                {

                                    nextR -= dr;
                                    nextC += dc;
                                    if (nextR < 0 || nextC >= col) break;
                                    chk = chk * 10 + board[nextR][nextC];
                                    if (sq.Contains(chk) && ret < chk) ret = chk;
                                }

                                // +- 방향
                                chk = start;
                                nextR = r;
                                nextC = c;
                                while (true)
                                {

                                    nextR += dr;
                                    nextC -= dc;
                                    if (nextR >= row || nextC < 0) break;
                                    chk = chk * 10 + board[nextR][nextC];
                                    if (sq.Contains(chk) && ret < chk) ret = chk;
                                }

                                // -- 방향
                                chk = start;
                                nextR = r;
                                nextC = c;
                                while(true)
                                {

                                    nextR -= dr;
                                    nextC -= dc;
                                    if (nextR < 0 || nextC < 0) break;
                                    chk = chk * 10 + board[nextR][nextC];
                                    if (sq.Contains(chk) && ret < chk) ret = chk;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(ret);
            }

            void Init()
            {

                // 제곱수 모음
                sq = new(31623);
                
                for (int i = 0; i <= 31622; i++)
                {

                    sq.Add(i * i);
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput());
                row = ReadInt();
                col = ReadInt();

                board = new int[row][];
                for (int r = 0; r < row; r++)
                {

                    board[r] = new int[col];
                    for (int c = 0; c < col; c++)
                    {

                        board[r][c] = sr.Read() - '0';
                    }

                    if (sr.Read() == '\r') sr.Read();
                }

                sr.Close();
            }

            int ReadInt()
            {

                int c, ret = 0;
                while(( c= sr.Read()) != -1 && c != ' ' && c != '\n')
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

#nullable disable

public class Program
{
    public static void Main()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), bufferSize: 65536);
        using var sw = new StreamWriter(Console.OpenStandardOutput(), bufferSize: 65536);

        var sq = new HashSet<int>();
        for (var v = 0; v * v <= 999999999; v++)
            sq.Add(v * v);

        var nm = sr.ReadLine().Split(' ').Select(Int32.Parse).ToArray();
        var n = nm[0];
        var m = nm[1];

        var a = new int[n][];
        for (var idx = 0; idx < n; idx++)
            a[idx] = sr.ReadLine().Select(v => v - '0').ToArray();

        var max = -1;

        for (var y = 0; y < n; y++)
            for (var x = 0; x < m; x++)
                for (var dy = -n; dy <= n; dy++)
                    for (var dx = -m; dx <= m; dx++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;

                        var dist = 0;
                        var num = 0;

                        while (true)
                        {
                            var newy = y + dist * dy;
                            var newx = x + dist * dx;
                            if (newx < 0 || newx >= m || newy < 0 || newy >= n)
                                break;

                            num = 10 * num + a[newy][newx];
                            if (sq.Contains(num))
                                max = Math.Max(max, num);

                            dist++;
                        }
                    }

        sw.WriteLine(max);
    }
}

#elif other2
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System;

class HelloWorld {
    static int N,M,answer = -1;
    static int[,] map;
  static void Main() {

      /*int start = 9;

      int root = (int)Math.Sqrt(start);
      WriteLine(Math.Pow(root,2) == start ? true:false);*/
      
      //행, 열개수 입력받기
      int[] colRow = Array.ConvertAll(ReadLine().Split(),int.Parse);
      N = colRow[0];
      M = colRow[1];
      
    //표 숫자 입력받기
      map = new int[N,M];
      
      string input;
      for(int i = 0; i<N; i++)
      {
          input = ReadLine();
          for(int j = 0; j<M; j++)
          {
              map[i,j] = input[j] - '0';
          }
      }
      
      //제곱수 만들기 함수 호출
      for(int i = 0; i<N; i++)
      {
          for(int j = 0; j<M; j++)
          {
              makeNum(j,i);
          }
      }
     
      WriteLine(answer);

  }

  static void makeNum(int c,int r)
  { 
      for(int i = -N; i<N; ++i)
      {
          for(int j = -M; j<M; ++j)
          {
              if(i==0 && j ==0)
                continue;
              
              int x,y;
              x = c;
              y = r;

              int sqr = 0;

              while(x>=0 && x<M && y>=0 && y<N)
                {
                    sqr *= 10;
                    sqr += map[y,x];

                    int root = (int)Math.Sqrt(sqr);

                    if(Math.Pow(root,2) == sqr)
                    {
                        answer = Math.Max(answer,sqr);
                    }

                    x+=j;
                    y+=i;

                }
          }
      }
  }
}
#endif
}
