using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 25
이름 : 배성훈
내용 : 농구 경기
    문제번호 : 1159번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1465
    {

        static void Main1465(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            int[] cnt = new int[255];
            for (int i = 0; i < n; i++)
            {

                string input = sr.ReadLine();
                cnt[input[0]]++;
            }
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            bool flag = true;
            for (int i = 0; i < 255; i++)
            {

                if (cnt[i] < 5) continue;
                sw.Write((char)i);
                flag = false;
            }

            if (flag) sw.Write("PREDAJA");
        }
    }
}
