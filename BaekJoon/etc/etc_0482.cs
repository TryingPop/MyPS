using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : LCD Test
    문제번호 : 2290번

    구현, 문자열 문제다
    조건대로 하드코딩으로 구현했다

    조금 더 낫게 코딩하려면 맨 위, 중앙, 맨 밑을 함수로,
    왼쪽과 오른쪽을 쓰는 부분을 메서드로 따로 처리하면 조금 더 깔끔해질거 같다
    함수로 바꾸니 속도랑 사용한 메모리가 줄어들었다 ?
    아마 함수에서 탈출하면서 안쓰는 메모리는 반환하니 줄어든거 같다
*/

namespace BaekJoon.etc
{
    internal class etc_0482
    {

        static void Main482(string[] args)
        {

            int[] num = { 0b_101_1111, 0b_000_0011, 0b_111_0110, 0b_111_0011, 0b_010_1011, 0b_111_1001,
                0b_111_1101, 0b_100_0011, 0b_111_1111, 0b_111_1011 };

            string[] input = Console.ReadLine().Split();
            int size = int.Parse(input[0]);
            using (StreamWriter sw = new(new BufferedStream(Console.OpenStandardOutput())))
            {

                // Top
                SetMid(sw, 6);

                // Toplr
                SetLR(sw, 1);

                // Mid
                SetMid(sw, 5);

                // Botlr
                SetLR(sw, 0);

                // Bot
                SetMid(sw, 4);
            }

            void SetMid(StreamWriter _sw, int _chk)
            {

                for (int i = 0; i < input[1].Length; i++)
                {

                    _sw.Write(' ');
                    int cur = input[1][i] - '0';
                    char c = ((1 << _chk) & num[cur]) == 0 ? ' ' : '-';
                    for (int j = 0; j < size; j++)
                    {

                        _sw.Write(c);
                    }

                    _sw.Write(' ');
                    _sw.Write(' ');
                }
                _sw.Write('\n');
            }

            void SetLR(StreamWriter _sw, int _chk)
            {

                for (int i = 0; i < size; i++)
                {

                    for (int j = 0; j < input[1].Length; j++)
                    {

                        int cur = input[1][j] - '0';
                        char c = ((1 << (_chk + 2)) & num[cur]) == 0 ? ' ' : '|';
                        _sw.Write(c);
                        for (int k = 0; k < size; k++)
                        {

                            _sw.Write(' ');
                        }

                        c = ((1 << _chk) & num[cur]) == 0 ? ' ' : '|';
                        _sw.Write(c);
                        _sw.Write(' ');
                    }
                    _sw.Write('\n');
                }
            }
        }
    }
}
