using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 15
이름 : 배성훈
내용 : 다리 놓기
    문제번호 : 1010번

    동적계획법을 이용해 풀었다
    곱셈과 나눗셈을 따로 할 시 15, 30을 입력받으면 숫자가 너무 커져서
    오버플로우가 발생한다
*/

namespace BaekJoon._17
{
    internal class _17_05
    {

        static void Main5(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            const int max = 30;

            int length = int.Parse(Console.ReadLine());

            int[,] pascal = new int[max + 1, max + 1];

            for (int i = 0; i < length; i++)
            {

                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                for (int j = 0; j <= inputs[1]; j++)
                {

                    for (int k = 0; k <= inputs[1]; k++)
                    {

                        if (k >= j)
                        {

                            pascal[j, k] = 1;
                            break;
                        }
                        else if (k == 0)
                        {

                            pascal[j, k] = 1;
                        }
                        else
                        {

                            pascal[j, k] = pascal[j - 1, k - 1] + pascal[j - 1, k];
                        }
                    }
                }

                int result = pascal[inputs[1], inputs[0]];

                if (result == 0)
                {

                    result = 1;
                }

                sb.AppendLine(result.ToString());
            }

            Console.WriteLine(sb);

            /*
            // 대략 다음과 같이 풀어쓰면 된다 
            int num1 = inputs[0];
            int num2 = inputs[1];
            int mul = num2;
            int cnt = num2 - num1 > num1 ? num1 : num2 - num1;

            long result = 1;
            for (int i = 1; i <= cnt; i++)
            {

                result *= mul;
                result /= i;
                mul--;
            }

            Console.WriteLine(result);
            */
        }
    }
}
