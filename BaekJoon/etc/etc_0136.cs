using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 2
이름 : 배성훈
내용 : 수강변경
    문제번호 : 23305번

    처음에는 
        1 2 3
        3 1 5
    인경우 0 개가 아닐까 생각했는데, 문제 조건을 보니 바꿀 수 있었다
    그래서 이상없이 통과했다
*/

namespace BaekJoon.etc
{
    internal class etc_0136
    {

        static void Main136(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int len = ReadInt(sr);

            int[] get = new int[1_000_001];
            for (int i = 0; i < len; i++)
            {
                int idx = ReadInt(sr);
                get[idx]++;
            }

            int[] change = new int[1_000_001];
            for (int i = 0; i < len; i++)
            {

                int idx = ReadInt(sr);
                change[idx]++;
            }

            sr.Close();

            int ret = len;
            for (int i = 0; i < 1_000_001; i++)
            {

                if (get[i] == 0 || change[i] == 0) continue;
                int min = get[i] < change[i] ? get[i] : change[i];

                ret -= min;
            }

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
