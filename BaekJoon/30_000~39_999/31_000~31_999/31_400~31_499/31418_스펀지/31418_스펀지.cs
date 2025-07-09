using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 20
이름 : 배성훈
내용 : 스펀지
    문제번호 : 31418번

    오버 플로우 문제로 한 번 틀렸다
    입력 범위가 최대 높이와 너비는 100만이다 -> 즉 너비 x 높이의 경우 1조까지 간다
    이를 9억 까지 가는 ret에 이 수를 곱해주니 오버플로우는 당연했다;
    이후 이상없이 통과했다

    주된 아이디어는 다음과 같다
    찾는 시간에 있을 수 있는 공간을 구했다 8방향 이동과 대기가 가능하기에 이동 범위는 사각형 범위가 되고,
    그리고 각 바이러스의 위치를 곱하면 찾는 결과가 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0071
    {

        static void Main71(string[] args)
        {

            long DIV = 998_244_353;
            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int MAX_WIDTH = ReadInt(sr);
            int MAX_HEIGHT = ReadInt(sr);

            int virus = ReadInt(sr);
            int TIME = ReadInt(sr);

            long ret = 1;

            for (int i = 0; i < virus; i++)
            {

                // w 위치
                int w = ReadInt(sr);

                int minW = w - TIME;
                minW = minW < 1 ? 1 : minW;

                int maxW = w + TIME;
                maxW = maxW <= MAX_WIDTH ? maxW : MAX_WIDTH;

                // h 위치
                int h = ReadInt(sr);

                int minH = h - TIME;
                minH = minH < 1 ? 1 : minH;

                int maxH = h + TIME;
                maxH = maxH <= MAX_HEIGHT ? maxH : MAX_HEIGHT;

                // 있을 수 있는 사각형 범위 계산
                long W = maxW - minW + 1;
                long H = maxH - minH + 1;

                // ret *= W * H;
                // ret %= DIV;

                ret *= W;
                ret %= DIV;
                ret *= H;
                ret %= DIV;
            }

            sr.Close();

            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
