using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 15
이름 : 배성훈
내용 : 다항 함수의 적분
    문제번호 : 17214번

    수학, 문자열, 많은 조건 분기, 파싱, 미적분학 문제다
    문제 설명이 모호? 불친절하다;

    문제에서는 "계수는 절댓값이 10_000을 넘지 않는 0이 아닌 2의 배수라"
    나와있어 앞의 항이 0인 경우는 없겠네 하고 풀었다

    그런데 78%쯤에서 a = 0 이고, b > 0인 경우가 존재한다;
    그래서 b의 앞에 + 를 붙이면 안되는 경우도 존재해 여러 번 틀렸다

    확인해야할 케이스는 다음과 같다
        2x+1        -> xx+x+W
        -2x+1       -> -xx+x+W
        2x-1        -> xx-x+W
        -2x-1       -> -xx-x+W

        2x          -> xx+W
        -2x         -> -xx+W

        1           -> x+W
        -1          -> -x+W
        0           -> W
        2           -> 2x+W
        -2          -> -2x+W

        4x+3        -> 2xx+3x+W
        -4x+2       -> -2xx+2x+W
*/

namespace BaekJoon.etc
{
    internal class etc_1061
    {

        static void Main1061(string[] args)
        {

            int a, b;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                a /= 2;
                
                if (a < 0)
                {

                    a = -a;
                    Console.Write('-');
                }

                if (a != 1 && a != 0) Console.Write(a.ToString());
                if (a != 0) Console.Write("xx");

                if (b < 0)
                {

                    b = -b;
                    Console.Write('-');
                }
                else if (a != 0 && b != 0) Console.Write('+');

                if (b != 1 && b != 0) Console.Write(b.ToString());
                if (b != 0) Console.Write('x');
                if (a != 0 || b != 0) Console.Write('+');
                Console.Write('W');
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split('x');
                
                if (temp.Length > 1)
                {

                    a = int.Parse(temp[0]);
                    if (temp[1] != "") b = int.Parse(temp[1]);
                    else b = 0;
                }
                else
                {

                    a = 0;
                    b = int.Parse(temp[0]);
                }
                sr.Close();
            }
        }
    }
}
