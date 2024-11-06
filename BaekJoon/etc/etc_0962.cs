using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 12
이름 : 배성훈
내용 : ROT13
    문제번호 : 11655번

    구현, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0962
    {

        static void Main962(string[] args)
        {


            Solve();
            void Solve()
            {

                string str = Console.ReadLine();
                int[] ret = new int[str.Length];

                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] == ' ' || ('0' <= str[i] && str[i] <= '9'))
                    {

                        ret[i] = str[i];
                        continue; 
                    }

                    int add;
                    if (str[i] < 'a') add = 'A';
                    else add = 'a';

                    ret[i] = (str[i] - add + 13) % 26;
                    ret[i] += add;
                }

                using (StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536))
                {

                    for (int i = 0; i < ret.Length; i++)
                    {

                        sw.Write((char)ret[i]);
                    }
                }
            }
        }
    }
}
