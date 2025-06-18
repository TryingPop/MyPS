using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 온도 맞추기
    문제번호 : 30703번

    수학 문제다
*/

namespace BaekJoon.etc
{
    internal class etc_0146
    {

        static void Main146(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            int len = ReadInt(sr);

            int[] inits = new int[len];
            for (int i = 0; i < len; i++)
            {

                inits[i] = ReadInt(sr);
            }

            int[] targets = new int[len];
            for (int i = 0; i < len; i++)
            {

                targets[i] = ReadInt(sr);
            }

            int[] change = new int[len];
            for (int i = 0; i < len; i++)
            {

                change[i] = ReadInt(sr);
            }
            sr.Close();

            int ret = -1;                           // 결과
            bool first = true;                      // 처음 여부
            bool isOdd = false;                     // 홀수턴?
            for (int i = 0; i < len; i++)
            {

                int calc = inits[i] - targets[i];
                calc = calc < 0 ? -calc : calc;

                int chk = calc / change[i];
                if (calc % change[i] != 0)
                {

                    // 나눠떨어지지 않으면 해당 값으로 못맞춘다
                    ret = -1;
                    break;
                }
                
                if (first)
                {

                    // 처음에만 홀짝 여부 설정!
                    ret = chk;
                    first = false;
                    isOdd = (chk & 1) == 1;
                }
                else
                {

                    // 처음 홀짝과 다른 경우!
                    if (isOdd != ((chk & 1) == 1))
                    {

                        // 못만든다!
                        ret = -1;
                        break;
                    }

                    // 큰 값으로 갱신!
                    if (ret < chk) ret = chk;
                }
            }

            sr.Close();
            Console.WriteLine(ret);
        }

        static int ReadInt(StreamReader _sr)
        {

            int c, ret = 0;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;
                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
