using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 1. 8
이름 : 배성훈
내용 : Watching Mooloo
    문제번호 : 27851번

    그리디 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1264
    {

        static void Main1264(string[] args)
        {

            StreamReader sr;
            long n, k;
            

            Solve();
            void Solve()
            {

                sr = new(Console.OpenStandardInput(), bufferSize: 65536);
                n = ReadLong();
                k = ReadLong();

                long prev = ReadLong();
                long ret = 1 + k;

                for (int i = 1; i < n; i++)
                {

                    long cur = ReadLong();
                    if (cur - prev <= k) ret += cur - prev;
                    else ret += 1 + k;

                    prev = cur;
                }

                Console.Write(ret);
                sr.Close();
            }

            long ReadLong()
            {

                long ret = 0;

                while (TryReadLong()) { }
                return ret;

                bool TryReadLong()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == '\n' || c == ' ') return true;

                    ret = c - '0';

                    while((c = sr.Read()) != -1 && c != '\n' && c != ' ')
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
