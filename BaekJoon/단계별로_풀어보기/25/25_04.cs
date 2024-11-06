using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 23
이름 : 배성훈
내용 : N과 M (4)
    문제번호 : 15652번
*/

namespace BaekJoon._25
{
    internal class _25_04
    {

        static void Main4(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] result = new int[info[0]];

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            Back(sw, info, result);
            sw.Close();
        }

        static void Back(StreamWriter sw, int[] info, int[] result, int step = 0, int before = 1)
        {

            if (step >= info[1])
            {

                for (int i = 0; i < step; i++)
                {

                    sw.Write($"{result[i]} ");
                }
                sw.Write('\n');

                return;
            }

            // 이전 값보다 크거나 같은 경우만 실행
            for (int i = before; i <= info[0]; i++)
            {

                result[step] = i;

                // 재귀 함수 호출 방법에 대해 알면 이상 무
                Back(sw, info, result, step + 1, i);
            }
        }
    }
}
