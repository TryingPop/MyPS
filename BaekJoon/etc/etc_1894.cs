using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 17
이름 : 배성훈
내용 : Course Prerequisites
    문제번호 : 30611번

    해시 문제다.
    B의 모든 내용을 들었는지 확인하면 된다.
    여기서는 HashSet을 이용해 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1894
    {

        static void Main1894(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            string[] temp = sr.ReadLine().Split();
            HashSet<string> set = new(n);
            for (int i = 0; i < n; i++)
            {

                set.Add(temp[i]);
            }

            int m = int.Parse(sr.ReadLine());
            temp = sr.ReadLine().Split();

            for (int i = 0; i < m; i++)
            {

                if (set.Contains(temp[i])) continue;
                Console.Write(0);
                return;
            }

            Console.Write(1);
        }
    }
}
