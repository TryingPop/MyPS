using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 22
이름 : 배성훈
내용 : Śpiew
    문제번호 : 8575번

    그리디 문제다.
    책을 주는데 인접한 사람이 있는 경우 2번째 사람에게 주는게 최소임이 보장된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1639
    {

        static void Main1639(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            string temp = sr.ReadLine();

            int ret = 0;
            int adj = 0;
            for (int i = 0; i < n; i++)
            {

                if (temp[i] == 'W') 
                {

                    adj = 0;
                    continue; 
                }
                else
                {

                    if (adj == 0)
                    {

                        ret++;
                        adj = 2;
                    }
                    else adj--;
                }
            }

            Console.Write(ret);
        }
    }
}
