using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 바구니 뒤집기
    문제번호 : 10811번
*/

namespace BaekJoon._23
{
    internal class _23_15
    {

        static void Main15(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] pocket = new int[info[0] + 1];

            for (int i = 1; i < pocket.Length; i++)
            {

                pocket[i] = i;
            }

            for (int i = 0; i < info[1]; i++)
            {

                int[] input = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                int temp;

                // 뒤집기
                for (int j = input[0]; j <= (input[1] + input[0]) / 2; j++)
                {

                    temp = pocket[j];
                    pocket[j] = pocket[input[1] + input[0] - j];
                    pocket[input[1] + input[0] - j] = temp;
                }
            }

            // 출력
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < pocket.Length; i++)
            {

                sb.Append(pocket[i].ToString()).Append(' ');
            }
            sb.Append('\n');

            Console.WriteLine(sb);
        }
    }
}
