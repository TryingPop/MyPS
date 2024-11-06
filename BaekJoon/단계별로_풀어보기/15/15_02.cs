using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 14
이름 : 배성훈
내용 : 최댓값
    문제번호 : 2566번

    문제 조건 잘 읽기!
    max = 0으로 했을 때, 올 0인 행렬에 대해 틀린다!    
    // pos 의 초기값을 1, 1로 하던, 혹은 max의 값을 minValue로 해야한다
*/

namespace BaekJoon._15
{
    internal class _15_02
    {

        static void Main2(string[] args)
        {

            int max = int.MinValue;
            int[] pos = new int[2] { 1, 1 };

            for (int i = 1; i < 10; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                
                for (int j = 0; j < 9; j++)
                {

                    if (max < inputs[j])
                    {

                        max = inputs[j];
                        pos[0] = i;
                        pos[1] = j + 1;
                    }
                }
            }

            Console.WriteLine(max);
            Console.WriteLine($"{pos[0]} {pos[1]}");
        }
    }
}
