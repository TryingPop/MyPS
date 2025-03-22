using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 수리공 항승
    문제번호 : 1449번

    그리디, 정렬 문제다.
    가장 왼쪽에 밴드를 붙이는게 좋다.
*/

namespace BaekJoon.etc
{
    internal class etc_1445
    {

        static void Main1445(string[] args)
        {

            int MAX = 1_000;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt(), l = ReadInt();
            int[] arr = new int[n];
            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt();
            }

            Array.Sort(arr);
            int e = -1;

            for (int i = 0; i < n; i++)
            {

                if (arr[i] <= e) continue;
                e = arr[i] + l - 1;
                ret++;
            }

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';
                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
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
