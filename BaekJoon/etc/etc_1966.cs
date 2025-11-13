using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 11. 7
이름 : 배성훈
내용 : 반복수열
    문제번호 : 2331번

    수학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1966
    {

        static void Main1966(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            bool[] visit = new bool[500_000];
            int[] pow = new int[10];

            for (int i = 0; i < 10; i++)
            {

                pow[i] = 1;
                for (int j = 0; j < arr[1]; j++)
                {

                    pow[i] *= i;
                }
            }

            int stop = arr[0];
            while (!visit[stop])
            {

                visit[stop] = true;
                stop = Next(stop);
            }

            int cur = arr[0];
            int ret = 0;
            while (cur != stop)
            {

                cur = Next(cur);
                ret++;
            }

            Console.Write(ret);

            int Next(int val)
            {

                int ret = 0;
                while (val > 0)
                {

                    int cur = val % 10;
                    val /= 10;
                    ret += pow[cur];
                }

                return ret;
            }

        }
    }
}
