using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 27
이름 : 배성훈
내용 : Pencil Crayons
    문제번호 : 33646번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1844
    {

        static void Main1844(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string[] temp = sr.ReadLine().Split();
            int n = int.Parse(temp[0]);
            int k = int.Parse(temp[1]);

            HashSet<string> use = new(k);
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                temp = sr.ReadLine().Split();
                for (int j = 0; j < k; j++)
                {

                    if (use.Contains(temp[j])) ret++;
                    else use.Add(temp[j]);
                }

                use.Clear();
            }

            Console.Write(ret);
        }
    }
}
