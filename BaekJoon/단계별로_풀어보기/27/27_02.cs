using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 10
이름 : 배성훈
내용 : 쿼드트리
    문제번호 : 1992번
*/


namespace BaekJoon._27
{
    internal class _27_02
    {

        static void Main2(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = int.Parse(sr.ReadLine());

            char[][] board = new char[len][];

            for (int i = 0; i < len; i++)
            {

                board[i] = sr.ReadLine().ToCharArray();
            }

            sr.Close();

            
            StringBuilder sb = new StringBuilder();

            // 풀이
            Divide(sb, board, len);


            // 출력
            Console.WriteLine(sb);
        }

        /// <summary>
        /// 분할
        /// </summary>
        public static void Divide(StringBuilder _sb, char[][] _board, int _maxlen, int _row = 0, int _col = 0)
        {

            char init = _board[_row][_col];

            for (int i = 0; i < _maxlen; i++)
            {

                for (int j = 0; j < _maxlen; j++)
                {

                    // 다른게 있으면 4등분
                    if (init != _board[_row + i][_col + j])
                    {

                        // 조건에 의해 여기서 소괄호 넣는다
                        _sb.Append('(');

                        int half = _maxlen / 2;
                        Divide(_sb, _board, half, _row, _col);
                        Divide(_sb, _board, half, _row, _col + half);
                        Divide(_sb, _board, half, _row + half, _col);
                        Divide(_sb, _board, half, _row + half, _col + half);

                        _sb.Append(')');
                        return;
                    }
                }
            }

            Conquer(_sb, init);
        }

        /// <summary>
        /// 정복
        /// </summary>
        public static void Conquer(StringBuilder _sb, char _chk)
        {

            // 해당 정보 기입
            if (_chk == '0') _sb.Append('0');
            else _sb.Append('1');
        }
    }
}
