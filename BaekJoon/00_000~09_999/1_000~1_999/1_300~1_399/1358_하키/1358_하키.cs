using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 21
이름 : 배성훈
내용 : 하키
    문제번호 : 1358번

    기하학 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1438
    {

        static void Main1438(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int w = ReadInt();
            int h = ReadInt();
            int x = ReadInt();
            int y = ReadInt();
            int p = ReadInt();

            int r = h / 2;
            int cx1 = x;
            int cy1 = y + r;
            int cx2 = x + w;
            int cy2 = y + r;

            int ret = 0;
            for (int i = 0; i < p; i++)
            {

                int curX = ReadInt();
                int curY = ReadInt();

                if (ChkInner(curX, curY)) ret++;
            }

            Console.Write(ret);

            bool ChkInner(int _x, int _y)
            {

                // 사각형 테스트
                if (x <= _x && _x <= x + w && y <= _y && _y <= y + h) return true;

                // 왼쪽 원 테스트
                int chkX = _x - cx1;
                int chkY = _y - cy1;

                if (chkX * chkX + chkY * chkY <= r * r) return true;

                chkX = _x - cx2;
                chkY = _y - cy2;

                if (chkX * chkX + chkY * chkY <= r * r) return true;

                return false;
            }

            int ReadInt()
            {

                int ret = 0;

                while (TryReadInt()) { }
                return ret;

                bool TryReadInt()
                {

                    int c = sr.Read();
                    if (c == '\r') c = sr.Read();
                    if (c == ' ' || c == '\n') return true;

                    bool positive = c != '-';
                    ret = positive ? c - '0' : 0;

                    while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                    {

                        if (c == '\r') continue;
                        ret = ret * 10 + c - '0';
                    }

                    ret = positive ? ret : -ret;
                    return false;
                }
            }
        }
    }
}
