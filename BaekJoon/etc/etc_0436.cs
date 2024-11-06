using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 2
이름 : 배성훈
내용 : Base Conversion
    문제번호 : 11576번

    수학, 구현, 정수론 문제다
    A진법을 -> 10진법으로 바꾸고
    10진법으로 바꾼 숫자를 다시 B진법으로 바꿔서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0436
    {

        static void Main436(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());

            int from = ReadInt();
            int to = ReadInt();
            int n = ReadInt();

            int num = 0;
            for (int i = 0; i < n; i++)
            {

                num = num * from + ReadInt();
            }
            sr.Close();

            int[] ret = new int[31];
            int idx = 0;
            
            while(num > 0)
            {

                ret[idx++] = num % to;
                num /= to;
            }

            for (int i = idx - 1; i >= 0; i--)
            {

                sw.Write(ret[i]);
                sw.Write(' ');
            }

            if (idx == 0) sw.Write('0');

            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
