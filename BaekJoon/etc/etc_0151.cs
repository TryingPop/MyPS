using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 3
이름 : 배성훈
내용 : 계산기
    문제번호 : 2200번

    구현 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0151
    {

        static void Main151(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int n = ReadInt(sr);

            int[] powTen = new int[11];
            powTen[1] = 1;
            for (int i = 1; i < 10; i++)
            {

                powTen[i + 1] = 10 * powTen[i];
            }

            // 처음에 x만 넣으면 된다
            // 그런데 해당연산에서는 +, 1, *, x를 넣어 3개 더 많다
            // 그리고 마지막에 * x 가 아닌 =를 넣어야한다
            // 총 4개가 많아 -4이다
            int ret = -4;
            for (int i = 0; i <= n; i++)
            {

                int chk = ReadInt(sr);

                if (chk == 0)
                {

                    ret += 2;
                    continue;
                }

                ret++;

                for (int j = 10; j >= 1; j--)
                {

                    if (powTen[j] <= chk)
                    {

                        ret += j;
                        break;
                    }
                }

                ret += 2;
            }
            sr.Close();

            Console.WriteLine(ret);
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
