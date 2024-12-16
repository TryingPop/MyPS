using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 12. 15
이름 : 배성훈
내용 : 부분배열 고르기 2
    문제번호 : 1989번

    스택 문제다.
    모두 0인 예외 케이스를 처리 못해 2번 틀렸다.
    아이디어는 히스토그램 아이디어처럼 풀었다.
*/

namespace BaekJoon.etc
{
    internal class etc_1190
    {

        static void Main1190(string[] args)
        {

            int n;
            long[] sum;
            int[] arr, stk;
            Solve();
            void Solve()
            {

                Input();

                GetRet();
            }

            void GetRet()
            {

                long ret1 = -1;
                long retL = -1, retR = -1;
                int len = 0;
                arr[n + 1] = -1;
                for (int i = 1; i < arr.Length; i++)
                {

                    while (len > 0 && arr[i] < arr[stk[len - 1]])
                    {

                        len--;
                        int right = i - 1;
                        int left = len == 0 ? 0 : stk[len - 1];
                        int height = arr[stk[len]];

                        long cur = (sum[right] - sum[left]) * height;
                        if (ret1 < cur)
                        {

                            ret1 = cur;
                            retL = left + 1;
                            retR = right;
                        }
                    }

                    stk[len++] = i;
                }

                Console.Write($"{ret1}\n{retL} {retR}");
            }

            void Input()
            {

                StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n + 2];
                sum = new long[n + 2];
                stk = new int[n + 1];
                for (int i = 1; i <= n; i++)
                {

                    arr[i] = ReadInt();
                    sum[i] = sum[i - 1] + arr[i];
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
}