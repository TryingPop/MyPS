using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2023. 7. 10
이름 : 배성훈
내용 : ATM
    문제번호 : 11399번
*/

namespace BaekJoon._13
{
    internal class _13_03
    {

        static void Main3(string[] args)
        {

            StreamReader sr = new StreamReader(Console.OpenStandardInput());

            int length = int.Parse(sr.ReadLine());
            int[] inputs = Array.ConvertAll(sr.ReadLine().Split(' '), item => int.Parse(item));
            sr.Close();

            Array.Sort(inputs);             // 오름차순 정렬

            int time = 0;
            int result = 0;

            for (int i = 0; i < length; i++)
            {

                time += inputs[i];  // 인출하는데 걸리는 시간
                result += time;     // 모든 인원이 인출하는데 걸리는 시간
            }

            Console.WriteLine(result);
        }
    }
}
