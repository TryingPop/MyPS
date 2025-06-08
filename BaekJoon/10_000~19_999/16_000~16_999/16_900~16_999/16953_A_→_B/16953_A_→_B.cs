using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : A → B
    문제번호 : 16953번

    그리디 알고리즘, DFS 문제다
    먼저 BFS와 dp를 이용해 풀었다
    다른 사람의 풀이를 보니 DFS가 더 좋아보인다
*/

namespace BaekJoon.etc
{
    internal class etc_0339
    {

        static void Main339(string[] args)
        {

            long[] input = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            Queue<long> q = new Queue<long>(60);
            Dictionary<long, int> dp = new(60);
            dp[input[0]] = 1;
            q.Enqueue(input[0]);
            while(q.Count > 0)
            {

                long node = q.Dequeue();

                long move = node * 2;
                if (move <= input[1] && !dp.ContainsKey(move))
                {

                    dp[move] = dp[node] + 1;
                    q.Enqueue(move);
                }

                move = node * 10 + 1;
                if (move <= input[1] && !dp.ContainsKey(move))
                {

                    dp[move] = dp[node] + 1;
                    q.Enqueue(move);
                }
            }

            if (dp.ContainsKey(input[1])) Console.WriteLine(dp[input[1]]);
            else Console.WriteLine(-1);
        }
    }

#if other
class Program
{
    public static int A;
    public static int B;
    public static void R(long n, int count)
    {
        if (n > B)
        {
            return;
        }
        if (n == B)
        {
            Console.WriteLine(count + 1);
            Environment.Exit(0);
        }
        R(n * 2, count + 1);
        R(n * 10 + 1, count + 1);
    }
    

    public static void Main()
    {
        string[] input = Console.ReadLine().Split(" ");
        A = int.Parse(input[0]);
        B = int.Parse(input[1]);
        R(A, 0);

        Console.WriteLine("-1");
    }
}
#endif
}
