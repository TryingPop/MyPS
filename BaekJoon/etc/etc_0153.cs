using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 5
이름 : 배성훈
내용 : 0 = not cute / 1 = cute
    문제번호 : 10886번
*/

namespace BaekJoon.etc
{
    internal class etc_0153
    {

        static void Main153(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);
            int[] chk = new int[2];

            for (int i = 0; i < len; i++)
            {

                int cur = ReadInt(sr);

                chk[cur]++;
            }
            sr.Close();

            if (chk[0] > chk[1]) Console.WriteLine("Junhee is not cute!");
            else Console.WriteLine("Junhee is cute!");
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
