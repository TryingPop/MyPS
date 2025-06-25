using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 13
이름 : 배성훈
내용 : 블로그 2
    문제번호 : 20365번

    색깔 하나를 정해서 구간이 어떻게 되는지 찾는 문제다
    여기서 구간은 BBBB 같은 색으로 이어진 곳 중 가장 긴 것을 말한다

    그리디 방법으로 접근했다 
    몇 가지 예시로 확인해보자
        BRB
            B, R, B로 나뉜다, 총 구간이 3개다
            처음에 구간 개수가 많은 B(2개)로 칠하고, 중앙에 R(1)을 넣는다
            총 2회 (3 / 2 + 1)

        BRRB
            B, RR, B로 나뉜다, 총 구간이 3개다
            처음에 구간 개수가 많은 B(2)로 칠하고, 중앙에 RR(1)을 칠한다
            총 2회 (3 / 2 + 1)

        BRRBBBRRRRBRBRB
            B, RR, BBB, RRRR, B, R, B, R, B로 나뉜다 총 구간이 9개이다
            처음에 구간 개수가 많은 B(5)로 칠하고, 중앙에 R(4)들을 칠한다
            총 5회 ( 9 / 2 + 1 )

        구간이 많은걸 먼저 칠하고 이후에 부분 구간들을 칠하는게 최소 경우지 않을까 추론했다
        또한 경우를 보면 구간을 세고, 전체 구간의 값을 반으로 나눈 뒤(소수점 버림) + 1을 하면 추론하는 값과 같아짐을 알게되었다

    이를 코드로 표현해서 제출하니 정답이었다
*/

namespace BaekJoon.etc
{
    internal class etc_0023
    {

        static void Main23(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int ret = 0;
            int cur = -1;
            for (int i = 0; i < len; i++)
            {

                int chk = sr.Read();

                // 전체 구간 찾기
                if (cur != chk)
                {

                    ret++;
                    cur = chk;
                }
            }

            sr.Close();

            // 구간 적은쪽의 개수로 만든다
            ret /= 2;
            // 그리고 많은쪽 1추가
            ret++;
            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret *= 10;
                ret += c - '0';
            }

            return ret;
        }
    }
}
