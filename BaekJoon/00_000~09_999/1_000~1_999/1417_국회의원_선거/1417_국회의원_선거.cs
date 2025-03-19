using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 18
이름 : 배성훈
내용 : 국회의원 선거
    문제번호 : 1417번

    우선순위 큐, 그리디 문제다.
    선거에 출마하는 사람은 50명이고, 최대 득표수는 100명이므로 1씩 빼도 많아야 5000번을 연산한다.
    우선순위 큐를 이용해 가장 많은 사람 1명씩 표를 매수하며 시뮬레이션 돌렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1418
    {

        static void Main1418(string[] args)
        {

            int n;
            int my;

            PriorityQueue<int, int> pq;

            Input();

            GetRet();
            void GetRet()
            {

                int ret = 0;

                while (pq.Count > 0 && my <= pq.Peek())
                {

                    int pop = pq.Dequeue();
                    ret++;
                    pop--;
                    my++;
                    if (pop > 0) pq.Enqueue(pop, -pop);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                pq = new(n - 1);
                my = int.Parse(sr.ReadLine());

                for (int i = 1; i < n; i++)
                {

                    int num = int.Parse(sr.ReadLine());
                    pq.Enqueue(num, -num);
                }
            }
        }
    }
}
