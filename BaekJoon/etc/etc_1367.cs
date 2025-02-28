using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 28
이름 : 배성훈
내용 : 나는 친구가 적다 (Small)
    문제번호 : 16171번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1367
    {

        static void Main1367(string[] args)
        {

            string allStr = Console.ReadLine();
            string findStr = Console.ReadLine();

            Solve();

            void Solve()
            {

                for (int i = 0; i < allStr.Length; i++)
                {

                    int match = 0;
                    for (int j = i; j < allStr.Length; j++)
                    {

                        if ('0' <= allStr[j] && allStr[j] <= '9') continue;
                        if (allStr[j] == findStr[match])
                        {

                            match++;
                            if (match == findStr.Length)
                            {

                                Console.Write(1);
                                return;
                            }
                            
                            continue;
                        }

                        break;
                    }
                }

                Console.Write(0);
            }


        }
    }
}
