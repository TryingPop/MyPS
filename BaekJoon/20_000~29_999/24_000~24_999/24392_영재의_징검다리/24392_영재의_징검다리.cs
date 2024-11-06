using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 24
이름 : 배성훈
내용 : 영재의 징검다리
    문제번호 : 24392번

    DFS와 dp를 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0088
    {

        static void Main88(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt(sr);
            int col = ReadInt(sr);

            bool[,] board = new bool[row, col];
            int[,] dp = new int[row, col];

            for (int r = 0; r < row; r++)
            {

                for (int c = 0; c < col; c++)
                {

                    board[r, c] = 1 == ReadInt(sr);
                    dp[r, c] = -1;
                }
            }
            sr.Close();

            int ret = 0;
            for (int i = 0; i < col; i++)
            {

                // 갈 수 있는 길만 센다!
                if (board[row - 1, i]) 
                { 
                    
                    ret += DFS(dp, board, row - 1, i);
                    ret %= 1_000_000_007;
                }
            }

            Console.WriteLine(ret);
        }

        static int DFS(int[,] _dp, bool[,] _board, int _r, int _c)
        {

            if (_r == 0)
            {

                // 골인지점
                _dp[_r, _c] = 1;
                return 1;
            }
            // 이미 지나간 곳
            else if (_dp[_r, _c] != -1) return _dp[_r, _c];

            _dp[_r, _c] = 0;

            // 왼쪽길이 갈 수 있는지 확인하고 간다
            if (_c - 1 >= 0 && _board[_r - 1, _c - 1]) _dp[_r, _c] += DFS(_dp, _board, _r - 1, _c - 1); 

            // 중앙길
            if (_board[_r - 1, _c])
            {

                _dp[_r, _c] += DFS(_dp, _board, _r - 1, _c);
                _dp[_r, _c] %= 1_000_000_007;
            }

            // 오른쪽 길
            if (_c + 1 < _dp.GetLength(1) && _board[_r - 1, _c + 1]) 
            { 
                
                _dp[_r, _c] += DFS(_dp, _board, _r - 1, _c + 1);
                _dp[_r, _c] %= 1_000_000_007;
            }

            return _dp[_r, _c];
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
