using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 3. 30
이름 : 배성훈
내용 : 알파벳 다이아몬드
    문제번호 : 1262번

    구현 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1496
    {

        static void Main1496(string[] args)
        {

            int n, r1, c1, r2, c2;
            int len;

            Input();

            GetRet();

            void GetRet()
            {

                using StreamWriter sw = new(Console.OpenStandardOutput(), bufferSize: 65536);
                for (int r = r1; r <= r2; r++)
                {

                    for (int c = c1; c <= c2; c++)
                    {

                        sw.Write(GetVal(r, c));
                    }

                    sw.Write('\n');
                }
            }

            void Input()
            {

                string[] temp = Console.ReadLine().Split();
                n = int.Parse(temp[0]);
                r1 = int.Parse(temp[1]);
                c1 = int.Parse(temp[2]);
                r2 = int.Parse(temp[3]);
                c2 = int.Parse(temp[4]);

                len = n * 2 - 1;
            }

            char GetVal(int _r, int _c)
            {

                // 첫 마름모 범위 안에 들게 한다.
                if (_r >= len) _r %= len;
                if (_c >= len) _c %= len;

                // 마름모 중 왼쪽 위 삼각혀엥 들게 한다.
                if (_r >= n) _r = len - 1 - _r;
                if (_c >= n) _c = len - 1 - _c;

                // 택시거리로 값 찾기
                int ret = n - 1 - _r + n - 1 - _c;
                if (ret >= n) return '.';
                else return (char)(ret % 26 + 'a');
            }
        }
    }
}
