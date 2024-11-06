using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 킹, 퀸, 비숍, 나이트, 폰
    문제번호 : 3003번
*/

namespace BaekJoon._23
{
    internal class _23_05
    {

        static void Main5(string[] args)
        {

            int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] chk = new int[6] { 1, 1, 2, 2, 2, 8 };

            for (int i = 0; i < chk.Length; i++)
            {

                // 개수 파악
                Console.Write(chk[i] - input[i]);
                

                if (i < chk.Length - 1)
                {

                    // 마지막이 아닌 경우
                    Console.Write(' ');
                }
                else
                {

                    // 마지막인 경우
                    Console.Write("\n");
                }
            }
        }
    }
}
