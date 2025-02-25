using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 4
이름 : 배성훈
내용 : 자기복제수
    문제번호 : 2028번

    수학 문제다.
    뒷자리에 자기자신이 있는지는 빼줘서 0이되는지로 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1314
    {

        static void Main1314(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = int.Parse(sr.ReadLine());
            int[] digit = { 1, 10, 100, 1_000, 10_000 };

            for (int i = 0; i < n; i++)
            {

                string input = sr.ReadLine();
                int cur = int.Parse(input);
                int pow = cur * cur;

                pow -= cur;
                if (pow % digit[input.Length] == 0) sw.Write(Y);
                else sw.Write(N);
            }
        }
    }
}
