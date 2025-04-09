using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 15
이름 : 배성훈
내용 : 감시 피하기
    문제번호 : 18428번

    브루트 포스, 백트래킹 문제이다
    DFS로 구현했으며 for문에서 이전 r과 c의 값을 계승해서 중복 진입을 방지했다
*/

namespace BaekJoon.etc
{
    internal class etc_0230
    {

        static void Main230(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[,] board = new int[n, n];
            List<(int r, int c)> teacher = new(36);
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    int chk = ReadInt(sr);

                    if (chk == 'X' - '0') chk = 0;
                    else if (chk == 'S' - '0') chk = 1;
                    else 
                    { 
                        
                        chk = 2;
                        teacher.Add((i, j));
                    }

                    board[i, j] = chk;
                }
            }

            sr.Close();

            int[] dirR = { -1, 1, 0, 0 };
            int[] dirC = { 0, 0, -1, 1 };
            bool ret = DFS(board, 0, teacher, dirR, dirC, n, 0, -1);

            if (ret) Console.WriteLine("YES");
            else Console.WriteLine("NO");
        }

        static bool DFS(int[,] _board, int _depth, List<(int r, int c)> _teacher, int[] _dirR, int[] _dirC, int _n, int _beforeR, int _beforeC)
        {

            if (_depth == 3)
            {

                for (int cur = 0; cur < _teacher.Count; cur++)
                {

                    int curR = _teacher[cur].r;
                    int curC = _teacher[cur].c;

                    for (int i = 0; i < 4; i++)
                    {

                        for (int j = 1; j < _n; j++)
                        {

                            int nextR = curR + j * _dirR[i];
                            int nextC = curC + j * _dirC[i];

                            if (ChkInvalidPos(nextR, nextC, _n) || _board[nextR, nextC] == -1) break;

                            if (_board[nextR, nextC] == 1) return false;
                        }
                    }
                }

                return true;
            }

            bool ret = false;

            for (int r = _beforeR; r < _n; r++)
            {

                int s = r == _beforeR ? _beforeC + 1 : 0;
                for (int c = s; c < _n; c++)
                {

                    if (_board[r, c] != 0) continue;
                    _board[r, c] = -1;

                    ret = ret || DFS(_board, _depth + 1, _teacher, _dirR, _dirC, _n, r, c);
                    _board[r, c] = 0;
                }
            }

            return ret;
        }

        static bool ChkInvalidPos(int _r, int _c, int _n)
        {

            if (_r < 0 || _r >= _n || _c < 0 || _c >= _n) return true;
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
