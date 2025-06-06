using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 6
이름 : 배성훈
내용 : 새트리
    문제번호 : 3652번

    수학, 재귀 문제다.
    문제를 이해하는데 시간이 많이 걸렸다.
    유클리드 호제법 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1680
    {

        static void Main1680(string[] args)
        {

            int u, d;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (u != d)
                {

                    int nU, nD;
                    if (u > d)
                    {

                        sw.Write('R');
                        nU = d;
                        nD = u - d;
                    }
                    else
                    {

                        sw.Write('L');
                        nU = d - u;
                        nD = u;
                    }

                    u = nU;
                    d = nD;
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split('/');
                u = int.Parse(temp[0]);
                d = int.Parse(temp[1]);
            }
        }
    }
}
