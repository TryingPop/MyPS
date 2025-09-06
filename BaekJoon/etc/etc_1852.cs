using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 8. 31
이름 : 배성훈
내용 : Knight Cruising
    문제번호 : 32208번

    홀짝성 문제다.
    1 + 2 + 3 = 6, -1 + 2 + 3 = 4, 1 -2 + 3 = 2, 1 + 2 - 3 = 0
    ...
    나이트의 이동경로들을 보면 항상 합이 짝수임을 알 수 있다.
    그래서 홀수인 경우는 이동못한다.

    짝수인 경우는 이동할 수 있지 않을까 추론했다.
    3개 중 합이 짝수이므로 가능한 경우는 홀수가 0개이거나 2개뿐인 경우다.

    그래서 홀수 0개인 0 0 2의 경우를 보자.
    +2 +3 +1 -> -2, -3, +1 로 이동하면 된다.

    0 1 1의 경우를 보자.
    +1 +2 +3 -> +1 -2 -3 : 2 0 0
    -2 +1 +3 -> +2 +3 -1 -> -2 -3 -1로 이동하면 0 1 1 이된다.
    이렇게 홀수 2개를 만들 수 있다.

    그리고 순서를 적절히 섞으면 0 2 0, 2 0 0이나 1 1 0, 1 0 1를 만들 수 있고
    이들 합으로 합이 짝수인 경우를 모두 표현할 수 있다.(기저!)
    그래서 짝수합으로는 모두 이동이 가능하다.
*/

namespace BaekJoon.etc
{
    internal class etc_1852
    {

        static void Main1852(string[] args)
        {

            string Y = "YES\n";
            string N = "NO\n";

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int n = ReadInt();
            while (n-- > 0)
            {

                int ret = ReadInt() % 2;
                ret += ReadInt() % 2;
                ret += ReadInt() % 2;

                ret %= 2;
                if (ret == 0) sw.Write(Y);
                else sw.Write(N);
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
                    ret = c == '-' ? 0 : c - '0';

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
