using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 12. 10
이름 : 배성훈
내용 : 행렬 곱셈
    문제번호 : 2740번
*/

namespace BaekJoon._27
{
    internal class _27_06
    {

        static void Main6(string[] args)
        {

            // 입력
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] inputs = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] MatA = new int[inputs[0]][];
            
            for (int i = 0; i < inputs[0]; i++)
            {

                MatA[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            inputs = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

            int[][] MatB = new int[inputs[0]][];


            for (int i = 0; i < inputs[0]; i++)
            {

                MatB[i] = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();
            }

            sr.Close();


            StringBuilder sb = new StringBuilder();

            // 행렬 곱
            for (int i = 0; i < MatA.Length; i++)
            {

                for (int j = 0; j < MatB[0].Length; j++)
                {

                    int result = 0;

                    for (int k = 0; k < MatA[i].Length; k++)
                    {

                        result += MatA[i][k] * MatB[k][j];
                    }

                    sb.Append(result);
                    sb.Append(' ');
                }

                sb.AppendLine();
            }

            // 출력
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                sw.Write(sb);
            }
        }
    }
}
