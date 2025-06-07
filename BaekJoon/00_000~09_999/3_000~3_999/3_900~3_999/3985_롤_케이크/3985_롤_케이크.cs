using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 2. 26
이름 : 배성훈
내용 : 롤 케이크
    문제번호 : 3985번

    구현, 시뮬레이션 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1362
    {

        static void Main1362(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int l = ReadInt();
            int n = ReadInt();

            bool[] arr = new bool[l + 1];
            int max1 = 0, max2 = 0;
            int ret1 = 0, ret2 = 0;
            for (int i = 1; i <= n; i++)
            {

                int s = ReadInt();
                int e = ReadInt();

                if (max1 < e - s + 1)
                {

                    max1 = e - s + 1;
                    ret1 = i;
                }

                int cnt = 0;
                
                for (int j = s; j <= e; j++)
                {

                    if (arr[j]) continue;
                    arr[j] = true;
                    cnt++;
                }

                if (cnt <= max2) continue;
                max2 = cnt;
                ret2 = i;
            }

            Console.Write($"{ret1}\n{ret2}");

            int ReadInt()
            {

                int c, ret = 0;

                while ((c = sr.Read()) != -1 && c != ' ' && c!= '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
