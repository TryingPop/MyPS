using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 4. 5
이름 : 배성훈
내용 : 기타리스트
    문제번호 : 1495번

    dp 문제다
    배낭 문제처럼 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0455
    {

        static void Main455(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt();
            int s = ReadInt();
            int max = ReadInt();

            bool[] cV = new bool[max + 1];
            bool[] nV = new bool[max + 1];

            cV[s] = true;
            for (int i = 0; i < n; i++)
            {

                int cur = ReadInt();
                for (int j = 0; j <= max; j++)
                {

                    if (!cV[j]) continue;

                    if (j - cur >= 0) nV[j - cur] = true;
                    if (j + cur <= max) nV[j + cur] = true;

                    cV[j] = false;
                }

                bool[] temp = cV;
                cV = nV;
                nV = temp;
            }

            sr.Close();

            int ret = -1;
            for (int i = max; i >= 0; i--)
            {

                if (!cV[i]) continue;
                ret = i;
                break;
            }

            Console.WriteLine(ret);

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
