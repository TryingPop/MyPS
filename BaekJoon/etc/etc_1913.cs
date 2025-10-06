using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 9. 23
이름 : 배성훈
내용 : 미로 탈출
    문제번호 : 31834번

    애드 혹 문제다.
    아이디어는 다음과 같다.
    최단 경로의 거리는 n이다.
    그래서 n의 경로로 가는데 텔레포트 최소화하는 경우의 수를 찾았다.

    먼저 s가 1 또는 n에서 시작하는 경우
    e가 양끝점이면 그냥 e방향으로 간다.
    아니라면 e 바로 앞인 e - 1 또는 e + 1까지 이동한다.
    그리고 텔레포트 해서 n으로 이동한 뒤 e로 가면 1번의 텔레포트를 써야 최단 경로가 나온다.

    이제 이외 경우를 보면, e가 s 와 거리가 1 인 경우를 본다.
    그러면 s에서 e와 반대방향 끝으로 간 뒤 1이나 n으로 이동해 e로 가면 최단 경로가 나온다.
    이외 경우는 e방향으로 바로 앞까지 간 뒤 1과 n중 e로 갈때 시작지점을 들리는 곳으로 간다.
    시작지점 바로 앞에서 멈추고 남은 끝점으로 텔레포트 한다
    그리고 e로 달려가면 최단 경로가 된다.
*/

namespace BaekJoon.etc
{
    internal class etc_1913
    {

        static void Main1913(string[] args)
        {

            string T = "2\n";
            string O = "1\n";
            string Z = "0\n";
            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            int t = ReadInt();

            while (t-- > 0)
            {

                int n = ReadInt();
                int s = ReadInt();
                int e = ReadInt();

                if (s == 1)
                {

                    if (e == n) sw.Write(Z);
                    else sw.Write(O);
                }
                else if (s == n)
                {

                    if (e == 1) sw.Write(Z);
                    else sw.Write(O);
                }
                else if (Math.Abs(s - e) == 1) sw.Write(O);
                else sw.Write(T);
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
