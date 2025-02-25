using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 20
이름 : 배성훈
내용 : 고급 수학
    문제번호 : 7510번

    수학 문제다.
    피타고라스 정리가 성립하는지 확인하면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1350
    {

        static void Main1350(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] arr;
            int n = int.Parse(sr.ReadLine());
            
            for (int i = 1; i <= n; i++)
            {

                arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);

                sw.Write($"Scenario #{i}:\n");
                if (Solve()) sw.Write("yes\n\n");
                else sw.Write("no\n\n");
            }

            bool Solve()
            {

                Array.Sort(arr);
                return arr[0] * arr[0] + arr[1] * arr[1] == arr[2] * arr[2];
            }
        }
    }
}
