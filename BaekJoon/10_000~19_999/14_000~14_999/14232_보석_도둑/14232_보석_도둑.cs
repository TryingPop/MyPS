using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 27
이름 : 배성훈
내용 : 보석 도둑
    문제번호 : 14232번

    소인수 분해하는 문제
*/

namespace BaekJoon.etc
{
    internal class etc_0107
    {

        static void Main107(string[] args)
        {

            long chk = long.Parse(Console.ReadLine());

            int cnt = 0;
            long calc = chk;

            long[] save = new long[41];

            for (long i = 2; i < chk; i++)
            {

                if (i * i > calc) break;

                while (calc % i == 0)
                {

                    save[cnt++] = i;
                    calc = calc / i;
                }
            }

            if (calc != 1)
            {

                save[cnt++] = calc;
            }

            using (StreamWriter sw = new StreamWriter(Console.OpenStandardOutput()))
            {

                sw.Write(cnt);
                sw.Write('\n');

                for (int i = 0; i < cnt; i++)
                {

                    sw.Write(save[i]);
                    sw.Write(' ');
                }
            }
        }
    }
}
