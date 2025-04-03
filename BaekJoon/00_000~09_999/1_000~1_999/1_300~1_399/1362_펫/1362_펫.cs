using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 펫
    문제번호 : 1362번

    시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1498
    {

        static void Main1362(string[] args)
        {

            string H = ":-)\n";
            string S = ":-(\n";
            string R = "RIP\n";

            char F = 'F';
            char E = 'E';
            char LAST = '#';

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int o, w;
            int idx = 1;
            while (Input())
            {

                GetRet();
            }

            void GetRet()
            {

                string input;
                while ((input = sr.ReadLine())[0] != LAST)
                {

                    if (w == 0) continue;
                    int val = int.Parse(input.Split()[1]);
                    if (input[0] == F)
                        w += val;
                    else
                    {

                        w -= val;
                        if (w < 0) w = 0;
                    }
                }

                sw.Write(idx++);
                sw.Write(' ');
                if (w == 0) sw.Write(R);
                else if (w < o * 2 && w > o / 2) sw.Write(H);
                else sw.Write(S);
            }

            bool Input()
            {

                string[] temp = sr.ReadLine().Split();
                o = int.Parse(temp[0]);
                w = int.Parse(temp[1]);

                return o != 0 && w != 0;
            }
        }
    }
}
