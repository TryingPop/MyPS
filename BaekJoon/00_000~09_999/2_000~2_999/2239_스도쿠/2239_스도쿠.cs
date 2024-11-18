using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 5
이름 : 배성훈
내용 : 스도쿠
    문제번호 : 2239번

    일부 숫자가 적힌 스도쿠 보드판에 완벽한 스도쿠를 만드는 문제이다
    아닌 경우 끊고 나와야해서 백트래킹?을 쓰는 문제이다

    1.2초 나왔다
*/

namespace BaekJoon.etc
{
    internal class etc_0150
    {

        static void Main150(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[][] board = new int[9][];

            for (int i = 0; i < 9; i++)
            {

                string str = sr.ReadLine();
                board[i] = new int[9];

                for (int j = 0; j < 9; j++)
                {

                    board[i][j] = str[j] - '0';
                }
            }

            sr.Close();
            bool complete = false;
            DFS(board, 0, 0, ref complete);

            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {

                    sw.Write(board[i][j]);
                }

                sw.Write('\n');
            }

            sw.Close();
        }

        static void DFS(int[][] _board, int _curX, int _curY, ref bool _complete)
        {

            // 완료되면 탈출
            if (_complete) return;
            else if (_curX == 9)
            {

                _complete = true;
                return;
            }

            int nextX = _curX;
            int nextY = _curY + 1;

            if (nextY >= 9) 
            {

                nextX += 1;
                nextY -= 9;
            }

            if (_board[_curX][_curY] != 0)
            {

                DFS(_board, nextX, nextY, ref _complete);
                return;
            }

            for (int i = 1; i <= 9; i++)
            {

                _board[_curX][_curY] = i;
                if (ChkOverlap(_board, _curX, _curY)) continue;

                DFS(_board, nextX, nextY, ref _complete);

                // 완료되면 끊고 탈출
                if (_complete) return;
            }

            // 완료되면 탈출
            if (_complete) return;

            // 못찾으면 초기화
            _board[_curX][_curY] = 0;
        }
        
        static bool ChkOverlap(int[][] _board, int _curX, int _curY)
        {

            for (int i = 0; i < 9; i++)
            {

                if (_curY == i) continue;
                if (_board[_curX][_curY] == _board[_curX][i]) return true;
            }

            for (int i = 0; i < 9; i++)
            {

                if (_curX == i) continue;
                if (_board[_curX][_curY] == _board[i][_curY]) return true;
            }

            int chkX = _curX / 3;
            int chkY = _curY / 3;

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {

                    if (_curX == chkX * 3 + i && _curY == chkY * 3 + j) continue;
                    if (_board[_curX][_curY] == _board[chkX * 3 + i][chkY * 3 + j]) return true;
                }
            }

            return false;
        }
    }
#if other
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;
using System.Data;

namespace Algorithm
{
    class Program
    {
        
        static int n = 9;
        static int[,] map = new int[n,n];
        static int[,] res = new int[n,n];
        static bool[,] ver = new bool[n+1,n+1];
        static bool[,] hor = new bool[n+1,n+1];
        static bool[,] sqr = new bool[n+1,n+1];
        static void wornl(int cnt)
        {
            int x = cnt/n;
            int y = cnt%n;
            if(cnt == 81)
            {
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i<n; i++)
                {
                    for(int j =0; j<n; j++)
                        sb.Append(map[i,j]);
                    sb.AppendLine();
                    
                }
                Console.WriteLine(sb);
                Environment.Exit(0);
            }

            if(map[x,y] == 0)
            {
                for(int i = 1; i<=9; i++)
                {
                    if(hor[x,i]||ver[y,i]||sqr[(x/3)*3+(y/3),i])
                        continue;
                    hor[x,i] = true;
                    ver[y,i] = true;
                    sqr[(x/3)*3+(y/3),i] = true;
                    map[x,y] = i;
                    wornl(cnt+1);
                    hor[x,i] = false;
                    ver[y,i] = false;
                    sqr[(x/3)*3+(y/3),i] = false;
                    map[x,y] = 0;
                }
            }
            else
                wornl(cnt+1);
        }
        static void Main()
        {
            StreamReader sr = new StreamReader(Console.OpenStandardInput());
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for(int i = 0; i<9; i++)
            {
                string s = sr.ReadLine();
                for(int j = 0; j<9; j++)
                {
                    int a = s[j]-48;
                    if(a != 0)
                    {
                        hor[i,a] = true;
                        ver[j,a] = true;
                        sqr[(i/3)*3+(j/3),a] = true;
                    }
                    map[i,j] = a;
                }
            }
            wornl(0);
            
            sw.WriteLine(sb);
            sr.Close();
            sw.Close(); 
            
        }
        
    }
}
#elif other2
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
class main {
    static StreamReader rd = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
    static StreamWriter wr = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
    static StringBuilder std = new StringBuilder();
    static int[,] arr;
    static bool finsh;
    static bool[,] garo;
    static bool[,] sero;
    static bool[,] box;
    static List<int[]> nullbox;
  static void Main(){
      arr = new int[9,9];
      garo = new bool[9,10];
      sero = new bool[10,9];
      box = new bool[9,10];
      nullbox = new List<int[]>();
      for(int i = 0; i < 9; i++)
      {
          string s = rd.ReadLine();
          for(int j = 0; j < 9; j++)
          {
              int py = i/3;
              int px = j/3;
              int data = s[j] - '0';
              arr[i,j] = data;
              if(data == 0)
              {
                  nullbox.Add(new int[]{i,j});
              }
              garo[i,data] = true;
              sero[data,j] = true;
              box[py*3+px, data] = true;
          }
      }
      finsh = false;
      stokue(0);
      
      wr.Write(std);
      wr.Close();
  }
  static void stokue(int level)
  {
      if(level == nullbox.Count)
      {
          for(int i = 0; i < 9; i++)
          {
              for(int j = 0; j < 9; j++)
              {
                  std.Append(arr[i,j]);
              }
              std.Append("\n");
          }
          finsh = true;
      }
      if(finsh)
      {
          return;
      }
      int[] data = nullbox[level];
      int y = data[0];
      int x = data[1];
      for(int k = 1; k <= 9; k++)
      {
          if(check(y,x,k))
          {
            int py = y/3;
            int px = x/3;
            arr[y,x] = k;
            garo[y,k] = true;
            sero[k,x] = true;
            box[py*3+px,k] = true;
            stokue(level + 1);
            arr[y,x] = 0;
            garo[y,k] = false;
            sero[k,x] = false;
            box[py*3+px,k] = false;
          }
       }
  }
  static bool check(int y, int x, int n)
  {
      int py = y / 3;
      int px = x / 3;
      if(garo[y,n] || sero[n,x] || box[py*3+px,n])
      {
          return false;
      }
      return true;
  }
}


#endif
}
