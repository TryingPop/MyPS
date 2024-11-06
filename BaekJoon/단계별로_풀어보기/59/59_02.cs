using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 7. 26
이름 : 배성훈
내용 : 도키도키 간식드리미
    문제번호 : 12789번

    스택 문제다
    배열을 이용해 스택처럼 풀었다
*/

namespace BaekJoon._59
{
    internal class _59_02
    {

        static void Main2(string[] args)
        {

            string YES = "Nice";
            string NO = "Sad";

            StreamReader sr;
            int n;
            int[] stack;
            int len;

            Solve();

            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();

                len = 0;
                stack = new int[n];
                int cur = 1;
                for (int i = 0; i < n; i++)
                {

                    int num = ReadInt();

                    stack[len++] = num;

                    // 입장가능한지 확인
                    while (len > 0 && stack[len - 1] == cur)
                    {

                        len--;
                        cur++;
                    }
                }

                sr.Close();

                if (len == 0) Console.Write(YES);
                else Console.Write(NO);
            }

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
