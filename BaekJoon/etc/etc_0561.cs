using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 17
이름 : 배성훈
내용 : 특별한 서빙
    문제번호 : 27896번

    그리디, 우선순위 큐 문제다
    풀이 방법은 빠르게 떠올랐으나, 
    처리 로직이 잘못되어 3번 틀렸다
    순차적으로 진행하기에 정렬해서 큰 값들만 지불하는 식은 안된다
    
    아이디어는 다음과 같다
    불만이 m 이상이되면 사용안된 가장 큰 값을 주어 불만을 제거하는게 
    가장 효율적이고 최소가 보장된다
    그래서 우선 순위 큐를 이용해 가장 큰 값을 알아내고 초과하면 달래주는 식으로 진행했다
*/

namespace BaekJoon.etc
{
    internal class etc_0561
    {

        static void Main561(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            Solve();
            sr.Close();

            void Solve()
            {

                int n = ReadInt();
                int m = ReadInt();
                PriorityQueue<int, int> q = new(n, Comparer<int>.Create((x, y) => y.CompareTo(x)));

                int ret = 0;
                long total = 0;
                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    q.Enqueue(cur, cur);
                    total += cur;
                    if (total >= m)
                    {

                        total -= 2 * q.Dequeue();
                        ret++;
                    }
                }

                Console.Write(ret);
            }

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
    }

#if other
string[] input = Console.ReadLine()!.Split();
int n = int.Parse(input[0]),
    m = int.Parse(input[1]);

PriorityQueue<int, int> pq = new();
input = Console.ReadLine()!.Split();

int b = 0;
int r = 0;
for (int i = 0; i < n; i++)
{
    int x = int.Parse(input[i]);
    b += x;
    pq.Enqueue(x, -x);

    if (b >= m)
    {
        int a = pq.Dequeue();
        b -= a + a;
        r++;
    }
}

Console.WriteLine(r);
#endif
}
