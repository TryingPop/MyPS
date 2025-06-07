using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 22
이름 : 배성훈
내용 : 노솔브 방지문제야!!
    문제번호 : 15917번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1355
    {

        static void Main1355(string[] args)
        {

            HashSet<int> corr = new(32);

            for (int i = 0; i < 32; i++)
            {

                corr.Add(1 << i);
            }
            
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
            int q = int.Parse(sr.ReadLine());

            for (int i = 0; i < q; i++)
            {

                int cur = int.Parse(sr.ReadLine());
                if (corr.Contains(cur)) sw.WriteLine(1);
                else sw.WriteLine(0);
            }
        }
    }
}
