using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 3
이름 : 배성훈
내용 : 지뢰찾기
    문제번호 : 4108번

    구현 문제다
    제목도 흥미롭고, 영상도 매우 흥미로운 문제다
    BFS 탐색으로 지뢰수를 탐색했다
*/

namespace BaekJoon.etc
{
    internal class etc_0439
    {

        static void Main439(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int[,] board = new int[100, 100];

            Queue<(int r, int c)> q = new(100 * 100);
            int[] dirR = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int[] dirC = { 0, 1, 1, 1, 0, -1, -1, -1 };

            while (true)
            {

                int row = ReadInt();
                int col = ReadInt();

                if (row == col && row == 0) break;

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        int cur = sr.Read();
                        board[r, c] = cur;

                        if (cur == '.') q.Enqueue((r, c));
                    }

                    sr.ReadLine();
                }

                while(q.Count > 0)
                {

                    var node = q.Dequeue();

                    int put = 0;
                    for (int i = 0; i < 8; i++)
                    {

                        int nextR = node.r + dirR[i];
                        int nextC = node.c + dirC[i];

                        if (ChkInvalidPos(nextR, nextC, row, col) || board[nextR, nextC] != '*') continue;
                        put++;
                    }

                    board[node.r, node.c] = '0' + put;
                }

                for (int r = 0; r < row; r++)
                {

                    for (int c = 0; c < col; c++)
                    {

                        char a = (char)board[r, c];
                        sw.Write(a);
                    }
                    sw.Write('\n');
                }
            }

            sr.Close();
            sw.Close();
            
            bool ChkInvalidPos(int _r, int _c, int _row, int _col)
            {

                if (_r < 0 || _r >= _row || _c < 0 || _c >= _col) return true;
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
// cs4108 - rby
// 2023-11-05 17:17:20
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cs4108
{
    internal class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static int N;
        static int M;
        static char[,] box;

        static void Main(string[] args)
        {
            int[] line;
            int count;
            string strline;
            while (true)
            {
                line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                N = line[0];
                M = line[1];

                if (N == 0 && M == 0) break;

                box = new char[N, M];
                for(int i = 0; i < N; i++)
                {
                    strline = sr.ReadLine();
                    for(int j = 0; j < M; j++)
                    {
                        box[i, j] = strline[j];
                    }
                }

                for(int i = 0; i < N; i++)
                {
                    for(int j = 0; j < M; j++)
                    {
                        if (box[i, j] == '*')
                        {
                            sb.Append('*');
                            continue;
                        }

                        count = 0;
                        for(int s = -1; s <= 1; s++)
                        {
                            for (int t = -1; t <= 1; t++)
                            {
                                count += Find(i + s, j + t);
                            }
                        }
                        sb.Append(count.ToString());
                    }
                    sb.AppendLine();
                }
            }

            sw.Write(sb);
            sw.Close();
            sr.Close();
        }

        static int Find(int i, int j)
        {
            if (i < 0 || i >= N || j < 0 || j >= M) return 0;
            if (box[i, j] == '*') return 1;
            else return 0;
        }
    }
}
#endif
}
