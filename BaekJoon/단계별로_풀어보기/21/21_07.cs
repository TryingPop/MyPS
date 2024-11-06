using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 19
이름 : 배성훈
내용 : 대칭 차집합
    문제번호 : 1269번
*/

namespace BaekJoon._21
{
    internal class _21_07
    {

        static void Main7(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int[] info = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            HashSet<int> A = new HashSet<int>(info[0]);
            // HashSet<int> B = new HashSet<int>(info[1]);

            {

                // int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                int[] inputs = sr.ReadLine().Split(' ').Select(int.Parse).ToArray();

                for (int i = 0; i < info[0]; i++)
                {

                    A.Add(inputs[i]);
                }
            }

            int result = info[0] + info[1];

            {

                int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);
                for (int i = 0; i < info[1]; i++)
                {

                    if (A.Contains(inputs[i]))
                    {

                        result -= 2;
                    }
                }
            }

            Console.WriteLine(result);
        }
    }
}
