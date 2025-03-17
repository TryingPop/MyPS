using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 22
이름 : 배성훈
내용 : 큰 수 구성하기
    문제번호 : 18511번

    브루트포스, 재귀 문제다.
    k가 3으로 매우 작다.
    그리고 n의 값이 1억을 넘지 않으므로
    전체 경우의 수는 3^9 < 100만을 넘지 못한다.
    그래서 브루트포스로 해결했다.

    그리디를 이용해 직접 만들어 풀 수도 있다.
*/

namespace BaekJoon.etc
{
    internal class etc_1290
    {

        static void Main1290(string[] args)
        {

            int n, k, e;
            int[] arr;

            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet() 
            {

                int ret = -1;
                DFS();
                Console.Write(ret);

                void DFS(int _val = 0)
                {

                    if (_val > n) return;
                    ret = Math.Max(_val, ret);

                    for (int i = 0; i < k; i++)
                    {

                        DFS(_val * 10 + arr[i]);
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                e = temp[0].Length;
                n = int.Parse(temp[0]);
                k = int.Parse(temp[1]);

                temp = Console.ReadLine().Split();
                arr = new int[k];
                
                for (int i = 0; i < k; i++)
                {

                    arr[i] = temp[i][0] - '0';
                }
            }
        }
    }
}
