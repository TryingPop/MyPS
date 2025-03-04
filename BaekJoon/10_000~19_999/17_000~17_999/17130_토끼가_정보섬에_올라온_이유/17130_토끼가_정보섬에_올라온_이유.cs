using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 토끼가 정보섬에 올라온 이유
    문제번호 : 17130번

    문을 만나면 바로 탈출하는 줄 알고 바로 탈출해서 한번 틀렸다
    문을 만나도 더 탐색할지 선택할 수 있다;

    그리디한 BFS 탐색으로 당근 먹은 개수를 기록하며 풀었다
    시간은 152ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0195
    {

        static void Main195(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            int[,] board = new int[row, col];
            int[,] memo = new int[row, col];

            Queue<(int r, int c)> q = new Queue<(int r, int c)>(2 * row);

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    int read = sr.Read();
                    if (read == 'R') 
                    { 
                        
                        // 시작지점
                        q.Enqueue((r, c)); 
                        memo[r, c] = 1;
                    }
                    // 당근
                    else if (read == 'C') board[r, c] = 1;
                    // 문
                    else if (read == 'O') board[r, c] = 2;
                    // 벽
                    else if (read == '#') board[r, c] = -1;
                }
                sr.ReadLine();
            }

            int[] dirR = { -1, 1, 0 };
            int[] dirC = { 1, 1, 1 };
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 3; i++)
                {

                    int nextR = node.r + dirR[i];
                    int nextC = node.c + dirC[i];

                    // 벽이나 막힌 경우 못지나간다
                    if (ChkInvalidPos(nextR, nextC, row, col) || board[nextR, nextC] == -1) continue;

                    int chk = memo[node.r, node.c];
                    // 당근이면 당근 개수 추가
                    if (board[nextR, nextC] == 1) chk++;
                    // 처음 방문만 memo에 넣는다
                    // -> 방향 탐색이므로 1번만 넣으면 된다!
                    if (memo[nextR, nextC] == 0) q.Enqueue((nextR, nextC));
                    // 더 큰 값이 있는경우 값 갱신
                    if (memo[nextR, nextC] < chk) memo[nextR, nextC] = chk;

                }
            }

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    // 문 찾기
                    if (board[r, c] == 2) q.Enqueue((r, c));
                }
            }

            int max = 0;
            while(q.Count > 0)
            {

                // 문 중에서 최대 당근 먹은 경우를 찾는다
                var node = q.Dequeue();
                if (max < memo[node.r, node.c]) max = memo[node.r, node.c];
            }

            Console.WriteLine(max - 1);
        }

        static bool ChkInvalidPos(int _r, int _c, int _row, int _col)
        {

            if (_r < 0 || _r >= _row || _c < 0 || _c >= _col) return true;
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
}
