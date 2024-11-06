using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 11
이름 : 배성훈
내용 : 오리와 박수치는 춘배
    문제번호 : 30404번

    스위핑? 그리디 문제다
    오름차순으로 주어지기에 떠나기 직전에 못떠나게 박수를 쳐주면 된다
*/

namespace BaekJoon.etc
{
    internal class etc_0199
    {

        static void Main199(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);
            int k = ReadInt(sr);

            int ret = 0;
            int cur = -1;
            for (int i = 0; i < n; i++)
            {

                int t = ReadInt(sr);
                if (t > cur)
                {

                    ret++;
                    cur = t + k;
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
