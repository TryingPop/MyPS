using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 10. 17
이름 : 배성훈
내용 : Hotel Rewards
    문제번호 : 13873번

    그리디, 우선순위 큐 문제다
    쿠폰이 k개 될 때마다 pq의 사이즈를 1씩 늘려주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_1064
    {

        static void Main1064(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int k = ReadInt();

            PriorityQueue<int, int> pq = new(n);

            long ret = 0;
            int coupon = 0;
            for (int i = 0; i < n; i++)
            {

                int p = ReadInt();
                ret += p;

                if (coupon == k)
                {

                    pq.Enqueue(p, p);
                    coupon = 0;
                }
                else
                {

                    if (pq.Count > 0 && pq.Peek() < p)
                    {

                        pq.Dequeue();
                        pq.Enqueue(p, p);
                    }

                    coupon++;
                }
            }

            while(pq.Count > 0)
            {

                ret -= pq.Dequeue();
            }

            Console.Write(ret);

            sr.Close();
            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c < '0' || '9' < c) return true;

                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
