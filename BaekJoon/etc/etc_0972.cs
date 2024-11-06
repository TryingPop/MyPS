using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 9. 16
이름 : 배성훈
내용 : 일곱 난쟁이
    문제번호 : 2309번

    브루트포스, 정렬 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0972
    {

        static void Main972(string[] args)
        {

            int[] arr;
            int[] ret;

            Solve();
            void Solve()
            {

                Input();

                DFS();

                for (int i = 0; i < 7; i++)
                {

                    Console.WriteLine(ret[i]);
                }
            }

            bool DFS(int _depth = 0, int _sum = 0, int _s = 0)
            {

                if (_depth == 7)
                {

                    return _sum == 100;
                }

                for (int i = _s; i < 9; i++)
                {

                    ret[_depth] = arr[i];
                    if (DFS(_depth + 1, _sum + arr[i], i + 1)) return true;
                }

                return false;
            }

            void Input()
            {

                arr = new int[9];
                for (int i = 0; i < 9; i++)
                {

                    arr[i] = int.Parse(Console.ReadLine());
                }

                Array.Sort(arr);
                ret = new int[7];
            }
        }
    }
}
