using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2024. 3. 20
이름 : 배성훈
내용 : 단체줄넘기
    문제번호 : 30457번

    그리디 문제다
    그리디하게 풀었다
*/

namespace BaekJoon.etc
{
    internal class etc_0305
    {

        static void Main305(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));

            int n = ReadInt(sr);

            int[] arr = new int[1_001];
            for (int i = 0; i < n; i++)
            {

                int idx = ReadInt(sr);
                arr[idx]++;
            }

            sr.Close();
            int ret = 0;
            for (int i = 1; i < arr.Length; i++)
            {

                int add = arr[i] <= 2 ? arr[i] : 2;
                ret += add;
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
