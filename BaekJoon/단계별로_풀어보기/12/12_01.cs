using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 7
이름 : 배성훈
내용 : 약수
    문제번호 : 1037번

    1과 자기자신을 제외한 모든 약수가 주어질 때,
    해당 수를 구하는 프로그램을 만드시오
*/

namespace BaekJoon._12
{
    internal class _12_01
    {

        static void Main1(string[] args)
        {

            int length = int.Parse(Console.ReadLine());

            int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), 
                input => int.Parse(input));

            int result;

            // 오름차순 정렬
            // Array.Sort 함수를 써도 되나 연습삼아 코드로 구현
            for (int i  = 0; i < length - 1; i++)
            {

                for (int j  = 0; j < length - i - 1; j++)
                {

                    if (inputs[j] > inputs[j + 1])
                    {

                        int temp = inputs[j];
                        inputs[j] = inputs[j + 1];
                        inputs[j + 1] = temp;
                    }
                }
            }


            bool square = true;
            result = inputs[0];

            for (int i = 1; i < length; i++)
            {

                int temp = 1;

                for (int j = 0; j < i; j++)
                {

                    if (inputs[j] == 1)
                    {

                        continue;
                    }

                    temp = inputs[i];

                    // 합성수, 제곱수인지 판별
                    while (temp > inputs[j])
                    {

                        if (temp % inputs[j] != 0)
                        {

                            break;
                        }

                        temp /= inputs[j];
                    }

                    
                    if (temp == inputs[j])
                    {

                        // 제곱수 경우
                        temp = inputs[j];
                        break;
                    }
                    else if (temp < inputs[i])
                    {

                        // 합성수인 경우
                        temp = 1;
                        break;
                    }
                }

                // 제곱수 판정
                if (temp == inputs[i] || temp == 1)
                {

                    square = false;
                }

                inputs[i] = temp;
                result *= inputs[i];
            }

            // 제곱수면 한번 더 곱해줘야한다
            if (square)
            {

                result *= inputs[0];
            }

            Console.WriteLine(result);
        }
    }
}
