using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 14
이름 : 배성훈
내용 : Forest Picture
    문제번호 : 14981번

    구현 문제다.
    조건대로 그림 그리는것을 구현하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1703
    {

        static void Main1703(string[] args)
        {

            int EXIT = -1_000_000_001;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int m, n = 0;
            char[][] board;
            (int s, int x, int y)[] tree;

            Init();

            while (Input())
            {

                GetRet();
                sw.Write('\n');
            }

            void Init()
            {

                int MAX_M = 100;
                int MAX_N = 100_000;

                board = new char[MAX_M][];
                for (int i = 0; i < MAX_M; i++)
                {

                    board[i] = new char[MAX_M];
                }

                tree = new (int s, int x, int y)[MAX_N];
            }

            bool Input()
            {

                m = ReadInt();
                if (m == EXIT) return false;

                n = ReadInt();
                for (int i = 0; i < n; i++)
                {

                    tree[i] = (ReadInt(), ReadInt(), ReadInt());
                }

                return true;
            }

            void GetRet()
            {

                for (int i = 0; i < m; i++)
                {

                    Array.Fill(board[i], '.', 0, m);
                }

                for (int i = 0; i < n; i++)
                {

                    TryDraw(i);
                }

                DrawFRAME(m + 2, true);

                for (int i = m - 1; i >= 0; i--)
                {

                    DrawFRAME(1);

                    for (int j = 0; j < m; j++)
                    {

                        sw.Write(board[i][j]);
                    }

                    DrawFRAME(1, true);
                }

                DrawFRAME(m + 2, true);

                void TryDraw(int _idx)
                {

                    if (tree[_idx].s == 0)
                    {

                        int x = tree[_idx].x;
                        int y = tree[_idx].y;
                        ChkDraw(y, x - 1, '_');
                        ChkDraw(y, x, 'o');
                        ChkDraw(y, x + 1, '_');
                        return;
                    }

                    int left = tree[_idx].x - 1;
                    int right = tree[_idx].x + 1;

                    int bot = tree[_idx].y;
                    int top = tree[_idx].y + tree[_idx].s + 1;

                    if (right < 0 || left >= m || top < 0 || bot >= m) return;

                    bool drawLeft = 0 <= left;
                    bool drawRight = right < m;

                    bool drawBot = 0 <= bot;
                    bool drawTop = top < m;

                    if (drawBot)
                    {

                        if (drawLeft) Draw(bot, left, '_');
                        if (drawRight) Draw(bot, right, '_');
                        ChkDraw(bot, left + 1, '|');
                    }

                    if (drawTop)
                        ChkDraw(top, left + 1, '^');

                    top = Math.Min(top - 1, m - 1);
                    bot = Math.Max(bot + 1, 0);

                    for (int y = bot; y <= top; y++)
                    {

                        if (drawLeft) Draw(y, left, '/');
                        if (drawRight) Draw(y, right, '\\');

                        ChkDraw(y, left + 1, '|');
                    }

                    void Draw(int _r, int _c, char _val)
                    {

                        board[_r][_c] = _val;
                    }

                    void ChkDraw(int _r, int _c, char _val)
                    {

                        if (_r < 0 || _c < 0 || _r >= m || _c >= m) return;
                        board[_r][_c] = _val;
                    }
                }

                void DrawFRAME(int _cnt, bool _line = false)
                {

                    for (int i = 0; i < _cnt; i++)
                    {

                        sw.Write('*');
                    }

                    if (_line) sw.Write('\n');
                }
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
                    else if (c == -1)
                    {

                        ret = EXIT;
                        return false;
                    }
                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
