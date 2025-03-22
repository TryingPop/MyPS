using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 22
이름 : 배성훈
내용 : 정사각형
    문제번호 : 1485번

    정렬, 기하학 문제다.
    내적을 이용해 확인했다.
*/

namespace BaekJoon.etc
{
    internal class etc_1449
    {

        static void Main1449(string[] args)
        {

            string Y = "1\n";
            string N = "0\n";

            int t;
            (int x, int y)[] rect;

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);

            t = ReadInt();
            rect = new (int x, int y)[4];
            while (t-- > 0)
            {

                rect[0] = (ReadInt(), ReadInt());
                rect[1] = (ReadInt(), ReadInt());
                rect[2] = (ReadInt(), ReadInt());
                rect[3] = (ReadInt(), ReadInt());

                Array.Sort(rect, (x, y) =>
                {

                    int ret = x.x.CompareTo(y.x);
                    if (ret == 0) ret = x.y.CompareTo(y.y);
                    return ret;
                });


                bool ret = Chk(0, 1, 2) && Chk(3, 1, 2);

                sw.Write(ret ? Y : N);

                bool Chk(int _cen, int _idx1, int _idx2)
                {

                    int aH = rect[_idx1].x - rect[_cen].x;
                    int bH = rect[_idx1].y - rect[_cen].y;

                    int aW = rect[_idx2].x - rect[_cen].x;
                    int bW = rect[_idx2].y - rect[_cen].y;

                    return (aH == bW && bH == -aW)
                        || (aH == -bW && bH == aW);
                }
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
                    if (c == '\n' || c == ' ') return true;
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
