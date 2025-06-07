using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 13
이름 : 배성훈
내용 : 복호화
    문제번호 : 9046번

    문자열 문제다.
    암호랑 전혀 상관없이 가장 많이 출현한 알파벳을 찾는 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1403
    {

        static void Main1403(string[] args)
        {

            
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n;

            n = int.Parse(sr.ReadLine());

            int[] cnt = new int[26];
            for (int i = 0; i < n; i++)
            {

                string str = sr.ReadLine();
                for (int j = 0; j < str.Length; j++)
                {

                    if ('a' <= str[j] && str[j] <= 'z') cnt[str[j] - 'a']++;
                }

                int max = 0;
                int chk = 0;
                int ret = 0;

                for (int j = 0; j < 26; j++)
                {

                    if (cnt[j] > max)
                    {

                        max = cnt[j];
                        chk = 1;
                        ret = j;
                    }
                    else if (cnt[j] == max)
                        chk++;

                    cnt[j] = 0;
                }

                if (chk != 1) sw.Write('?');
                else sw.Write((char)(ret + 'a'));
                sw.Write('\n');
            }
        }
    }
}
