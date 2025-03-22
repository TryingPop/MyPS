using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 11. 23
이름 : 배성훈
내용 : 순열
    문제번호 : 1115번

    그래프 이론 문제다.
    문제를 분석하면 사이클의 개수와 정답이 거의 일치함을 알 수 있다.
    사이클이 1개인 경우 0으로만 해주면 완벽히 일치한다.
*/

namespace BaekJoon.etc
{
    internal class etc_1129
    {

        static void Main1129(string[] args)
        {

            int n;
            int[] arr;
            bool[] visit;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                visit = new bool[n];

                int ret = 0;
                for (int i = 0; i < n; i++)
                {

                    if (visit[i]) continue;
                    ret++;
                    DFS(i);
                }

                Console.Write(ret == 1 ? 0 : ret);
                void DFS(int _idx)
                {

                    if (visit[_idx]) return;
                    visit[_idx] = true;

                    DFS(arr[_idx]);
                }
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

                sr.Close();
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
    }
}
