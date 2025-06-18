using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 16
이름 : 배성훈
내용 : 개구리 매칭
    문제번호 : 30012번

    브루트포스로 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0048
    {

        static void Main48(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int s = ReadInt(sr);            // 주호 개구리 위치
            int n = ReadInt(sr);            // 개구리 수

            int[] pos = new int[n + 1];

            for (int i = 1; i <= n; i++)
            {

                pos[i] = ReadInt(sr);
            }

            int k = ReadInt(sr);            // 최대 점프 거리
            int l = ReadInt(sr);            // 이동당 라이프

            sr.Close();

            // 브루트포스
            int min = 1_000_000_000;
            int minIdx = 0;

            for (int i = 1; i <= n; i++)
            {

                // 두 좌표 차이
                int dis = pos[i] - s;
                dis = dis < 0 ? -dis : dis;

                // 각자 최대 점프 했을 때 최대 점프 거리랑 비교
                int calc = dis - 2 * k;
                if (calc < 0)
                {

                    // 작은 경우
                    // 차이가 최소 코스트이다
                    calc = -calc;

                    if (min > calc)
                    {

                        min = calc;
                        minIdx = i;
                    }
                }
                else
                {

                    // 최대 점프하고 걸어야한다
                    calc *= l;

                    if (min > calc)
                    {

                        min = calc;
                        minIdx = i;
                    }
                }
            }

            // 출력!
            Console.Write($"{min} {minIdx}");
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0;
            int c;

            while((c = _sr.Read()) != '\n' && c != ' ' && c != -1)
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
