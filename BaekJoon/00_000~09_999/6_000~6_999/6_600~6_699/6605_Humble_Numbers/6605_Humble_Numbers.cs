using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 9
이름 : 배성훈
내용 : Humble Numbers
    문제번호 : 6605번

    우선순위 큐, 브루트포스 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1757
    {

        static void Main1757(string[] args)
        {

            string HEAD = "The ";
            string MID = " humble number is ";
            string TAIL = ".\n";
            string ST = "st";
            string ND = "nd";
            string RD = "rd";
            string TH = "th";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            long[] ret;

            Init();
            int n;

            while ((n = int.Parse(sr.ReadLine())) > 0)
            {

                sw.Write(HEAD);
                sw.Write(n);
                Type(n % 100);
                
                sw.Write(MID);
                sw.Write(ret[n]);
                sw.Write(TAIL);
            }

            void Type(int _n)
            {

                int o = _n % 10;
                int t = _n / 10;
                switch (o)
                {

                    case 1:
                        if (t == 1) sw.Write(TH);
                        else sw.Write(ST);
                        return;

                    case 2:
                        if (t == 1) sw.Write(TH);
                        else sw.Write(ND);
                        return;

                    case 3:
                        if (t == 1) sw.Write(TH);
                        else sw.Write(RD);
                        return;

                    default:
                        sw.Write(TH);
                        return;
                }
            }

            void Init()
            {

                int TOTAL = 5842;
                PriorityQueue<long, long> pq = new(TOTAL * 4);
                HashSet<long> use = new(TOTAL);
                int[] mul = { 2, 3, 5, 7 };
                use.Add(1);
                pq.Enqueue(1, 1);
                ret = new long[TOTAL + 1];
                int idx = 0;

                while (idx < TOTAL)
                {

                    long cur = pq.Dequeue();
                    ret[++idx] = cur;

                    for (int i = 0; i < 4; i++)
                    {

                        long next = cur * mul[i];
                        if (use.Contains(next)) continue;
                        use.Add(next);
                        pq.Enqueue(next, next);
                    }
                }
            }
        }
    }
}
