using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 27
이름 : 배성훈
내용 : 폴짝폴짝
    문제번호 : 1326번

    BFS 문제다.
    입력 문제로 2번 틀렸다. 
    Array.ConvertAll 로 제출하니 Format 에러가 떠서 알아차렸다.

    시간이 2초이고 n의 크기가 1만이므로
    N^2의 방법을 택했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1297
    {

        static void Main1297(string[] args)
        {

            int n;
            int[] jump, turn;
            int s, e;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                turn = new int[n + 1];
                Array.Fill(turn, -1);
                turn[s] = 0;
                Queue<int> q = new(n);
                q.Enqueue(s);

                while(q.Count > 0)
                {

                    int node = q.Dequeue();
                    int cur = turn[node];
                    int start = node % jump[node];
                    if (start == 0) start += jump[node];

                    for (int i = start; i <= n; i += jump[node])
                    {

                        if (turn[i] != -1) continue;
                        turn[i] = cur + 1;
                        q.Enqueue(i);
                    }
                }

                Console.Write(turn[e]);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = int.Parse(sr.ReadLine());

                jump = new int[n + 1];

                int[] temp = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                for (int i = 1; i <= n; i++)
                {

                    jump[i] = temp[i - 1];
                }

                temp = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
                s = temp[0];
                e = temp[1];
            }
        }
    }
}
