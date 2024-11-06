using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 10
이름 : 배성훈
내용 : 종이의 개수
    문제번호 : 1780번
*/

namespace BaekJoon._27
{
    internal class _27_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            int[][] board = new int[len][];

            for (int i = 0; i < len; i++)
            {

                board[i] = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            }

            sr.Close();

            int[] result = new int[3];

            Divide(result, board, len);

            for (int i = 0; i < result.Length; i++)
            {

                Console.WriteLine(result[i]);
            }
        }

        /// <summary>
        /// 분할
        /// </summary>
        public static void Divide(int[] _result, int[][] _board, int _maxlen, int _row = 0, int _col = 0)
        {

            int init = _board[_row][_col];

            for (int i = 0; i < _maxlen; i++)
            {

                for (int j = 0; j < _maxlen; j++)
                {

                    // 다른 경우 판정하고 조건에 맞게 분할하고 탈출
                    if (init != _board[_row + i][_col + j])
                    {

                        int div = _maxlen / 3;

                        Divide(_result, _board, div, _row, _col);
                        Divide(_result, _board, div, _row, _col + div);
                        Divide(_result, _board, div, _row, _col + div * 2);

                        Divide(_result, _board, div, _row + div, _col);
                        Divide(_result, _board, div, _row + div, _col + div);
                        Divide(_result, _board, div, _row + div, _col + div * 2);
                        
                        Divide(_result, _board, div, _row + div * 2, _col);
                        Divide(_result, _board, div, _row + div * 2, _col + div);
                        Divide(_result, _board, div, _row + div * 2, _col + div * 2);

                        return;
                    }
                }
            }

            Conquer(_result, init);
        }

        /// <summary>
        /// 정복
        /// </summary>
        public static void Conquer(int[] _result, int _chk)
        {

            // -1, 0, 1 순서로 담기에 다음과 같이 추가
            _result[_chk + 1]++;
        }
    }
}
