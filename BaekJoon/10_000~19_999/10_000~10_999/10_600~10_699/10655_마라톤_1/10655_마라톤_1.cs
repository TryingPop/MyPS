using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 16
이름 : 배성훈
내용 : 마라톤 1
    문제번호 : 10655번

    구현, 브루트 포스, 기하학 문제다
    이전 거리를 반복해서 사용하므로 dp? 이용해 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0254
    {

        static void Main254(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] nojumpDis = new int[n - 1];
            int[] oneJumpDis = new int[n - 2];

            int bX, bY, bbX, bbY;
            bbX = ReadInt(sr);
            bbY = ReadInt(sr);

            bX = ReadInt(sr);
            bY = ReadInt(sr);

            nojumpDis[0] = GetTaxiDis(bX, bY, bbX, bbY);

            int total = nojumpDis[0];
            for (int i = 1; i < n - 1; i++)
            {

                int curX = ReadInt(sr);
                int curY = ReadInt(sr);

                nojumpDis[i] = GetTaxiDis(curX, curY, bX, bY);
                oneJumpDis[i - 1] = GetTaxiDis(curX, curY, bbX, bbY);

                bbX = bX;
                bbY = bY;

                bX = curX;
                bY = curY;

                total += nojumpDis[i];
            }

            sr.Close();

            // A, B, C 가 인접한 지점들이라할 때,
            // B를 건너뛴 거리는
            // 전체 거리 - (A -> B 거리) - (B -> C거리) + (A -> C 거리)
            int ret = total;
            for (int i = 0; i < n - 2; i++)
            {

                int calc = total;
                calc -= nojumpDis[i] + nojumpDis[i + 1];
                calc += oneJumpDis[i];

                if (calc < ret) ret = calc;
            }

            Console.WriteLine(ret);
        }

        static int GetTaxiDis(int _x, int _y, int _beforeX, int _beforeY)
        {

            int ret;

            int diffX = _x - _beforeX;
            diffX = diffX < 0 ? -diffX : diffX;
            int diffY = _y - _beforeY;
            diffY = diffY < 0 ? -diffY : diffY;
            ret = diffX + diffY;
            return ret;
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            bool plus = true;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
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
