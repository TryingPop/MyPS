using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 27
이름 : 배성훈
내용 : 강의실
    문제번호 : 1374번

    그리디, 정렬, 우선순위 큐 문제다.
    시간으로 정렬해 시간별로 시뮬레이션 돌려 
    강의간 겹치는 최대 개수를 찾는다.

    해당 개수가 강의실 최소 배정 갯수와 같다.
*/

namespace BaekJoon.etc
{
    internal class etc_1135
    {

        static void Main1135(string[] args)
        {

            int n;
            (int s, int e)[] lecture;
            PriorityQueue<int, int> pq;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                pq = new(n);
                Array.Sort(lecture, (x, y) => x.s.CompareTo(y.s));

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    int curTime = lecture[i].s;
                    while (pq.Count > 0 && pq.Peek() <= curTime)
                    {

                        pq.Dequeue();
                    }

                    pq.Enqueue(lecture[i].e, lecture[i].e);
                    ret = Math.Max(ret, pq.Count);
                }

                Console.Write(ret);
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                lecture = new (int s, int e)[n];
                for (int i = 0; i < n; i++)
                {

                    int idx = ReadInt() - 1;
                    lecture[i] = (ReadInt(), ReadInt());
                }

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

                        if (c == '\n' || c == ' ') return true;
                        ret = c - '0';

                        while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
}
