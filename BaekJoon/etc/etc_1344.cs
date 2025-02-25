using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 18
이름 : 배성훈
내용 : 직육면체 교집합
    문제번호 : 11665번

    기하학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1344
    {

        static void Main1344(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = Convert.ToInt32(sr.ReadLine());

            int[] ret = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            for (int i = 1; i < n; i++)
            {

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                ret[0] = Math.Max(ret[0], temp[0]);
                ret[1] = Math.Max(ret[1], temp[1]);
                ret[2] = Math.Max(ret[2], temp[2]);

                ret[3] = Math.Min(ret[3], temp[3]);
                ret[4] = Math.Min(ret[4], temp[4]);
                ret[5] = Math.Min(ret[5], temp[5]);
            }

            int x = ret[3] - ret[0];
            int y = ret[4] - ret[1];
            int z = ret[5] - ret[2];

            if (x > 0 && y > 0 && z > 0) Console.Write(x * y * z);
            else Console.Write(0);
        }
    }
}
