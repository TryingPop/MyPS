using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 23
이름 : 배성훈
내용 : N과 M (3)
    문제번호 : 15651번

    중복순열 문제
    클래스로 푸는게 빨라보인다
*/

namespace BaekJoon._25
{
    internal class _25_03
    {

        static void Main3(string[] args)
        {

            int[] info = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[] result = new int[info[0]];

            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            Back(sw, info, result);
            sw.Close();
        }

        static void Back(StreamWriter sw, int[] info, int[] result, int step = 0)
        {

            // 탈출구문
            if (step >= info[1])
            {

                for (int i = 0; i < info[1]; i++)
                {

                    sw.Write($"{result[i]} ");
                }

                sw.Write('\n');
                return;
            }

            // 조건 확인
            for (int i = 1; i <= info[0]; i++)
            {

                // 해당 문자를 담는다
                result[step] = i;

                // step위치에 i인 경우를 모두 출력
                Back(sw, info, result, step + 1);

                // 탈출할 때에는 다음 단계로
            }
        }
    }
}
