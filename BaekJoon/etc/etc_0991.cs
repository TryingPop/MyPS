using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 23
이름 : 배성훈
내용 : 모노미노도미노 2
    문제번호 : 20061번

    구현, 시뮬레이션 문제다
    조건대로 구현하면 된다
    BOJ 19235번 모노미노도미노는 줄지움 연산이 이뤄지면 
    아래 블록이나 벽이 있을 때까지 내려가야해서 2와는 다른 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0991
    {

        static void Main991(string[] args)
        {

            int ROW = 6;
            int COL = 4;

            StreamReader sr;
            int n;
            int[][] green, blue;
            int ret1, ret2;

            Solve();
            void Solve()
            {

                Init();

                GetRet();
            }

            void GetRet()
            {

                n = ReadInt();
                ret1 = 0;
                ret2 = 0;

                for (int i = 0; i < n; i++)
                {

                    int t = ReadInt();
                    int r = ReadInt();
                    int c = ReadInt();

                    Insert(green, t, c);
                    if (t == 2) t = 3;
                    else if (t == 3) t = 2;
                    Insert(blue, t, r);
                }

                for (int r = 2; r < ROW; r++)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        if (green[r][c] != 0) ret2++;
                        if (blue[r][c] != 0) ret2++;
                    }
                }

                Console.Write($"{ret1}\n{ret2}");
            }

            // 블록 놓기
            void Insert(int[][] _board, int _t, int _c)
            {

                if (_t == 1) Move1(_board, _c);
                if (_t == 2) Move2(_board, _c);
                else if (_t == 3) Move3(_board, _c);
            }

            // 1 x 1 블록
            void Move1(int[][] _board, int _c)
            {

                int mR = ROW - 1;

                for (int r = 2; r < ROW; r++)
                {


                    if (_board[r][_c] == 0) continue;
                    mR = r - 1;
                    break;
                }
                _board[mR][_c] = 1;
                if (ChkClear(_board, mR)) 
                { 
                    
                    Clear(_board, mR);
                    Gravity(_board, mR);
                }

                if (ChkTop(_board)) Gravity(_board, ROW - 1);
            }

            // 1 x 2 블록
            void Move2(int[][] _board, int _c)
            {

                int mR = ROW - 1;
                for (int r = ROW - 1; r >= 2; r--)
                {

                    if (_board[r][_c] == 1 || _board[r][_c + 1] == 1) mR = r - 1;
                }

                _board[mR][_c] = 1;
                _board[mR][_c + 1] = 1;

                if (ChkClear(_board, mR))
                {

                    Clear(_board, mR);
                    Gravity(_board, mR);
                }

                if (ChkTop(_board)) Gravity(_board, ROW - 1);
            }

            // 2 x 1 블록
            void Move3(int[][] _board, int _c)
            {

                int mR = ROW - 1;
                for (int r = 2; r < ROW; r++)
                {

                    if (_board[r][_c] == 0) continue;
                    mR = r - 1;
                    break;
                }

                _board[mR][_c] = 1;
                _board[mR - 1][_c] = 1;

                if (ChkClear(_board ,mR - 1))
                {

                    Clear(_board, mR - 1);
                    Gravity(_board, mR - 1);
                }

                if (ChkClear(_board, mR))
                {

                    Clear(_board, mR);
                    Gravity(_board, mR);
                }

                while (ChkTop(_board))
                {

                    Gravity(_board, ROW - 1);
                }
            }

            // 줄 비움
            void Clear(int[][] _board, int _r)
            {

                for (int c = 0; c < COL; c++)
                {

                    _board[_r][c] = 0;
                }

                ret1++;
            }

            // 중력 작용
            // 1칸씩 내려간다
            void Gravity(int[][] _board, int _r)
            {

                for (int r = _r; r >= 1; r--)
                {

                    for (int c = 0; c < COL; c++)
                    {

                        _board[r][c] = _board[r - 1][c];
                        _board[r - 1][c] = 0;
                    }
                }
            }

            // 줄이 모두 찼는지 확인
            bool ChkClear(int[][] _board, int _r)
            {

                for (int c = 0; c < COL; c++)
                {

                    if (_board[_r][c] == 0) return false;
                }

                return true;
            }

            // 1 쪽에 블럭이 있는지 확인
            bool ChkTop(int[][] _board)
            {

                for (int c = 0; c < COL; c++)
                {

                    if (_board[1][c] == 1) return true;
                }

                return false;
            }

            void Init()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                green = new int[ROW][];
                blue = new int[ROW][];

                for (int r = 0; r < ROW; r++)
                {

                    green[r] = new int[COL];
                    blue[r] = new int[COL];
                }
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ALGO
{
    class Program
    {   
        static int[,] map = new int[10, 10];
        static int[] xPos = { 0, 1 }; // b, r
        static int[] yPos = { 1, 0};
        static int N, score, tiles;

        static StringBuilder sb = new StringBuilder();
        static StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
        static StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

        static void Main(string[] args)
        {
            sw.AutoFlush = true;
            N = Convert.ToInt32(sr.ReadLine());

            for (int i = 0; i < N; i++)
            {
                string[] tmp = sr.ReadLine().Split(' ');
                int type = Convert.ToInt32(tmp[0]);
                int y = Convert.ToInt32(tmp[1]); // 행
                int x = Convert.ToInt32(tmp[2]); // 열

                MoveGreen(y, x, type);
                MoveBlue(y, x, type);

                // delete
                DeleteGreen();
                DeleteBlue();

                // special
                SpecialGreen();
                SpecialBlue();
            }
            
            // tile count
            for(int i = 0; i < 4; i++)
            {
                for (int j = 6; j < 10; j++)
                {
                    if (map[i, j] == 1) tiles++;
                    if (map[j, i] == 1) tiles++;
                }
            }

            //Print();

            sw.WriteLine(score.ToString());
            sw.WriteLine(tiles.ToString());
        }

        private static void MoveBlue(int y, int x, int type)
        {
            int nx;
            switch (type)
            {
                case 1:
                    for (int i = x; i < 9; i++)
                    {
                        nx = x + 1;

                        if (map[y, nx] == 0)
                        {
                            map[y, nx] = 1;
                            map[y, x] = 0;
                            x = nx;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case 2: // **
                    for (int i = x; i < 9; i++)
                    {
                        nx = x + 1;
                        if (map[y, nx] == 0)
                        {
                            map[y, nx] = 1;
                            map[y, x] = 1;
                            if(x - 1 >= 0)
                            {
                                map[y, x - 1] = 0;
                            }
                            x = nx;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = x; i < 9; i++)
                    {
                        nx = x + 1;
                        if(map[y, nx] == 0 && map[y + 1, nx] == 0)
                        {
                            map[y, nx] = 1;
                            map[y + 1, nx] = 1;
                            map[y, x] = 0;
                            map[y + 1, x] = 0;
                            x = nx;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
            }
        }

        private static void MoveGreen(int y, int x, int type) // dir : 0
        {
            int ny;
            switch(type)
            {
                case 1:
                    for (int i = y; i < 9; i++)
                    {
                        ny = y + 1;

                        if (map[ny, x] == 0)
                        {
                            map[ny, x] = 1;
                            map[y, x] = 0;
                            y = ny;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case 2: // **
                    for (int i = y; i < 9; i++)
                    {
                        ny = y + 1;

                        if (map[ny, x] == 0 && map[ny, x + 1] == 0)
                        {
                            map[ny, x] = 1;
                            map[ny, x + 1] = 1;
                            map[y, x] = 0;
                            map[y, x + 1] = 0;
                            y = ny;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                case 3:
                    for (int i = y; i < 9; i++)
                    {
                        ny = y + 1;

                        if (map[ny, x] == 0)
                        {
                            map[ny, x] = 1;
                            map[y, x] = 1;
                            if(y - 1 >= 0)
                            {
                                map[y - 1, x] = 0;
                            }
                            y = ny;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
            }
        }

        private static void Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    sb.Append(map[i, j] + " ");
                }
                sb.Append(Environment.NewLine);
            }
            sw.WriteLine(sb.ToString());
        }

        private static void SpecialBlue()
        {
            int cnt = 0;
            if (map[0, 4] == 1 || map[1, 4] == 1 || map[2, 4] == 1 || map[3, 4] == 1)
            {
                DeleteBlueColumn(9);
                cnt++;
            }
            if (map[0, 5] == 1 || map[1, 5] == 1 || map[2, 5] == 1 || map[3, 5] == 1)
            {
                if (cnt == 0)
                {
                    DeleteBlueColumn(9);
                }
                else if (cnt == 1)
                {
                    DeleteBlueColumn(8);
                }
                cnt++;
            }

            // 당기기
            if (cnt > 0)
            {
                for (int i = 9; i >= 3; i--)
                {
                    map[0, i] = map[0, i-cnt];
                    map[1, i] = map[1, i-cnt];
                    map[2, i] = map[2, i-cnt];
                    map[3, i] = map[3, i-cnt];
                }
            }
        }

        private static void SpecialGreen()
        {
            int cnt = 0;
            if (map[4, 0] == 1 || map[4, 1] == 1 || map[4, 2] == 1 || map[4, 3] == 1)
            {
                DeleteGreenRow(9);
                cnt++;
            }
            if (map[5, 0] == 1 || map[5, 1] == 1 || map[5, 2] == 1 || map[5, 3] == 1)
            {
                if(cnt == 0)
                {
                    DeleteGreenRow(9);
                }
                else if(cnt == 1)
                {
                    DeleteGreenRow(8);
                }
                cnt++;
            }

            // 당기기
            if(cnt > 0)
            {
                for(int i = 9; i >= 3; i--)
                {
                    map[i, 0] = map[i-cnt, 0];
                    map[i, 1] = map[i-cnt, 1];
                    map[i, 2] = map[i-cnt, 2];
                    map[i, 3] = map[i-cnt, 3];
                }
            }
        }

        private static void DeleteBlue()
        {
            for (int j = 9; j > 3; j--)
            {
                int cnt = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (map[i, j] == 1) cnt++;
                }

                if (cnt == 4)
                {
                    // 타일 사라진다.
                    DeleteBlueColumn(j);
                    score++;

                    // 사라진 열은 당긴다.
                    for (int k = j - 1; k >= 3; k--)
                    {
                        map[0, k + 1] = map[0, k];
                        map[1, k + 1] = map[1, k];
                        map[2, k + 1] = map[2, k];
                        map[3, k + 1] = map[3, k];
                    }
                    j++; // ***************
                }
            }
        }

        private static void DeleteBlueColumn(int j)
        {
            map[0, j] = map[1, j] = map[2, j] = map[3, j] = 0;
        }

        private static void DeleteGreen()
        {
            for (int i = 9; i > 3; i--)
            {
                int cnt = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 1) cnt++;
                }

                if(cnt == 4)
                {
                    // 타일 사라진다.
                    DeleteGreenRow(i);                    
                    score++;

                    // 사라진 행은 당긴다.
                    for(int k = i - 1; k >= 3; k--)
                    {
                        map[k + 1, 0] = map[k, 0];
                        map[k + 1, 1] = map[k, 1];
                        map[k + 1, 2] = map[k, 2];
                        map[k + 1, 3] = map[k, 3];
                    }
                    i++;  // ***************
                }
            }
        }

        private static void DeleteGreenRow(int i)
        {
            map[i, 0] = map[i, 1] = map[i, 2] = map[i, 3] = 0;
        }
    }
}

#endif
}
