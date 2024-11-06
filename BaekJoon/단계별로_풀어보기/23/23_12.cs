using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 공 넣기
    문제번호 : 10810번
*/

namespace BaekJoon._23
{
    internal class _23_12
    {


        static void Main12(string[] args)
        {

            // 0 : 바구니 갯수, 1: 공 넣는 시행 횟수
            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // 바구니 원소는 공번호
            int[] pockets = new int[info[0]];


            for (int i = 0; i < info[1]; i++)
            {

                int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

                // 공 넣기
                for (int j = input[0] - 1; j < input[1]; j++)
                {

                    pockets[j] = input[2];
                }
            }

            foreach (var s in pockets)
            {

                Console.Write(s);
                Console.Write(" ");
            }
            Console.WriteLine();
        }
    }
}
