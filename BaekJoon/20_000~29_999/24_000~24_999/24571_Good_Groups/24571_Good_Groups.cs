using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
날짜 : 2025. 6. 4
이름 : 배성훈
내용 : Good Groups
    문제번호 : 24571번

    해시 문제다.
*/

namespace BaekJoon.etc
{
    internal class etc_1673
    {

        static void Main1673(string[] args)
        {

            using StreamReader sr = new(Console.OpenStandardInput(), bufferSize: 65536);
            int n = int.Parse(sr.ReadLine());

            string[][] s = new string[n][];
            for (int i = 0; i < n; i++)
            {

                s[i] = sr.ReadLine().Split();
            }

            int m = int.Parse(sr.ReadLine());
            string[][] d = new string[m][];
            for (int i = 0; i < m; i++)
            {

                d[i] = sr.ReadLine().Split();
            }

            int k = int.Parse(sr.ReadLine());
            Dictionary<string, int> dic = new(3 * k);
            for (int i = 0; i < k; i++)
            {

                string[] temp = sr.ReadLine().Split();
                for (int j = 0; j < 3; j++)
                {

                    dic[temp[j]] = i;
                }
            }

            int ret = 0;

            for (int i = 0; i < n; i++)
            {

                if (dic[s[i][0]] == dic[s[i][1]]) continue;
                ret++;
            }

            for (int i = 0; i < m; i++)
            {

                if (dic[d[i][0]] != dic[d[i][1]]) continue;
                ret++;
            }

            Console.Write(ret);
        }
    }
}
