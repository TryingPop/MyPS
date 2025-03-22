using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 18
이름 : 배성훈
내용 : 소용돌이 예쁘게 출력하기
    문제번호 : 1022번

    점들의 좌표로 값을 찾는 함수를 만들었다
    그리고 원소가 2개 이상인 경우, 가장 큰 자리수를 조사해서 해당 값들을 구했다

    그리고 switch로 문자열 보간이 되게 했다
    ... 문자열 보간에서는 상수만 넣을 수있다!
    해당 방법이 아니라면, 문자열 변환을 하면서 앞에 간격을 채워 넣었을거 같다

    그리고 좌표를 계산으로만 하려고 했었는데,
    그냥 표를 줬기에 표와 비교하면서 빠르게 작성했다
*/

namespace BaekJoon.etc
{
    internal class etc_0051
    {

        static void Main51(string[] args)
        {
            
            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            // 왼쪽 위
            int r1 = ReadInt(sr);
            int c1 = ReadInt(sr);

            // 오른쪽 아래
            int r2 = ReadInt(sr);
            int c2 = ReadInt(sr);

            sr.Close();

            bool only = r2 == r1 && c2 == c1;
            int[,] board = new int[r2 - r1 + 1, c2 - c1 + 1];
            int max = -1;
            for (int row = r1; row <= r2; row++)
            {

                for (int col = c1; col <= c2; col++)
                {

                    // 좌표를 값으로 변환
                    int chk = GetValue(row, col);
                    board[row - r1, col - c1] = chk;

                    if (chk > max) max = chk;
                }
            }

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                if (only) sw.Write(board[0, 0]);
                else
                {

                    int size = (int)Math.Log10(max);

                    for (int r = 0; r < board.GetLength(0); r++)
                    {

                        for (int c = 0; c < board.GetLength(1); c++)
                        {

                            switch (size)
                            {

                                case 0:
                                    sw.Write(board[r, c]);
                                    break;

                                case 1:
                                    sw.Write($"{board[r, c],2}");
                                    break;

                                case 2:
                                    sw.Write($"{board[r, c],3}");
                                    break;

                                case 3:
                                    sw.Write($"{board[r, c],4}");
                                    break;

                                case 4:
                                    sw.Write($"{board[r, c],5}");
                                    break;

                                case 5:
                                    sw.Write($"{board[r, c],6}");
                                    break;

                                case 6:
                                    sw.Write($"{board[r, c],7}");
                                    break;

                                case 7:
                                    sw.Write($"{board[r, c],8}");
                                    break;

                                case 8:
                                    sw.Write($"{board[r, c],9}");
                                    break;
                            }
                            sw.Write(' ');
                        }
                        sw.Write('\n');
                    }
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;
            bool plus = true;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                if (c == '-')
                {

                    plus = false;
                    continue;
                }
                ret = ret * 10 + c - '0';
            }

            ret = plus ? ret : -ret;

            return ret;
        }

        static int GetValue(int _r, int _c)
        {

            int ret;
            int MAX;
            if (_r - _c >= 0)
            {

                //ㄴ 모양!
                if (_r + _c >= 0)
                {

                    // -
                    MAX = (_r * 2) + 1;
                    MAX *= MAX;

                    ret = MAX - (_r - _c);
                }
                else
                {

                    // |
                    MAX = (-_c * 2) + 1;
                    MAX *= MAX;

                    ret = MAX + (2 * _c) + (_c + _r);
                }
            }
            else
            {

                // ㄱ 모양!
                if (_r + _c >= 0)
                {

                    // |
                    MAX = _c * 2;
                    MAX *= MAX;

                    ret = MAX - 2 * _c + 1 - (_r + _c);
                }
                else
                {

                    // -
                    MAX = -_r * 2;
                    MAX *= MAX;
                    ret = MAX + 2 * _r - (_r + _c) + 1;
                }
            }

            return ret;
        }
    }
}
