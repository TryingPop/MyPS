using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 25
이름 : 배성훈
내용 : 수열과 수열 2
    문제번호 : 34155번

    수학, 조합론 문제다.
    서브테스크 문제다.
    2번 서브테스크 조건도 1번처럼 적용되는줄 알고 제출하다가 1번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1950
    {

        static void Main1950(string[] args)
        {

            int MOD = 998_244_353;

            int n;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                long ret = 1;
                int pop1 = n - 1;
                int pop2 = n - 2;
                for (int i = 0; i < n; i++)
                {

                    int mul = arr[i] == i + 1 ? pop1 : pop2;
                    ret = (ret * mul) % MOD;
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                arr = new int[n];
                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadInt();
                }

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
}
