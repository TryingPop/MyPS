using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 7. 5
이름 : 배성훈
내용 : 나는 기말고사형 인간이야
    문제번호 : 23254번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1747
    {

        static void Main1747(string[] args)
        {

            int n, m;
            int[] a, cnt;
            long ret;

            Input();

            GetRet();

            void GetRet()
            {

                for (int i = 100; i > 0 && n > 0; i--)
                {

                    if (cnt[i] <= n)
                    {

                        n -= cnt[i];
                        ret += cnt[i] * i;
                    }
                    else
                    {

                        ret += n * i;
                        n = 0;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadInt() * 24;
                m = ReadInt();

                ret = 0;
                cnt = new int[101];
                a = new int[m];
                for (int i = 0; i < m; i++)
                {

                    a[i] = ReadInt();
                    ret += a[i];
                }

                for (int i = 0; i < m; i++)
                {

                    int b = ReadInt();
                    int r = 100 - a[i];

                    int chk = r / b;
                    cnt[b] += chk;
                    cnt[r % b]++;
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
