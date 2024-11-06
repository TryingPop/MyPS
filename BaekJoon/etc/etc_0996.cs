using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 25
이름 : 배성훈
내용 : Zlagalica
    문제번호 : 31683번

    구현, 시뮬레이션 문제다
    문제대로 구현했다
    만들 수 있는 길이를 계산해보니 한 방향으로 200을 못넘기기에
    연산보다는 메모리를 써서 400 x 400 보드에 그냥 그림을 전부 그리고
    그림 영역이 어딘지 판별해 출력했다
*/

namespace BaekJoon.etc
{
    internal class etc_0996
    {

        static void Main996(string[] args)
        {

            StreamReader sr;
            StreamWriter sw;

            int n;
            (char alpha, int r, int c, int u, int d)[] pieces;
            int[] order;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                char[][] board = new char[401][];
                for (int i = 0; i <= 400; i++)
                {

                    board[i] = new char[401];
                    Array.Fill(board[i], '.');
                }

                for (int r = 0; r < pieces[order[0]].r; r++)
                {

                    for (int c = 0; c < pieces[order[0]].c; c++)
                    {

                        board[r + 200][c] = pieces[order[0]].alpha;
                    }
                }

                // LeftTop - lt
                int ltR = 200 - 1;
                int ltC = 0;            

                // RightBotom - rb
                int rbR = ltR + 1 + pieces[order[0]].r; 
                int rbC = ltC + pieces[order[0]].c;
                for (int i = 1; i < n; i++)
                {

                    int idx = order[i];
                    int u = pieces[order[i - 1]].u;
                    int d = pieces[order[i - 1]].d;

                    if (u == 0) Up(idx, d);
                    else Right(idx, d);

                    Draw(idx);
                }

                int minR = 500, maxR = 0, minC = 500, maxC = 0;

                Find();
                Ret();

                void Ret()
                {

                    sw.Write($"{maxR - minR + 1} {maxC - minC + 1}\n");
                    for (int r = minR; r <= maxR; r++)
                    {

                        for (int c = minC; c <= maxC; c++)
                        {

                            sw.Write(board[r][c]);
                        }
                        sw.Write('\n');
                    }

                    sw.Close();
                }

                void Find()
                {

                    for (int r = 0; r < 401; r++)
                    {

                        for (int c = 0; c < 401; c++)
                        {

                            if (board[r][c] == '.') continue;
                            minR = Math.Min(r, minR);
                            maxR = Math.Max(r, maxR);

                            minC = Math.Min(c, minC);
                            maxC = Math.Max(c, maxC);
                        }
                    }
                }

                void Up(int _idx, int _d)
                {

                    int sizeR = pieces[_idx].r;
                    int sizeC = pieces[_idx].c;

                    _d--;

                    ltR -= sizeR;
                    ltC += _d;

                    rbR = ltR + 1 + sizeR;
                    rbC = ltC + sizeC;
                }

                void Right(int _idx, int _d)
                {

                    int sizeR = pieces[_idx].r;
                    int sizeC = pieces[_idx].c;

                    ltR -= -_d + sizeR;
                    ltC = rbC;

                    rbR = ltR + 1 + sizeR;
                    rbC = ltC + sizeC;
                }

                void Draw(int _idx)
                {

                    int sizeR = pieces[_idx].r;
                    int sizeC = pieces[_idx].c;

                    for (int r = 0; r < sizeR; r++)
                    {

                        for (int c = 0; c < sizeC; c++)
                        {

                            board[ltR + 1 + r][ltC + c] = pieces[_idx].alpha;
                        }
                    }
                }
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                n = ReadInt();
                pieces = new (char alpha, int r, int c, int u, int d)[n];

                for (int i = 0; i < n; i++)
                {

                    pieces[i] = (ReadAlpha(), ReadInt(), ReadInt(), ReadInt(), ReadInt());
                }

                order = new int[n];
                for (int i = 0; i < n; i++)
                {

                    order[i] = ReadInt() - 1;
                }

                sr.Close();
            }

            char ReadAlpha()
            {

                char ret;

                while (TryReadAlpha()) { }

                return ret;

                bool TryReadAlpha()
                {

                    ret = '.';
                    int c = sr.Read();

                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n' || c == -1) return true;

                    ret = (char)c;
                    return false;
                }
            }

            int ReadInt()
            {

                int ret;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    ret = 0;
                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n' || c == -1) return true;
                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }

#if other
// #include <iostream>
// #include <algorithm>
// #define INF 999
using namespace std;

struct Block {
    char b;
    int r, c, u, d;
};

char canvas[252][252];
Block blocks[21];
int sequence[21];

int main() {
    cin.tie(0)->sync_with_stdio(0);

    int N;
    cin >> N;

    for (int r = 1; r <= 250; r++) {
        for (int c = 1; c <= 250; c++) {
            canvas[r][c] = '.';
        }
    }

    int pos_r = 250;
    int pos_c = 1;

    for (int i = 1; i <= N; i++) {
        cin >> blocks[i].b >> 
            blocks[i].r >>
            blocks[i].c >> 
            blocks[i].u >> 
            blocks[i].d;
    }

    for (int i = 1; i <= N; i++) {
        cin >> sequence[i];
    }

    for (int i = 1; i <= N; i++) {
        Block block = blocks[sequence[i]];

        for (int r = pos_r; r >= pos_r - block.r + 1; r--) {
            for (int c = pos_c; c <= pos_c + block.c - 1; c++) {
                canvas[r][c] = block.b;
            }
        }

        if (block.u == 0) {
            pos_r -= block.r;
            pos_c += block.d - 1;
        } else {
            pos_r -= block.r - block.d;
            pos_c += block.c;
        }
    }

    int start_r = INF;
    int start_c = INF;
    int end_r = -INF;
    int end_c = -INF;

    for (int r = 1; r <= 250; r++) {
        bool is_empty = true;

        for (int c = 1; c <= 250; c++) {
            if (canvas[r][c] != '.') {
                is_empty = false;
                break;
            }
        }

        if (!is_empty) {
            start_r = min(start_r, r);
            end_r = r;
        }
    }

    for (int c = 1; c <= 250; c++) {
        bool is_empty = true;

        for (int r = 1; r <= 250; r++) {
            if (canvas[r][c] != '.') {
                is_empty = false;
                break;
            }
        }

        if (!is_empty) {
            start_c = min(start_c, c);
            end_c = c;
        }
    }

    cout << end_r - start_r + 1 << ' ' << end_c - start_c + 1 << '\n';

    for (int r = start_r; r <= end_r; r++) {
        for (int c = start_c; c <= end_c; c++) {
            cout << canvas[r][c];
        }

        cout << '\n';
    }
}

#endif
}
