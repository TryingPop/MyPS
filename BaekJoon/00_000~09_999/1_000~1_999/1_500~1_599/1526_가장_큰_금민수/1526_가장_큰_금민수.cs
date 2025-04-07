using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 7
이름 : 배성훈
내용 : 가장 큰 금민수
    문제번호 : 1526번

    구현, 브루트포스 문제다.
    DFS를 이용해 해결했다.
*/
namespace BaekJoon.etc
{
    internal class etc_1526
    {

        static void Main1526(string[] args)
        {

            int n = int.Parse(Console.ReadLine());

            int ret = 0;
            DFS();
            Console.Write(ret);
            void DFS(int _cur = 0)
            {

                if (_cur > n) return;

                ret = Math.Max(ret, _cur);
                DFS(_cur * 10 + 4);
                DFS(_cur * 10 + 7);
            }
        }
    }
}
