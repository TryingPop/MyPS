using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 20
이름 : 배성훈
내용 : N과 M (11)
    문제번호 : 15665번

    백트래킹 문제다.
    구현을 잘못해 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1430
    {

        static void Main1430(string[] args)
        {

            int n, m;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                int[] ret = new int[m];

                DFS(0);

                void DFS(int _dep)
                {

                    if (_dep == m)
                    {

                        for (int i = 0; i < m; i++)
                        {

                            sw.Write($"{ret[i]} ");
                        }

                        sw.Write('\n');

                        return;
                    }

                    for (int i = 0; i < n; i++)
                    {

                        ret[_dep] = arr[i];
                        DFS(_dep + 1);
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                m = int.Parse(temp[1]);

                temp = Console.ReadLine().Split();
                int[] chk = new int[n];
                for (int i = 0; i < n; i++)
                {

                    chk[i] = int.Parse(temp[i]);
                }

                Array.Sort(chk);
                int len = 0;
                int prev = -1;
                for (int i = 0; i < n; i++)
                {

                    if (chk[i] != prev) len++;
                    prev = chk[i];
                }

                arr = new int[len];
                prev = -1;
                int idx = 0;
                for (int i = 0; i < n; i++)
                {

                    if (chk[i] == prev) continue;
                    prev = chk[i];
                    arr[idx++] = chk[i];
                }

                n = len;
            }
        }
    }
}
