using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 15
이름 : 배성훈
내용 : Identifying tea
    문제번호 : 11549번

    구현문제다.
    처음에 정답이 들어오고, 5개의 입력이 주어진다.
    정답과 일치하는 것의 갯수를 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1276
    {

        static void Main1276(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int ret = 0;
            for (int i = 0; i < 5; i++)
            {

                if (arr[i] == n) ret++;
            }

            Console.Write(ret);
        }
    }
}
