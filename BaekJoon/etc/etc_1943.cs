using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 18
이름 : 배성훈
내용 : 눈덩이 굴리기
    문제번호 : 21735번

    브루트포스, 백트래킹 문제다.
    배낭 dp를 이용해 해결했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1943
    {

        static void Main1943(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

            int n = ReadInt();
            int m = ReadInt();
            int[] arr = new int[n + 3];
            for (int i = 1; i <= n; i++)
            {

                arr[i] = ReadInt();
            }

            int[] cur = new int[n + 3];
            int[] next = new int[n + 3];

            for (int i = 0; i <= n; i++)
            {

                cur[i] = -1;
                next[i] = -1;
            }

            cur[0] = 1;

            for (int i = 0; i < m; i++)
            {

                for (int j = 0; j < n; j++)
                {

                    if (cur[j] == -1) continue;
                    next[j + 1] = Math.Max(arr[j + 1] + cur[j], next[j + 1]);
                    next[j + 2] = Math.Max(arr[j + 2] + cur[j] / 2, next[j + 2]);
                }

                for (int j = 0; j < n; j++)
                {

                    cur[j] = next[j];
                    next[j] = -1;
                }
            }

            int ret = next[n];
            for (int i = 1; i < n; i++)
            {

                ret = Math.Max(cur[i], ret);
            }

            Console.Write(ret);

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) ;
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;
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
