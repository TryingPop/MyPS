using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 10. 1
이름 : 배성훈
내용 : Schronisko
    문제번호 : 8760번

    간단한 수학 문제다.
    2 x 1의 타일에 1명씩 거주할 수 있다.
    전체 크기가 주어졌을 때 거주할 수 있는 최대 인원을 찾아야 한다.

    우선 2가지 경우로 나눠서 풀 수 있다.
    가로 or 세로 중 적어도 1개가 짝수인 경우와 모두 홀수이 경우로 나뉜다.
    가로 or 세로 중 적어도 1개가 짝수인 경우를 보자.
    편의상 가로를 짝수라 하자. 만약 짝수가 홀수이고 세로가 짝수라면 90도 회전하면 된다.
    그러면 가로로 인접하게 이어붙이면 모두 채울 수 있고 이때 경우의 수는 w x h / 2이다.

    이제 가로, 세로 모두 홀수인 경우를 보자.
    우선 가로로 2 x 1로 채운다.
    그러면 결국 가로 1줄만 남고 모두 채울 수 있다.
    이제 세로로 채우면 1칸만 제외하고 모두 채울 수 있다.
    이는 2로 나눈 나머지가 1이므로 무조건 1칸 남을 수 밖에 없고
    1칸을 제외한 모든 경우를 채우는 것이므로 최댓값이된다.

    그래서 각 경우의 경우를 구하면 w x h // 2가 됨을 알 수 있다.
    여기서 //는 나눗셈에서 몫을 찾는 연산이다.
*/

namespace BaekJoon.etc
{
    internal class etc_1923
    {

        static void Main1923(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput());
            using StreamWriter sw = new(Console.OpenStandardOutput());

            int z = ReadInt();

            for (int i = 0; i < z; i++)
            {

                int w = ReadInt();
                int h = ReadInt();

                sw.WriteLine((w * h) >> 1);
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
