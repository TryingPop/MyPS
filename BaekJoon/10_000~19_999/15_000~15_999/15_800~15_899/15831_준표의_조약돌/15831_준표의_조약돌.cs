using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 6
이름 : 배성훈
내용 : 준표의 조약돌
    문제번호 : 15831번

    두 포인터 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1682
    {

        static void Main1682(string[] args)
        {

            int N, W, B;
            string str;

            Input();

            GetRet();

            void GetRet()
            {

                int l = 0;
                int r = -1;
                int b = 0, w = 0;
                int ret = 0;

                while (r < N)
                {

                    if (b <= B)
                    {

                        r++;
                        if (r == N) break;

                        if (str[r] == 'W') w++;
                        else b++;
                    }
                    else
                    {

                        if (str[l++] == 'W') w--;
                        else b--;
                    }

                    if (b <= B) ret = Math.Max(ret, b + w);
                }

                if (ret < W + B) ret = 0;
                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string[] temp = sr.ReadLine().Split();
                N = int.Parse(temp[0]);
                B = int.Parse(temp[1]);
                W = int.Parse(temp[2]);

                str = sr.ReadLine();
            }
        }
    }
}
