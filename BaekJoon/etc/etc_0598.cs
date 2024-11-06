using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 22
이름 : 배성훈
내용 : 3차원 지뢰찾기
    문제번호 : 29733번

    구현 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0598
    {

        static void Main598(string[] args)
        {

            int BOMB = '*';
            StreamReader sr;
            StreamWriter sw;
            int[,,] board;
            int r, c, h;

            Solve();

            void Solve()
            {

                Input();
                Find();
                Output();
            }

            void Output()
            {

                sw = new(new BufferedStream(Console.OpenStandardOutput()), bufferSize: 65536 * 16);

                for (int i = 0; i < h; i++)
                {

                    for (int j = 0; j < r; j++)
                    {

                        for (int k = 0; k < c; k++)
                        {

                            if (board[i, j, k] == BOMB) sw.Write('*');
                            else sw.Write(board[i, j, k]);
                        }

                        sw.Write('\n');
                    }
                }

                sw.Close();
            }

            void Find()
            {

                int[] dirH = {  -1, -1, -1,
                                -1, -1, -1,
                                -1, -1, -1,

                                0,  0,  0,
                                0,      0,
                                0,  0,  0,

                                1,  1,  1,
                                1,  1,  1,
                                1,  1,  1 
                };

                int[] dirR = {  -1, -1, -1,
                                0,  0,  0,
                                1,  1,  1,

                                -1, -1, -1,
                                0,      0,
                                1,  1,  1,

                                -1, -1, -1,
                                0,  0,  0,
                                1,  1,  1,
                };

                int[] dirC = {  -1, 0,  1,
                                -1, 0,  1,
                                -1, 0,  1,
                
                                -1, 0,  1,
                                -1,     1,
                                -1, 0,  1,
                
                                -1, 0,  1,
                                -1, 0,  1,
                                -1, 0,  1 };


                for (int i = 0; i < h; i++)
                {

                    for (int j = 0; j < r; j++)
                    {

                        for (int k = 0; k < c; k++)
                        {

                            if (board[i, j, k] == BOMB) continue;
                            int cur = 0;
                            for (int dir = 0; dir < 26; dir++)
                            {

                                int nextR = j + dirR[dir];
                                int nextC = k + dirC[dir];
                                int nextH = i + dirH[dir];

                                if (ChkInvalid(nextR, nextC, nextH) || board[nextH, nextR, nextC] != '*') continue;
                                cur++;
                            }

                            cur %= 10;
                            board[i, j, k] = cur;
                        }
                    }
                }
            }

            bool ChkInvalid(int _r, int _c, int _h)
            {

                if (_r < 0 || _r >= r || _c < 0 || _c >= c || _h < 0 || _h >= h) return true;
                return false;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                r = ReadInt();
                c = ReadInt();
                h = ReadInt();

                board = new int[h, r, c];

                for (int i = 0; i < h; i++)
                {

                    for (int j = 0; j < r; j++)
                    {

                        for (int k = 0; k < c; k++)
                        {

                            board[i, j, k] = sr.Read();
                        }

                        if (sr.Read() == '\r') sr.Read();
                    }
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
// contest1058_18 - rby
// 2023-09-10 17:12:13
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace contest1058_18
{
    class Program
    {
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {
            int[] line = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int A = line[0];
            int B = line[1];
            int C = line[2];

            string input;
            int[,,] cube = new int[C, A, B];
            for(int i = 0; i < C; i++)
            {
                for(int j = 0; j < A; j++)
                {
                    input = sr.ReadLine();
                    for(int k = 0; k < B; k++)
                    {
                        cube[i, j, k] = input[k] == '.' ? 0 : -1;
                    }
                }
            }

            for (int i = 0; i < C; i++)
            {
                for (int j = 0; j < A; j++)
                {
                    for (int k = 0; k < B; k++)
                    {
                        if (cube[i, j, k] == -1)
                            continue;

                        if (i != 0)
                        {
                            if (j != 0)
                            {
                                if (k != 0)
                                    if (cube[i - 1, j - 1, k - 1] == -1)
                                        cube[i, j, k] += 1;
                                if (k != B - 1)
                                    if (cube[i - 1, j - 1, k + 1] == -1)
                                        cube[i, j, k] += 1;
                                if (cube[i - 1, j - 1, k] == -1)
                                    cube[i, j, k] += 1;
                            }

                            if (j != A - 1)
                            {
                                if (k != 0)
                                    if (cube[i - 1, j + 1, k - 1] == -1)
                                        cube[i, j, k] += 1;
                                if (k != B - 1)
                                    if (cube[i - 1, j + 1, k + 1] == -1)
                                        cube[i, j, k] += 1;
                                if (cube[i - 1, j + 1, k] == -1)
                                    cube[i, j, k] += 1;
                            }

                            if (k != 0)
                                if (cube[i - 1, j, k - 1] == -1)
                                    cube[i, j, k] += 1;
                            if (k != B - 1)
                                if (cube[i - 1, j, k + 1] == -1)
                                    cube[i, j, k] += 1;
                            if (cube[i - 1, j, k] == -1)
                                cube[i, j, k] += 1;
                        }

                        if (i != C - 1)
                        {
                            if (j != 0)
                            {
                                if (k != 0)
                                    if (cube[i + 1, j - 1, k - 1] == -1)
                                        cube[i, j, k] += 1;
                                if (k != B - 1)
                                    if (cube[i + 1, j - 1, k + 1] == -1)
                                        cube[i, j, k] += 1;
                                if (cube[i + 1, j - 1, k] == -1)
                                    cube[i, j, k] += 1;
                            }

                            if (j != A - 1)
                            {
                                if (k != 0)
                                    if (cube[i + 1, j + 1, k - 1] == -1)
                                        cube[i, j, k] += 1;
                                if (k != B - 1)
                                    if (cube[i + 1, j + 1, k + 1] == -1)
                                        cube[i, j, k] += 1;
                                if (cube[i + 1, j + 1, k] == -1)
                                    cube[i, j, k] += 1;
                            }

                            if (k != 0)
                                if (cube[i + 1, j, k - 1] == -1)
                                    cube[i, j, k] += 1;
                            if (k != B - 1)
                                if (cube[i + 1, j, k + 1] == -1)
                                    cube[i, j, k] += 1;
                            if (cube[i + 1, j, k] == -1)
                                cube[i, j, k] += 1;
                        }

                        if (j != 0)
                        {
                            if (k != 0)
                                if (cube[i, j - 1, k - 1] == -1)
                                    cube[i, j, k] += 1;
                            if (k != B - 1)
                                if (cube[i, j - 1, k + 1] == -1)
                                    cube[i, j, k] += 1;
                            if (cube[i, j - 1, k] == -1)
                                cube[i, j, k] += 1;
                        }

                        if (j != A - 1)
                        {
                            if (k != 0)
                                if (cube[i, j + 1, k - 1] == -1)
                                    cube[i, j, k] += 1;
                            if (k != B - 1)
                                if (cube[i, j + 1, k + 1] == -1)
                                    cube[i, j, k] += 1;
                            if (cube[i, j + 1, k] == -1)
                                cube[i, j, k] += 1;
                        }

                        if (k != 0)
                            if (cube[i, j, k - 1] == -1)
                                cube[i, j, k] += 1;
                        if (k != B - 1)
                            if (cube[i, j, k + 1] == -1)
                                cube[i, j, k] += 1;
                    }
                }
            }
            
            for (int i = 0; i < C; i++)
            {
                for (int j = 0; j < A; j++)
                {
                    for (int k = 0; k < B; k++)
                    {
                        cube[i, j, k] %= 10;
                        sb.Append(cube[i, j, k] == -1 ? "*" : cube[i, j,k].ToString());
                    }
                    sb.AppendLine();
                }
            }

            sw.Write(sb);
            sw.Close();
            sr.Close();
        }
    }
}
#elif other2
// #include <iostream>
using namespace std;

int R, C, H;
char board[102][102][102];
int ans[102][102][102];

int main() {
    ios::sync_with_stdio(0);
    cin.tie(0);
    
    cin >> R >> C >> H;
    for (int h = 1; h <= H; h++) {
        for (int r = 1; r <= R; r++) {
            for (int c = 1; c <= C; c++) {
                cin >> board[h][r][c];
                if (board[h][r][c] == '*') {
                    for (int dh = -1; dh < 2; dh++) {
                        for (int dr = -1; dr < 2; dr++) {
                            for (int dc = -1; dc < 2; dc++) {
                                ans[h + dh][r + dr][c + dc]++;
                            }
                        }
                    }
                }
            }
        }
    }
    
    for (int h = 1; h <= H; h++) {
        for (int r = 1; r <= R; r++) {
            for (int c = 1; c <= C; c++) {
                if (board[h][r][c] == '*') cout << '*';
                else cout << ans[h][r][c] % 10;
            }
            cout << '\n';
        }
    }
}
#endif
}
