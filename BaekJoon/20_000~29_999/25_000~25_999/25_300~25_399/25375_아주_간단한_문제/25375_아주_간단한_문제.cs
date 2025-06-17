using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 17
이름 : 배성훈
내용 : 아주 간단한 문제
    문제번호 : 25375번

    수학, 정수론 문제다
    a, b가 주어지면 gcd(x, y) = a이고 x + y = b인
    x, y가 존재하는지 확인하는 문제다

    gcd(x, y) = a 와 x + y = b를 보면

    먼저 gcd(x, y) = a에서 적당한 자연수 c, d가 존재해
    ac = x & ad = y라하자 그러면 c, d는 서로소이다

    이를 x + y = b에 적용하면
    a(c + d) = b이고 a가 b를 나눠야한다

    그리고 나눴을 때 몫이 2 이상이어야 함을 알 수 있다
    2 미만인 경우 c 또는 d가 0이되어 gcd(x, y) = a에 모순이고
    이상이면 상관없다

    이를 코드로 구현하니 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0266
    {

        static void Main266(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));
            int test = (int)ReadLong(sr);

            while(test-- > 0)
            {

                long a = ReadLong(sr);
                long b = ReadLong(sr);

                if (b % a != 0 || (b / a) < 2)
                {

                    sw.WriteLine(0);
                    continue;
                }

                sw.WriteLine(1);
            }

            sw.Close();
            sr.Close();
        }

        static long ReadLong(StreamReader _sr)
        {

            int c;
            long ret = 0;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
