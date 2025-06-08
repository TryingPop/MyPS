using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 20
이름 : 배성훈
내용 : 탈출
    문제번호 : 16397번

    BFS 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1559
    {

        static void Main1559(string[] args)
        {

            int n, t, g;

            Input();

            GetRet();

            void GetRet()
            {

                int INF = 99_999;
                int[] arr = { 10_000, 1_000, 100, 10, 1 };

                int[] visit = new int[100_000];
                Array.Fill(visit, -1);
                visit[n] = 0;

                Queue<int> q = new(100_000);
                q.Enqueue(n);

                while (q.Count > 0)
                {

                    int cur = q.Dequeue();

                    for (int i = 0; i < 2; i++)
                    {

                        int next = Next(i);
                        if (next == -1 || visit[next] != -1) continue;
                        visit[next] = visit[cur] + 1;
                        q.Enqueue(next);
                    }

                    int Next(int _i)
                    {

                        int ret;
                        if (_i == 0)
                        {

                            ret = cur + 1;
                            if (ret > INF) return -1;
                            return ret;
                        }
                        else
                        {

                            ret = cur * 2;
                            if (ret > INF || ret == 0) return -1;

                            for (int i = 0; i < 5; i++)
                            {

                                if (ret < arr[i]) continue;
                                ret -= arr[i];
                                break;
                            }

                            return ret;
                        }
                    }
                }

                if (visit[g] > t || visit[g] == -1) Console.Write("ANG");
                else Console.Write(visit[g]);
            }

            void Input()
            {

                int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                n = input[0];
                t = input[1];
                g = input[2];
            }
        }
    }
}
