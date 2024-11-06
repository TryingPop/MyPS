using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 파도반 수열
    문제번호  : 9461번
*/

namespace BaekJoon._14
{
    internal class _14_04
    {

        static void Main4(string[] args)
        {

            StringBuilder sb = new StringBuilder();

            int length = int.Parse(Console.ReadLine());

            long[] results = new long[100];
            // long[] pro = new long[100];
            int num = 7;

            results[0] = 1;
            // pro[0] = 1;
            results[1] = 1;
            // pro[1] = 1;
            results[2] = 1;
            // pro[2] = 1;
            results[3] = 2;
            // pro[3] = 2;

            results[4] = 2;
            // pro[4] = 2;
            results[5] = 3;
            // pro[5] = 3;
            results[6] = 4;
            // pro[6] = 4;

            for (int i= 0; i < length; i++)
            {

                int input = int.Parse(Console.ReadLine());

                if (num >= input)
                {

                    sb.AppendLine(results[input - 1].ToString());
                }
                else
                {

                    for (int j = num; j < input; j++)
                    {

                        results[j] = results[j - 2] + results[j - 3];
                        // pro[j] = pro[j - 1] + pro[j - 5];
                    }

                    num = input;
                    sb.AppendLine(results[input - 1].ToString());
                }
            }

            Console.WriteLine(sb.ToString());

        }
    }
}
