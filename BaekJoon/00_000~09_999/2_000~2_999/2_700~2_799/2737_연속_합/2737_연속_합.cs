using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 1
이름 : 배성훈
내용 : 연속 합
    문제번호 : 2737번

    수학 문제다.
    s에서 e까지 연속된 수들의 합은 가우스 방법을 이용하면 (s + e) * (e - s + 1) / 2가 된다.
    그래서 e - s + 1을 찾고 유효한지 확인해 풀었다.
    그런데 s, e가 자연수이므로 e - s + 1 < s + e임은 자명하다.
    이에 직접 나누면서 찾아주면 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1602
    {

        static void Main1602(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            while (t-- > 0)
            {

                int n = ReadInt();

                sw.Write($"{GetRet(n)}\n");
            }

            int GetRet(long _num)
            {

                _num <<= 1;

                int ret = 0;

                for (long len = 2; len * len <= _num; len++)
                {

                    if (_num % len != 0) continue;

                    long s = _num / len - len + 1;
                    if (s <= 0 || s % 2 == 1L) continue;
                    ret++;
                }

                return ret;
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
