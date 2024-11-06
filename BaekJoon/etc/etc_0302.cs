using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 맥주 축제
    문제번호 : 17503번

    그리디, 정렬, 우선순위 큐 문제다
    for문쪽에 시작 부분을 잘못 처리해서 여러 번 틀렸다
    아이디어는 보석 도둑(etc_0308)의 아이디어를 썼다
    주된 아이디어는 다음과 같다

    도수로 먼저 정렬하고, 이후 맥주를 앞에서부터 k개 담는다
    그리고 선호도의 합이 조건을 만족하는 선호도인지 확인한다

    만족하면 마지막에 넣은 술의 도수가 정답이다
    만족안하면 다음 술을 넣는다
    현재 k + 1 개이므로 이 중에서 선호도가 가장 작은 것을 뺀다
    가장 작은 것을 찾을 때는 우선순위 큐로 자료구조를 선정해서 우선순위 큐에서 알아서 찾게 했다

    이후에 제출하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0302
    {

        static void Main302(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int m = ReadInt();
            int k = ReadInt();

            Beer[] beer = new Beer[k];
            for (int i = 0; i < k; i++)
            {

                beer[i].Set(ReadInt(), ReadInt());
            }

            sr.Close();

            Array.Sort(beer);
            PriorityQueue<int, int> q = new(n + 1);

            long sum = 0;
            for (int i = 0; i < n; i++)
            {

                q.Enqueue(beer[i].v, beer[i].v);
                sum += beer[i].v;
            }

            int ret = n - 1;
            if (sum < m)
            {

                for (int i = n; i < k; i++)
                {

                    /*
                    // 해당 방법이 연산이 적어 더 빠르다
                    if (beer[i].v > q.Peek())
                    {

                        sum -= q.Dequeue();
                        sum += beer[i].v;
                        q.Enqueue(beer[i].v, beer[i].v);
                    } 
                    */

                    q.Enqueue(beer[i].v, beer[i].v);
                    sum += beer[i].v;
                    
                    sum -= q.Peek();
                    q.Dequeue();

                    if (sum >= m)
                    {

                        ret = i;
                        break;
                    }
                }
            }

            if (sum >= m) Console.WriteLine(beer[ret].c);
            else Console.WriteLine(-1);

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }

        struct Beer : IComparable<Beer>
        {

            public int v;
            public int c;

            public void Set(int _v, int _c)
            {

                v = _v;
                c = _c;
            }

            public int CompareTo(Beer other)
            {

                return c.CompareTo(other.c);
            }
        }
    }
}
