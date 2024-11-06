using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 문자열 암호화
    문제번호 : 6616번

    구현, 문자열 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0461
    {

        static void Main461(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            StringBuilder sb = new(10_000);

            int[] lens = new int[1_000];
            char[] ret = new char[10_000];
            
            while (true)
            {

                int n = int.Parse(sr.ReadLine());
                if (n == 0) break;

                string[] str = sr.ReadLine().Split(' ');
                int total = 0;

                for (int i = 0; i < str.Length; i++)
                {

                    total += str[i].Length;
                }

                int s = 0;
                int curIdx = 0;
                for (int i = 0; i < str.Length; i++)
                {

                    for (int j = 0; j < str[i].Length; j++)
                    {

                        char cur = str[i][j];
                        if ('Z' < cur) cur = (char)(cur - 'a' + 'A');
                        ret[curIdx] = cur;
                        curIdx += n;
                        if (curIdx >= total)
                        {

                            curIdx = ++s;
                        }
                    }
                }

                for (int i = 0; i < total; i++)
                {

                    sb.Append(ret[i]);
                }
                sb.Append('\n');

                sw.Write(sb);
                sb.Clear();

                sw.Flush();
            }

            sw.Close();
            sr.Close();
        }
    }
}
