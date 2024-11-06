using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 10. 23
이름 : 배성훈
내용 : 구간 합 구하기 5
    문제번호 : 11660번
*/

namespace BaekJoon._26
{
    internal class _26_05
    {

        static void Main5(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            // 0 : 사각형 크기, 1 : 문제 수
            int[] info = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // i, j : 1, 1 에서 i, j 사각형 안의 숫자들의 총합
            int[,] sums = new int[info[0] + 1, info[0] + 1];

            for (int i = 1; i <= info[0]; i++)
            {

                // i 줄 입력
                int[] input = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                int sum = 0;

                // 총합 값 입력
                for (int j = 1; j <= info[0]; j++)
                {

                    sum += input[j - 1];
                    sums[i, j] = sum + sums[i - 1, j];
                }
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < info[1]; i++)
            {

                int[] input = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
                
                // 해당 구간 구한다
                int result = sums[input[2], input[3]] + sums[input[0] - 1, input[1] - 1] 
                    - sums[input[2],input[1] - 1] - sums[input[0] - 1, input[3]];

                // 정답을 모은다
                sb.AppendLine(result.ToString());
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.WriteLine(sb);
            }
        }
    }
}
