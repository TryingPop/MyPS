using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 26
이름 : 배성훈
내용 : Roads Around The Farm
    문제번호 : 6182번

    분할 정복 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1952
    {

        static void Main1952(string[] args)
        {

            long n, k;

            Input();

            GetRet();

            void GetRet()
            {

                int ret = DFS(n);

                Console.Write(ret);
                int DFS(long cur)
                {

                    if (Chk()) return 1;

                    long next = (cur - k) / 2;
                    return DFS(next) + DFS(next + k);

                    bool Chk()
                    {

                        long chk = cur - k;
                        if (chk <= 0 || chk % 2 == 1) return true;
                        else return false;
                    }
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = long.Parse(temp[0]);
                k = long.Parse(temp[1]);
            }
        }
    }
}
