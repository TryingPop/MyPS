using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 29
이름 : 배성훈
내용 : 진짜 공간
    문제번호 : 1350번

    사칙연산 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1489
    {

        static void Main1489(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());
            int[] arr = Array.ConvertAll(sr.ReadLine().Split(), int.Parse);
            int div = int.Parse(sr.ReadLine());

            long ret = 0;
            for (int i = 0; i < n; i++)
            {

                // 클러스터 갯수 추가
                long add = arr[i] / div;
                add += arr[i] % div == 0 ? 0 : 1;

                ret += add;
            }

            Console.Write(ret * div);
        }
    }
}
