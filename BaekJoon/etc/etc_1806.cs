using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 5
이름 : 배성훈
내용 : 콘센트
    문제번호 : 23843번

    그리디 문제다.
    먼저 가장 오래 걸리는거부터 충전을 하고
    비어있는 곳부터 먼저 꽂으면
    그리디로 최소가 됨을 알 수 있다.
    (3중)
    
    실제로 비어있는곳부터 꽂는게 최소임이 보장된다.
    그리고 가장 큰거부터 꽂는게 최소 시간이 보장된다.
    이에 최소에서 최소로 나아가기에 그리디로 최소가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1806
    {

        static void Main1806(string[] args)
        {

            int n;
            int m;

            PriorityQueue<int, int> max, min;

            Input();

            GetRet();

            void GetRet()
            {

                while (max.Count > 0)
                {

                    int cur = min.Dequeue() + max.Dequeue();
                    min.Enqueue(cur, cur);
                }

                while (min.Count > 1)
                {

                    min.Dequeue();
                }

                Console.Write(min.Peek());
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadInt();
                max = new(n);
                min = new(m);

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    max.Enqueue(cur, -cur);
                }

                for (int i = 0; i < m; i++)
                {

                    min.Enqueue(0, 0);
                }

                int ReadInt()
                {

                    int ret = 0;

                    while (TryReadInt()) ;
                    return ret;

                    bool TryReadInt()
                    {

                        int c = sr.Read();
                        if (c == '\r') c = sr.Read();
                        if (c == '\n' || c == ' ') return true;
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
}
