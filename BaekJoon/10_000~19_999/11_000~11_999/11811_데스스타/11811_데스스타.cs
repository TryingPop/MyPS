using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 2. 22
이름 : 배성훈
내용 : 데스스타
    문제번호 : 11811번

    ai, aj의 and 비트연산한게 담긴다
    i열을 기준으로 보면 해당 1인 부분은 모두 포함해야한다!
    그래서 또는으로 하면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0077
    {

        static void Main77(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            // 결과를 담는다
            int[] ret = new int[len];

            for (int i = 0; i < len; i++)
            {

                for (int j = 0; j < len; j++)
                {

                    ret[i] |= ReadInt(sr);
                }
            }

            sr.Close();

            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput())))
            {

                for (int i = 0; i < len; i++)
                {

                    sw.Write(ret[i]);
                    sw.Write(' ');
                }
            }
        }

        static int ReadInt(StreamReader _sr)
        {

            int ret = 0, c;

            while((c = _sr.Read()) != -1 && c != '\n' && c != ' ')
            {

                if (c == '\r') continue;

                ret = ret * 10 + c - '0';
            }

            return ret;
        }
    }
}
