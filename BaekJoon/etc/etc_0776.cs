using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 6. 28
이름 : 배성훈
내용 : 월향, 비상
    문제번호 : 29336번

    그리디 알고리즘, 정렬 문제다
    우선순위 큐를 이용해서 해결했다

    아이디어는 다음과 같다
    문제 퀄리티의 최대 값은 새로 만드는게 점수 폭이 더 높으므로
    무조건 문제를 만들어야한다

    그리고 1일당 역량이 1씩 상승하므로 1명 역량을 상승시키는 것 보단
    2명 역량을 상승시키는 것이 최대값을 갖는 경우가 보장된다
    그래서 날짜에 맞춰 문제를 제출해야하는 경우 큰 사람들을 최대한 제출하게 함으로
    역량 상승하는 사람을 최대수로 되게 세팅한다

    그리고 모든 사람이 문제를 만들었어도 해당 날짜에 난이도가 안되면,
    -1을 제출하게 하니 이상없이 통과했다

    힌트를 보니 정렬이 있는데,
    길이를 나타내는 포인터를 하나 둬서 해결가능해 보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0776
    {

        static void Main776(string[] args)
        {

            StreamReader sr;

            PriorityQueue<int, int> q;
            int n, m;

            Solve();
            void Solve()
            {

                Input();

                Console.Write(Chk());

                sr.Close();
            }

            long Chk()
            {

                long ret = 0L;

                int day = 0;
                for (int i = 0; i < m; i++)
                {

                    day = ReadInt();
                    int dest = ReadInt();
                    
                    while (ret < dest && q.Count > 0)
                    {

                        ret += q.Dequeue() + day;
                    }

                    if (ret < dest) return -1;
                }

                while(q.Count > 0)
                {

                    ret += q.Dequeue() + day;
                }
                return ret;
            }

            void Input()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536 * 16);
                n = ReadInt();
                m = ReadInt();

                Comparer<int> comp = Comparer<int>.Create((x, y) => y.CompareTo(x));
                q = new(n, comp);

                for (int i = 0; i < n; i++)
                {

                    int cur = ReadInt();
                    q.Enqueue(cur, cur);
                }
            }

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }

#if other
using System;

public class Program
{
    static void Main()
    {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]), m = int.Parse(nm[1]);
        int[] array = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        Array.Sort(array, (a, b) => b - a);
        int p = 0;
        long answer = 0;
        for (int i = 1; i <= m; i++)
        {
            string[] tq = Console.ReadLine().Split(' ');
            int t = int.Parse(tq[0]), q = int.Parse(tq[1]);
            while (answer < q)
            {
                if (p == n)
                {
                    Console.Write(-1);
                    return;
                }
                answer += array[p++] + t;
            }
            if (i == m)
            {
                while (p < n)
                {
                    answer += array[p++] + t;
                }
            }
        }
        Console.Write(answer);
    }
}
#endif
}
