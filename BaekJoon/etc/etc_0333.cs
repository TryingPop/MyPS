using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 23
이름 : 배성훈
내용 : 와이파이
    문제번호 : 23826번

    구현, 브루트포스 문제다
    이중 포문을 이용해 탐색했다
*/

namespace BaekJoon.etc
{
    internal class etc_0333
    {

        static void Main333(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            (int x, int y, int pow)[] pos = new (int x, int y, int pow)[n + 1];

            for (int i = 0; i <= n; i++)
            {

                pos[i] = (ReadInt(sr), ReadInt(sr), ReadInt(sr));
            }

            sr.Close();

            int ret = 0;

            for (int i = 1; i <= n; i++)
            {

                int mainPow = GetPow(pos[0], pos[i]);
                for (int j = 1; j <= n; j++)
                {

                    mainPow -= GetPow(pos[j], pos[i]);
                    if (mainPow > 0) continue;
                    mainPow = 0;
                    break;
                }

                ret = ret < mainPow ? mainPow : ret;
            }

            if (ret == 0) Console.WriteLine("IMPOSSIBLE");
            else Console.WriteLine(ret);

            int GetPow((int x, int y, int pow) _wifiPos, (int x, int y, int pow) _usePos)
            {

                int diffX = _wifiPos.x - _usePos.x;
                diffX = diffX < 0 ? -diffX : diffX;

                int diffY = _wifiPos.y - _usePos.y;
                diffY = diffY < 0 ? -diffY : diffY;

                int ret = _wifiPos.pow - diffX - diffY;
                ret = ret < 0 ? 0 : ret;

                return ret;
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;
            while((c = _sr.Read()) != -1 && c != ' ' && c != '\n')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
