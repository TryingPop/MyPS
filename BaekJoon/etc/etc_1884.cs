using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 14
이름 : 배성훈
내용 : 겹치는 건 싫어
    문제번호 : 20922번

    두 포인터 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1884
    {

        static void Main1884(string[] args)
        {

            int n, k;
            int[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                int[] use = new int[100_001];
                int l = 0;
                int cnt = 0;
                int ret = 1;
                for (int r = 0; r < n; r++)
                {

                    use[arr[r]]++;
                    while (use[arr[r]] > k)
                    {

                        use[arr[l++]]--;
                    }

                    ret = Math.Max(ret, r - l + 1);
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt();
                k = ReadInt();

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
