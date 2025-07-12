using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 5. 2
이름 : 배성훈
내용 : 꺾이지 않는 마음 1
    문제번호 : 26142번

    dp, 그리디 문제다.
    https://blog.naver.com/jihwanmoon0319/223686429936
    해당 사이트를 참고해서 풀었다.

    아이디어는 다음과 같다.
    우선 최대한 다른 용들을 잡는게 좋다.
    같은 용을 여러 번 잡는 경우를 생각해보면, 가장 마지막에 잡는날이 k일이라하면
    여러 번 잡는 경우의 합은 h + kd이기 때문이다.
    이는 k일날 용을 1번 잡는 것과 같다.

    그래서 가장 효율이 낮은 용을 1마리씩 제외해간다.
*/

namespace BaekJoon.etc
{
    internal class etc_1606
    {

        static void Main1606(string[] args)
        {

            int n;
            (long d, long h, bool pop)[] dragon;

            Input();

            GetRet();

            void GetRet()
            {

                long[] ret = new long[n];
                int len = 0;
                Array.Sort(dragon, (x, y) => x.d.CompareTo(y.d));

                for (int i = 0; i < n; i++)
                {

                    ret[len++] = Calc();
                }

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

                while (len-- > 0)
                {

                    sw.Write($"{ret[len]}\n");
                }

                long Calc()
                {

                    long max = 1_234_567_890_123_456_789;

                    long chk = 0;
                    int pop = 0;
                    long ret = 0;
                    for (int i = 0, day = 0; i < n; i++)
                    {

                        if (dragon[i].pop) continue;

                        chk -= dragon[i].d;
                        // 빼도 되는 것을 찾는다.
                        if (chk + dragon[i].d * day + dragon[i].h < max)
                        {

                            max = chk + dragon[i].d * day + dragon[i].h;
                            pop = i;
                        }

                        ret += dragon[i].h + dragon[i].d * day++;
                    }

                    dragon[pop].pop = true;

                    return ret;
                }
            }

            void Input()
            {

                using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);

                n = ReadInt();

                dragon = new (long d, long h, bool pop)[n];
                for (int i = 0; i < n; i++)
                {

                    dragon[i] = (ReadInt(), ReadLong(), false);
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
