using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 26
이름 : 배성훈
내용 : Greedily Increasing Subsequence
    문제번호 : 17931번
    
    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1471
    {

        static void Main1471(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            int[] ret = new int[n];
            int len = 0;

            ret[len++] = ReadInt();

            for (int i = 1; i < n; i++)
            {

                int cur = ReadInt();
                if (ret[len - 1] < cur) ret[len++] = cur;
            }

            sw.Write($"{len}\n");
            for (int i = 0; i < len; i++)
            {

                sw.Write($"{ret[i]} ");
            }

            int ReadInt()
            {

                int ret = 0;
                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    ret = c - '0';

                    while ((c = sr.Read()) != -1 && c != ' ' && c != '\n') 
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    return false;
                }
            }
        }
    }
}
