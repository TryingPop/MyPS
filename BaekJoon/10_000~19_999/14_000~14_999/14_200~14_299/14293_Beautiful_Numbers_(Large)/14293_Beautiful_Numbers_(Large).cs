using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 29
이름 : 배성훈
내용 : Beautiful Numbers (Large)
    문제번호 : 14293번

    수학, 브루트포스, 이분 탐색 문제다.
    길이 찾은거랑 연산에 맞지 않아 5번 틀렸다.
*/

namespace BaekJoon.etc
{
    internal class etc_1654
    {

        static void Main1654(string[] args)
        {

#if MY_DEBUG

            long MAX = 1_000_000_000_000_000_001;
            Before();
            void Before()
            {

                long e = 1_000_000_000;
                for (int len = 3; len <= 60; len++)
                {

                    long mul;
                    for (mul = 0; mul < e;)
                    {

                        if (Chk(len, mul + 1) > MAX) break;
                        mul++;
                    }
                    e = mul;
                    Console.Write($"{e}, ");
                }

                long Chk(int _len, long _mul)
                {

                    long ret = 0;
                    long cur = 1;
                    for (int i = 0; i < _len; i++, cur *= _mul)
                    {

                        ret += cur;
                    }

                    return ret;
                }
            }
#endif

            long INF = 1_000_000_000_000_000_001;
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            int[] len = { 0, 0, 999_999_999, 999_999, 31_622, 3_980, 999, 372, 177, 99, 62, 43, 31, 24, 19, 15, 13, 11, 9, 8, 7, 7, 6, 6, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

            for (int i = 1; i <= t; i++)
            {

                long cur = ReadLong();

                sw.Write($"Case #{i}: {GetRet(cur)}\n");
            }

            long GetRet(long _val)
            {

                long ret = _val - 1;

                for (int i = 2; i < len.Length; i++)
                {

                    ret = Math.Min(ret, BinarySearch(i));
                }

                return ret;

                long BinarySearch(int _len)
                {

                    int l = 2;
                    int r = len[_len];

                    while (l <= r)
                    {

                        int mid = (l + r) >> 1;

                        long chk = Chk(mid);
                        if (_val < chk) r = mid - 1;
                        else if (_val > chk) l = mid + 1;
                        else return mid;
                    }

                    return INF;

                    long Chk(long _mul)
                    {

                        long ret = 0;
                        long cur = 1;
                        for (int i = 0; i <= _len; i++)
                        {

                            ret += cur;
                            cur *= _mul;
                        }

                        return ret;
                    }
                }


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
