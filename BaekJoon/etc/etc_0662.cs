using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 30
이름 : 배성훈
내용 : 정수 a를 k로 만들기
    문제번호 : 25418번

    dp, BFS 문제다
    풀고나서 보니 힌트대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0662
    {

        static void Main662(string[] args)
        {

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            Queue<int> q = new(arr[1]);

            int[] dp = new int[arr[1] + 1];
            dp[arr[0]] = 1;
            q.Enqueue(arr[0]);

            while(q.Count > 0)
            {

                int node = q.Dequeue();

                int next = node + 1;
                if (next > arr[1]) break;
                if (dp[next] == 0)
                {

                    dp[next] = dp[node] + 1;
                    q.Enqueue(next);
                }

                next = node * 2;
                if (next > arr[1]) continue;
                if (dp[next] == 0)
                {

                    dp[next] = dp[node] + 1;
                    q.Enqueue(next);
                }
            }

            int ret = dp[arr[1]] - 1;
            Console.WriteLine(ret);
        }
    }

#if other
using System;

public static class Program {
    public static void Main() {
        var input_str = Console.ReadLine().Split(' ');
        var (input, target) = (int.Parse(input_str[0]), int.Parse(input_str[1]));
        var count = 0;
        var multipies = (int) Math.Log2(target / input);
        count += multipies;
        target -= input * (int) Math.Pow(2, multipies);
        if (target == 0)
        {
            Console.WriteLine(count);
            return;
        }

        for (; multipies >= 0; multipies--)
        {
            var div = (int) Math.Pow(2, multipies);
            var share = target / div;
            count += share;
            target -= div * share;
            if (target != 0) continue;
            
            Console.WriteLine(count);
            return;
        }
        Console.WriteLine(count);
    }
}
#elif other2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C_Project
{
    class Program
    {

        static void Main(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            String[] input = Console.ReadLine().Split();
            int A = Int32.Parse(input[0]);
            int K = Int32.Parse(input[1]);

            int[] distance = new int[K + 1];

            for (int i = 0; i <= K; ++i)
            {
                distance[i] = 1000000;
            }

            distance[A] = 0;
            for (int i = A; i <= K; ++i)
            {
                if (i + 1 <= K && distance[i] + 1 < distance[i + 1])
                {
                    distance[i + 1] = distance[i] + 1;
                }
                if (i * 2 <= K && distance[i] + 1 < distance[i * 2])
                {
                    distance[i * 2] = distance[i] + 1;
                }
            }

            sb.Append(distance[K]);

            Console.WriteLine(sb.ToString());
        }
    }
}

#endif
}
