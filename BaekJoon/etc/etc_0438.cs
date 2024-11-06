using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 포스터 만들기
    문제번호 :  29693번

    구현 문제다
    출력 부분에서 YES 안했다고 엄청나게 틀렸다
    그리고, 조건에서 마크는 좌우 대칭 부분을 놓쳐서 Wrong 부분처럼
    해당 문제와 맞지 않는 코드가 짜이기도 했다
    
    주된 아이디어는 다음과 같다
    먼저 대칭이 되어야하므로 한쪽이 파란색이면 반대쪽도 파랑색이여야한다
    그래서 반쪽을 검색해서 색이 있으면 반대쪽을 채운다
    
    이후 중앙선을 기준으로 검색한다
    그리고, 중앙선이 비어있고 왼쪽이 비어있는 경우
    중앙선은 노랑색, 왼쪽은 흰색을 채우고 종료한다

    Wrong 부분은 좌우 대칭이 비어있는 점을 찾고 BFS 로 길찾아가는 코드다;
    경로가 노란색이 된다 그런데 해당 방법은 좌우 대칭이 보장되지 않는다;
*/

namespace BaekJoon.etc
{
    internal class etc_0438
    {

        static void Main438(string[] args)
        {

#if Wrong
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row, col];
            (int v, int r, int c)[,] visit = new (int v, int r, int c)[row, col];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int cur = sr.Read();
                    board[r, c] = cur;
                    if (cur == 'B') visit[r, c].v = -1;
                }

                sr.ReadLine();
            }

            sr.Close();

            Queue<(int r, int c)> q = new(row * col);

            int mid = (col - 1) / 2;
            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < mid; c++)
                {

                    if (board[r, c] != board[r, col - 1 - c]) continue;
                    if (board[r, c] == 'X') q.Enqueue((r, c));
                }
            }

            bool impo = true;
            Queue<(int r, int c)> bfs = new(row * col);
            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };
            int tryN = 0;
            
            while(q.Count > 0)
            {

                tryN++;
                var start = q.Dequeue();
                (int r, int c) end = (start.r, col - 1 - start.c);
                if (visit[start.r, start.c].v == 0)
                {

                    visit[start.r, start.c] = (tryN, -1, -1);
                    bfs.Enqueue(start);

                    while (bfs.Count > 0)
                    {

                        var node = bfs.Dequeue();

                        for (int i = 0; i < 4; i++)
                        {

                            int nextR = node.r + dirR[i];
                            int nextC = node.c + dirC[i];

                            if (ChkInValidPos(nextR, nextC) || visit[nextR, nextC].v != 0) continue;
                            visit[nextR, nextC] = (tryN, node.r, node.c);
                            bfs.Enqueue((nextR, nextC));
                        }
                    }
                }

                if (visit[end.r, end.c].v == visit[start.r, start.c].v)
                {

                    impo = false;

                    board[end.r, end.c] = 'W';
                    while (visit[end.r, end.c].r != -1)
                    {

                        end = (visit[end.r, end.c].r, visit[end.r, end.c].c);
                        board[end.r, end.c] = 'Y';
                    }

                    board[start.r, start.c] = 'W';
                    break;
                }
            }

            if (impo)
            {

                Console.WriteLine("NO");
                return;
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r, c] == 'X') board[r, c] = 'B';
                        sw.Write((char)board[r, c]);
                    }

                    sw.Write('\n');
                }
            }

            bool ChkInValidPos(int _r, int _c)
            {

                if (_r < 0 || _c < 0 || _r >= row || _c >= col) return true;
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
#else

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int row = ReadInt();
            int col = ReadInt();

            int[,] board = new int[row, col];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = sr.Read();
                }

                sr.ReadLine();
            }

            sr.Close();

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < (col / 2); c++)
                {

                    if (board[r, c] == board[r, col - 1 - c]) continue;
                    else if (board[r, c] == 'X' && board[r, col - 1 - c] != 'X') board[r, c] = board[r, col - 1 - c];
                    else if (board[r, c] != 'X' && board[r, col - 1 - c] == 'X') board[r, col - 1 - c] = board[r, c];
                }
            }

            bool impo = true;
            int mid = (col - 1) / 2;
            for (int r = 0; r < row; r++)
            {

                if (board[r, mid] != 'X') continue;
                if (board[r, mid - 1] == 'X')
                {

                    impo = false;

                    board[r, mid] = 'Y';
                    board[r, col - 1 - mid] = 'Y';

                    board[r, mid - 1] = 'W';
                    board[r, col - mid] = 'W';
                    break;
                }
            }

            if (impo)
            {

                Console.WriteLine("NO");
                return;
            }

            using (StreamWriter sw = new(Console.OpenStandardOutput()))
            {

                sw.WriteLine("YES");
                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        if (board[r, c] == 'X') board[r, c] = 'B';
                        sw.Write((char)board[r, c]);
                    }

                    sw.Write('\n');
                }
            }

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
#endif

        }
    }

#if other
import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Main {

  public static void main(String[] args) throws Exception {
    BufferedReader br = new BufferedReader(new InputStreamReader(System.in));

    String[] split = br.readLine().split("\\s");
    int Y = Integer.parseInt(split[0]);
    int X = Integer.parseInt(split[1]);

    if (X % 2 == 0) { // XXXX=>WYYW
      int sIdx = X / 2 - 2;
      int eIdx = X / 2 + 1;
      String defaultLine = br.readLine();
      for (int i = 1; i < Y - 1; i++) {
        if ("XXXX".equals(br.readLine().substring(sIdx, eIdx + 1))) {
          StringBuffer sb = new StringBuffer();
          for (int j = 0; j < i; j++) {
            sb.append(defaultLine).append("\n");
          }
          sb.append(defaultLine.substring(0, sIdx)).append("WYYW")
              .append(defaultLine.substring(eIdx + 1)).append("\n");
          for (int j = i + 1; j < Y; j++) {
            sb.append(defaultLine).append("\n");
          }
          System.out.println("YES");
          System.out.println(sb.toString());
          return;
        }
      }

    } else { // XXX=>WYW
      int sIdx = X / 2 - 1;
      int eIdx = X / 2 + 1;
      String defaultLine = br.readLine();
      for (int i = 1; i < Y - 1; i++) {
        if ("XXX".equals(br.readLine().substring(sIdx, eIdx + 1))) {
          StringBuffer sb = new StringBuffer();
          for (int j = 0; j < i; j++) {
            sb.append(defaultLine).append("\n");
          }
          sb.append(defaultLine.substring(0, sIdx)).append("WYW")
              .append(defaultLine.substring(eIdx + 1)).append("\n");
          for (int j = i + 1; j < Y; j++) {
            sb.append(defaultLine).append("\n");
          }
          System.out.println("YES");
          System.out.println(sb.toString());
          return;
        }
      }

    }

    System.out.println("NO");
  }
}

#endif
}
