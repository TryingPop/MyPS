using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaekJoon.etc
{
    internal class etc_1372
    {

        static void Main(string[] args)
        {

            // 5389번
            // 현재 오답..
            // a^2 + input = b^2인 b를 찾는건
            // 저장할 값이 너무 많고 탐색이 너무 많다.
            // 그래서 (a + b)(a - b) = n인 점을 이용해 풀자
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());

            string IMPO = "IMPOSSIBLE\n";
            for (int i = 0; i < n; i++)
            {

                int input = int.Parse(sr.ReadLine());

                int half = input >> 1;
                for (int j = 1; j * j <= input; j++)
                {


                }
            }
        }
    }
}
