using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 11
이름 : 배성훈
내용 : 정수 삼각형
    문제번호 : 1932번
*/

namespace BaekJoon._14
{
    internal class _14_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int length = int.Parse(sr.ReadLine());
            int[][] inputs = new int[length][];
            int[] maxs = new int[length];
            for (int i = 0; i < length; i++)
            {

                inputs[i] = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));

                for (int j = inputs[i].Length - 1; j >= 0; j--)
                {

                    if (j == 0)
                    {

                        maxs[j] += inputs[i][j];
                    }
                    else
                    {

                        maxs[j] = maxs[j - 1] >= maxs[j] ? maxs[j - 1] : maxs[j];
                        maxs[j] += inputs[i][j];
                    }
                }
            }
            sr.Close();

            int result = maxs.Max();
            Console.WriteLine(result);
        }
    }
}
