using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 09
이름 : 배성훈
내용 : 색종이 만들기
    문제번호 : 2630번
*/

namespace BaekJoon._27
{
    internal class _27_01
    {

        static void Main1(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] board = new int[len][];
            bool[][] used = new bool[len][];

            // 입력 받아오기
            for (int i = 0; i < len; i++)
            {

                board[i] = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                used[i] = new bool[len];
            }

            sr.Close();

            int white = 0;
            int blue = 0;

            // 분할과 정복
            Divide(ref white, ref blue, board, board.Length);

            // 출력
            Console.WriteLine(white);
            Console.WriteLine(blue);
        }

        /// <summary>
        /// 분할
        /// </summary>
        public static void Divide(ref int _white, ref int _blue, int[][] _board, int _maxlen, int _row = 0, int _col = 0)
        {

            // 비교할 초기 값
            int init = _board[_row][_col];

            for (int i = 0; i < _maxlen; i++)
            {

                for (int j = 0; j < _maxlen; j++)
                {

                    // 값이 다른 경우 4등분 분할
                    if (init != _board[_row + i][_col + j])
                    {

                        int half = _maxlen / 2;
                        Divide(ref _white, ref _blue, _board, half, _row, _col);
                        Divide(ref _white, ref _blue, _board, half, _row, _col + half);
                        Divide(ref _white, ref _blue, _board, half, _row + half, _col);
                        Divide(ref _white, ref _blue, _board, half, _row + half, _col + half);
                        // 탈출
                        return;
                    }
                }
            }

            // 색 연산
            Conquer(ref _white, ref _blue, init);
        }

        /// <summary>
        /// 정복
        /// </summary>
        public static void Conquer(ref int _white, ref int _blue, int _chk)
        {

            // 색 추가
            if (_chk == 0) _white++;
            else _blue++;
        }
    }
}
