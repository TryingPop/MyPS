using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 17
이름 : 배성훈
내용 : 두더지 찾기
    문제번호 : 32633번

    수학, 정수론 문제다.
    아이디어는 다음과 같다.
    두더지가 가능한 가장 작은 시간은 최소 공배수 LCM이다.
    그래서 LCM을 찾아갔다.

    여기서 불가능한 경우는
    LCM이 t를 초과하는 경우가 있다.
    그리고 b[i]가 
*/

namespace BaekJoon.etc
{
    internal class etc_1707
    {

        static void Main1707(string[] args)
        {

            int n;
            long t;
            int[] a;
            bool[] b;

            Input();

            GetRet();

            void GetRet()
            {

                long lcm = 1;
                for (int i = 0; i < n; i++)
                {

                    if (!b[i]) continue;

                    lcm = GetLCM(lcm, a[i]);
                    if (lcm > t)
                    {

                        Console.Write(-1);
                        return;
                    }
                }

                for (int i = 0; i < n; i++)
                {

                    if (b[i]) continue;
                    if (lcm % a[i] == 0)
                    {

                        Console.Write(-1);
                        return;
                    }
                }

                Console.Write(lcm);

                long GetLCM(long _a, long _b)
                {

                    long gcd = GetGCD(_a, _b);
                    return (_a / gcd) * _b;
                }

                long GetGCD(long _a, long _b)
                {

                    while (_b > 0)
                    {

                        long temp = _a % _b;
                        _a = _b;
                        _b = temp;
                    }

                    return _a;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();
                t = ReadLong();
                a = new int[n];
                b = new bool[n];

                for (int i = 0; i < n; i++) 
                {

                    a[i] = ReadInt();
                }

                for (int i = 0; i < n; i++) 
                {

                    b[i] = ReadInt() == 1;
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
