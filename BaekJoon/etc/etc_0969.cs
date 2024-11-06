using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 14
이름 : 배성훈
내용 : 알파벳 개수
    문제번호 : 10808번

    구현, 문자열 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0969
    {

        static void Main969(string[] args)
        {

            int[] alpha;

            Solve();
            void Solve()
            {

                string str = Console.ReadLine();

                alpha = new int[26];
                for (int i = 0; i < str.Length; i++)
                {

                    int idx = str[i] - 'a';
                    alpha[idx]++;
                }

                for (int i = 0; i < 26; i++)
                {

                    Console.Write(alpha[i]);
                    Console.Write(' ');
                }
            }
        }
    }
}
