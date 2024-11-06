using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 26
이름 : 배성훈
내용 : UCPC는 무엇의 약자일까?
    문제번호 : 15904번

    그리디, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0774
    {

        static void Main774(string[] args)
        {

            string YES = "I love UCPC";
            string NO = "I hate UCPC";
            string UCPC = "UCPC";

            StreamReader sr;
            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                string str = sr.ReadLine();
                int idx = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    if (str[i] != UCPC[idx]) continue;
                    idx++;
                    if (idx == 4) break;
                }

                Console.Write(idx == 4 ? YES : NO);
                sr.Close();
            }
        }
    }
}
