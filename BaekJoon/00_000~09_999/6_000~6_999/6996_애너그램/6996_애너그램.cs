using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 11
이름 : 배성훈
내용 : 애너그램
    문제번호 : 6996번

    문자열 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1329
    {

        static void Main1329(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int[] cnt = new int[255];
            int n = int.Parse(sr.ReadLine());

            while(n-- > 0)
            {

                string[] temp = sr.ReadLine().Split();

                for (int i = 0; i < temp[0].Length; i++)
                {

                    cnt[temp[0][i]]++;
                }

                for (int i = 0; i < temp[1].Length; i++)
                {

                    cnt[temp[1][i]]--;
                }

                bool flag = false;
                for (int i = 'a'; i <= 'z'; i++)
                {

                    if (cnt[i] == 0) continue;
                    cnt[i] = 0;
                    flag = true;
                }

                sw.Write($"{temp[0]} & {temp[1]} are ");
                if (flag) sw.Write("NOT ");
                sw.Write("anagrams.\n");
            }
        }
    }
}
