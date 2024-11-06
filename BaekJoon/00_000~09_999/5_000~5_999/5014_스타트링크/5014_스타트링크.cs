using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 10
이름 : 배성훈
내용 : 스타트링크
    문제번호 : 5014번

    BFS를 이용하면 쉽게 풀린다
    입출력 함수 잘못 짜서 여러 번 틀렸다

    비주얼 스튜디오에서는 ReadInt에서 해당 문제의 경우 '\r'이되면 탈출해도 이상없으나,
    백준에 제출하면 틀렸다고 나온다
*/

namespace BaekJoon.etc
{
    internal class etc_0011
    {

        static void Main1(string[] args)
        {

            string NO = "use the stairs";
            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int max = info[0];
            int start = info[1];
            int goal = info[2];

            int[] elevator = new int[2];
            elevator[0] = info[3];
            elevator[1] = -info[4];

            int[] dp = new int[max + 1];
            Array.Fill(dp, -1);
            Queue<int> q = new Queue<int>();

            q.Enqueue(start);
            dp[start] = 0;

            // BFS 탐색
            while(q.Count > 0)
            {

                var node = q.Dequeue();

                for (int i = 0; i < 2; i++)
                {

                    int next = elevator[i] + node;
                    if (next < 1 || next > max || dp[next] != -1) continue;

                    dp[next] = dp[node] + 1;
                    q.Enqueue(next);
                }
            }

            if (dp[goal] == -1) Console.WriteLine(NO);
            else Console.WriteLine(dp[goal]);
        }

        static int ReadInt()
        {

            int n = 0;
            int c;

            while((c = Console.Read()) != -1 && c != ' ' && c != '\r')
            {

                if (c == '\r') continue;

                n *= 10;
                n += c - '0';
            }

            return n;
        }
    }
}
