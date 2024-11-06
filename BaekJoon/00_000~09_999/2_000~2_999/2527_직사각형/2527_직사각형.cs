using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 25
이름 : 배성훈
내용 : 직사각형
    문제번호 : 2527번

    조건만 잘 나누면 이상없다
*/

namespace BaekJoon.etc
{
    internal class etc_0093
    {

        static void Main93(string[] args)
        {

            StreamReader sr = new(Console.OpenStandardInput());
            StreamWriter sw = new(Console.OpenStandardOutput());

            while (true)
            {

                int leftX = ReadInt(sr);

                // 탈출 용도!
                if (leftX == -1) break;

                int bottomY = ReadInt(sr);
                int rightX = ReadInt(sr);
                int topY = ReadInt(sr);

                int chkLeftX = ReadInt(sr);
                int chkBottomY = ReadInt(sr);
                int chkRightX = ReadInt(sr);
                int chkTopY = ReadInt(sr);

                int meetX;
                if (chkRightX < leftX || rightX < chkLeftX) meetX = 0;
                else if (chkRightX == leftX || chkLeftX == rightX) meetX = 1;
                else meetX = 2;

                int meetY;
                if (topY < chkBottomY || chkTopY < bottomY) meetY = 0;
                else if (topY == chkBottomY || chkTopY == bottomY) meetY = 1;
                else meetY = 2;

                char ret;
                if (meetX == 0 || meetY == 0) ret = 'd';
                else if (meetX == 2 && meetY == 2) ret = 'a';
                else if (meetX == 2 || meetY == 2) ret = 'b';
                else ret = 'c';

                sw.Write(ret);
                sw.Write('\n');
            }

            sr.Close();
            sw.Close();
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while ((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            if (ret == 0) return -1;
            return ret;
        }
    }
}
