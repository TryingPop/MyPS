using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 컨테이너
    문제번호 : 3693번

    10^12까지 입력된다
    실제 컨테이너를 5층까지 쌓으면 값 조사는 10^11 * 2(200억)이 된다

    그리고 가능한 사각형 너비는
    루트를 씌우면 되므로, 10^5 * 5 정도이다
    100회의 테스트 케이스가 존재하므로 5000만이다

    그래서 해당 방법으로 풀었다
    나눗셈 연산이 많아 약간 불안하긴 했으나, 이상없이 88ms로 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0082
    {

        static void Main82(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int test = (int)ReadLong(sr);

            while(test-- > 0)
            {

                long chk = ReadLong(sr);
                long calc = chk / 5;
                if (chk % 5 != 0) calc++;

                long ret3 = long.MaxValue;
                long ret1 = 0;
                long ret2 = 0;
                for (long i = 1; i <= chk; i++)
                {

                    if (i * i > calc) break;

                    long other1 = i;
                    long other2 = calc / i;
                    if (calc % i != 0) other2++;

                    // 먼저 other1을 가로 개수
                    long w = GetWidth(other1);
                    long h = GetHeight(other2);
                    long area = w * h;

                    if (area < ret3)
                    {

                        ret3 = area;

                        if (w > h)
                        {

                            ret1 = w;
                            ret2 = h;
                        }
                        else
                        {

                            ret1 = h;
                            ret2 = w;
                        }
                    }

                    // other1을 세로
                    w = GetWidth(other2);
                    h = GetHeight(other1);
                    area = w * h;

                    if (area < ret3)
                    {

                        ret3 = area;

                        if (w > h)
                        {

                            ret1 = w;
                            ret2 = h;
                        }
                        else
                        {

                            ret1 = h;
                            ret2 = w;
                        }
                    }
                }

                // 최소값 출력
                sw.Write($"{ret1} X {ret2} = {ret3}\n");
            }

            sr.Close();
            sw.Close();
        }

        static long GetWidth(long _n)
        {

            long ret = 48 + 44 * (_n - 1);

            return ret;
        }

        static long GetHeight(long _n)
        {

            long ret = 12 + 10 * (_n - 1);

            return ret;
        }

        static long ReadLong(StreamReader _sr)
        {

            long ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
