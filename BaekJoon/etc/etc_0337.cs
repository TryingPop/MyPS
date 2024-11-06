using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 화살표 연산자
    문제번호 : 16114번

    구현, 많은 조건 분기 문제다
    조건을 제대로 분석안해서 2번 틀렸다
    -가 1개인 경우다!

    그리고 시행횟수는 조건을 나눠 구할 수 있으나
    수 범위가 적어 구현으로 해결했다

    ERROR경우나, INFINITE 경우를 제외하고는
    시행횟수는 n <= 0 이면 0회
    n > 0 이면, m / 2로 나눠 소수점은 올림하고 1을 뺀 값이 시행횟수가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0337
    {

        static void Main337(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());

            int n = ReadInt();
            int m = ReadInt();

            if (m != 1 && m % 2 == 1) Console.WriteLine("ERROR");
            else if (m == 1 && n < 0) Console.WriteLine("INFINITE");
            else if (n > 0 && m == 0) Console.WriteLine("INFINITE");
            else
            {

                int minus = m / 2;
                int ret = 0;
                if (m == 1) n = -n;
                while(true)
                {

                    n -= minus;

                    if (n <= 0) break;
                    ret++;
                }

                Console.WriteLine(ret);
            }

            int ReadInt()
            {

                int c, ret = 0;
                bool plus = true;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    else if (c == '-')
                    {

                        plus = false;
                        continue;
                    }
                    ret = ret * 10 + c - '0';
                }

                return plus ? ret : -ret;
            }
        }
    }
}
