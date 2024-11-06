using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 14
이름 : 배성훈
내용 : 행렬덧셈
    문제번호 : 2738번

    stringbuilder를이용하면 최소 5배 이상의 시간 단축이 된다
*/

namespace BaekJoon._15
{
    internal class _15_01
    {

        static void Main1(string[] args)
        {

            int[] info = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[,] result = new int[info[0], info[1]];

            for (int i = 0; i < info[0]; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                for (int  j = 0; j < info[1]; j++)
                {

                    result[i, j] += inputs[j];
                }
            }

            for (int i = 0; i < info[0]; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < info[1]; j++)
                {

                    result[i, j] += inputs[j];
                }
            }

            for (int i = 0; i < info[0]; i++)
            {

                for (int j = 0; j < info[1]; j++)
                {

                    Console.Write(result[i, j]);
                    Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
    }
}
