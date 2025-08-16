using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 12
이름 : 배성훈
내용 : 카드1
    문제번호 : 2161번

    큐 문제다.
*/


namespace BaekJoon.etc
{
    internal class etc_1818
    {

        static void Main1818(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            Queue<int> q = new(n);

            for (int i = 1; i <= n; i++)
            {

                q.Enqueue(i);
            }
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            while (q.Count > 0)
            {

                sw.Write($"{q.Dequeue()} ");

                if (q.Count == 0) break;
                q.Enqueue(q.Dequeue());
            }
        }
    }
}
