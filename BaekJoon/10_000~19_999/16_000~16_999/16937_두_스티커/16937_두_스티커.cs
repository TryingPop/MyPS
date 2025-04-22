using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 두 스티커
    문제번호 : 16937번

    구현, 브루트포스, 많은 조건 분기, 기하학 문제다
    정답은 그리디 하게 접근하면 한 변의 끝에 이어 붙여
    90도 회전시키면서 서로 겹치지 않는 경우가 있는지 확인하면 쉽게 찾아진다
    그런데 조건 구현하면서 실수에 의해 4번 틀렸다
*/

namespace BaekJoon.etc
{
    internal class etc_0340
    {

        static void Main340(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int row = ReadInt();
            int col = ReadInt();
            int n = ReadInt();

            (int r, int c)[] sticker = new (int r, int c)[n];
            for (int i = 0; i < n; i++)
            {

                sticker[i] = (ReadInt(), ReadInt());
            }

            sr.Close();

            int ret = 0;
            for (int i = 0; i < n; i++)
            {

                int area = sticker[i].r * sticker[i].c;

                
                int max = 0;
                for (int j = i + 1; j < n; j++)
                {

                    int curArea = sticker[j].r * sticker[j].c;
                    if (ChkBound(sticker[i].r, sticker[i].c, sticker[j].r, sticker[j].c))
                    {

                        max = max < area + curArea ? area + curArea : max;
                    }
                }

                if (ret < max) ret = max;
            }

            Console.WriteLine(ret);

            bool ChkBound(int _r1, int _c1, int _r2, int _c2)
            {

                if (_r1 + _r2 <= row && _c1 <= col && _c2 <= col) return true;
                if (_r1 + _c2 <= row && _c1 <= col && _r2 <= col) return true;
                if (_c1 + _r2 <= row && _r1 <= col && _c2 <= col) return true;
                if (_c1 + _c2 <= row && _r1 <= col && _r2 <= col) return true;

                if (_c1 + _c2 <= col && _r1 <= row && _r2 <= row) return true;
                if (_c1 + _r2 <= col && _r1 <= row && _c2 <= row) return true;
                if (_r1 + _c2 <= col && _c1 <= row && _r2 <= row) return true;
                if (_r1 + _r2 <= col && _c1 <= row && _c2 <= row) return true;
                return false;
            }

            int ReadInt()
            {

                int c, ret = 0;
                while ((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
