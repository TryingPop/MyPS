using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 29
이름 : 배성훈
내용 : Boring Numbers
    문제번호 : 23926번

    수학, 구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1847
    {

        static void Main1847(string[] args)
        {

            // 23926번 Boring Numbers - 
            long[] fPow = new long[19];
            long[] tPow = new long[19];
            long[] fSum = new long[19];
            fPow[0] = 1;
            tPow[0] = 1;
            for (int i = 1; i < 19; i++)
            {

                fPow[i] = fPow[i - 1] * 5;
                tPow[i] = tPow[i - 1] * 10;

                fSum[i] = fSum[i - 1] + fPow[i];
            }

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            for (int i = 1; i <= t; i++)
            {

                long l = Cnt(ReadLong() - 1);
                long r = Cnt(ReadLong());

                sw.Write($"Case #{i}: {r - l}\n");
            }

            long Cnt(long _a)
            {

                if (_a == 0) return 0;
                int digit;
                for (digit = 18; digit >= 0; digit--)
                {

                    if (_a >= tPow[digit]) break;
                }

                long ret = fSum[digit];

                bool isEven = false;
                for (; digit > 0; digit--)
                {

                    long val = _a / tPow[digit];
                    long half = (val + (isEven ? 1 : 0)) >> 1;

                    ret += half * fPow[digit];
                    if ((isEven && (val & 1L) == 1L) 
                        || (!isEven && (val & 1L) == 0L)) return ret;
                    
                    _a %= tPow[digit];
                    isEven = !isEven;
                }

                if (isEven) _a = (_a / 2) + 1;
                else _a = (_a + 1) / 2;
                ret += _a;

                return ret;
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
