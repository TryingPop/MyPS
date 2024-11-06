using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 8
이름 : 배성훈
내용 : 피보나치 함수
    문제번호 : 1003번

    dp 문제다
    범위가 40개 밖에 안되고 케이스는 모르니 모든 경우를 구하고 해당 값을 저장해서 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0479
    {

        static void Main479(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int[] f1 = new int[41];
            int[] f2 = new int[41];

            f1[0] = 1;
            f1[1] = 0;
            f2[0] = 0;
            f2[1] = 1;

            for (int i = 2; i <= 40; i++)
            {

                f1[i] = f1[i - 1] + f1[i - 2];
                f2[i] = f2[i - 1] + f2[i - 2];
            }

            int test = ReadInt();

            while(test-- > 0)
            {

                int n = ReadInt();

                sw.Write($"{f1[n]} {f2[n]}\n");
            }

            sr.Close();
            sw.Close();

            int ReadInt()
            {

                int c, ret = 0;
                while((c = sr.Read()) != -1 && c != ' ' && c != '\n')
                {

                    if (c == '\r') continue;
                    ret = ret * 10 + c - '0';
                }

                return ret;
            }
        }
    }
}
