using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 4. 30
이름 : 배성훈
내용 : 차이를 M 이상으로
    문제번호 : 32396번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1597
    {

        static void Main1597(string[] args)
        {

            int n;
            long m;
            long[] arr;

            Input();

            GetRet();

            void GetRet()
            {

                long CHANGE = 1_000_000_000_000_000;
                int ret = 0;
                for (int i = 1; i < n; i++)
                {

                    if (Math.Abs(arr[i - 1] - arr[i]) < m)
                    {

                        ret++;
                        arr[i] = CHANGE;
                    }
                }

                Console.Write(ret);
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                m = ReadLong();

                arr = new long[n];

                for (int i = 0; i < n; i++)
                {

                    arr[i] = ReadLong();
                }

                long ReadLong()
                {

                    long ret = 0;

                    while (TryReadLong()) ;
                    return ret;

                    bool TryReadLong()
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
