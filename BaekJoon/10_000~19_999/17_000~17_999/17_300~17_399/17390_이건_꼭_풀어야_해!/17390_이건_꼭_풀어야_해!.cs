using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/*
날짜 : 2024. 3. 21
이름 : 배성훈
내용 : 이건 꼭 풀어야 해!
    문제번호 : 17390번

    정렬, 누적합 문제다
    조건대로 구현했다
*/

namespace BaekJoon.etc
{
    internal class etc_0306
    {

        static void Main306(string[] args)
        {

            StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput()));
            StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()));

            int n = ReadInt(sr);
            int m = ReadInt(sr);

            int[] arr = new int[n];
            int[] sum = new int[n + 1];

            for (int i = 0; i < n; i++)
            {

                arr[i] = ReadInt(sr);
            }


            Array.Sort(arr);

            for(int i = 0; i < n; i++)
            {

                sum[i + 1] = sum[i] + arr[i];
            }

            for (int i = 0; i < m; i++)
            {

                int l = ReadInt(sr) - 1;
                int r = ReadInt(sr);

                sw.WriteLine(sum[r] - sum[l]);
            }

            sr.Close();
            sw.Close();
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
