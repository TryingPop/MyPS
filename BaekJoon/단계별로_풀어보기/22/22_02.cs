using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 20
이름 : 배성훈
내용 : 약수 구하기
    문제번호 : 2501번
*/

namespace BaekJoon._22
{
    internal class _22_02
    {

        static void Main2(string[] args)
        {

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int result = 0;

            {

                int count = 0;

                for (int i = 1; i <= inputs[0]; i++)
                {

                    // 약수 인 경우
                    if (inputs[0] % i == 0)
                    {

                        count++;
                    }

                    
                    // 찾는 값인 경우
                    if (count == inputs[1])
                    {

                        result = i;
                        break;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
