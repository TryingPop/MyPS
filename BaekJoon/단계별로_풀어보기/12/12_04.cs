using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 8
이름 : 배성훈
내용 : 통계학
    문제번호 : 2108번

    숫자가 주어질 때 산술평균, 중앙값, 최빈값, 범위를
    출력하는 프로그램 만들기
*/

namespace BaekJoon._12
{
    internal class _12_04
    {

        static void Main4(string[] args)
        {


            int length = int.Parse(Console.ReadLine());
            int[] inputs = new int[length];
            int[] counts = new int[length];

            int r1 = 0, r2 = 0, r3 = 0, r4 = 0;     // 산술평균, 중앙값, 최빈값, 범위


            for (int i = 0; i < length; i++)
            {

                inputs[i] = int.Parse(Console.ReadLine());
            }

            Array.Sort(inputs);

            for (int i = 0; i < length; i++)
            {

                int idx = i;

                for (int j = i + 1; j < length; j++)
                {

                    if (inputs[idx] != inputs[j])
                    {

                        break;
                    }

                    i++;
                    counts[idx]++;
                }
            }


            bool seconds = false;
            int max = counts[0];

            for (int i = 1; i < length; i++)
            {

                if (max >= counts[i])
                {

                    if (!seconds)
                    {

                        seconds = true;
                        r3 = i;
                        max = counts[i];
                    }

                    continue;
                }

                r3 = i;
                max = counts[i];

                seconds = false;
            }
            
            r1 = (int)Math.Round(inputs.Average());
            r2 = inputs[(length / 2)];
            r3 = inputs[r3];
            r4 = inputs[length - 1] - inputs[0];

            Console.WriteLine(r1);
            Console.WriteLine(r2);
            Console.WriteLine(r3);
            Console.WriteLine(r4);
        }
    }
}
