using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 13
이름 : 배성훈
내용 : 가장 많은 글자
    문제번호 : 1371번

    구현, 문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1402
    {

        static void Main1402(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            string str;
            int[] cnt = new int[128];
            while ((str = sr.ReadLine()) != null)
            {

                for (int i = 0; i < str.Length; i++)
                {

                    cnt[str[i]]++;
                }
            }
            int max = 0;
            for (int i = 'a'; i <= 'z'; i++)
            {

                max = Math.Max(max, cnt[i]);
            }

            using StreamWriter sw = new(Console.OpenStandardOutput());
            for (char i = 'a'; i <= 'z'; i++)
            {

                if (cnt[i] == max) sw.Write(i);
            }
        }
    }
}
